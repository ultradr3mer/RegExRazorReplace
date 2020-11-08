namespace RegExRazorReplace.Data
{
  /// <summary>Represents a parse request.</summary>
  internal class ParseRequest
  {
    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="ParseRequest" /> class.</summary>
    /// <param name="input">The code.</param>
    /// <param name="regExPattern">The template.</param>
    public ParseRequest(string input, string regExPattern, string template)
    {
      this.Input = input ?? string.Empty;
      this.RegExPattern = regExPattern ?? string.Empty;
      this.Template = template ?? string.Empty;
    }

    #endregion

    #region Properties

    /// <summary>Gets the code.</summary>
    public string Input { get; }

    /// <summary>Gets the template.</summary>
    public string RegExPattern { get; }

    public string Template { get; }
    public string Name { get;  set; }

    #endregion
  }
}