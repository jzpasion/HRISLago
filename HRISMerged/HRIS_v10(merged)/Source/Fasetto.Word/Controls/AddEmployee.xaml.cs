using System.Windows;
using System.Windows.Controls;
using Fasetto.Word.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : UserControl
    {
        //int selectedDesignation = -1;
        ObservableCollection<EducationItem> vocationalEduCollection = new ObservableCollection<EducationItem>();
        ObservableCollection<ExperienceItem> workExpCollection = new ObservableCollection<ExperienceItem>();
        ObservableCollection<TrainingItem> trainCollection = new ObservableCollection<TrainingItem>();
        EducationItem selectedEduItem = new EducationItem();
        ExperienceItem selectedExpItem = new ExperienceItem();
        TrainingItem selectedTrainItem = new TrainingItem();
        string selectedEmpStatus;
        bool hasSelectedEdu = false, hasSelectedWork = false, hasSelectedTraining = false;
        double parseResult;
        public AddEmployee()
        {
            InitializeComponent();
            
            dpBirthday.SelectedDate = DateTime.Today;
            dpDateEmployed.SelectedDate = DateTime.Today;
            dpEduDoctoralDate.SelectedDate = DateTime.Today;
            dpEduMasteralDate.SelectedDate = DateTime.Today;
            dpEduPrimaryDate.SelectedDate = DateTime.Today;
            dpEduSecondaryDate.SelectedDate = DateTime.Today;
            dpEduTertiaryDate.SelectedDate = DateTime.Today;
            dpEduVocationalDate.SelectedDate = DateTime.Today;
            dpEndProvision.SelectedDate = DateTime.Today;
            dpTrainingFinished.SelectedDate = DateTime.Today;
            dpWorkEnd.SelectedDate = DateTime.Today;
            dpWorkStart.SelectedDate = DateTime.Today;

            cbPosition.ItemsSource = StaticPositionCollection.staticPositionList;
            cbEmpStatus.ItemsSource = Enum.GetValues(typeof(EmployeeStatus)).Cast<EmployeeStatus>();

            string stringGenID = "";
            bool isIdUnique = false;

            while (!isIdUnique)
            {
                stringGenID = generatedID();
                isIdUnique = isGeneretedIDUnique(stringGenID);
            }

            tbEmployeeId.Text = stringGenID;
        }

        private bool isGeneretedIDUnique(string genId)
        {
            var tempEmpList = StaticEmpoyeeCollection.staticEmployeeList.Where(t => t._EMP_NO.Equals(genId));

            if (tempEmpList.Count() != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string generatedID()
        {
            Random generator = new Random();
            return generator.Next(0, 99999).ToString("D5");
        }

        private string genderSelectResult()
        {
            if (rbMale.IsChecked == true)
            {
                return "Male";
            }
            else if (rbFemale.IsChecked == true)
            {
                return "Female";
            }
            else
            {
                return "";
            }
        }

        private bool InputValidation()
        {
            if (tbEmployeeId.Text == "" || tbEmployeeId.Text == null)
            {
                MessageBox.Show("Please input Employee ID");
                return false;
            }
            else if (tbFirstName.Text == "" || tbFirstName.Text == null)
            {
                MessageBox.Show("Please input First Name");
                return false;
            }
            else if (tbMiddleName.Text == "" || tbMiddleName.Text == null)
            {
                MessageBox.Show("Please input Middle Name");
                return false;
            }
            else if (tbLastName.Text == "" || tbLastName.Text == null)
            {
                MessageBox.Show("Please input Last Name");
                return false;
            }
            else if (pbPassword.Password == "" || pbPassword.Password == null)
            {
                MessageBox.Show("Please input Password");
                return false;
            }
            else if (pbConfirmPass.Password == "" || pbConfirmPass.Password == null)
            {
                MessageBox.Show("Please input Confirmation Password");
                return false;
            }
            else if (rbMale.IsChecked == false && rbFemale.IsChecked == false)
            {
                MessageBox.Show("Please select gender");
                return false;
            }
            else if (dpBirthday.Text == "" || dpBirthday.Text == null)
            {
                MessageBox.Show("Please input Birth Date");
                return false;
            }
            else if (tbNationality.Text == "" || tbNationality == null)
            {
                MessageBox.Show("Please input Nationality");
                return false;
            }
            else if (tbPresentAddress.Text == "" || tbPresentAddress.Text == null)
            {
                MessageBox.Show("Please input Present Address");
                return false;
            }
            else if (string.IsNullOrEmpty(tbSchoolAddressPrimary.Text) && !string.IsNullOrEmpty(tbSchoolNamePrimary.Text))
            {
                MessageBox.Show("Please input Primary school address.");
                return false;
            }
            else if (!string.IsNullOrEmpty(tbSchoolAddressPrimary.Text) && string.IsNullOrEmpty(tbSchoolNamePrimary.Text))
            {
                MessageBox.Show("Please input Primary school name.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbSchoolAddressSecondary.Text) && !string.IsNullOrEmpty(tbSchoolNameSecondary.Text))
            {
                MessageBox.Show("Please input Secondary school address.");
                return false;
            }
            else if (!string.IsNullOrEmpty(tbSchoolAddressSecondary.Text) && string.IsNullOrEmpty(tbSchoolNameSecondary.Text))
            {
                MessageBox.Show("Please input Secondary school name.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbSchoolAddressTertiary.Text) && !string.IsNullOrEmpty(tbSchoolNameTertiary.Text))
            {
                MessageBox.Show("Please input Tertiary school address.");
                return false;
            }
            else if (!string.IsNullOrEmpty(tbSchoolAddressTertiary.Text) && string.IsNullOrEmpty(tbSchoolNameTertiary.Text))
            {
                MessageBox.Show("Please input Tertiary school name.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbDegreeTertiary.Text) && !string.IsNullOrEmpty(tbSchoolNameTertiary.Text))
            {
                MessageBox.Show("Please input Tertiary Degree Earned.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbSchoolAddressMasteral.Text) && !string.IsNullOrEmpty(tbSchoolNameMasteral.Text))
            {
                MessageBox.Show("Please input Masteral school address.");
                return false;
            }
            else if (!string.IsNullOrEmpty(tbSchoolAddressMasteral.Text) && string.IsNullOrEmpty(tbSchoolNameMasteral.Text))
            {
                MessageBox.Show("Please input Masteral school name.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbDegreeMasteral.Text) && !string.IsNullOrEmpty(tbSchoolNameMasteral.Text))
            {
                MessageBox.Show("Please input Masteral Degree Earned.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbCompanyName.Text) && !string.IsNullOrEmpty(tbDesignation.Text))
            {
                MessageBox.Show("Please input Company name.");
                return false;
            }
            else if (!string.IsNullOrEmpty(tbCompanyName.Text) && string.IsNullOrEmpty(tbDesignation.Text))
            {
                MessageBox.Show("Please input Designation.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbComanyLocation.Text) && !string.IsNullOrEmpty(tbDesignation.Text))
            {
                MessageBox.Show("Please input Company location.");
                return false;
            }
            else if (IsWorkExpDateInvalid(dpWorkStart.Text, dpWorkEnd.Text) && !string.IsNullOrEmpty(tbDesignation.Text))
            {
                MessageBox.Show("Invalid working experience date");
                return false;
            }
            else if (string.IsNullOrEmpty(tbInstitution.Text) && !string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("Please input training institution.");
                return false;
            }
            else if (!string.IsNullOrEmpty(tbInstitution.Text) && string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("Please input training title.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbLocation.Text) && !string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("Please input training location.");
                return false;
            }
            else if (!double.TryParse(tbMonthlySalary.Text, out parseResult))
            {
                MessageBox.Show("Please input valid employee salary.");
                return false;
            }
            else if (!double.TryParse(tbDedSSS.Text, out parseResult))
            {
                MessageBox.Show("Please input valid SSS deduction amount.");
                return false;
            }
            else if (!double.TryParse(tbDedPagIbig.Text, out parseResult))
            {
                MessageBox.Show("Please input valid Pag-Ibig deduction amount.");
                return false;
            }
            else if (!double.TryParse(tbDedPhilHealth.Text, out parseResult))
            {
                MessageBox.Show("Please input valid Phil Health deduction amount.");
                return false;
            }
            else if (!double.TryParse(tbDedBIR.Text, out parseResult))
            {
                MessageBox.Show("Please input valid BIR deduction amount.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsWorkExpDateInvalid(string workStart, string workEnd)
        {
            DateTime cWorkStart = DateTime.Parse(workStart);
            DateTime cWorkEnd = DateTime.Parse(workEnd);
            if (cWorkStart.Date < cWorkEnd.Date)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetValues()
        {
            if (InputValidation())
            {
                if (pbPassword.Password != pbConfirmPass.Password)
                {
                    MessageBox.Show("Password did'nt match the Confirm Password");
                }
                else
                {
                    EmployeeItem item = new EmployeeItem();

                    item._EMP_NO = tbEmployeeId.Text;
                    item._FIRST_NAME = tbFirstName.Text;
                    item._MIDDLE_NAME = tbMiddleName.Text;
                    item._LAST_NAME = tbLastName.Text;
                    item._EMP_PASSWORD = pbPassword.Password;

                    if (rbMale.IsChecked == true)
                    {
                        item._GENDER = "Male";
                    }
                    else if (rbFemale.IsChecked == true)
                    {
                        item._GENDER = "Female";
                    }
                    item._BIRTHDAY = dpBirthday.Text;
                    item._NATIONALITY = tbNationality.Text;
                    item._PASSPORT = tbPassportNo.Text;
                    item._RELIGION = tbReligion.Text;
                    item._EMAIL_ADDRESS = tbEmail.Text;
                    item._CONTACT = tbContactNumber.Text;
                    item._BIRTH_PLACE = tbBirthPlace.Text;
                    item._PRESENT_ADDRESS = tbPresentAddress.Text;
                    item._PERMANENT_ADDRESS = tbPermanentAddress.Text;
                    item._IOE_PERSON = tbNamePTC.Text;
                    item._IOE_CONTACT = tbContactPTC.Text;
                    item._IOE_RELATION = tbRelationPTC.Text;
                    item._IOE_ADDRESS = tbAddressPTC.Text;
                    item._EMP_STATUS = selectedEmpStatus;
                    item._DATE_JOINED = dpDateEmployed.Text;
                    item._END_PROVITION = dpEndProvision.Text;
                    item._POS_ID = (int)cbPosition.SelectedValue;
                    item._MONTHLY_SALARY = double.Parse(tbMonthlySalary.Text);
                    item._SSS_NO = tbSSSNo.Text;
                    item._PHIL_HEALTH_NO = tbPhHealth.Text;
                    item._PAG_IBIG_NO = tbPagIbig.Text;
                    item._BIR_NO = tbBIR.Text;
                    item._DEDUC_SSS = double.Parse(tbDedSSS.Text);
                    item._DEDUC_PHIL_HEALTH = double.Parse(tbDedPhilHealth.Text);
                    item._DEDUC_PAG_IBIG = double.Parse(tbDedPagIbig.Text);
                    item._DEDUC_BIR = double.Parse(tbDedBIR.Text);

                    item._HOURLY_RATE = 300;

                    SetEduBackground(item._EMP_NO);
                    SetExpBackground(item._EMP_NO);
                    SetTrainBackground(item._EMP_NO);

                    try
                    {
                        SaveEmpoyeeDetails(item);
                        MessageBox.Show("New Employee added.");
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Error saving new employee.");
                    }

                    EmployeeCollection myEmpList = new EmployeeCollection();
                    myEmpList.RetreiveAllEmployee();

                    //ClearInputFields();

                    EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
                    EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
 
                }
            }
        }

        private void SetEduBackground(string empId)
        {
            EducationManager mEduManager = new EducationManager();
            List<EducationItem> eduBackgroundList = new List<EducationItem>();

            if (!string.IsNullOrEmpty(tbSchoolNamePrimary.Text))
            {
                var eduItem = new EducationItem();
                eduItem._EMP_ID = empId;
                eduItem._EDU_LEVEL = "Primary";
                eduItem._EDU_SCHOOL_NAME = tbSchoolNamePrimary.Text;
                eduItem._EDU_SCHOOL_ADDRESS = tbSchoolAddressPrimary.Text;
                eduItem._EDU_DATE_GRADUATED = dpEduPrimaryDate.Text;

                eduBackgroundList.Add(eduItem);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameSecondary.Text))
            {
                var eduItem = new EducationItem();
                eduItem._EMP_ID = empId;
                eduItem._EDU_LEVEL = "Secondary";
                eduItem._EDU_SCHOOL_NAME = tbSchoolNameSecondary.Text;
                eduItem._EDU_SCHOOL_ADDRESS = tbSchoolAddressPrimary.Text;
                eduItem._EDU_DATE_GRADUATED = dpEduSecondaryDate.Text;

                eduBackgroundList.Add(eduItem);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameTertiary.Text))
            {
                var eduItem = new EducationItem();
                eduItem._EMP_ID = empId;
                eduItem._EDU_LEVEL = "Tertiary";
                eduItem._EDU_SCHOOL_NAME = tbSchoolNameTertiary.Text;
                eduItem._EDU_SCHOOL_ADDRESS = tbSchoolAddressTertiary.Text;
                eduItem._EDU_DATE_GRADUATED = dpEduTertiaryDate.Text;
                eduItem._EDU_DEGREE_EARNED = tbDegreeTertiary.Text;

                eduBackgroundList.Add(eduItem);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameMasteral.Text))
            {
                var eduItem = new EducationItem();
                eduItem._EMP_ID = empId;
                eduItem._EDU_LEVEL = "Masteral";
                eduItem._EDU_SCHOOL_NAME = tbSchoolNameMasteral.Text;
                eduItem._EDU_SCHOOL_ADDRESS = tbSchoolAddressMasteral.Text;
                eduItem._EDU_DATE_GRADUATED = dpEduMasteralDate.Text;
                eduItem._EDU_DEGREE_EARNED = tbDegreeMasteral.Text;

                eduBackgroundList.Add(eduItem);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameDoctoral.Text))
            {
                var eduItem = new EducationItem();
                eduItem._EMP_ID = empId;
                eduItem._EDU_LEVEL = "Doctoral";
                eduItem._EDU_SCHOOL_NAME = tbSchoolNameDoctoral.Text;
                eduItem._EDU_SCHOOL_ADDRESS = tbSchoolAddressDoctoral.Text;
                eduItem._EDU_DATE_GRADUATED = dpEduDoctoralDate.Text;
                eduItem._EDU_DEGREE_EARNED = tbDegreeDoctoral.Text;

                eduBackgroundList.Add(eduItem);
            }

            if (vocationalEduCollection.Count != 0)
            {
                vocationalEduCollection.Remove(selectedEduItem);

                foreach (var item in vocationalEduCollection)
                {
                    eduBackgroundList.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(tbSchoolNameVocational.Text))
            {
                var eduItem = new EducationItem();
                eduItem._EMP_ID = empId;
                eduItem._EDU_LEVEL = "Vocational";
                eduItem._EDU_SCHOOL_NAME = tbSchoolNameVocational.Text;
                eduItem._EDU_SCHOOL_ADDRESS = tbSchoolAddressVocational.Text;
                eduItem._EDU_DATE_GRADUATED = dpEduVocationalDate.Text;
                eduItem._EDU_DEGREE_EARNED = tbDegreeVocational.Text;

                eduBackgroundList.Add(eduItem);
            }

            foreach (var item in eduBackgroundList)
            {
                mEduManager.SaveEduData(item);
            }
        }

        private void SetExpBackground(string empId)
        {
            ExperienceManager mExpManager = new ExperienceManager();
            List<ExperienceItem> workExpList = new List<ExperienceItem>();

            if (workExpCollection.Count != 0)
            {
                workExpCollection.Remove(selectedExpItem);

                foreach (var item in workExpCollection)
                {
                    workExpList.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(tbDesignation.Text))
            {
                var expItem = new ExperienceItem();
                expItem._EMP_NO = empId;
                expItem._DESIGNATION = tbDesignation.Text;
                expItem._COMPANY = tbCompanyName.Text;
                expItem._WORK_LOCATION = tbComanyLocation.Text;
                expItem._DATE_START = dpWorkStart.Text;
                expItem._DATE_END = dpWorkEnd.Text;

                workExpList.Add(expItem);
            }

            foreach (var item in workExpList)
            {
                mExpManager.SaveExpData(item);
            }
        }

        private void SetTrainBackground(string empId)
        {
            TrainingManager mTrainManager = new TrainingManager();
            List<TrainingItem> trainList = new List<TrainingItem>();

            if (trainCollection.Count != 0)
            {
                trainCollection.Remove(selectedTrainItem);

                foreach (var item in trainCollection)
                {
                    trainList.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(tbTitle.Text))
            {
                var trainItem = new TrainingItem();
                trainItem._EMP_ID = empId;
                trainItem._TITLE = tbTitle.Text;
                trainItem._INSTITUTION = tbInstitution.Text;
                trainItem._TRAINING_LOCATION = tbLocation.Text;
                trainItem._TRAINING_DATE = dpTrainingFinished.Text;

                trainList.Add(trainItem);
            }

            foreach (var item in trainList)
            {
                mTrainManager.SaveTrainingData(item);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        private void SaveEmpoyeeDetails(EmployeeItem myItem)
        {
            EmployeeManager myManager = new EmployeeManager();
            myManager.SaveData(myItem);
        }

        private void ClearInputFields()
        {
            tbEmployeeId.Text = "";
            tbFirstName.Text = "";
            tbMiddleName.Text = "";
            tbLastName.Text = "";
            tbNationality.Text = "";
            tbEmail.Text = "";
            tbContactNumber.Text = "";
            tbReligion.Text = "";
            tbPresentAddress.Text = "";
            tbPermanentAddress.Text = "";
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
            EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
        }

        private void CbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var item = cbPosition.SelectedItem as DesignationItem;
            //selectedDesignation = Int32.Parse(item._POS_ID.ToString());
            //MessageBox.Show(selectedDesignation.ToString());
        }

        private void CbEmpStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmpStatus = cbEmpStatus.SelectedValue.ToString();
        }

        private void BtnAddVocational_Click(object sender, RoutedEventArgs e)
        {
            if(VocEduValidation())
            {
                if (hasSelectedEdu)
                {
                    hasSelectedEdu = false;
                    ChangeVocBtnIcon();
                    ClearVocationalEduFields();
                    cbVocationalCollect.SelectedIndex = -1;

                    selectedEduItem = null;
                }
                else
                {
                    var eduItem = new EducationItem();
                    eduItem._EMP_ID = tbEmployeeId.Text;
                    eduItem._EDU_LEVEL = "Vocational";
                    eduItem._EDU_SCHOOL_NAME = tbSchoolNameVocational.Text;
                    eduItem._EDU_SCHOOL_ADDRESS = tbSchoolAddressVocational.Text;
                    eduItem._EDU_DATE_GRADUATED = dpEduVocationalDate.Text;
                    eduItem._EDU_DEGREE_EARNED = tbDegreeVocational.Text;
                    eduItem._EDU_HOLDER = "Education " + (vocationalEduCollection.Count + 1);

                    vocationalEduCollection.Add(eduItem);

                    cbVocationalCollect.ItemsSource = vocationalEduCollection;

                    ClearVocationalEduFields();

                    MessageBox.Show("New School Record added.");
                }
            }
        }

        private bool VocEduValidation()
        {
            if (string.IsNullOrEmpty(tbSchoolAddressVocational.Text))
            {
                MessageBox.Show("Please input Vocational school address.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbSchoolNameVocational.Text))
            {
                MessageBox.Show("Please input Vocational school name.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbDegreeVocational.Text))
            {
                MessageBox.Show("Please input Vocational Degree Earned.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbEmployeeId.Text))
            {
                MessageBox.Show("Please input Employee ID");
                return false;
            }
            else
            {
                return true;
            }

        }

        private bool WorkExpValidation()
        {
            if (string.IsNullOrEmpty(tbCompanyName.Text))
            {
                MessageBox.Show("Please input Company name.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbDesignation.Text))
            {
                MessageBox.Show("Please input Designation.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbComanyLocation.Text))
            {
                MessageBox.Show("Please input Company location.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbEmployeeId.Text))
            {
                MessageBox.Show("Please input Employee ID");
                return false;
            }
            else if (IsWorkExpDateInvalid(dpWorkStart.Text, dpWorkEnd.Text))
            {
                MessageBox.Show("Invalid working experience date");
                return false;
            }
            else
            {
                return true;
            }

        }

        private bool TrainingValidation()
        {
            if (string.IsNullOrEmpty(tbInstitution.Text))
            {
                MessageBox.Show("Please input training institution.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("Please input training title.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbLocation.Text))
            {
                MessageBox.Show("Please input training location.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbEmployeeId.Text))
            {
                MessageBox.Show("Please input Employee ID");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ClearVocationalEduFields()
        {
            tbSchoolNameVocational.Text = "";
            tbSchoolAddressVocational.Text = "";
            tbDegreeVocational.Text = "";

            dpEduVocationalDate.SelectedDate = DateTime.Today;
        }

        private void ClearWorkExpFields()
        {
            tbDesignation.Text = "";
            tbCompanyName.Text = "";
            tbComanyLocation.Text = "";

            dpWorkStart.SelectedDate = DateTime.Today;
            dpWorkEnd.SelectedDate = DateTime.Today;
        }

        private void ClearTrainingFields()
        {
            tbTitle.Text = "";
            tbInstitution.Text = "";
            tbLocation.Text = "";

            dpTrainingFinished.SelectedDate = DateTime.Today;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //cbVocationalCollect.ItemsSource = vocationalEdu;
        }

        private void CbVocationalCollect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVocationalCollect.SelectedIndex != -1)
            {
                var item = cbVocationalCollect.SelectedItem as EducationItem;
                selectedEduItem = item;

                ViewSelectedEdu();
                hasSelectedEdu = true;
                ChangeVocBtnIcon();
            }
        }

        private void ChangeVocBtnIcon()
        {
            if (!hasSelectedEdu)
            {
                btnVocAdd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Add;
                btnSaveVocational.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnVocAdd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Cancel;
                btnSaveVocational.Visibility = Visibility.Visible;
            }
        }

        private void ChangeWorkBtnIcon()
        {
            if (!hasSelectedWork)
            {
                btnWorkAdd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Add;
                btnSaveWorkExp.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnWorkAdd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Cancel;
                btnSaveWorkExp.Visibility = Visibility.Visible;
            }
        }

        private void ChangeTrainBtnIcon()
        {
            if (!hasSelectedTraining)
            {
                btnTrainAdd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Add;
                btnSaveTrainings.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnTrainAdd.Kind = MaterialDesignThemes.Wpf.PackIconKind.Cancel;
                btnSaveTrainings.Visibility = Visibility.Visible;
            }
        }

        private void ViewSelectedEdu()
        {
            if (cbVocationalCollect.SelectedIndex != -1)
            {
                tbSchoolNameVocational.Text = selectedEduItem._EDU_SCHOOL_NAME;
                tbSchoolAddressVocational.Text = selectedEduItem._EDU_SCHOOL_ADDRESS;
                tbDegreeVocational.Text = selectedEduItem._EDU_DEGREE_EARNED;
                dpEduVocationalDate.SelectedDate = DateTime.Parse(selectedEduItem._EDU_DATE_GRADUATED);
            }
        }

        private void ViewSelectedExp()
        {
            if (cbWorkCollection.SelectedIndex != -1)
            {
                tbDesignation.Text = selectedExpItem._DESIGNATION;
                tbCompanyName.Text = selectedExpItem._COMPANY;
                tbComanyLocation.Text = selectedExpItem._WORK_LOCATION;
                dpWorkStart.SelectedDate = DateTime.Parse(selectedExpItem._DATE_START);
                dpWorkEnd.SelectedDate = DateTime.Parse(selectedExpItem._DATE_END);
            }
        }

        private void ViewSelectedTraining()
        {
            if (cbTrainingCollection.SelectedIndex != -1)
            {
                tbTitle.Text = selectedTrainItem._TITLE;
                tbInstitution.Text = selectedTrainItem._INSTITUTION;
                tbLocation.Text = selectedTrainItem._TRAINING_LOCATION;
                dpTrainingFinished.SelectedDate = DateTime.Parse(selectedTrainItem._TRAINING_DATE);
            }
        }

        private void BtnSaveVocational_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSchoolNameVocational.Text) && string.IsNullOrEmpty(tbSchoolAddressVocational.Text) && string.IsNullOrEmpty(tbDegreeVocational.Text))
            {
                vocationalEduCollection.Remove(selectedEduItem);
            }
            else
            {
                if (VocEduValidation())
                {
                    var item = selectedEduItem;
                    item._EDU_SCHOOL_NAME = tbSchoolNameVocational.Text;
                    item._EDU_SCHOOL_ADDRESS = tbSchoolAddressVocational.Text;
                    item._EDU_DEGREE_EARNED = tbDegreeVocational.Text;
                    item._EDU_DATE_GRADUATED = dpEduVocationalDate.Text;

                }
            }
            hasSelectedEdu = false;
            ChangeVocBtnIcon();
            ClearVocationalEduFields();
            cbVocationalCollect.SelectedIndex = -1;

            selectedEduItem = null;

            MessageBox.Show("School record successfully updated.");
        }

        private void CbWorkCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbWorkCollection.SelectedIndex != -1)
            {
                var item = cbWorkCollection.SelectedItem as ExperienceItem;
                selectedExpItem = item;

                ViewSelectedExp();
                hasSelectedWork = true;
                ChangeWorkBtnIcon();
            }
        }

        private void BtnSaveWorkExp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbDesignation.Text) && string.IsNullOrEmpty(tbCompanyName.Text) && string.IsNullOrEmpty(tbComanyLocation.Text))
            {
                workExpCollection.Remove(selectedExpItem);
            }
            else
            {
                if (WorkExpValidation())
                {
                    var item = selectedExpItem;
                    item._DESIGNATION = tbDesignation.Text;
                    item._COMPANY = tbCompanyName.Text;
                    item._WORK_LOCATION = tbComanyLocation.Text;
                    item._DATE_START = dpWorkStart.Text;
                    item._DATE_END = dpWorkEnd.Text;
                }
            }

            hasSelectedWork = false;
            ChangeWorkBtnIcon();
            ClearWorkExpFields();
            cbWorkCollection.SelectedIndex = -1;

            selectedExpItem = null;

            MessageBox.Show("Work experience record successfully updated.");
        }

        private void BtnSaveTrainings_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTitle.Text) && string.IsNullOrEmpty(tbInstitution.Text) && string.IsNullOrEmpty(tbLocation.Text))
            {
                trainCollection.Remove(selectedTrainItem);
            }
            else
            {
                if (TrainingValidation())
                {
                    var item = selectedTrainItem;
                    item._TITLE = tbTitle.Text;
                    item._INSTITUTION = tbInstitution.Text;
                    item._TRAINING_LOCATION = tbLocation.Text;
                    item._TRAINING_DATE = dpTrainingFinished.Text;
                }
            }

            hasSelectedTraining = false;
            ChangeTrainBtnIcon();
            ClearTrainingFields();
            cbTrainingCollection.SelectedIndex = -1;

            selectedTrainItem = null;

            MessageBox.Show("Training record successfully updated.");
        }

        private void CbTrainingCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTrainingCollection.SelectedIndex != -1)
            {
                var item = cbTrainingCollection.SelectedItem as TrainingItem;
                selectedTrainItem = item;

                ViewSelectedTraining();
                hasSelectedTraining= true;
                ChangeTrainBtnIcon();
            }
        }

        private void BtnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            PositionManagerUI posManagetUI = new PositionManagerUI();
            EmployeeManagement.mEmpTransitioner.Items.Add(posManagetUI);
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 2;
        }

        private void BtnAddTrainings_Click(object sender, RoutedEventArgs e)
        {
            if (TrainingValidation())
            {
                if (hasSelectedTraining)
                {
                    hasSelectedTraining = false;
                    ChangeTrainBtnIcon();
                    ClearTrainingFields();
                    cbTrainingCollection.SelectedIndex = -1;

                    selectedTrainItem = null;
                }
                else
                {
                    var trainItem = new TrainingItem();
                    trainItem._EMP_ID = tbEmployeeId.Text;
                    trainItem._TITLE = tbTitle.Text;
                    trainItem._INSTITUTION = tbInstitution.Text;
                    trainItem._TRAINING_LOCATION = tbLocation.Text;
                    trainItem._TRAINING_DATE = dpTrainingFinished.Text;
                    trainItem._TRAIN_HOLDER = "Training " + (trainCollection.Count + 1);

                    trainCollection.Add(trainItem);

                    cbTrainingCollection.ItemsSource = trainCollection;

                    ClearTrainingFields();

                    MessageBox.Show("New work training record added.");
                }
            }
        }

        private void BtnAddWorkExp_Click(object sender, RoutedEventArgs e)
        {
            if (WorkExpValidation())
            {
                if (hasSelectedWork)
                {
                    hasSelectedWork = false;
                    ChangeWorkBtnIcon();
                    ClearWorkExpFields();
                    cbWorkCollection.SelectedIndex = -1;

                    selectedExpItem = null;
                }
                else
                {
                    var expItem = new ExperienceItem();
                    expItem._EMP_NO = tbEmployeeId.Text;
                    expItem._DESIGNATION = tbDesignation.Text;
                    expItem._COMPANY = tbCompanyName.Text;
                    expItem._DATE_START = dpWorkStart.Text;
                    expItem._DATE_END = dpWorkEnd.Text;
                    expItem._WORK_LOCATION = tbComanyLocation.Text;
                    expItem._EXP_HOLDER = "Work " + (workExpCollection.Count + 1);

                    workExpCollection.Add(expItem);

                    cbWorkCollection.ItemsSource = workExpCollection;

                    ClearWorkExpFields();

                    MessageBox.Show("New work experience record added.");
                }
            }
        }
    }
}
