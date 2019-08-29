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
    /// Interaction logic for ApprovalModal.xaml
    /// </summary>
    public partial class ApprovalModal : Window
    {
        int idholder;
        string typeholder;
        string emppos;
        UserItem mitem = new UserItem();
        public ApprovalModal(UserItem item,int id , string type,string pos)
        {
            InitializeComponent();
            mitem = item;
            idholder = id;
            typeholder = type;
            emppos = pos;
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

            if (typeholder == "Leave" && emppos == "Head")
            {
                Delete.Visibility = Visibility.Hidden;
                Cancel.Visibility = Visibility.Hidden;
                string time = DateTime.Now.ToString("hh:mm:ss tt");
                UserPending upend = new UserPending();
                upend.SpecificPendingLeave(idholder, typeholder);





                empname.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_NAME;
                emptype.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_TYPE;
                from.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_LEAVE_FROM;
                to.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_LEAVE_TO;
                Reason.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_LEAVE_REASON;

               

            }
            else if (typeholder == "Overtime" && emppos == "Head")
            {
                Delete.Visibility = Visibility.Hidden;
                Cancel.Visibility = Visibility.Hidden;
                UserPending upend = new UserPending();
                upend.SpecificPendingOT(idholder, typeholder);



                empname.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_NAME;
                emptype.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_TYPE;
                from.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_OT_FROM;
                to.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_OT_TO;
                Reason.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_OT_REASON;

            }else if(typeholder == "Leave" && emppos == "Employee")
            {
                Approve.Visibility = Visibility.Hidden;
                Decline.Visibility = Visibility.Hidden;
                string time = DateTime.Now.ToString("hh:mm:ss tt");
                UserPending upend = new UserPending();
                upend.SpecificPendingLeave(idholder, typeholder);


                empname.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_NAME;
                emptype.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_TYPE;
                from.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_LEAVE_FROM;
                to.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_LEAVE_TO;
                Reason.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_LEAVE_REASON;

            }else if(typeholder == "Overtime" && emppos == "Employee")
            {
                Approve.Visibility = Visibility.Hidden;
                Decline.Visibility = Visibility.Hidden;
                string time = DateTime.Now.ToString("hh:mm:ss tt");
                UserPending upend = new UserPending();
                upend.SpecificPendingOT(idholder, typeholder);


                empname.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_NAME;
                emptype.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_TYPE;
                from.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_OT_FROM;
                to.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_OT_TO;
                Reason.Text = StaticApprovalItem.staticApprovalModalItem.PENDING_OT_REASON;
            }




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserPending upend = new UserPending();
            string stat = "Approved by Head";
            string appby = ""+mitem._FNAME+" "+mitem._LNAME+"";
            int empid = StaticApprovalItem.staticApprovalModalItem.PENDING_ID;
            upend.Approve(appby, empid,stat);
            MessageBox.Show("Approved!");
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserPending upend = new UserPending();
            string stat = "Declined by Head";
            string appby = "" + mitem._FNAME + " " + mitem._LNAME + "";
            int empid = StaticApprovalItem.staticApprovalModalItem.PENDING_ID;
            upend.Approve(appby, empid, stat);
            MessageBox.Show("Declined!");
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int empid = StaticApprovalItem.staticApprovalModalItem.PENDING_ID;
           
            if (MessageBox.Show("Are you Sure you want to Delete?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                UserPending upend = new UserPending();
                upend.DeleteSpecific(empid);
                MessageBox.Show("Request Deleted!");
                this.Close();
            }
            else
            {
                this.Close();
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
