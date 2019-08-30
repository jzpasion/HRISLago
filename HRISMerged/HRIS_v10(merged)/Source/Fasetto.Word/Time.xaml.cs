using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
            DateTime timeout;
            DateTime.TryParse(DateTime.Now.ToString("hh:mm:ss tt"), out timeout);
            DateTime time5pm = DateTime.Parse("" + ClockInItem.Clockin.DATE + ", 5:00:00 PM");
            if (timeout == time5pm)
            {
                Form f = new Form();// object of the form
                f.WindowState = FormWindowState.Maximized;
                f.BringToFront();
                f.TopMost = true;
                f.FormBorderStyle = FormBorderStyle.None;
                f.Opacity = 0.5;
                f.Load += new EventHandler(f_Load);
                f.Show();
                return;
            }
        }
        void f_Load(object sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("Its Already 5:00PM Save your files!");
            ((Form)sender).Close();
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
        double totalLateminutes = 0;
        double totalUndertimeMinutes = 0;
        double totalOTMinutes = 0;
        double OThour = 0;
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
            DateTime t4 = DateTime.Parse("" + ClockInItem.Clockin.DATE + ", 1:00:00 PM");
            DateTime t5 = DateTime.Parse("" + ClockInItem.Clockin.DATE + ", 6:00:00 PM");
            //Normal day
            if (timein <= t1 &&( timeout >= t2 && timeout < t5))
            {

                totalHours = t2.Subtract(t1).TotalHours-1;
                totalMinutes = t2.Subtract(t1).TotalMinutes-60;
                remarks = "On-Duty";
            }
            //Half-day morning  8 to after 12pm
            else if(timein < t1 &&  timeout > t3 && timeout < t4 && timeout <t2)
            {
                totalHours = t3.Subtract(t1).TotalHours;
                totalMinutes = t3.Subtract(t1).TotalMinutes ;
                remarks = "Half-day";
            }
            //Half-day morning 8 to before 12pm
            else if (timein < t1 && timeout < t3)
            {
                totalUndertimeMinutes = t3.Subtract(timeout).TotalMinutes;
                totalHours = timeout.Subtract(t1).TotalHours;
                totalMinutes = timeout.Subtract(t1).TotalMinutes;
                remarks = "Half-day";
            }
            //half-day morning late 8 to before 12pm
            else if (timein > t1 && timeout < t3)
            {
                totalUndertimeMinutes = t3.Subtract(timeout).TotalMinutes;
                totalLateminutes = timein.Subtract(t1).Minutes;
                totalHours = timeout.Subtract(timein).TotalHours;
                totalMinutes = timeout.Subtract(timein).TotalMinutes;

                remarks = "Half-day";
            }
            //half-day morning late 8 to after 12pm
            else if (timein > t1 && timeout >= t3 && timeout < t4  && timeout <t2)
            {
                totalLateminutes = timein.Subtract(t1).Minutes;
                totalHours = t3.Subtract(timein).TotalHours;
                totalMinutes = t3.Subtract(timein).TotalMinutes;
                remarks = "Half-day";
            }
            //Late
            else if (timein > t1 && timeout >= t2 && timeout < t5)
            {
                totalLateminutes = timein.Subtract(t1).Minutes;
                totalHours = t2.Subtract(timein).TotalHours - 1;
                totalMinutes = t2.Subtract(timein).TotalMinutes - 60;

                remarks = "On-Duty";

            }
            //half-day afternoon 1pm to after 5pm
            else if (timein > t3 && timein <= t4 && timeout >= t2 && timeout < t5)
            {
                totalHours = t2.Subtract(t4).TotalHours;
                totalMinutes = t2.Subtract(t4).TotalMinutes;
                remarks = "Half-day";
            }

            //half-day afternoon late 1pm to  after 5pm
            else if(timein > t4 && timeout >= t2 && timeout < t5)
            {
                totalLateminutes = timein.Subtract(t4).TotalMinutes;
                totalHours = t2.Subtract(timein).TotalHours;
                totalMinutes = t2.Subtract(timein).TotalMinutes;
                remarks = "Half-day";
            }

            //half-day afternoon 1pm to before 5pm
            else if(timein > t3 &&timein <= t4 && timeout < t2)
            {
                totalUndertimeMinutes = t2.Subtract(timeout).TotalMinutes;
                totalHours = timeout.Subtract(t4).TotalHours;
                totalMinutes = timeout.Subtract(t4).TotalMinutes;
                    remarks = "Half-day";
            }
            //half-day afternoon late 1pm to before 5pm
            else if(timein > t4 && timeout < t2)
            {
                totalUndertimeMinutes = t2.Subtract(timeout).TotalMinutes;
                totalLateminutes = timein.Subtract(t4).TotalMinutes;
                totalHours = timeout.Subtract(timein).TotalHours;
                totalMinutes = timeout.Subtract(timein).TotalMinutes;
                remarks = "Half-day";
            }
            //Late and UnderTime
            else if (timein > t1 && timeout < t2)
            {
                totalLateminutes = timein.Subtract(t1).TotalMinutes;
                totalUndertimeMinutes = t2.Subtract(timeout).TotalMinutes;
                totalHours = timeout.Subtract(timein).TotalHours - 1;
                totalMinutes = timeout.Subtract(timein).TotalMinutes - 60;
                remarks = "On-Duty";

            }
            //UnderTIme
            else if (timein < t1 && timeout < t2)
            {
                totalUndertimeMinutes = t2.Subtract(timeout).TotalMinutes;
                totalHours = timeout.Subtract(t1).TotalHours - 1;
                totalMinutes = timeout.Subtract(t1).TotalMinutes - 60;
                remarks = "On-Duty";
            }

            //half-day afternoon Late OT
            else if (timein > t4 && timeout < t5)
            {
                totalOTMinutes = timeout.Subtract(t2).TotalMinutes;
                totalLateminutes = timein.Subtract(t4).Minutes;
                totalHours = timeout.Subtract(timein).TotalHours;
                totalMinutes = timeout.Subtract(timein).TotalMinutes;
                remarks = "Half-day";
            }
            //half-day afternoon OT
            else if (timein <= t4 && timeout < t5)
            {
                totalOTMinutes = timeout.Subtract(t2).TotalMinutes;
                totalHours = timeout.Subtract(t4).TotalHours;
                totalMinutes = timeout.Subtract(t4).TotalMinutes;
                remarks = "Half-day";
            }
            //OT
            else if(timein <= t1 && timeout >= t5)
            {
                totalOTMinutes = timeout.Subtract(t2).TotalMinutes;
                totalHours = timeout.Subtract(t1).TotalHours - 1;
                totalMinutes = timeout.Subtract(t1).TotalMinutes -60;
                remarks = "On-Duty";
            }
            

            //OT LATE 
            else if (timein > t1 && timeout >= t5)
            {
                totalLateminutes = timein.Subtract(t1).Minutes;
                totalOTMinutes = timeout.Subtract(t2).TotalMinutes;
                totalHours = timeout.Subtract(timein).TotalHours - 1;
                totalMinutes = timeout.Subtract(timein).TotalMinutes - 60;
            }

            else
            {
                itemtime.EMP_ID = mitem._EMPID;
                itemtime.TIME_OUT = "Forgot to Timeout";
                itemtime.HOURS = 0;
                itemtime.LOG_ID = LogItem.staticLogIdItem.LOG_ID;
                itemtime.MINUTES = 0;
                itemtime.PAY_DAY = 0;
                itemtime.REMARKS = "Forgot to Timeout";
                itemtime.LOG_LATE_MINUTES = 0;
                itemtime.LOG_LATE_DEDUC = 0;
                itemtime.LOG_OT_MINUTES = 0;
                itemtime.LOG_OT_TOTAL = 0;
                itemtime.LOG_UNDERTIME = 0;
                Timeout(itemtime);
                btn_time_in.IsEnabled = true;
                btn_time_out.IsEnabled = false;
                this.Close();
            }




            //2decimal place total hours, total minutes
            double roundhrs = Math.Round((Double)totalHours,2 ) ;
            double roundminutes = Math.Round((Double)totalMinutes,2 );

            //get employee salary per hour
            double hourate = mitem._HOURLY_RATE;

            //computation hourlyrate (rate per hour / 60 minutes)
            double minuterate = (hourate / 60);

            //computation late
            double TotalMinuteDeduction = ((totalUndertimeMinutes+totalLateminutes) * minuterate);

            //computation OT

            if(totalOTMinutes >= 60 && totalOTMinutes >90)
            {
                OThour = 1;
                
            }
            else if (totalOTMinutes >= 90 && totalOTMinutes < 120)
            {
                OThour = 1.5;
            }
            else if (totalOTMinutes >= 120 && totalOTMinutes >150)
            {
                OThour = 2;
            }
            else if (totalOTMinutes >= 150 && totalOTMinutes < 180)
            {
                OThour = 2.5;
            }
            else if (totalOTMinutes >= 180 && totalOTMinutes < 210)
            {
                OThour = 3;
            }
            else if (totalOTMinutes >= 210 && totalOTMinutes < 240)
            {
                OThour = 3.5;
            }
            else if (totalOTMinutes >= 240 && totalOTMinutes < 270)
            {
                OThour = 4;
            }
            else if (totalOTMinutes >= 270 && totalOTMinutes < 300)
            {
                OThour = 4.5;
            }
            else if (totalOTMinutes >= 300 && totalOTMinutes < 330)
            {
                OThour = 5;
            }
            else if (totalOTMinutes >= 330 && totalOTMinutes < 360)
            {
                OThour = 5.5;
            }
            else if (totalOTMinutes >= 360 && totalOTMinutes < 390)
            {
                OThour = 6;
            }
            else if (totalOTMinutes >= 390 && totalOTMinutes < 420)
            {
                OThour = 6.5;
            }
            else if (totalOTMinutes >= 420 && totalOTMinutes < 450)
            {
                OThour = 7;
            }
            else if (totalOTMinutes >= 450 && totalOTMinutes < 480)
            {
                OThour = 7.5;
            }
            else if (totalOTMinutes >= 480)
            {
                OThour = 8;
            }

            double TotalOvertime = (minuterate * OThour * 1.25);

            //computation PayDay
            double paydaywdeduc = (minuterate * roundminutes - TotalMinuteDeduction);
            double finpaydaywOT = (paydaywdeduc + TotalOvertime);
            finalpayday = Math.Round((double)finpaydaywOT, 2);
     


            itemtime.EMP_ID = mitem._EMPID;
            itemtime.TIME_OUT = DateTime.Now.ToString("hh:mm:ss tt");
            itemtime.HOURS = roundhrs;
            itemtime.LOG_ID = LogItem.staticLogIdItem.LOG_ID;
            itemtime.MINUTES = roundminutes;
            itemtime.PAY_DAY = finalpayday;
            itemtime.REMARKS = remarks;
            itemtime.LOG_LATE_MINUTES = totalLateminutes;
            itemtime.LOG_LATE_DEDUC = TotalMinuteDeduction;
            itemtime.LOG_OT_MINUTES = totalOTMinutes;
            itemtime.LOG_OT_TOTAL = TotalOvertime;
            itemtime.LOG_UNDERTIME = totalUndertimeMinutes;




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
