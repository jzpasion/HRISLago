using System.Collections.ObjectModel;

namespace Fasetto.Word.Core
{
    public static class StaticHolidayCollection
    {
        public static ObservableCollection<HolidayItem> staticHolidayList = new ObservableCollection<HolidayItem>();
    }
}
