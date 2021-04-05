namespace RegExRazorReplace.Views
{
  using System.ComponentModel;
  using System.Windows;

  using RegExRazorReplace.ViewModels;

  /// <summary>Interaction logic for MainWindow.xaml</summary>
  public partial class MainWindow : Window
  {
    #region Constructors

    public MainWindow()
    {
      this.InitializeComponent();

      this.DataContext = new MainWindowViewModel();
    }

    #endregion

    #region Properties

    /// <summary>Gets the view model which is the data context of this view.</summary>
    private MainWindowViewModel ViewModel
    {
      get { return this.DataContext as MainWindowViewModel; }
    }

    #endregion
  }
}