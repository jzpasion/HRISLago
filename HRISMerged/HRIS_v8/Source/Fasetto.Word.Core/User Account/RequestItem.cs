using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class RequestItem
    {
        public int LEAVE_ID { get; set; }
        public int EMP_ID { get; set; } 

        public string DATE { get; set; }
        public string TYPE { get; set; }
        public string REASON { get; set; }
        public string STATUS { get; set; }
        public string LEAVE_START { get; set; }
        public string LEAVE_END { get; set; }

        public string TIME_FROM { get; set; }

        public string TIME_TO { get; set; }
        public string PROJECT { get; set; }

        public int POS_ID { get; set; }
    }
}
