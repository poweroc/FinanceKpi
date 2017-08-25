using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceKpi.Models
{
    public class Refund
    {
        public int StepID { get; set; }
        public int TaskID { get; set; }
        public DateTime ReceiveTime { get; set; }
        public string ProcessName { get; set; }
        public string FinAccount { get; set; }
        public string Refunding { get; set; }
        public string Refunded { get; set; }
    }
}