using System.Windows.Controls;
using MaterialDesignThemes.Wpf.Transitions;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for EmployeeManagement.xaml
    /// </summary>
    public partial class EmployeeManagement : BasePage
    {
        public static Transitioner mEmpTransitioner = new Transitioner();
        public EmployeeManagement()
        {
            InitializeComponent();

            mEmpTransitioner = empTransitioner;

            AddEmployee addEmp = new AddEmployee();
        }
    }
}
