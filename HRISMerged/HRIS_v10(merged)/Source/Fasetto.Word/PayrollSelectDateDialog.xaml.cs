using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Fasetto.Word.Core;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for PayrollSelectDateDialog.xaml
    /// </summary>
    public partial class PayrollSelectDateDialog : Window
    {
        string mSelectedEmpId;
        string mEmpName;
        UserPayroll upay = new UserPayroll();
        public PayrollSelectDateDialog(string selectedEmpId)
        {
            InitializeComponent();

            mSelectedEmpId = selectedEmpId;

            var empItem = (EmployeeItem)StaticEmpoyeeCollection.staticEmployeeList.Where(t => t._EMP_NO.Equals(mSelectedEmpId)).FirstOrDefault();

            mEmpName = empItem._LAST_NAME + ", " + empItem._FIRST_NAME + " " + empItem._FIRST_NAME;

            tbEmployeeName.Text = mEmpName;
            dpPayFrom.SelectedDate = DateTime.Today;
            dpPayTo.SelectedDate = DateTime.Today;
        }

        public PayrollSelectDateDialog()
        {
            InitializeComponent();

            lblDialogName.Text = "Time Kepping Genarator";
            tbEmployeeName.Visibility = Visibility.Collapsed;
            Height = 350;
            Width = 400;
            pnlLoading.Visibility = Visibility.Collapsed;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {


            
            DateTime fd = DateTime.Parse(dpPayFrom.ToString());
            string passfrom = fd.ToString("MM/dd/yy");

            DateHolder.dates.FROM_DATE = passfrom;

            DateTime td = DateTime.Parse(dpPayTo.ToString());
            string passto = td.ToString("MM/dd/yy");
            DateHolder.dates.TO_DATE = passto;

            upay.GetId(mSelectedEmpId);

            int id = PayrollDetails.paydetails.EMP_ID;

            upay.between(id, passfrom, passto);


            int days = Convert.ToInt32(tdDays.Text);

            PayrollUI payrollui = new PayrollUI(mSelectedEmpId, passfrom, passto , PayrollDetails.paydetails.DEPARTMENT, days);
            EmployeeManagement.mEmpTransitioner.Items.Add(payrollui);
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 1;
            Close();
        }
    }
}
