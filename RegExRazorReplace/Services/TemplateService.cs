namespace RegExRazorReplace.Services
{
  using Microsoft.CodeAnalysis.CSharp.Scripting;
  using Microsoft.CodeAnalysis.Scripting;
  using Microsoft.Practices.Unity;
  using Prism.Events;
  using RazorEngine.Templating;
  using RegExRazorReplace.Data;
  using RegExRazorReplace.Events;
  using System;
  using System.Collections.Concurrent;
  using System.Security;
  using System.Security.Policy;
  using System.Text.RegularExpressions;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Web.Razor;

  /// <summary>The Template Service for parsing razor templates.</summary>
  internal class TemplateService
  {
    #region Fields

    private readonly IEventAggregator eventAggregator;

    private readonly ConcurrentQueue<ParseRequest> requests = new ConcurrentQueue<ParseRequest>();

    private readonly IRazorEngineService service;
    private readonly object result;
    private bool running;

    private static readonly Type modelType = typeof(Match);

    #endregion Fields

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="TemplateService" /> class.</summary>
    /// <param name="container">The container.</param>
    public TemplateService(IUnityContainer container)
    {
      this.eventAggregator = container.Resolve<IEventAggregator>();

      this.service = IsolatedRazorEngineService.Create(TemplateService.SandboxCreator);
    }

    #endregion Constructors

    #region Methods

    /// <summary>Parses the specified code and template.</summary>
    /// <param name="code">The code.</param>
    /// <param name="template">The template.</param>
    public void Parse(string code, string regExPattern, string template)
    {
      this.requests.Enqueue(new ParseRequest(code, regExPattern, template));

      if (!this.running)
      {
        this.running = true;
        Task.Factory.StartNew(this.ProcessRequests);
      }
    }

    /// <summary>We have to create seperate domain shizzle to prevent hackers from beeing able abuse our templating power. I would like to understand better how this works, just doing as told by this page now: https://antaris.github.io/RazorEngine/Isolation.html </summary>
    /// <returns>An AppDomain that you should use to execute the sandbox stuff.</returns>
    private static AppDomain SandboxCreator()
    {
      Evidence evidence = new Evidence();
      evidence.AddHostEvidence(new Zone(SecurityZone.Internet));
      PermissionSet permissionSet = SecurityManager.GetStandardSandbox(evidence);

      // You have to sign your assembly in order to get strong names (http://stackoverflow.com/questions/8349573/getting-null-from-gethostevidence)
      // But doing this results in:
      // Error	1	Assembly generation failed -- Referenced assembly 'RazorEngine' does not have a strong name	D:\git\presence-engine\RazorExperiment\CSC	RazorExperiment
      // Error	2	Assembly generation failed -- Referenced assembly 'Microsoft.AspNet.Razor' does not have a strong name	D:\git\presence-engine\RazorExperiment\CSC	RazorExperiment
      StrongName razorEngineAssembly = typeof(RazorEngineService).Assembly.Evidence.GetHostEvidence<StrongName>();
      StrongName razorAssembly = typeof(RazorTemplateEngine).Assembly.Evidence.GetHostEvidence<StrongName>();

      AppDomainSetup appDomainSetup = new AppDomainSetup
      {
        ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
      };

      AppDomain result = AppDomain.CreateDomain("Sandbox", null, appDomainSetup, permissionSet, razorEngineAssembly, razorAssembly);
      return result;
    }

    /// <summary>Dequeues all but the latest.</summary>
    private void DequeueAllButTheLatest()
    {
      while (this.requests.Count > 1)
      {
        this.requests.TryDequeue(out _);
      }
    }

    /// <summary>Processes the requests.</summary>
    private void ProcessRequests()
    {
      while (this.running)
      {
        this.DequeueAllButTheLatest();

        if (!this.requests.TryDequeue(out ParseRequest request))
        {
          continue;
        }

        var result = this.CompileTemplate(new ParseResult(), request);
        result = RunRegex(result, request, (m, req, res) => this.RunTemplate(m, req, res));

        this.eventAggregator.GetEvent<ParseCompleted>().Publish(result);

        Thread.Sleep(500);
      }
    }

    private ParseResult CompileTemplate(ParseResult result, ParseRequest request)
    {
      if (!result.IsValid)
      {
        return result;
      }

      try
      {
        request.Name = request.Template.GetHashCode().ToString("X");
        this.service.Compile(request.Template, request.Name, modelType);
      }
      catch (Exception e)
      {
        result.IsValid = false;
        result.TemplateDiagnostics = e.Message;
      }

      return result;
    }

    private ParseResult RunRegex(ParseResult result, ParseRequest request, Func<Match, ParseRequest, ParseResult, string> matchEvaluation)
    {
      if (!result.IsValid)
      {
        return result;
      }

      try
      {
        result.Value = Regex.Replace(request.Input, request.RegExPattern, m => matchEvaluation(m, request, result));
      }
      catch (Exception e)
      {
        result.IsValid = false;
        result.TemplateDiagnostics = e.Message;
      }

      return result;
    }

    /// <summary>Runs the template.</summary>
    /// <param name="result">The parse result.</param>
    /// <param name="request">The parse request.</param>
    private string RunTemplate(Match match, ParseRequest request, ParseResult result)
    {
      if(!result.IsValid)
      {
        return match.Value;
      }

      try
      {
        return this.service.Run(request.Name, modelType, match);
      }
      catch (Exception e)
      {
        result.IsValid = false;
        result.TemplateDiagnostics = e.Message;
      }

      return match.Value;
    }

    #endregion Methods
  }
}