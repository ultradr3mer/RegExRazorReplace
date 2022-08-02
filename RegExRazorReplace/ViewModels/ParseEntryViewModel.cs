using Prism.Commands;
using Prism.Events;
using RegExRazorReplace.Composite;
using RegExRazorReplace.Data;
using RegExRazorReplace.Events;
using RegExRazorReplace.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace RegExRazorReplace.ViewModels
{
  internal class ParseEntryViewModel : BaseViewModel<ParseEntryData>
  {
    #region Fields

    private readonly IEventAggregator eventAggregator;
    private readonly TemplateService templateService;

    #endregion Fields

    #region Constructors

    public ParseEntryViewModel(TemplateService templateService, IEventAggregator eventAggregator)
    {
      this.Id = Guid.NewGuid();
      this.ExecuteCommand = new DelegateCommand(this.ExecuteCommandExecute);
      this.RemoveCommand = new DelegateCommand(this.RemoveCommandExecute);
      this.templateService = templateService;
      this.eventAggregator = eventAggregator;

      Initialize();
    }

    internal void RegexEscape(Selection selection)
    {
      var escapedText = System.Text.RegularExpressions.Regex.Escape(selection.SelectedText);

      this.RegEx = this.RegEx.Substring(0, selection.SelectionStart) + 
                    escapedText + 
                    this.RegEx.Substring(selection.SelectionStart + selection.SelectionLength);
    }

    #endregion Constructors

    #region Properties

    public ICommand ExecuteCommand { get; }

    public Guid Id { get; }

    public bool IsExpanded { get; set; } = true;

    public MainWindowViewModel MainWindowViewModel { get; set; }

    public string Name { get; set; }

    public string Razor { get; set; }

    public string RazorDiagnostics { get; set; }

    public string RegEx { get; set; }

    public string RegExDiagnostics { get; set; }

    public ICommand RemoveCommand { get; set; }

    public string RazorNonMatchDiagnostics { get; set; }

    public string RazorNonMatch { get; set; }

    #endregion Properties

    #region Methods

    protected virtual void Initialize()
    {
      this.eventAggregator.GetEvent<ParseCompleted>().Subscribe(this.OnParseCompleted);
    }

    private void ExecuteCommandExecute()
    {
      this.templateService.Parse(this.MainWindowViewModel.Input, RegEx, Razor, RazorNonMatch, this.Id);
    }

    private void OnParseCompleted(ParseResult data)
    {
      if (data.CallerId != this.Id)
      {
        return;
      }

      this.RazorDiagnostics = data.RazorDiagnostics;
      this.RegExDiagnostics = data.RegExDiagnostics;
      this.RazorNonMatchDiagnostics = data.RazorNonMatchDiagnostics;
    }

    private void RemoveCommandExecute()
    {
      if(MessageBox.Show("Are You Sure", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
      {
        this.MainWindowViewModel.Entries.Remove(this);
      }
    }

    #endregion Methods
  }
}