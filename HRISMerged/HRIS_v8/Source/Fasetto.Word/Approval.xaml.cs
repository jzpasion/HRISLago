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
    /// Interaction logic for Approval.xaml
    /// </summary>

    public partial class Approval : Window
    {
        UserItem mitem = new UserItem();
        UserPending pitem = new UserPending();
        public Approval(UserItem item)
        {
            InitializeComponent();

            mitem = item;
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            StaticApprovalList.staticApprovalList.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string headstatus = "Approved by Head";
            
            pitem.RetrievePending(mitem._EMPID,headstatus,mitem._POSITION);

            approval.ItemsSource = StaticApprovalList.staticApprovalList;
            total.Content = StaticApprovalList.staticApprovalList.Count;
        }
        string id;
            string type;

        private void Approval_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object item2 = approval.SelectedItem;
            
            id = (approval.SelectedCells[0].Column.GetCellContent(item2) as TextBlock).Text;
            int idparse = Int32.Parse(id);
            type = (approval.SelectedCells[2].Column.GetCellContent(item2) as TextBlock).Text;

            string pos = "Head";

            Window main = GetWindow(this);
            main.Hide();
            ApprovalModal app = new ApprovalModal(mitem,idparse , type,pos);
            app.ShowDialog();
            main.Close();

            StaticApprovalList.staticApprovalList.Clear();
        }
        private void Pendings(object sender, RoutedEventArgs e)
        {
            StaticApprovalList.staticApprovalList.Clear();
            string headstatus = "Approved by Head";

            pitem.RetrievePending(mitem._EMPID, headstatus, mitem._POSITION);

            approval.ItemsSource = StaticApprovalList.staticApprovalList;
            pending.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StaticApprovalList.staticApprovalList.Clear();

            pitem.RetrieveDeclined(mitem._EMPID, mitem._POSITION);
            pending.Visibility = Visibility.Visible;
            approval.ItemsSource = StaticApprovalList.staticApprovalList;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StaticApprovalList.staticApprovalList.Clear();
  

            pitem.RetrieveApproved(mitem._EMPID, mitem._POSITION);
            pending.Visibility = Visibility.Visible;
            approval.ItemsSource = StaticApprovalList.staticApprovalList;
        }
    }

}