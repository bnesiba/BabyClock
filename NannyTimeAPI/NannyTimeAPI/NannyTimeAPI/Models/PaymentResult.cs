using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyTimeAPI.Models
{
    public class PaymentResult
    {
        public List<ClockEntry> arthurTimes { get;set; }
        public List<ClockEntry> emiliatimes { get; set; }
        public double arthurCost { get; set; }
        public double emiliaCost { get; set; }
    }

    public class ClockEntry
    {
        public string baby { get; set; }
        public bool clockedIn { get; set; }
        public DateTime clockTime { get; set; }
    }
}
