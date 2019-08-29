using Fasetto.Word.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for HRISMainWindow.xaml
    /// </summary>
    public partial class HRISMainWindow : Window
    {
        public static UserItem mItem = new UserItem();
        bool menuStateClosed = true, windowMaximized = false;
        EmployeeCollection myEmpCollection = new EmployeeCollection();
        PositionCollection myPosCollection = new PositionCollection();
        public HRISMainWindow(UserItem item)
        {
            InitializeComponent();

            mItem = item;
            DataContext = new HRISWindowViewModel(this);
            getAllEmployees();
            getAllJobPositions();
            getAllHolidays();
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            menuStateClosed = !menuStateClosed;
            if (menuStateClosed)
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
        }

        private void getAllEmployees()
        {
            myEmpCollection.RetreiveAllEmployee();
        }

        private void getAllJobPositions()
        {
            myPosCollection.RetreiveAllPositions();
        }

        private void getAllHolidays()
        {
            HolidayCollection myHolidayCollection = new HolidayCollection();
            myHolidayCollection.RetreiveAllHolidays();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //_= RegisterAsync();
            _ = PersistentSearchAsync();
        }

        public async Task PersistentSearchAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.PersistentSearch);

            await Task.Delay(1);
        }

        public async Task PayrollAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Payroll);

            await Task.Delay(1);
        }

        public async Task HomeAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Home);

            await Task.Delay(1);
        }
        public async Task RegisterAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Register);

            await Task.Delay(1);
        }

        public async Task LoginAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Login);

            await Task.Delay(1);
        }

        public async Task AttendanceAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Attendance);

            await Task.Delay(1);
        }
        public async Task ChatAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Chat);

            await Task.Delay(1);
        }

        public async Task EmployeeAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.EmpoyeeManagement);

            await Task.Delay(1);
        }

        public async Task DashboardPageAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.DashboardPage);

            await Task.Delay(1);
        }


        private void MenuHome_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_ = HomeAsync();
            _ = DashboardPageAsync();
        }

        private void MenuEmployee_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _ = EmployeeAsync();
        }

        private void MenuAttendance_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _ = AttendanceAsync();
        }

        private void MenuPayroll_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_ = RegisterAsync();
            _ = PayrollAsync();
        }

        private void MenuReport_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _ = HomeAsync();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            windowMaximized = !windowMaximized;
            if (windowMaximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            menuStateClosed = !menuStateClosed;
            if (menuStateClosed)
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
