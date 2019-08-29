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
    /// Interaction logic for Overtime.xaml
    /// </summary>
    /// 
    public partial class Overtime : Window
    {
        UserItem mitem = new UserItem();
        public Overtime(UserItem item)
        {
            InitializeComponent();
            mitem = item;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (project.Text == "" || Reasonbox.Text == "" || Timefrom.Text == "" || TimeTo.Text == "" || toDate.Text == "" || CB.Text == "")
            {
                MessageBox.Show("Please fill required inputs");
            }
            else
            {
                RequestItem item = new RequestItem();

                item.EMP_ID = mitem._EMPID;
                item.DATE = toDate.Text;
                item.PROJECT = project.Text;
                item.REASON = Reasonbox.Text;
                item.STATUS = "Waiting for Approval";
                item.TIME_FROM = Timefrom.Text;
                item.TIME_TO = TimeTo.Text;

                PendingItem pitem = new PendingItem();

                pitem.EMPID = mitem._EMPID;
                pitem.PENDING_TYPE = "Overtime";
                pitem.PENDING_LEAVE_REASON = Reasonbox.Text;
                pitem.PENDING_STATUS = "Waiting for Approval";
                pitem.PENDING_DATE = DateTime.Now.ToString("MM/dd/yyyy");
                pitem.PENDING_POSITION = mitem._POSITION;
                pitem.PENDING_TIME = DateTime.Now.ToString("hh:mm:ss tt");
                pitem.PENDING_OT_FROM = Timefrom.Text;
                pitem.PENDING_OT_TO = TimeTo.Text;
                pitem.SEND_TO = CB.Text;
                pitem.APPROVED_BY = "Waiting for Approval";

                addOT(item);
                addPend(pitem,Reasonbox.Text);
                clear();
                MessageBox.Show("Request sent!");
                this.Close();
            }

            
        }
        private void addPend(PendingItem newitem, string reason)
        {
            UserPending upend = new UserPending();
            upend.AddPendingOT(newitem , reason);
        }
        private void addOT(RequestItem newitem)
        {
            AddRequest myrequest = new AddRequest();
            myrequest.AddOt(newitem);
        }

        private void clear()
        {
            project.Text = "";
            Reasonbox.Text = "";
            Timefrom.Text = "";
            TimeTo.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Comboboxitem.ComboItem.Clear();
            AttendanceItem aitem = new AttendanceItem();
            Timefrom.SelectedTime = aitem.NOW;
            TimeTo.SelectedTime = aitem.NOW;
            toDate.SelectedDate = aitem.NOW;

            UserPending upend = new UserPending();
            upend.ComboItems();

            CB.ItemsSource = Comboboxitem.ComboItem;
        }
    }
}
