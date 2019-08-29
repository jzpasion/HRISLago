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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for PayrollUI.xaml
    /// </summary>
    public partial class PayrollUI : UserControl
    {
        string employee_no;
        public PayrollUI(string emp_no)
        {
            InitializeComponent();
            employee_no = emp_no;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
            EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UserPayroll upay = new UserPayroll();
            upay.UPayroll(employee_no);

            tbEmpId.Text = employee_no;
            tbFirstName.Text = PayrollDetails.paydetails.FIRST_NAME;
            tbMiddleName.Text = PayrollDetails.paydetails.MIDDLE_NAME;
            tbLastName.Text = PayrollDetails.paydetails.LAST_NAME;
            tbDepartment.Text = PayrollDetails.paydetails.DEPARTMENT;
            tbSSS.Text = PayrollDetails.paydetails.SSS;
            tbPagIbig.Text = PayrollDetails.paydetails.PAG_IBIG;
            tbPhHealth.Text = PayrollDetails.paydetails.PHIL;
            tbBir.Text = PayrollDetails.paydetails.BIR;
            tbBasicPay.Text = PayrollDetails.paydetails.MONTHLY_SALARY;
        }
    }
}
