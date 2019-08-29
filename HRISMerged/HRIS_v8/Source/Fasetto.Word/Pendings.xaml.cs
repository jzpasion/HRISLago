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
    /// Interaction logic for Pendings.xaml
    /// </summary>
    public partial class Pendings : Window
    {
        UserItem mitem = new UserItem();

        public Pendings(UserItem item)
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
            StaticPendingList.staticPendingList.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserPending pitem = new UserPending();
            pitem.GetPending(mitem._EMPID);
          
            pending.ItemsSource = StaticPendingList.staticPendingList;

            total.Content = StaticPendingList.staticPendingList.Count; 

        }

        private void Pending_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object item2 = pending.SelectedItem;

            string id = (pending.SelectedCells[0].Column.GetCellContent(item2) as TextBlock).Text;
            int idparse = Int32.Parse(id);
            string type = (pending.SelectedCells[2].Column.GetCellContent(item2) as TextBlock).Text;
            string pos = "Employee";
            Window main = GetWindow(this);
            main.Hide();
            ApprovalModal app = new ApprovalModal(mitem, idparse, type,pos);
            app.ShowDialog();
            main.Close();
            StaticPendingList.staticPendingList.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to Delete?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                UserPending upend = new UserPending();
                upend.DeleteAll(mitem._EMPID);
                MessageBox.Show("All Request Deleted!");
                this.Close();
                StaticPendingList.staticPendingList.Clear();
            }
            else
            {
                this.Close();
            }

        }
    }
}
