using System.Collections.Generic;

namespace NannyTimeAPI.Models.Payment
{
    public class PaymentResult
    {
        public List<DateTimeSpan> arthurTimes { get;set; }
        //public List<DateTimeSpan> emiliatimes { get; set; }
        public List<DateTimeSpan> shareTimes { get; set; }
        public double arthurCost { get; set; }
        //public double emiliaCost { get; set; }
        public List<PaymentWeek> arthurWeeks { get; set; }
        //public List<PaymentWeek> emiliaWeeks { get; set; }
    }
}
