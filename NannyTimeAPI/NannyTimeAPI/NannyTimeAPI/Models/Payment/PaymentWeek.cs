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
                if (!HadOvertime)
                {
                    if (remainingNormalHours > hours)
                    {
                        ShareHours += hours;
                    }
                    else
                    {
                        ShareHours += remainingNormalHours;
                        ShareOverTimeHours += (hours - remainingNormalHours);
                        HadOvertime = true;
                    }
                }
                else
                {
                    ShareOverTimeHours += hours;
                }
            }
            else if (!HadOvertime)
            {
                if (remainingNormalHours > hours)
                {
                    NormalHours += hours;
                }
                else
                {
                    NormalHours += remainingNormalHours;
                    OverTimeHours += (hours - remainingNormalHours);
                    HadOvertime = true;
                }
            }
            else
            {
                OverTimeHours += hours;
            }
            calculatePayments();
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
