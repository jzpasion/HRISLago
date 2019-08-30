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
    /// Interaction logic for TimeKeepingForm.xaml
    /// </summary>
    public partial class TimeKeepingForm : Window
    {
        DateTime selectedFromdate = new DateTime();
        DateTime selectedToDate = new DateTime();
        public TimeKeepingForm()
        {
            InitializeComponent();

            dpFrom.SelectedDate = DateTime.Today;
            dpTo.SelectedDate = DateTime.Today;
        }
    }
}
