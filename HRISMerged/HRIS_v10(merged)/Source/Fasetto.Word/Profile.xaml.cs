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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        UserItem mitem = new UserItem();
        public Profile(UserItem item)
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
          
            empno.Text =  mitem._FNAME + " " + mitem._MNAME + " " + mitem._LNAME;
            empname.Text = mitem._EMPNO;
            emppos.Text = mitem._POSITION ;
            empdep.Text = mitem._DEPARTMENT;
        }
    }
}
