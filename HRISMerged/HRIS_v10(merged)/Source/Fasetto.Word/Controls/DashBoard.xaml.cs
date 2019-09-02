using Fasetto.Word.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : UserControl
    {
        public DashBoard()  
        {
            InitializeComponent();
            Storyboard sb = this.FindResource("PopupButton") as Storyboard;
            sb.Begin();
        }

        private void RunStoryBoardFromName(string animName, string targetName = null)
        {
            Storyboard storyBoard = (Storyboard)this.Resources[animName];
            if (targetName != null)
            {
                foreach (var anim in storyBoard.Children)
                {
                    Storyboard.SetTargetName(anim, targetName);
                }
            }
            storyBoard.Begin();
        }

        private void ButtonAE_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonAE");
        }

        private void ButtonAE_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonAE");
        }

        private void ButtonIE_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonIE");
        }

        private void ButtonIE_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonIE");
        }

        private void ButtonA_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonA");
        }

        private void ButtonA_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonA");
        }

        private void ButtonH_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "ButtonH");
        }

        private void ButtonH_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "ButtonH");
        }

        private void ButtonRA_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonRA");
        }

        private void ButtonRA_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonRA");
        }

        private void ButtonTK_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonTK");
        }

        private void ButtonTK_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonTK");
        }

        private void ButtonA_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var pw = Window.GetWindow(this);
            pw.Hide();
            Approval approve = new Approval(HRISMainWindow.mItem);
            approve.ShowDialog();
            pw.Close();
        }

        private void ButtonH_Click(object sender, RoutedEventArgs e)
        {
            HolidayManagerUI holidayManagetUI = new HolidayManagerUI();
            DashboardPage.mHolidayTransitioner.Items.Add(holidayManagetUI);
            DashboardPage.mHolidayTransitioner.SelectedIndex = 1;
        }

        private void ButtonTK_Click(object sender, RoutedEventArgs e)
        {
            PayrollSelectDateDialog tkDialog = new PayrollSelectDateDialog();
            tkDialog.ShowDialog();
        }
    }
}
