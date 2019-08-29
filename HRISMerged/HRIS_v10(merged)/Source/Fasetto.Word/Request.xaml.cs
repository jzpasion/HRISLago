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
    /// Interaction logic for Request.xaml
    /// </summary>
    public partial class Request : Window
    {
        UserItem mitem = new UserItem();
        public Request(UserItem item)
        {
            InitializeComponent();
            mitem = item;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var leave = Window.GetWindow(this);

            leave.Hide();
            Leave le = new Leave(mitem);
            le.ShowDialog();
            leave.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var overtime = Window.GetWindow(this);

            overtime.Hide();
            Overtime over = new Overtime(mitem);
            over.ShowDialog();
            overtime.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var daily = Window.GetWindow(this);

            daily.Hide();
            Daily dailyrep = new Daily();
            dailyrep.ShowDialog();
            daily.Close();
        }
    }
}
