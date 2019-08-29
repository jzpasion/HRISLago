using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
   public class AttendanceItem
    {
        public string FNAME { get; set; }

        public string MNAME { get; set; }

        public string LNAME { get; set; }

        public string TIMEIN { get; set; }

        public string TIMEOUT { get; set; }

        public string DATE { get; set; }

        public int LOG_ID { get; set; }
        public DateTime NOW { get; set; } = DateTime.Now;
    }
}
    