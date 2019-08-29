using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fasetto.Word.Core;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for PositionManagerUI.xaml
    /// </summary>
    public partial class PositionManagerUI : UserControl
    {
        string selectedPosId = null;
        public PositionManagerUI()
        {
            InitializeComponent();

            GetAllPositions();
        }

        private void GetAllPositions()
        {
            positionTable.ItemsSource = StaticPositionCollection.staticPositionList;
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (EmployeeManagement.mEmpTransitioner.Items.Count == 3)
            {
                EmployeeManagement.mEmpTransitioner.SelectedIndex = 1;
                EmployeeManagement.mEmpTransitioner.Items.RemoveAt(2);
            }
            else
            {
                EmployeeManagement.mEmpTransitioner.SelectedIndex = 0;
                EmployeeManagement.mEmpTransitioner.Items.RemoveAt(1);
            }
        }

        private bool posValidation()
        {
            if (string.IsNullOrEmpty(tbJobTitle.Text))
            {
                MessageBox.Show("Please input job title.");
                return false;
            }
            else if (string.IsNullOrEmpty(tbDepartment.Text))
            {
                MessageBox.Show("Please input job department.");
                return false;
            }
            else if (!isNewJobTitleUnique(tbJobTitle.Text))
            {
                MessageBox.Show("Job title already existed.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isNewJobTitleUnique(string designation)
        {
            var tempPosList = StaticPositionCollection.staticPositionList.Where(t => t._POS_NAME.ToLower().Equals(designation.ToLower()));

            if (tempPosList.Count() != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                DesignationItem item = new DesignationItem();

                if (string.IsNullOrEmpty(selectedPosId))
                {
                    if (posValidation())
                    {
                        item._POS_NAME = tbJobTitle.Text;
                        item._POS_DEPARTMENT = tbDepartment.Text;

                        SaveDesignationDetails(item);
                    }
                }
                else
                {
                    item._POS_ID = int.Parse(selectedPosId);
                    item._POS_NAME = tbJobTitle.Text;
                    item._POS_DEPARTMENT = tbDepartment.Text;

                    UpdateDesignationDetails(item);
                }

                PositionCollection myPosList = new PositionCollection();
                myPosList.RetreiveAllPositions();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Cannot connect to server", "Error");
            }
        }

        private void UpdateDesignationDetails(DesignationItem myItem)
        {
            PositionManager myManager = new PositionManager();
            myManager.UpdateDesignation(myItem);

            ClearInputFields();
            changeButtonIcons();
            MessageBox.Show("Position successfully updated.");
        }

        private void SaveDesignationDetails(DesignationItem myItem)
        {
            PositionManager myManager = new PositionManager();
            myManager.SaveDesignation(myItem);

            ClearInputFields();
            MessageBox.Show("New position successfully added.");
        }

        private void SetValuesToInputFields()
        {
            object item = positionTable.SelectedItem;
            tbJobTitle.Text = (positionTable.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            tbDepartment.Text = (positionTable.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
        }

        private void ClearInputFields()
        {
            tbJobTitle.Text = "";
            tbDepartment.Text = "";
            selectedPosId = null;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            changeButtonIcons();
        }

        private void Row_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            object item = positionTable.SelectedItem;
            selectedPosId = (positionTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            SetValuesToInputFields();
            changeButtonIcons();
        }

        private void changeButtonIcons()
        {
            if (string.IsNullOrEmpty(selectedPosId))
            {
                btnSaveIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ContentSave;
                btnDelete.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnSaveIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ContentSaveEdit;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Do you want to delete this position?";
            string sCaption = "Are you Sure?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = System.Windows.MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    DeletePosition();
                    ClearInputFields();
                    changeButtonIcons();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void DeletePosition()
        {
            PositionManager myManager = new PositionManager();
            myManager.DeleteDesignation(selectedPosId);

            PositionCollection myPosList = new PositionCollection();
            myPosList.RetreiveAllPositions();
        }
    }
}
