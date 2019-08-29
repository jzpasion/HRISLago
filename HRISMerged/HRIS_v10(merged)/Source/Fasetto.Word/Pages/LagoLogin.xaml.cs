using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Fasetto.Word.Core;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for LagoLogin.xaml
    /// </summary>
    public partial class LagoLogin : LoginBasePage
    {
        Userlogin myLogin = new Userlogin();

        public LagoLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);


            //if (tbUserName.Text != "")
            //{

            //    parentWindow.Hide();
            //    //parentWindow.Visibility = Visibility.Hidden;
            //    Time mw = new Time();
            //    mw.ShowDialog();
            //    parentWindow.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Please input User and Password");
            //}
            var user = myLogin.RetrieveUser(tbUserName.Text , pbPassword.Password);
            
            if(user != null)
            {
            
                parentWindow.Hide();
               parentWindow.Visibility = Visibility.Hidden;
                 Time mw = new Time(user);
                 mw.ShowDialog();
                parentWindow.Close();
            }
            else
            {
                MessageBox.Show("User not found");
            }

        }
    }
}
