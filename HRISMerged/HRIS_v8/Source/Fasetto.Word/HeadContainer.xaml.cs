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
    /// Interaction logic for HeadContainer.xaml
    /// </summary>
    public partial class HeadContainer : Window
    {
        UserItem mitem = new UserItem();
        public HeadContainer(UserItem item)
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

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Btn_profile_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);

            parentWindow.Hide();
            Profile mw = new Profile(mitem);
            mw.ShowDialog();
            parentWindow.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var requestWindow = Window.GetWindow(this);

            requestWindow.Hide();
            Request mw = new Request(mitem);
            mw.ShowDialog();
            requestWindow.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var atten = Window.GetWindow(this);

            atten.Hide();
            Attendance att = new Attendance(mitem);
            att.ShowDialog();
            atten.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var main = Window.GetWindow(this);
            main.Hide();
            Pendings pendings = new Pendings(mitem);
            pendings.ShowDialog();
            main.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var pw = Window.GetWindow(this);
            pw.Hide();
            Approval approve = new Approval(mitem);
            approve.ShowDialog();
            pw.Close();
        }
    }
}
