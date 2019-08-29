using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Fasetto.Word.Core;

namespace Fasetto.Word
{

    /// <summary>
    /// Interaction logic for EmployeeTable.xaml
    /// </summary>
    public partial class EmployeeTable : UserControl
    {

        EmployeeManager myManager = new EmployeeManager();
        EmployeeCollection myCollection = new EmployeeCollection();
        string selectedEmployeeId;
        ObservableCollection<EmployeeItem> empCollection = new ObservableCollection<EmployeeItem>();
        public EmployeeTable()
        {
            InitializeComponent();

            //EmployeeItem mark = new EmployeeItem();
            //mark._employeeId = "124asf";
            //mark._firstName = "Mark";
            //mark._middleName = "Sese";
            //mark._lastName = "Del Moro";
            //mark._nationality = "Filipino";
            //mark._eMail = "mark@gmail.com";
            //mark._contactNum = "0999999";
            //mark._religion = "INC";

            //EmployeeItem mark2 = new EmployeeItem();
            //mark2._employeeId = "34fgd";
            //mark2._firstName = "Mark2";
            //mark2._middleName = "Sese2";
            //mark2._lastName = "Del Moro2";
            //mark2._nationality = "Filipino2";
            //mark2._eMail = "mark@gmail.com2";
            //mark2._contactNum = "09999992";
            //mark2._religion = "INC2";

            //employeeTable.Items.Add(mark);
            //employeeTable.Items.Add(mark2);

            GetAllEmployees();

            Storyboard sb = this.FindResource("ButtonPopup") as Storyboard;
            sb.Begin();
        }

        private void ButtonViewEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
            {
                MessageBox.Show("Please select employee");
            }
            else
            {
                EducationCollection selectedEduList = new EducationCollection();
                ExperienceCollection selectedExpList = new ExperienceCollection();
                TrainingCollection selectedTrainList = new TrainingCollection();

                var vEmpItem = new EmployeeItem();
                var vEduList = new List<EducationItem>();
                var vExpList = new List<ExperienceItem>();
                var vTrainList = new List<TrainingItem>();
                vEmpItem = empCollection.Where(t => t._EMP_NO.Equals(selectedEmployeeId)).FirstOrDefault();
                vEduList = selectedEduList.RetreiveEmpEducation(vEmpItem._EMP_NO);
                vExpList = selectedExpList.RetreiveEmpExperience(vEmpItem._EMP_NO);
                vTrainList = selectedTrainList.RetreiveEmpTraining(vEmpItem._EMP_NO);
                ViewEmployeeDetails viewEmp = new ViewEmployeeDetails(vEmpItem, vEduList, vExpList, vTrainList);
                EmployeeManagement.mEmpTransitioner.Items.Add(viewEmp);
                EmployeeManagement.mEmpTransitioner.SelectedIndex = 1;
                GetAllEmployees();
            }
        }

        private void GetAllEmployees()
        {
            empCollection = StaticEmpoyeeCollection.staticEmployeeList;

            employeeTable.ItemsSource = empCollection;
        }

        private void ButtonNewEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddEmployee addEmp = new AddEmployee();
            EmployeeManagement.mEmpTransitioner.Items.Add(addEmp);
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 1;
            GetAllEmployees();
        }

        private void ButtonDeleteEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
            {
                MessageBox.Show("Please select employee");
            }
            else
            {
                DeleteEmployee();
            }
        }

        private void DeleteEmployee()
        {
            string sMessageBoxText = "Do you want to delete this employee?";
            string sCaption = "Are you Sure?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = System.Windows.MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    myManager.DeleteData(selectedEmployeeId);
                    myCollection.RetreiveAllEmployee();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void AddToObservable()
        {
            EmployeeItem mark2 = new EmployeeItem();
            //mark2._employeeId = "34fgd";
            //mark2._firstName = "Mark2";
            //mark2._middleName = "Sese2";
            //mark2._lastName = "Del Moro2";
            //mark2._nationality = "Filipino2";
            //mark2._eMail = "mark@gmail.com2";
            //mark2._contactNum = "09999992";
            //mark2._religion = "INC2";
            //mark2._presentAddress = "mt.veiw";
            //mark2._permanentAddress = "Gatiawin";

            StaticEmpoyeeCollection.staticEmployeeList.Add(mark2);
        }

        private void Row_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            object item = employeeTable.SelectedItem;
            selectedEmployeeId = (employeeTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
        }

        private void PersistentSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                filterEmpTable();
            }
            catch (Exception x)
            {

            }
            
        }

        private void ButtonAddPosition_Click(object sender, RoutedEventArgs e)
        {
            PositionManagerUI posManagetUI = new PositionManagerUI();
            EmployeeManagement.mEmpTransitioner.Items.Add(posManagetUI);
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 1;
        }

        private void filterEmpTable()
        {
            employeeTable.ItemsSource = empCollection.Where(t => t._EMP_NO.ToLower().Contains(searchBox.SearchTerm.ToLower()) || t._FIRST_NAME.ToLower().Contains(searchBox.SearchTerm.ToLower()) || t._MIDDLE_NAME.ToLower().Contains(searchBox.SearchTerm.ToLower()) || t._LAST_NAME.ToLower().Contains(searchBox.SearchTerm.ToLower()));
        }

        private void SearchBox_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            filterEmpTable();
        }

        private void SearchBox_Search(object sender, MaterialDesignExtensions.Controls.SearchEventArgs args)
        {
            filterEmpTable();
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                filterEmpTable();
            }
            catch (Exception x)
            {
                employeeTable.ItemsSource = empCollection;
            }
        }

        private void ButtonPayroll_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
            {
                MessageBox.Show("Please select employee");
            }
            else
            {
      

                PayrollUI payrollui = new PayrollUI(selectedEmployeeId);
                EmployeeManagement.mEmpTransitioner.Items.Add(payrollui);
                EmployeeManagement.mEmpTransitioner.SelectedIndex = 1;


                GetAllEmployees();
            }
        }
    }
}
