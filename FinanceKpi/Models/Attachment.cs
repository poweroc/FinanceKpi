using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceKpi.Models
{
    public class Attachment
    {
        public int TaskId { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Department { get; set; }
        public string AccountName { get; set; }
        public int Count { get; set; }
    }
}