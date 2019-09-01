using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class PayrollTotalItem
    {
        public int EMP_ID { get; set; }
        public string DAYS_COUNT { get; set; }

        public double TOTAL_EARNINGS { get; set; }

        public double TOTAL_OVERTIME { get; set; }
        public double TOTAL_DEDUCTION { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
    }
}
