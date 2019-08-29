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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : BasePage
    {
        public Home()
        {
            InitializeComponent();

            Storyboard sb = this.FindResource("RaisingCards") as Storyboard;
            sb.Begin();
        }
    }
}
