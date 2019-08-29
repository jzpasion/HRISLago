using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
   public class PendingItem
    {
        public int PENDING_ID { get; set; }
        public int EMPID { get; set; }
        public string PENDING_TYPE { get; set; }

        public string PENDING_STATUS { get; set; }  

        public string PENDING_DATE { get; set; }
        public string PENDING_NAME { get; set; }

        public string  PENDING_REASON { get; set; }
        public string PENDING_POSITION { get; set; }

        public string PENDING_LEAVE_FROM { get; set; }
        public string PENDING_LEAVE_TO { get; set; }
        public string PENDING_OT_FROM { get; set; }
        public string PENDING_OT_TO { get; set; }
        public string PENDING_LEAVE_REASON { get; set; }
        public string PENDING_TIME { get; set; }
        public string PENDING_OT_REASON { get; set; }

        public string APPROVED_BY { get; set; }
        public string SEND_TO { get; set; }
    }
}
