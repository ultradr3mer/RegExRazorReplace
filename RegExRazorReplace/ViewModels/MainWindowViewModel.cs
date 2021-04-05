namespace RegExRazorReplace.ViewModels
{
  using Microsoft.Practices.Unity;
  using Prism.Commands;
  using Prism.Events;
  using RegExRazorReplace.Composite;
  using RegExRazorReplace.Data;
  using RegExRazorReplace.Events;
  using RegExRazorReplace.Extensions;
  using RegExRazorReplace.Services;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Windows.Input;

  internal class MainWindowViewModel : BaseViewModel
  {
    #region Fields

    private IUnityContainer container;

    private TemplateService templateService;
    private SaveToHardDriveService saveService;

    #endregion Fields

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="MainWindowViewModel" /> class.</summary>
    public MainWindowViewModel()
    {
      this.Initialize();
    }

    #endregion Constructors

    #region Properties

    public ICommand AddCommand { get; set; }

    public BindingList<ParseEntryViewModel> Entries { get; set; }

    /// <summary>Gets or sets the input.</summary>
    public string Input { get; set; }

    public string Output { get; set; }

    /// <summary>Gets or sets the result.</summary>
    public string Result { get; set; }

    #endregion Properties

    #region Methods

    protected virtual void Initialize()
    {
      this.container = ContainerFactory.Create();
      this.templateService = container.Resolve<TemplateService>();
      var eventAggregator = container.Resolve<IEventAggregator>();
      this.saveService = container.Resolve<SaveToHardDriveService>();
      eventAggregator.GetEvent<ParseCompleted>().Subscribe(this.HandleParseCompletedEvent, ThreadOption.UIThread);

      this.Input = "INSERT INTO <user>.AppCommand (ID, Name, Description, StatusID, ContextID) VALUES (N'4de409b0-49fe-4ffd-ae2e-31bc910a9feb', N'nGroup.Info.eEvolution.MiddleLayer.CommandManager.DPDZonenTabelleCommand', NULL, 0, 'f99ad222-9552-4fd2-806b-f9cb3b031036');";

      this.AddCommand = new DelegateCommand(this.AddCommandExecute);

      this.Load();
    }

    private void Load()
    {
      var data = this.saveService.Load()?.Entries ?? new List<ParseEntryData>()
      {
        new ParseEntryData() {
          Name = "Guid Uppern",
          RegEx = "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}",
          Razor = "@Model.Value.ToUpper()" }
      };

      this.Entries = new BindingList<ParseEntryViewModel>(data.Select(o => container.Resolve<ParseEntryViewModel>().GetWithDataModel(o)).ToList());
      this.Entries.ListChanged += this.Entries_ListChanged;
    }

    private void Entries_ListChanged(object sender, ListChangedEventArgs e)
    {
      var data = this.Entries.Select(o => o.WriteToDataModel()).ToList();
      this.saveService.Save(new SaveData() { Entries = data });
    }


    private void AddCommandExecute()
    {
      var entry = container.Resolve<ParseEntryViewModel>().GetWithDataModel(new ParseEntryData());
      entry.MainWindowViewModel = this;
      this.Entries.Add(entry);
    }

    /// <summary>Handles the parse completed event.</summary>
    /// <param name="result">The result data.</param>
    private void HandleParseCompletedEvent(ParseResult result)
    {
      this.Result = result.Value;
    }

    #endregion Methods
  }
}