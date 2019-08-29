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
    /// Interaction logic for Attendance.xaml
    /// </summary>
    public partial class Attendance : Window
    {
        UserItem mitem = new UserItem();
        public Attendance(UserItem item)
        {
            InitializeComponent();

            mitem = item;
        }
        private void getAttendance()
        {
            
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            StaticAttendanceList.staticAttendanceList.Clear();
            StaticSortedListAttendance.staticSortedList.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAttendance aitem = new GetAttendance();
            aitem.GetUserAttendance(mitem._EMPID);

            attendance.ItemsSource = StaticAttendanceList.staticAttendanceList;
            AttendanceItem item = new AttendanceItem();


            fromDate.SelectedDate = item.NOW;
            toDate.SelectedDate = item.NOW;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
             StaticSortedListAttendance.staticSortedList.Clear();
            UserPending upend = new UserPending();

            DateTime fd = DateTime.Parse(fromDate.ToString());
            string passfrom = fd.ToString("MM/dd/yy");


            DateTime td = DateTime.Parse(toDate.ToString());
            string passto = td.ToString("MM/dd/yy");

            upend.BetweenDate(mitem._EMPID, passfrom, passto);

            StaticAttendanceList.staticAttendanceList.Clear();
          
            attendance.ItemsSource = StaticSortedListAttendance.staticSortedList;
           
        }
    }
}
