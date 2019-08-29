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
    /// Interaction logic for HRISContainer.xaml
    /// </summary>
    public partial class HRISContainer : Window
    {
        UserItem mitem = new UserItem();
        public HRISContainer(UserItem item)
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

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void Btn_profile_Click(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Hide();
            Profile pro = new Profile(mitem);
            pro.ShowDialog();
            parent.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Hide();
            Attendance att = new Attendance(mitem);
            att.ShowDialog();
            parent.Close();
        }
    }
}
