using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class PayrollItem
    {
        public int EMP_ID { get; set; }

        public string EMP_NO { get; set; }
        
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }

        public string DEPARTMENT { get; set; }
        public string SSS { get; set; }
        public string PAG_IBIG { get; set; }
        public string PHIL { get; set; }
        public string BIR { get; set; }

        public string MONTHLY_SALARY { get; set; }
        public double DEDUC_SSS { get; set; }
        public double DEDUC_PAG_IBIG { get; set; }
        public double DEDUC_PHIL { get; set; }
        public double DEDUC_BIR { get; set; }

    }
}
