using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Fasetto.Word.Core;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for HolidayManagerUI.xaml
    /// </summary>
    public partial class HolidayManagerUI : UserControl
    {
        private DateTime currentDate = new DateTime();
        private List<Border> borderDayList = new List<Border>();
        List<HolidayItem> holidayList;
        int prevDay = -1;

        public HolidayManagerUI()
        {
            InitializeComponent();
            //AddPanelToCalendar();
            currentDate = DateTime.Today;
            DisplayCurrentDate();
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

            foreach (var item in tempHolidayList.OrderBy(o => o._HOLIDAY_DATE))
            {
                int borderAddress = (item._HOLIDAY_DATE.Day -1);
                BrushConverter bc = new BrushConverter();
                Brush brush = (Brush)bc.ConvertFrom("#EF9A9A");
                if (prevDay == borderAddress)
                {
                    holidayList.Add(item);
                }
                else
                {
                    holidayList = new List<HolidayItem>();
                    holidayList.Add(item);
                    prevDay = borderAddress;
                }
                borderDayList[borderAddress].Background = brush;
                borderDayList[borderAddress].Child = generateBorderGrid(holidayList);
            }
            holidayList.Clear();
            prevDay = -1;
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

        private Grid generateBorderGridNoHoliday(string holDate, int holId)
        {
            Grid grid = new Grid();
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(100, GridUnitType.Star);
            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(100, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);
            grid.Cursor = Cursors.Hand;
            grid.Tag = int.Parse(holDate);
            grid.MouseUp += new MouseButtonEventHandler(showHolidayManager);

            TextBlock mDay = new TextBlock();
            mDay.Text = holDate;
            mDay.FontSize = 20;
            mDay.HorizontalAlignment = HorizontalAlignment.Right;
            mDay.Margin = new Thickness(10);
            mDay.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(mDay);

            return grid;
        }

        private Grid generateBorderGrid(List<HolidayItem> holidays)
        {
            Grid grid = new Grid();
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(100, GridUnitType.Star);
            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(100, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);
            grid.Cursor = Cursors.Hand;
            grid.Tag = int.Parse(holidays[0]._HOLIDAY_DATE.Day.ToString());
            grid.MouseUp += new MouseButtonEventHandler(showHolidayManager);

            TextBlock mDay = new TextBlock();
            mDay.Text = holidays[0]._HOLIDAY_DATE.Day.ToString();
            mDay.FontSize = 20;
            mDay.HorizontalAlignment = HorizontalAlignment.Right;
            mDay.Margin = new Thickness(10);
            mDay.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(mDay);


            ScrollViewer mScrollViewer = new ScrollViewer();

            mScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            StackPanel verStackPanel = new StackPanel();
            verStackPanel.Orientation = Orientation.Vertical;

            mScrollViewer.Content = verStackPanel;

            foreach (var item in holidays)
            {
                Label lblHoliday = new Label();
                lblHoliday.Padding = new Thickness(0);
                lblHoliday.Margin = new Thickness(20, 10, 10, 0);
                Hyperlink hlnHoliday = new Hyperlink();
                hlnHoliday.Inlines.Add(item._HOLIDAY_NAME);
                hlnHoliday.Tag = item._HOLIDAY_ID;
                hlnHoliday.Click += new RoutedEventHandler(showHolidayManagerEdit);
                lblHoliday.Content = hlnHoliday;
                lblHoliday.FontSize = 14;
                verStackPanel.Children.Add(lblHoliday);
            }
            
            grid.Children.Add(mScrollViewer);

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
            myBorder.Child = generateBorderGridNoHoliday(day.ToString(), -1);
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            DashboardPage.mHolidayTransitioner.SelectedIndex = 0;
            DashboardPage.mHolidayTransitioner.Items.RemoveAt(1);
        }
    }
}
