using System;

namespace NannyTimeAPI.Models
{
    public class ClockEntry
    {
        public string baby { get; set; }
        public bool clockedIn { get; set; }
        public DateTime clockTime { get; set; }
    }
}
