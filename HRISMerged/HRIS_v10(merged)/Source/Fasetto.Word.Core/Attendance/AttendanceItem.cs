using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
   public class AttendanceItem
    {
        public int _LOG_ID { get; set; }
        public int _EMP_ID { get; set; }

        public string _TIME_IN { get; set; }

        public string _TIME_OUT { get; set; }

        public string  _DATE { get; set; }

    }
}
