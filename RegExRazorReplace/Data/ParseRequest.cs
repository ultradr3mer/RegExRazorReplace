using System;

namespace RegExRazorReplace.Data
{
  /// <summary>Represents a parse request.</summary>
  internal class ParseRequest
  {
    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="ParseRequest" /> class.</summary>
    /// <param name="input">The code.</param>
    /// <param name="regExPattern">The template.</param>
    public ParseRequest(string input, string regExPattern, string template, Guid callerId)
    {
      this.Input = input ?? string.Empty;
      this.RegExPattern = regExPattern ?? string.Empty;
      this.Template = template ?? string.Empty;
      this.CallerId = callerId;
    }

    #endregion

    #region Properties

    public string Input { get; }
    public string RegExPattern { get; }
    public string Template { get; }
    public Guid CallerId { get; }
    public string Name { get; set; }

    #endregion
  }
}