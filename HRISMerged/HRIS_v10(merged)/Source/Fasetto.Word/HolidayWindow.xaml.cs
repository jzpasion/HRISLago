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
    /// Interaction logic for HolidayWindow.xaml
    /// </summary>
    public partial class HolidayWindow : Window
    {
        private DateTime currentDate = new DateTime();
        private List<Border> borderDayList = new List<Border>();
        public HolidayWindow()
        {

            InitializeComponent();
            HolidayCollection holicol = new HolidayCollection();
            holicol.RetreiveAllHolidays();
            currentDate = DateTime.Today;
            DisplayCurrentDate();
           
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void DisplayCurrentDate()
        {
            lblMonthYear.Text = currentDate.ToString("MMMM, yyyy");
            int totalDaysOfCurrentDate = GetTotalDaysOfCurrentDate();
            int firstDayOfWeekOfCurrentDate = GetFirstDayOfWeekOfCurrentDate();
            AddGeneratedPanelToGrid(totalDaysOfCurrentDate, firstDayOfWeekOfCurrentDate);
            AddHolidayToDay(firstDayOfWeekOfCurrentDate);
        }
        private void AddHolidayToDay(int startDayAtBorderNum)
        {
            var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            List<HolidayItem> tempHolidayList = StaticHolidayCollection.staticHolidayList.Where(t => t._HOLIDAY_DATE >= startDate && t._HOLIDAY_DATE <= endDate).ToList();

            foreach (var item in tempHolidayList)
            {
                int borderAddress = (item._HOLIDAY_DATE.Day - 1);
                BrushConverter bc = new BrushConverter();
                Brush brush = (Brush)bc.ConvertFrom("#EF9A9A");
                borderDayList[borderAddress].Background = brush;
                borderDayList[borderAddress].Child = generateBorderGrid(item._HOLIDAY_NAME, item._HOLIDAY_DATE.Day.ToString(), item._HOLIDAY_ID);
            }
        }
        private void PrevMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            DisplayCurrentDate();
        }
        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            DisplayCurrentDate();
        }
        private void Today()
        {
            currentDate = DateTime.Today;
            DisplayCurrentDate();
        }
        private int GetFirstDayOfWeekOfCurrentDate()
        {
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            return (int)(firstDayOfMonth.DayOfWeek + 1);
        }
        private int GetTotalDaysOfCurrentDate()
        {
            var firstDayOfCurrentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            return (int)(firstDayOfCurrentDate.AddMonths(1).AddDays(-1).Day);
        }

        //private void AddPanelToCalendar()
        //{
        //    var myBorder = new Border();
        //    myBorder.Background = Brushes.AliceBlue;
        //    myBorder.Margin = new Thickness(2.5, 2, 2.5, 2);
        //    myBorder.CornerRadius = new CornerRadius(10);
        //    myBorder.MouseUp += new MouseButtonEventHandler(showHolidayManager);
        //    grDayContainer.Children.Add(myBorder);

        //    var myBorder2 = new Border();
        //    myBorder2.Background = Brushes.AliceBlue;
        //    myBorder2.Margin = new Thickness(2.5, 2, 2.5, 2);
        //    myBorder2.CornerRadius = new CornerRadius(10);
        //    myBorder2.SetValue(Grid.ColumnProperty, 1);
        //    myBorder2.SetValue(Grid.RowProperty, 1);
        //    myBorder2.Child = generateBorderGrid("", "1", -1);
        //    grDayContainer.Children.Add(myBorder2);
        //}

        private Grid generateBorderGrid(string holidayName, string day, int holidayId)
        {
            Grid grid = new Grid();
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(100, GridUnitType.Star);
            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(100, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);
            grid.Tag = int.Parse(day);

            TextBlock mDay = new TextBlock();
            mDay.Text = day;
            mDay.FontSize = 20;
            mDay.HorizontalAlignment = HorizontalAlignment.Right;
            mDay.Margin = new Thickness(10);
            mDay.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(mDay);

            Label lblHoliday = new Label();
            Hyperlink hlnHoliday = new Hyperlink();
            hlnHoliday.Inlines.Add(holidayName);
            hlnHoliday.Cursor = Cursors.Arrow;
            hlnHoliday.Tag = holidayId;
            lblHoliday.Content = hlnHoliday;
            lblHoliday.VerticalAlignment = VerticalAlignment.Center;
            lblHoliday.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblHoliday.FontSize = 14;
            grid.Children.Add(lblHoliday);

            return grid;
        }

        private void AddGeneratedPanelToGrid(int totalDays, int startDay)
        {
            grDayContainer.Children.Clear();
            borderDayList = GenerateDayPanelList(totalDays, startDay);
            foreach (var item in borderDayList)
            {
                grDayContainer.Children.Add(item);
            }
        }

        private List<Border> GenerateDayPanelList(int totalDays, int startDay)
        {
            var tempBorderList = new List<Border>();
            int row = 0, col = 0, day = 0;
            for (int i = 1; i < (totalDays + startDay); i++, col++)
            {
                if (i >= startDay)
                {
                    day++;
                    if (col == 7)
                    {
                        row++;
                        col = 0;
                    }
                    tempBorderList.Add(GenerateDayPanel(row, col, day));
                }
            }
            return tempBorderList;
        }

        private Border GenerateDayPanel(int row, int col, int day)
        {
            var myBorder = new Border();
            myBorder.Background = Brushes.AliceBlue;
            myBorder.Margin = new Thickness(2.5, 2, 2.5, 2);
            myBorder.CornerRadius = new CornerRadius(10);
            myBorder.SetValue(Grid.ColumnProperty, col);
            myBorder.SetValue(Grid.RowProperty, row);
            myBorder.Child = generateBorderGrid("", day.ToString(), -1);
            return myBorder;
        }

        private void showHolidayManagerEdit(object sender, EventArgs e)
        {
            int id = (int)((Hyperlink)(sender)).Tag;
            if (id != -1)
            {
                var editHoliday = new AddHolidayWindow(DateTime.Today, id);
                editHoliday.ShowDialog();
                DisplayCurrentDate();
            }
        }

        private void showHolidayManager(object sender, MouseButtonEventArgs e)
        {
            int day = (int)((Grid)(sender)).Tag;
            if (day != 0)
            {
                var selectedDate = new DateTime(currentDate.Year, currentDate.Month, day);
                var addHoliday = new AddHolidayWindow(selectedDate, -1);
                addHoliday.ShowDialog();
                DisplayCurrentDate();
            }
        }

        private void BtnPrevMonth_Click(object sender, RoutedEventArgs e)
        {
            PrevMonth();
        }

        private void BtnNextMonth_Click(object sender, RoutedEventArgs e)
        {
            NextMonth();
        }

        private void BtnCurrentMonth_Click(object sender, RoutedEventArgs e)
        {
            Today();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
