using MaterialDesignThemes.Wpf.Transitions;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : BasePage
    {
        public static Transitioner mHolidayTransitioner = new Transitioner();
        public DashboardPage()
        {
            InitializeComponent();

            mHolidayTransitioner = holidayTransitioner;
        }
    }
}
