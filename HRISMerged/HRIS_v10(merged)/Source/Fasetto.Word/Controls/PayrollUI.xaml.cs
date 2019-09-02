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
        string datefrom;
        string dateto;
        string empdep;
        int payDays;
        public PayrollUI(string emp_no , string from , string to,string department,int days)
        {
            InitializeComponent();
            employee_no = emp_no;
            datefrom = from;
            dateto = to;
            empdep = department;
            payDays = days;
            var item = new EmployeeItem();
            item = StaticEmpoyeeCollection.staticEmployeeList.Where(t => t._EMP_NO.Equals(emp_no)).FirstOrDefault();

            PayrollTotalItem paytotal = new PayrollTotalItem();
            double netPay = PayrollTotals.Totals.Sum(x => x.TOTAL_EARNINGS);
            double absentlateDeduction = PayrollTotals.Totals.Sum(x => x.TOTAL_DEDUCTION);
            double totalOt = PayrollTotals.Totals.Sum(x => x.TOTAL_OVERTIME);
            
            
            //basic salary
            double basicpay = (item._MONTHLY_SALARY / 2);
            double basicpayround = Math.Round((double)basicpay, 2);

           
        
            //totalEarnings + overtime
            double totalEarnings = (netPay+absentlateDeduction);

            //days holiday/saturday paid
            double DaysPaid = ((basicpay / 13)*payDays);
            double roundDaysPaid = Math.Round((double)DaysPaid, 2);

            double count = PayrollTotals.Totals.Count() + payDays;

            double totalPaywithDays = (totalEarnings+ roundDaysPaid);

            //total Deduction
            double totalDeduction = (absentlateDeduction + item._DEDUC_BIR + item._DEDUC_PAG_IBIG + item._DEDUC_PHIL_HEALTH + item._DEDUC_SSS);

            //total netpay
            double totalNetPay = (totalPaywithDays - totalDeduction);


            tbEmpId.Text = item._EMP_NO;
            tbFirstName.Text = item._FIRST_NAME;
            tbMiddleName.Text = item._MIDDLE_NAME;
            tbLastName.Text = item._LAST_NAME;
            tbDepartment.Text = empdep;
            tbSSS.Text = item._SSS_NO;
            tbPagIbig.Text = item._PAG_IBIG_NO;
            tbPhHealth.Text = item._PHIL_HEALTH_NO;
            tbBir.Text = item._BIR_NO;
            dpWorkingFrom.Text = datefrom;
            dpWorkingTo.Text = dateto;
            tbOverTime.Text = totalOt.ToString();
            tbDedLateUndertimeAbsent.Text = absentlateDeduction.ToString();
            tbNetPay.Text = totalNetPay.ToString();
            tbWorkedDays.Text = count.ToString();
            tbBasicPay.Text = basicpayround.ToString();
            tbTotalEarnings.Text = totalPaywithDays.ToString();
            tbDedSSS.Text = item._DEDUC_SSS.ToString();
            tbDedPagIbig.Text = item._DEDUC_PAG_IBIG.ToString() ;
            tbDedPhHealth.Text = item._DEDUC_PHIL_HEALTH.ToString();
            tbDedBir.Text = item._DEDUC_BIR.ToString();
            tbTotalDeductions.Text = totalDeduction.ToString();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
            EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
            PayrollTotals.Totals.Clear();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

          
        }

    }
}
