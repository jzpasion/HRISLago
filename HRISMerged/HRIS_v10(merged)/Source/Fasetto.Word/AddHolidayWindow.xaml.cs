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
using System.Windows.Shapes;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for AddHolidayWindow.xaml
    /// </summary>
    public partial class AddHolidayWindow : Window
    {
        private bool isEditMode = false;
        private int selectedHolidayId;
        public AddHolidayWindow(DateTime selectedDate, int holidayId)
        {
            InitializeComponent();

            cbHolidayType.ItemsSource = Enum.GetValues(typeof(HolidayType)).Cast<HolidayType>();

            if (holidayId != -1)
            {
                EnterEditMode(holidayId);
            }
            else
            {
                isEditMode = false;
                dpHolidayDate.SelectedDate = selectedDate;
            } 
        }
        private void EnterEditMode(int mholidayId)
        {
            isEditMode = true;
            selectedHolidayId = mholidayId;
            HolidayItem selectedHoliday = (HolidayItem)StaticHolidayCollection.staticHolidayList.Where(t => t._HOLIDAY_ID.Equals(mholidayId)).FirstOrDefault();
            tbHolidayName.Text = selectedHoliday._HOLIDAY_NAME;
            dpHolidayDate.SelectedDate = selectedHoliday._HOLIDAY_DATE;
            cbHolidayType.SelectedValue = (HolidayType)Enum.Parse(typeof(HolidayType), selectedHoliday._HOLIDAY_TYPE);
            ChangeButtonAppearance();
        }

        private void ChangeButtonAppearance()
        {
            btnSave.Content = "Update";
            btnDelete.Visibility = Visibility.Visible;
        }

        private bool isHolidayValid()
        {
            if (string.IsNullOrEmpty(tbHolidayName.Text))
            {
                MessageBox.Show("Please input holiday name.");
                return false;
            }
            else if (!isHolidayUnique(tbHolidayName.Text, (DateTime)dpHolidayDate.SelectedDate))
            {
                MessageBox.Show("Holiday is already in the list.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isHolidayUnique(string holidayName, DateTime holidayDate)
        {
            var tempHolidayList = StaticHolidayCollection.staticHolidayList.Where(t => t._HOLIDAY_NAME.ToLower().Equals(holidayName.ToLower()) && t._HOLIDAY_DATE.Equals(holidayDate));

            if (tempHolidayList.Count() != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEditMode)
            {
                updateHoliday();
            }
            else
            {
                saveHoliday();
            } 
        }
        private void updateHoliday()
        {
            if (!string.IsNullOrEmpty(tbHolidayName.Text))
            {
                HolidayItem item = new HolidayItem();

                item._HOLIDAY_ID = selectedHolidayId;
                item._HOLIDAY_NAME = tbHolidayName.Text;
                item._HOLIDAY_DATE = (DateTime)dpHolidayDate.SelectedDate;
                item._HOLIDAY_TYPE = cbHolidayType.SelectedValue.ToString();

                try
                {
                    HolidayManager hm = new HolidayManager();
                    hm.UpdateHoliday(item);
                    MessageBox.Show("Holiday successfully updated.");
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Server not responding.");
                }

                HolidayCollection holidayList = new HolidayCollection();
                holidayList.RetreiveAllHolidays();

                Close();
            }
            else
            {
                MessageBox.Show("Please input holiday name.");
            }
        }

        private void saveHoliday()
        {
            if (isHolidayValid())
            {
                HolidayItem item = new HolidayItem();

                item._HOLIDAY_NAME = tbHolidayName.Text;
                item._HOLIDAY_DATE = (DateTime)dpHolidayDate.SelectedDate;
                item._HOLIDAY_TYPE = cbHolidayType.SelectedValue.ToString();

                try
                {
                    HolidayManager hm = new HolidayManager();
                    hm.SaveHoliday(item);
                    MessageBox.Show("New Holiday added.");
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Server not responding.");
                }

                HolidayCollection holidayList = new HolidayCollection();
                holidayList.RetreiveAllHolidays();

                Close();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteHoliday();
        }
        private void DeleteHoliday()
        {
            string sMessageBoxText = "Do you want to delete this holiday?";
            string sCaption = "Are you Sure?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = System.Windows.MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    HolidayManager hm = new HolidayManager();
                    HolidayCollection hc = new HolidayCollection();
                    hm.DeleteHoliday(selectedHolidayId);
                    hc.RetreiveAllHolidays();
                    MessageBox.Show("Holiday successfully deleted.");
                    Close();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
