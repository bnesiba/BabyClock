using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyTimeAPI.Models
{
    public class DateTimeSpan
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public double hours { get { return end.Subtract(start).TotalHours; } }

        public bool Overlaps(DateTimeSpan otherSpan, out DateTimeSpan overlapSpan)
        {
            if ((otherSpan.end <= this.end && otherSpan.end >= this.start) && (otherSpan.start >= this.start && otherSpan.start <= this.end))
            {//equal or entirely overlapping
                overlapSpan = otherSpan;
                return true;
            }
            if ((otherSpan.end <= this.end && otherSpan.end >= this.start) && !(otherSpan.start >= this.start && otherSpan.start <= this.end))
            {//other starts inside ends outside
                overlapSpan = new DateTimeSpan
                {
                    start = otherSpan.start,
                    end = this.end
                };
                return true;
            }
            if (!(otherSpan.end <= this.end && otherSpan.end >= this.start) && (otherSpan.start >= this.start && otherSpan.start <= this.end))
            {//other starts outside ends inside
                overlapSpan = new DateTimeSpan
                {
                    start = this.start,
                    end = otherSpan.end
                };
                return true;
            }
            overlapSpan = null;
            return false;
        }
    }
}
