using System.Windows;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for LoginContainer.xaml
    /// </summary>
    public partial class LoginContainer : Window
    {
        public LoginContainer()
        {
            InitializeComponent();

            this.DataContext = new LoginContainerViewModel(this);
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StackPanel_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {

        }
    }
}
