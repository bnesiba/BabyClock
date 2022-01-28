using NannyTimeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyTimeAPI.Utils
{
    public static class Extensions
    {
        public static List<DateTimeSpan> GetAllSpans(this Dictionary<DateTime, List<DateTimeSpan>> spanDict)
        {
            List<DateTimeSpan> results = new List<DateTimeSpan>();

            foreach(List<DateTimeSpan> spans in spanDict.Values)
            {
                results.AddRange(spans);
            }

            return results;
        }
    }
}
