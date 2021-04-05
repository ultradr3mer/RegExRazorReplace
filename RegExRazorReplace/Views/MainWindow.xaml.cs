namespace RegExRazorReplace.Views
{
  using MahApps.Metro.Controls;
  using RegExRazorReplace.ViewModels;

  /// <summary>Interaction logic for MainWindow.xaml</summary>
  public partial class MainWindow : MetroWindow
  {
    #region Constructors

    public MainWindow()
    {
      this.InitializeComponent();

      this.DataContext = new MainWindowViewModel();
    }

    #endregion Constructors

    #region Properties

    /// <summary>Gets the view model which is the data context of this view.</summary>
    private MainWindowViewModel ViewModel
    {
      get { return this.DataContext as MainWindowViewModel; }
    }

    #endregion Properties
  }
}