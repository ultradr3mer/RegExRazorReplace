using System;

namespace RegExRazorReplace.Data
{
  /// <summary>Represents a parse result.</summary>
  internal class ParseResult
  {
    #region Properties

    /// <summary>Gets or sets the code diagnostics.</summary>
    public string RegExDiagnostics { get; set; }

    /// <summary>Gets or sets the model.</summary>
    public object Model { get; set; }

    /// <summary>Gets or sets the template diagnostics.</summary>
    public string RazorDiagnostics { get; set; }

    /// <summary>Gets or sets the value.</summary>
    public string Value { get; set; }

    public bool IsValid { get; set; } = true;

    public Guid CallerId { get; set; }

    public string RazorNonMatchDiagnostics { get; set; }

    #endregion
  }
}