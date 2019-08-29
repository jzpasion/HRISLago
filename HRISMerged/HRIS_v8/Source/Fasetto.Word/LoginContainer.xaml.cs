using System;
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
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            this.DataContext = new LoginContainerViewModel(this);
           
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeShow.Content = DateTime.Now.ToString("hh:mm:ss tt");
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
