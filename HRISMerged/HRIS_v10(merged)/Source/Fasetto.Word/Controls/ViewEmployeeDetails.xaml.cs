using System.Windows;
using System.Windows.Controls;
using Fasetto.Word.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for ViewEmployeeDetails.xaml
    /// </summary>
    public partial class ViewEmployeeDetails : UserControl
    {
        ExperienceItem selectedExpItem = new ExperienceItem();
        TrainingItem selectedTrainItem = new TrainingItem();

        EmployeeItem mEmpItem = new EmployeeItem();
        List<EducationItem> mEduList = new List<EducationItem>();
        List<ExperienceItem> mExpList = new List<ExperienceItem>();
        List<TrainingItem> mTrainList = new List<TrainingItem>();

        bool editMode = false;
        bool hasSelectedEdu = false, hasSelectedWork = false, hasSelectedTraining = false;

        ObservableCollection<EducationItem> vocationalEduCollection = new ObservableCollection<EducationItem>();
        ObservableCollection<ExperienceItem> workExpCollection = new ObservableCollection<ExperienceItem>();
        ObservableCollection<TrainingItem> trainCollection = new ObservableCollection<TrainingItem>();

        EducationItem selectedPrimaryEdu = new EducationItem();
        EducationItem selectedSecondaryEdu = new EducationItem();
        EducationItem selectedTertiaryEdu = new EducationItem();
        EducationItem selectedMasteralEdu = new EducationItem();
        EducationItem selectedDoctoralEdu = new EducationItem();
        EducationItem selectedVocationalEdu = new EducationItem();

        string selectedEmpStatus;
        public ViewEmployeeDetails(EmployeeItem empItem, List<EducationItem> eduList, List<ExperienceItem> expList, List<TrainingItem> trainList)
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

            mEmpItem = empItem;
            mEduList = eduList;
            mExpList = expList;
            mTrainList = trainList;  
        }

        private void displayPersonalDetails(EmployeeItem empItem)
        {
            tbEmployeeId.Text = empItem._EMP_NO;
            tbFirstName.Text = empItem._FIRST_NAME;
            tbMiddleName.Text = empItem._MIDDLE_NAME;
            tbLastName.Text = empItem._LAST_NAME;
            tbReligion.Text = empItem._RELIGION;
            if (empItem._GENDER == "Male")
            {
                rbMale.IsChecked = true;
                rbFemale.IsChecked = false;
            }
            else if (empItem._GENDER == "Female")
            {
                rbFemale.IsChecked = true;
                rbMale.IsChecked = false;
            }
            dpBirthday.SelectedDate = DateTime.Parse(empItem._BIRTHDAY);
            tbNationality.Text = empItem._NATIONALITY;
            tbPassportNo.Text = empItem._PASSPORT;
            tbEmail.Text = empItem._EMAIL_ADDRESS;
            tbContactNumber.Text = empItem._CONTACT;
            tbBirthPlace.Text = empItem._BIRTH_PLACE;
            tbPresentAddress.Text = empItem._PRESENT_ADDRESS;
            tbPermanentAddress.Text = empItem._PERMANENT_ADDRESS;
            tbNamePTC.Text = empItem._IOE_PERSON;
            tbContactPTC.Text = empItem._IOE_CONTACT;
            tbRelationPTC.Text = empItem._IOE_RELATION;
            tbAddressPTC.Text = empItem._IOE_ADDRESS;
            cbPosition.SelectedValue = empItem._POS_ID;
            cbEmpStatus.SelectedItem = SetEmpStatus(empItem._EMP_STATUS);
            dpDateEmployed.SelectedDate = DateTime.Parse(empItem._DATE_JOINED);
            dpEndProvision.SelectedDate = DateTime.Parse(empItem._END_PROVITION);
            tbMonthlySalary.Text = empItem._MONTHLY_SALARY.ToString();
            tbSSSNo.Text = empItem._SSS_NO;
            tbPhHealth.Text = empItem._PHIL_HEALTH_NO;
            tbPagIbig.Text = empItem._PAG_IBIG_NO;
            tbBIR.Text = empItem._BIR_NO;
            tbDedSSS.Text = empItem._DEDUC_SSS.ToString();
            tbDedPhilHealth.Text = empItem._DEDUC_PHIL_HEALTH.ToString();
            tbDedPagIbig.Text = empItem._DEDUC_PAG_IBIG.ToString();
            tbDedBIR.Text = empItem._DEDUC_BIR.ToString();
        }

        private void displayEducationDetails(List<EducationItem> eduList)
        {
            if (eduList != null)
            {
                foreach (var item in eduList)
                {
                    switch (item._EDU_LEVEL)
                    {
                        case "Primary":
                            displayPrimaryEducation(item);
                            break;
                        case "Secondary":
                            displaySecondaryEducation(item);
                            break;
                        case "Tertiary":
                            displayTertiaryEducation(item);
                            break;
                        case "Masteral":
                            displayMasteralEducation(item);
                            break;
                        case "Doctoral":
                            displayDoctoralEducation(item);
                            break;
                        case "Vocational":
                            setVocationalEducations(item);
                            break;
                    }
                }
                cbVocationalCollect.ItemsSource = vocationalEduCollection;
                cbVocationalCollect.SelectedIndex = 0;
            }
        }

        private void displayPrimaryEducation(EducationItem item)
        {
            selectedPrimaryEdu = item;
            tbSchoolNamePrimary.Text = item._EDU_SCHOOL_NAME;
            tbSchoolAddressPrimary.Text = item._EDU_SCHOOL_ADDRESS;
            dpEduPrimaryDate.SelectedDate = DateTime.Parse(item._EDU_DATE_GRADUATED);
        }

        private void displaySecondaryEducation(EducationItem item)
        {
            selectedSecondaryEdu = item;
            tbSchoolNameSecondary.Text = item._EDU_SCHOOL_NAME;
            tbSchoolAddressSecondary.Text = item._EDU_SCHOOL_ADDRESS;
            dpEduSecondaryDate.SelectedDate = DateTime.Parse(item._EDU_DATE_GRADUATED);
        }

        private void displayTertiaryEducation(EducationItem item)
        {
            selectedTertiaryEdu = item;
            tbSchoolNameTertiary.Text = item._EDU_SCHOOL_NAME;
            tbSchoolAddressTertiary.Text = item._EDU_SCHOOL_ADDRESS;
            tbDegreeTertiary.Text = item._EDU_DEGREE_EARNED;
            dpEduTertiaryDate.SelectedDate = DateTime.Parse(item._EDU_DATE_GRADUATED);
        }

        private void displayMasteralEducation(EducationItem item)
        {
            selectedMasteralEdu = item;
            tbSchoolNameMasteral.Text = item._EDU_SCHOOL_NAME;
            tbSchoolAddressMasteral.Text = item._EDU_SCHOOL_ADDRESS;
            tbDegreeMasteral.Text = item._EDU_DEGREE_EARNED;
            dpEduMasteralDate.SelectedDate = DateTime.Parse(item._EDU_DATE_GRADUATED);
        }

        private void displayDoctoralEducation(EducationItem item)
        {
            selectedDoctoralEdu = item;
            tbSchoolNameDoctoral.Text = item._EDU_SCHOOL_NAME;
            tbSchoolAddressDoctoral.Text = item._EDU_SCHOOL_ADDRESS;
            tbDegreeDoctoral.Text = item._EDU_DEGREE_EARNED;
            dpEduDoctoralDate.SelectedDate = DateTime.Parse(item._EDU_DATE_GRADUATED);
        }

        private void setVocationalEducations(EducationItem item)
        {
            item._EDU_HOLDER = "Vocational " + (vocationalEduCollection.Count + 1);

            vocationalEduCollection.Add(item);
        }

        private void displayExperieceDetails(List<ExperienceItem> expList)
        {
            if (expList != null)
            {
                foreach (var item in expList)
                {
                    item._EXP_HOLDER = "Job " + (workExpCollection.Count + 1);

                    workExpCollection.Add(item);
                }
                cbWorkCollection.ItemsSource = workExpCollection;
                cbWorkCollection.SelectedIndex = 0;
            }
        }

        private void displayTrainingDetails(List<TrainingItem> trainList)
        {
            if (trainList != null)
            {
                foreach (var item in trainList)
                {
                    item._TRAIN_HOLDER = "Training " + (trainCollection.Count + 1);

                    trainCollection.Add(item);
                }
                cbTrainingCollection.ItemsSource = trainCollection;
                cbTrainingCollection.SelectedIndex = 0;
            }
        }

        private Enum SetEmpStatus(string item)
        {
            return (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), item);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
            EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            displayPersonalDetails(mEmpItem);
            displayEducationDetails(mEduList);
            displayExperieceDetails(mExpList);
            displayTrainingDetails(mTrainList);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ChangeBtnEditState();
        }

        private void ChangeBtnEditState()
        {
            editMode = !editMode;

            if (editMode)
            {
                EnableEditingFields();
                ButtonEdit.Content = "Save";
                ButtonBack.Content = "Cancel";
            }
            else
            {
                SetValues();
                ButtonEdit.Content = "Edit";
                ButtonBack.Content = "Back";
            }
        }

        private void SetValues()
        {
            if (InputValidation())
            {
                EmployeeItem item = new EmployeeItem();

                item._EMP_NO = tbEmployeeId.Text;
                item._FIRST_NAME = tbFirstName.Text;
                item._MIDDLE_NAME = tbMiddleName.Text;
                item._LAST_NAME = tbLastName.Text;

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

                //try
                //{
                UpdateEmpoyeeDetails(item);
                MessageBox.Show("Employee details updated.");
                //}
                //catch (System.Exception)
                //{
                //MessageBox.Show("Error saving new employee.");
                //}

                EmployeeCollection myEmpList = new EmployeeCollection();
                myEmpList.RetreiveAllEmployee();

                EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
                EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
            }
        }

        private void UpdateEmpoyeeDetails(EmployeeItem myItem)
        {
            EmployeeManager myManager = new EmployeeManager();
            myManager.UpdateData(myItem);
        }

        private void SetEduBackground(string empId)
        {
            EducationManager mEduManager = new EducationManager();
            List<EducationItem> eduBackgroundList = new List<EducationItem>();

            if (!string.IsNullOrEmpty(tbSchoolNamePrimary.Text))
            {
                selectedPrimaryEdu._EMP_ID = empId;
                selectedPrimaryEdu._EDU_LEVEL = "Primary";
                selectedPrimaryEdu._EDU_SCHOOL_NAME = tbSchoolNamePrimary.Text;
                selectedPrimaryEdu._EDU_SCHOOL_ADDRESS = tbSchoolAddressPrimary.Text;
                selectedPrimaryEdu._EDU_DATE_GRADUATED = dpEduPrimaryDate.Text;

                eduBackgroundList.Add(selectedPrimaryEdu);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameSecondary.Text))
            {
                selectedSecondaryEdu._EMP_ID = empId;
                selectedSecondaryEdu._EDU_LEVEL = "Secondary";
                selectedSecondaryEdu._EDU_SCHOOL_NAME = tbSchoolNameSecondary.Text;
                selectedSecondaryEdu._EDU_SCHOOL_ADDRESS = tbSchoolAddressSecondary.Text;
                selectedSecondaryEdu._EDU_DATE_GRADUATED = dpEduSecondaryDate.Text;

                eduBackgroundList.Add(selectedSecondaryEdu);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameTertiary.Text))
            {
                selectedTertiaryEdu._EMP_ID = empId;
                selectedTertiaryEdu._EDU_LEVEL = "Tertiary";
                selectedTertiaryEdu._EDU_SCHOOL_NAME = tbSchoolNameTertiary.Text;
                selectedTertiaryEdu._EDU_SCHOOL_ADDRESS = tbSchoolAddressTertiary.Text;
                selectedTertiaryEdu._EDU_DATE_GRADUATED = dpEduTertiaryDate.Text;
                selectedTertiaryEdu._EDU_DEGREE_EARNED = tbDegreeTertiary.Text;

                eduBackgroundList.Add(selectedTertiaryEdu);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameMasteral.Text))
            {
                selectedMasteralEdu._EMP_ID = empId;
                selectedMasteralEdu._EDU_LEVEL = "Masteral";
                selectedMasteralEdu._EDU_SCHOOL_NAME = tbSchoolNameMasteral.Text;
                selectedMasteralEdu._EDU_SCHOOL_ADDRESS = tbSchoolAddressMasteral.Text;
                selectedMasteralEdu._EDU_DATE_GRADUATED = dpEduMasteralDate.Text;
                selectedMasteralEdu._EDU_DEGREE_EARNED = tbDegreeMasteral.Text;

                eduBackgroundList.Add(selectedMasteralEdu);
            }

            if (!string.IsNullOrEmpty(tbSchoolNameDoctoral.Text))
            {
                selectedDoctoralEdu._EMP_ID = empId;
                selectedDoctoralEdu._EDU_LEVEL = "Doctoral";
                selectedDoctoralEdu._EDU_SCHOOL_NAME = tbSchoolNameDoctoral.Text;
                selectedDoctoralEdu._EDU_SCHOOL_ADDRESS = tbSchoolAddressDoctoral.Text;
                selectedDoctoralEdu._EDU_DATE_GRADUATED = dpEduDoctoralDate.Text;
                selectedDoctoralEdu._EDU_DEGREE_EARNED = tbDegreeDoctoral.Text;

                eduBackgroundList.Add(selectedDoctoralEdu);
            }

            if (vocationalEduCollection.Count != 0)
            {
                vocationalEduCollection.Remove(selectedVocationalEdu);

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

            mEduManager.DeleteEduData(empId);

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

            mExpManager.DeleteExpData(empId);

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

            mTrainManager.DeleteTrainData(empId);

            foreach (var item in trainList)
            {
                mTrainManager.SaveTrainingData(item);
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

        private void EnableEditingFields()
        {
            tbFirstName.IsReadOnly = false;
            tbMiddleName.IsReadOnly = false;
            tbLastName.IsReadOnly = false;
            tbReligion.IsReadOnly = false;
            rbMale.IsEnabled = true;
            rbFemale.IsEnabled = true;
            dpBirthday.IsEnabled = true;
            tbNationality.IsReadOnly = false;
            tbPassportNo.IsReadOnly = false;
            tbEmail.IsReadOnly = false;
            tbContactNumber.IsReadOnly = false;
            tbBirthPlace.IsReadOnly = false;
            tbPresentAddress.IsReadOnly = false;
            tbPermanentAddress.IsReadOnly = false;
            tbNamePTC.IsReadOnly = false;
            tbContactPTC.IsReadOnly = false;
            tbRelationPTC.IsReadOnly = false;
            tbAddressPTC.IsReadOnly = false;
            cbPosition.IsEnabled = true;
            cbEmpStatus.IsEnabled = true;
            dpDateEmployed.IsEnabled = true;
            dpEndProvision.IsEnabled = true;
            tbMonthlySalary.IsReadOnly = false;
            tbSSSNo.IsReadOnly = false;
            tbPagIbig.IsReadOnly = false;
            tbPhHealth.IsReadOnly = false;
            tbBIR.IsReadOnly = false;
            tbDedSSS.IsReadOnly = false;
            tbDedPhilHealth.IsReadOnly = false;
            tbDedPagIbig.IsReadOnly = false;
            tbDedBIR.IsReadOnly = false;
            //btnAddPosition.Visibility = Visibility.Visible;

            tbSchoolNamePrimary.IsReadOnly = false;
            tbSchoolAddressPrimary.IsReadOnly = false;
            dpEduPrimaryDate.IsEnabled = true;
            tbSchoolNameSecondary.IsReadOnly = false;
            tbSchoolAddressSecondary.IsReadOnly = false;
            dpEduSecondaryDate.IsEnabled = true;
            tbSchoolNameTertiary.IsReadOnly = false;
            tbSchoolAddressTertiary.IsReadOnly = false;
            tbDegreeTertiary.IsReadOnly = false;
            dpEduTertiaryDate.IsEnabled = true;
            tbSchoolNameMasteral.IsReadOnly = false;
            tbSchoolAddressMasteral.IsReadOnly = false;
            tbDegreeMasteral.IsReadOnly = false;
            dpEduMasteralDate.IsEnabled = true;
            tbSchoolNameDoctoral.IsReadOnly = false;
            tbSchoolAddressDoctoral.IsReadOnly = false;
            tbDegreeDoctoral.IsReadOnly = false;
            dpEduDoctoralDate.IsEnabled = true;
            tbSchoolNameVocational.IsReadOnly = false;
            tbSchoolAddressVocational.IsReadOnly = false;
            tbDegreeVocational.IsReadOnly = false;
            dpEduVocationalDate.IsEnabled = true;
            btnAddVocational.Visibility = Visibility.Visible;
            //btnSaveVocational.Visibility = Visibility.Visible;
            ChangeVocBtnIcon();

            tbDesignation.IsReadOnly = false;
            tbCompanyName.IsReadOnly = false;
            tbComanyLocation.IsReadOnly = false;
            dpWorkStart.IsEnabled = true;
            dpWorkEnd.IsEnabled = true;
            btnAddWorkExp.Visibility = Visibility.Visible;
            //btnSaveWorkExp.Visibility = Visibility.Visible;
            ChangeWorkBtnIcon();
            tbTitle.IsReadOnly = false;
            tbInstitution.IsReadOnly = false;
            tbLocation.IsReadOnly = false;
            dpTrainingFinished.IsEnabled = true;
            btnAddTrainings.Visibility = Visibility.Visible;
            //btnSaveTrainings.Visibility = Visibility.Visible;
            ChangeTrainBtnIcon();
        }

        private void CbVocationalCollect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVocationalCollect.SelectedIndex != -1)
            {
                var item = cbVocationalCollect.SelectedItem as EducationItem;
                selectedVocationalEdu = item;

                ViewSelectedEdu();
                hasSelectedEdu = true;
                ChangeVocBtnIcon();
            }
        }

        private void ViewSelectedEdu()
        {
            if (cbVocationalCollect.SelectedIndex != -1)
            {
                tbSchoolNameVocational.Text = selectedVocationalEdu._EDU_SCHOOL_NAME;
                tbSchoolAddressVocational.Text = selectedVocationalEdu._EDU_SCHOOL_ADDRESS;
                tbDegreeVocational.Text = selectedVocationalEdu._EDU_DEGREE_EARNED;
                dpEduVocationalDate.SelectedDate = DateTime.Parse(selectedVocationalEdu._EDU_DATE_GRADUATED);
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

        private void ViewSelectedTrain()
        {
            if (cbTrainingCollection.SelectedIndex != -1)
            {
                tbTitle.Text = selectedTrainItem._TITLE;
                tbInstitution.Text = selectedTrainItem._INSTITUTION;
                tbLocation.Text = selectedTrainItem._TRAINING_LOCATION;
                dpTrainingFinished.SelectedDate = DateTime.Parse(selectedTrainItem._TRAINING_DATE);
            }
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

        private void CbTrainingCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTrainingCollection.SelectedIndex != -1)
            {
                var item = cbTrainingCollection.SelectedItem as TrainingItem;
                selectedTrainItem = item;

                ViewSelectedTrain();
                hasSelectedTraining = true;
                ChangeTrainBtnIcon();
            }
        }

        private void CbEmpStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmpStatus = cbEmpStatus.SelectedValue.ToString();
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
                if (editMode)
                {
                    btnSaveVocational.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnSaveVocational_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSchoolNameVocational.Text) && string.IsNullOrEmpty(tbSchoolAddressVocational.Text) && string.IsNullOrEmpty(tbDegreeVocational.Text))
            {
                vocationalEduCollection.Remove(selectedVocationalEdu);
            }
            else
            {
                if (VocEduValidation())
                {
                    var item = selectedVocationalEdu;
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

            selectedVocationalEdu = null;

            MessageBox.Show("School record successfully updated.");
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
                if (editMode)
                {
                    btnSaveWorkExp.Visibility = Visibility.Visible;
                }
            }
        }

        private void ClearWorkExpFields()
        {
            tbDesignation.Text = "";
            tbCompanyName.Text = "";
            tbComanyLocation.Text = "";

            dpWorkStart.SelectedDate = DateTime.Today;
            dpWorkEnd.SelectedDate = DateTime.Today;
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
                    expItem._EXP_HOLDER = "Job " + (workExpCollection.Count + 1);

                    if (workExpCollection.Count == 0)
                    {
                        ChangeWorkBtnIcon();
                        cbWorkCollection.SelectedIndex = -1;
                    }

                    workExpCollection.Add(expItem);

                    cbWorkCollection.ItemsSource = workExpCollection;

                    ClearWorkExpFields();

                    MessageBox.Show("New work experience record added.");
                }
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
                if (editMode)
                {
                    btnSaveTrainings.Visibility = Visibility.Visible;
                }
            }
        }

        private void ClearTrainingFields()
        {
            tbTitle.Text = "";
            tbInstitution.Text = "";
            tbLocation.Text = "";

            dpTrainingFinished.SelectedDate = DateTime.Today;
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

                    if (trainCollection.Count == 0)
                    {
                        ChangeTrainBtnIcon();
                        cbTrainingCollection.SelectedIndex = -1;
                    }

                    trainCollection.Add(trainItem);

                    cbTrainingCollection.ItemsSource = trainCollection;

                    ClearTrainingFields();

                    MessageBox.Show("New work training record added.");
                }
            }
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

        private void BtnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            PositionManagerUI posManagetUI = new PositionManagerUI();
            EmployeeManagement.mEmpTransitioner.Items.Add(posManagetUI);
            EmployeeManagement.mEmpTransitioner.SelectedIndex = 2;
        }

        private void ClearVocationalEduFields()
        {
            tbSchoolNameVocational.Text = "";
            tbSchoolAddressVocational.Text = "";
            tbDegreeVocational.Text = "";

            dpEduVocationalDate.SelectedDate = DateTime.Today;
        }

        private void BtnAddVocational_Click(object sender, RoutedEventArgs e)
        {
            if (VocEduValidation())
            {
                if (hasSelectedEdu)
                {
                    hasSelectedEdu = false;
                    ChangeVocBtnIcon();
                    ClearVocationalEduFields();
                    cbVocationalCollect.SelectedIndex = -1;

                    selectedVocationalEdu = null;
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

                    if (vocationalEduCollection.Count == 0)
                    {
                        ChangeVocBtnIcon();
                        cbVocationalCollect.SelectedIndex = -1;
                    }

                    vocationalEduCollection.Add(eduItem);

                    cbVocationalCollect.ItemsSource = vocationalEduCollection;                    
                    
                    ClearVocationalEduFields();

                    MessageBox.Show("New School Record added.");
                }
            }
        }
    }
}
