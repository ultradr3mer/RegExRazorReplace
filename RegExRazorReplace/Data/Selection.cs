namespace RegExRazorReplace.Data
{
  internal class Selection
  {
    #region Constructors

    public Selection(string selectedText, int selectionStart, int selectionLength)
    {
      this.SelectedText = selectedText;
      this.SelectionStart = selectionStart;
      this.SelectionLength = selectionLength;
    }

    #endregion Constructors

    #region Properties

    public string SelectedText { get; }
    public int SelectionLength { get; }
    public int SelectionStart { get; }

    #endregion Properties
  }
}