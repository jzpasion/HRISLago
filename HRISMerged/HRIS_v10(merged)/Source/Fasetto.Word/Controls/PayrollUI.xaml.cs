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

            var item = new EmployeeItem();
            item = StaticEmpoyeeCollection.staticEmployeeList.Where(t => t._EMP_NO.Equals(emp_no)).FirstOrDefault();


            tbEmpId.Text = item._EMP_NO;
            tbFirstName.Text = item._FIRST_NAME;
            tbMiddleName.Text = item._MIDDLE_NAME;
            tbLastName.Text = item._LAST_NAME;
            //tbDepartment.Text = item.;
            tbSSS.Text = item._SSS_NO;
            tbPagIbig.Text = item._PAG_IBIG_NO;
            tbPhHealth.Text = item._PHIL_HEALTH_NO;
            tbBir.Text = item._BIR_NO;
            //tbBasicPay.Text = PayrollDetails.paydetails.MONTHLY_SALARY.ToString();
            //tbDedSSS.Text = PayrollDetails.paydetails.DEDUC_SSS.ToString();
            //tbDedPagIbig.Text = PayrollDetails.paydetails.DEDUC_PAG_IBIG.ToString();
            //tbDedPhHealth.Text = PayrollDetails.paydetails.DEDUC_PHIL.ToString();
            //tbDedBir.Text = PayrollDetails.paydetails.DEDUC_BIR.ToString();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
            EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

          
        }
    }
}
