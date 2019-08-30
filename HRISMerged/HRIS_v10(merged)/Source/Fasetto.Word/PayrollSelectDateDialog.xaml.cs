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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            PayrollUI payrollui = new PayrollUI(mSelectedEmpId);
            EmployeeManagement.mEmpTransitioner.Items.Add(payrollui);
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 1;
            Close();
        }
    }
}
