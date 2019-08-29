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
using Fasetto.Word.Core;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for Time.xaml
    /// </summary>
    public partial class Time : Window
    {
        UserItem mitem = new UserItem();
        UserTime utime = new UserTime();
        public Time(UserItem item)
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


            mitem = item;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timekeep.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
            buttonProfile.Content = mitem._FNAME;

            var now = DateTime.Today.ToString("MM/dd/yy");
            var check = utime.Checker(mitem._EMPID);

            if(check == null)
            {
                btn_time_out.IsEnabled = false;
                btn_time_in.IsEnabled = true;
            }
            else if (check.TIME_OUT == "waiting to timeout")
            {
                btn_time_in.IsEnabled = false;
                btn_time_out.IsEnabled = true;
            }
            else
            {
                
                btn_time_out.IsEnabled = false;
                btn_time_in.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            TimeItem itemtime = new TimeItem();
            itemtime.EMP_ID = mitem._EMPID;
            itemtime.TIME_IN = DateTime.Now.ToString("hh:mm");
            itemtime.TIME_OUT = "waiting to timeout";
            itemtime.DATE = DateTime.Today.ToString("MM/dd/yy");
            DateTime passdate = DateTime.Parse(itemtime.DATE.ToString());
            string finalpass = passdate.ToString("MM/dd/yy");


            AddTimein(itemtime);
            utime.GetSpecificId(itemtime);
            btn_time_out.IsEnabled = true;
            btn_time_in.IsEnabled = false;
        }

        private void AddTimein(TimeItem newitem)
        {
            utime.Time_in(newitem);
            
        }

        private void Timeout(TimeItem newitem)
        {
            utime.Time_out(newitem);
        }

        private void Btn_time_out_Click(object sender, RoutedEventArgs e)
        {
            TimeItem itemtime = new TimeItem();
            DateTime timein;
            DateTime.TryParse(itemtime.TIME_IN , out timein);

            DateTime timeout;
            DateTime.TryParse(DateTime.Now.ToString("HH:mm"), out timeout);

            double totalHours = (timeout - timein).TotalHours;

            itemtime.EMP_ID = mitem._EMPID;
            itemtime.TIME_OUT = DateTime.Now.ToString("hh:mm");
            itemtime.HOURS = totalHours;
            itemtime.LOG_ID = LogItem.staticLogIdItem.LOG_ID;

            Timeout(itemtime);
            btn_time_in.IsEnabled = true;
            btn_time_out.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            if(mitem._POSITION == "Engineering Head" || mitem._POSITION == "Electronics Head")
            {
                var pw = Window.GetWindow(this);

                pw.Hide();
                HeadContainer hc = new HeadContainer(mitem);
                hc.ShowDialog();
                pw.Show();
            }else if(mitem._POSITION == "Human Resources Head")
            {
                var parent = Window.GetWindow(this);
                parent.Hide();
                HRISContainer hr = new HRISContainer(mitem);
                hr.ShowDialog();
                parent.Show();
            }
            else
            {
                var parentWindow2 = Window.GetWindow(this);

                parentWindow2.Hide();
                EmployeeContainer mw = new EmployeeContainer(mitem);
                mw.ShowDialog();
                parentWindow2.Show();
            }

            
        }



    }

}
