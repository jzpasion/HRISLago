

using System.Collections.ObjectModel;

namespace Fasetto.Word.Core
{
    public class StaticAttendanceList
    {
        public static ObservableCollection<AttendanceItem> staticAttendanceList = new ObservableCollection<AttendanceItem>();
    }
    public class StaticSortedListAttendance
    {
        public static ObservableCollection<AttendanceItem> staticSortedList = new ObservableCollection<AttendanceItem>();
    }

    public class StaticPendingList
    {
        public static ObservableCollection<PendingItem> staticPendingList = new ObservableCollection<PendingItem>();
    }

    public class StaticApprovalList
    {
        public static ObservableCollection<PendingItem> staticApprovalList = new ObservableCollection<PendingItem>();
    }

    public class StaticApprovalItem
    {
        public static PendingItem staticApprovalModalItem = new PendingItem();
    }

    public class LogItem
    {
        public static TimeItem staticLogIdItem = new TimeItem();
    }

    public class Comboboxitem
    {
        public static ObservableCollection<ComboBoxItem> ComboItem = new ObservableCollection<ComboBoxItem>();
    }

}
