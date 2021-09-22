using RegExRazorReplace.Data;
using RegExRazorReplace.ViewModels;
using System.Windows.Controls;

namespace RegExRazorReplace.Views
{
  /// <summary>
  /// Interaction logic for ParseControl.xaml
  /// </summary>
  public partial class ParseControl : UserControl
  {
    #region Constructors

    public ParseControl()
    {
      InitializeComponent();
    }

    #endregion Constructors

    #region Properties

    private ParseEntryViewModel ViewModel { get => this.DataContext as ParseEntryViewModel; }

    #endregion Properties

    #region Methods

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      var selection = new Selection(RegEx.SelectedText, RegEx.SelectionStart, RegEx.SelectionLength);
      this.ViewModel.RegexEscape(selection);
    }

    #endregion Methods
  }
}