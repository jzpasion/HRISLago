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

        private void ButtonLA_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonLA");
        }

        private void ButtonLA_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonLA");
        }

        private void ButtonOA_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonOA");
        }

        private void ButtonOA_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonOA");
        }

        private void ButtonRA_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonRA");
        }

        private void ButtonRA_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonRA");
        }

        private void ButtonPA_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("ElevateBtn", "buttonPA");
        }

        private void ButtonPA_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RunStoryBoardFromName("FlatBtn", "buttonPA");
        }
    }
}
