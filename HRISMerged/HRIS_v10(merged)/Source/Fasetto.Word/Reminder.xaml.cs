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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for Reminder.xaml
    /// </summary>
    public partial class Reminder : Window
    {
        UserItem mitem = new UserItem();
        public Reminder(UserItem item)
        {
            InitializeComponent();
            mitem = item;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            UserHoliday uholiday = new UserHoliday();
            uholiday.getUpcoming();
            if(holidays.holitem._HOLIDAY_NAME == null)
            {
                Event.Text = "None";
                Date.Text = "None";
                Type.Text = "None";
            }
            else
            {

                DateTime fdate = DateTime.Parse(holidays.holitem._HOLIDAY_DATE.ToString());
                string passdate = fdate.ToString("MMMM dd, yyyy");


                Event.Text = "" + holidays.holitem._HOLIDAY_NAME + "";
                Date.Text = "" + passdate + "";
                Type.Text = "" + holidays.holitem._HOLIDAY_TYPE + "";
            }

        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var par = Window.GetWindow(this);
            par.Hide();
            HolidayWindow holwin = new HolidayWindow();
            holwin.ShowDialog();
            par.Close();
        }
    }
}
