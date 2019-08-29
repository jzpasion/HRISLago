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
            else if (check.TIME_OUT == "Waiting to Timeout")
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
            itemtime.TIME_IN = DateTime.Now.ToString("hh:mm:ss tt");
            itemtime.TIME_OUT = "Waiting to Timeout";
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
        string remarks;
        double finalpayday;
        double totalHours;
        double totalMinutes;
        private void Btn_time_out_Click(object sender, RoutedEventArgs e)
        {
            UserTime utiem = new UserTime();
            TimeItem itemtime = new TimeItem();
            utiem.getclockin(mitem._EMPID);

            DateTime timein;
            DateTime.TryParse("" + ClockInItem.Clockin.DATE + "," + ClockInItem.Clockin.TIME_IN + "", out timein);
            DateTime t1 = DateTime.Parse("" + ClockInItem.Clockin.DATE + ", 8:00:00 AM");

            DateTime timeout;
            DateTime.TryParse(DateTime.Now.ToString("HH:mm"), out timeout);
            DateTime t2 = DateTime.Parse("" + ClockInItem.Clockin.DATE + ", 5:00:00 PM");
            DateTime t3 = DateTime.Parse("" + ClockInItem.Clockin.DATE + ", 12:00:00 PM");

            if (timein < t1 && timeout > t2)
            {

                totalHours = t2.Subtract(t1).TotalHours-1;
                totalMinutes = t2.Subtract(t1).TotalMinutes-60;
                remarks = "On-Duty";
            }
            else if(timein > t1 && timeout > t2)
            {
                double totalLateminutes = timein.Subtract(t1).Minutes;
                totalHours = t2.Subtract(timein).TotalHours - 1;
                totalMinutes = t2.Subtract(timein).TotalMinutes - 60;

            }else if(timein < t1 && timeout > t3)
            {
                totalHours = t3.Subtract(t1).TotalHours;
                totalMinutes = t3.Subtract(t1).TotalMinutes ;
                remarks = "Half-day";
            }
            else if (timein < t1 && timeout < t3)
            {
                totalHours = t3.Subtract(t1).TotalHours;
                totalMinutes = t3.Subtract(t1).TotalMinutes;
                remarks = "Half-day";
            }
            else if (timein > t1 && timeout > t3)
            {
                double totalLateminutes = timein.Subtract(t1).Minutes;
                totalHours = t3.Subtract(t1).TotalHours;
                totalMinutes = t3.Subtract(t1).TotalMinutes;
                remarks = "Half-day";
            }
            else if (timein > t1 && timeout < t3)
            {
                double totalLateminutes = timein.Subtract(t1).Minutes;
                totalHours = t3.Subtract(t1).TotalHours;
                totalMinutes = t3.Subtract(t1).TotalMinutes;
                remarks = "Half-day";
            }





            //totalHours = timeout.Subtract(passTimein).TotalHours;
            //totalMinutes = timeout.Subtract(passTimein).TotalMinutes;

            double roundhrs = Math.Round((Double)totalHours,2 ) ;
            double roundminutes = Math.Round((Double)totalMinutes,2 );

            double hourate = mitem._HOURLY_RATE;

            double minuterate = (hourate / 60);
            

           

           
            if(roundminutes >= 0 && roundminutes <= 545)
            {
                remarks = "On-Duty";
                
                roundminutes = roundminutes - 60;
                double payday = (minuterate * roundminutes);
                finalpayday = Math.Round((double)payday, 2);
            }
            else 



            itemtime.EMP_ID = mitem._EMPID;
            itemtime.TIME_OUT = DateTime.Now.ToString("hh:mm:ss tt");
            itemtime.HOURS = roundhrs;
            itemtime.LOG_ID = LogItem.staticLogIdItem.LOG_ID;
            itemtime.MINUTES = roundminutes;
            itemtime.PAY_DAY = finalpayday;
            itemtime.REMARKS = remarks;


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
