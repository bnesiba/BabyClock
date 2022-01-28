using NannyTimeAPI.Models;
using NannyTimeAPI.Models.Payment;
using NannyTimeAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NannyTimeAPI.Utils
{
    public static class PaymentUtil
    {
        private static double arthurRate = 17; //TODO: get from config
        private static double shareRate = 12.67; //TODO: get from config
        public static PaymentResult GetPaymentInfo(DateTime start, DateTime end)
        {
            PaymentResult paymentInfo = new PaymentResult
            {
                arthurTimes = new List<DateTimeSpan>(),
                arthurWeeks = new List<PaymentWeek>(),
                shareTimes = new List<DateTimeSpan>()
            };
            List<ClockEntry> entiresBetweenDates = new List<ClockEntry>();

            entiresBetweenDates = StorageRepository.GetClockEntriesBetweenDates(start, end);
            var orderedDates = entiresBetweenDates.GroupBy(d => CultureInfo.CurrentCulture.DateTimeFormat.Calendar.GetWeekOfYear(d.clockTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday)).OrderByDescending(d => d);
            var datesDict = orderedDates.ToDictionary(d => d.Key, d => d.ToList());
            foreach(int i in datesDict.Keys)
            {
                List<DateTimeSpan> arthurHours;
                //List<DateTimeSpan> emiliaHours;
                List<DateTimeSpan> shareHours;
                var payWeek = CalculatePaymentForWeek(datesDict[i], out arthurHours, /*out emiliaHours,*/ out shareHours);
                paymentInfo.arthurCost += payWeek.TotalPayment;
                paymentInfo.arthurWeeks.Add(payWeek);
                paymentInfo.shareTimes = shareHours;
                paymentInfo.arthurTimes = arthurHours;
            }
            return paymentInfo;
        }

        private static PaymentWeek CalculatePaymentForWeek(List<ClockEntry> clockEntries, 
            out List<DateTimeSpan> allArthurHours, /*out List<DateTimeSpan> allEmiliaHours,*/ out List<DateTimeSpan> allShareHours)
        {

            Dictionary<DateTime, List<DateTimeSpan>> arthurTimes = new Dictionary<DateTime, List<DateTimeSpan>>();
            Dictionary<DateTime, List<DateTimeSpan>> emiliaTimes = new Dictionary<DateTime, List<DateTimeSpan>>();
            Dictionary<DateTime, List<DateTimeSpan>> overlappingTimes = new Dictionary<DateTime, List<DateTimeSpan>>();

            DateTimeSpan currentArthurSpan = new DateTimeSpan();
            DateTimeSpan currentEmiliaSpan = new DateTimeSpan();

            //generate times by baby
            foreach (ClockEntry entry in clockEntries)
            {
                if (entry.baby == Babies.Arthur)
                {
                    if (currentArthurSpan.start == DateTime.MinValue && entry.clockedIn)
                    {
                        currentArthurSpan.start = entry.clockTime;
                    }
                    if (currentArthurSpan.start != DateTime.MinValue && currentArthurSpan.end == DateTime.MinValue && !entry.clockedIn)
                    {
                        currentArthurSpan.end = entry.clockTime;
                    }

                    if(currentArthurSpan.start != DateTime.MinValue && currentArthurSpan.end != DateTime.MinValue)
                    {
                        if (arthurTimes.ContainsKey(currentArthurSpan.start.Date))
                        {
                            arthurTimes[currentArthurSpan.start.Date].Add(currentArthurSpan);
                        }
                        else
                        {
                            arthurTimes.Add(currentArthurSpan.start.Date, new List<DateTimeSpan> { currentArthurSpan });
                        }

                        currentArthurSpan = new DateTimeSpan();
                    }
                }

                if (entry.baby == Babies.Emilia)
                {
                    if (currentEmiliaSpan.start == DateTime.MinValue && entry.clockedIn)
                    {
                        currentEmiliaSpan.start = entry.clockTime;
                    }
                    if (currentEmiliaSpan.start != DateTime.MinValue && currentEmiliaSpan.end == DateTime.MinValue && !entry.clockedIn)
                    {
                        currentEmiliaSpan.end = entry.clockTime;
                    }

                    if (currentEmiliaSpan.start != DateTime.MinValue && currentEmiliaSpan.end != DateTime.MinValue)
                    {
                        if (emiliaTimes.ContainsKey(currentEmiliaSpan.start.Date))
                        {
                            emiliaTimes[currentEmiliaSpan.start.Date].Add(currentEmiliaSpan);
                        }
                        else
                        {
                            emiliaTimes.Add(currentEmiliaSpan.start.Date, new List<DateTimeSpan> { currentEmiliaSpan });
                        }

                        currentEmiliaSpan = new DateTimeSpan();
                    }
                }
            }

            //get overlappingTimes
            foreach (DateTime date in arthurTimes.Keys)
            {
                List<DateTimeSpan> timeOverlaps = new List<DateTimeSpan>();

                //do the stuff
                foreach (DateTimeSpan arthurSpan in arthurTimes[date])
                {
                    if (emiliaTimes.ContainsKey(date))
                    {
                        foreach (DateTimeSpan emiliaSpan in emiliaTimes[date])
                        {
                            DateTimeSpan overlap;
                            if (arthurSpan.Overlaps(emiliaSpan, out overlap))
                            {
                                timeOverlaps.Add(overlap);
                            }
                        }
                    }
                    if (timeOverlaps.Any())
                    {
                        overlappingTimes.Add(date, timeOverlaps);
                    }
                }
            }

            //calculate final payment info and whatnot
            PaymentWeek currentWeek = new PaymentWeek(arthurRate, shareRate);
            foreach(DateTime date in arthurTimes.Keys)
            {
                double totalArthurHours = 0;
                double shareHours = 0; 

                foreach(DateTimeSpan span in arthurTimes[date])
                {
                    totalArthurHours += span.hours;
                }
                if (overlappingTimes.ContainsKey(date))
                {
                    foreach(DateTimeSpan span in overlappingTimes[date])
                    {
                        shareHours += span.hours;
                    }
                }
                double nonShareHours = totalArthurHours - shareHours;

                currentWeek.AddHours(nonShareHours);
                currentWeek.AddHours(shareHours, true);

            }

            allArthurHours = arthurTimes.GetAllSpans();
            //allEmiliaHours = emiliaTimes.GetAllSpans();
            allShareHours = overlappingTimes.GetAllSpans(); 
            return currentWeek;
        }


    }
   
}
