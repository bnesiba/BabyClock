using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyTimeAPI.Models.Payment
{
    public class PaymentWeek
    {
        private double _rate;
        private double _shareRate;
        public PaymentWeek(double normalRate, double shareRate)
        {
            _rate = normalRate;
            _shareRate = shareRate;
        }
        public List<DateTimeSpan> AllTimes { get; set; }
        public List<DateTimeSpan> ShareTimes { get; set; }

        public double NormalHours { get; private set; }
        public double ShareHours { get; private set; }
        public double OverTimeHours { get; private set; }
        public double ShareOverTimeHours { get; private set; }
        public bool HadOvertime { get; private set; }
        public double TotalPayment { get; private set; }
        public double NormalHoursPayment { get; private set; }
        public double ShareHoursPayment { get; private set; }
        public double OverTimePayment { get; private set; }
        public double ShareOverTimePayment { get; private set; }

        public void AddHours(double hours, bool shared = false)
        {
            double remainingNormalHours = 40 - NormalHours - ShareHours;
            if (shared)
            {
                ShareHours += hours;
            }
            else
            {
                NormalHours += hours;
            }
            calculateOvertime();
            calculatePayments();
        }

        private void calculateOvertime()
        {
            if(ShareHours < 40)
            {
                var remainingHours = 40 - ShareHours;
                if(NormalHours > remainingHours)
                {
                    var normalOvertime1 = NormalHours - remainingHours;
                    NormalHours = remainingHours;
                    OverTimeHours = normalOvertime1;

                }
            }
            else
            {
                var overtimeShare = ShareHours - 40;
                ShareHours = overtimeShare;
                ShareOverTimeHours = overtimeShare;
                OverTimeHours = NormalHours;
                NormalHours = 0; 
            }
        }
        private void calculatePayments()
        {
            NormalHoursPayment = NormalHours * _rate;
            ShareHoursPayment = ShareHours * _shareRate;
            OverTimePayment = OverTimeHours * (_rate * 1.5);
            ShareOverTimePayment = ShareOverTimeHours * (_shareRate * 1.5);
            TotalPayment = NormalHoursPayment + ShareHoursPayment + OverTimePayment + ShareOverTimePayment;
        }
    }
}
