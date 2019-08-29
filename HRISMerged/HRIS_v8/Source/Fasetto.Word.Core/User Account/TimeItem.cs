using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class TimeItem
    {
        public int LOG_ID { get; set; }
        public int EMP_ID { get; set; }
        public string TIME_IN { get; set; }

        public string DATE { get; set; }
        public double HOURS { get; set; }

        public string TIME_OUT { get; set; }
    }
}
