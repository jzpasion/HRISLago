using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class SpecificItem
    {
        public int EMPID { get; set; }
        public string PENDING_TYPE { get; set; }

        public string PENDING_STATUS { get; set; }

        public string PENDING_DATE { get; set; }
        public string PENDING_NAME { get; set; }

        public string PENDING_POSITION { get; set; }

        public string PENDING_LEAVE_FROM { get; set; }
        public string PENDING_LEAVE_TO { get; set; }
        public string PENDING_OT_FROM { get; set; }
        public string PENDING_OT_TO { get; set; }
        public string PENDING_LEAVE_REASON { get; set; }

        public string PENDING_OT_REASON { get; set; }
    }
}
