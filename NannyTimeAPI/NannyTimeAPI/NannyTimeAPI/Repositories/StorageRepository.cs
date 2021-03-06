using NannyTimeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyTimeAPI.Repositories
{
    public static class StorageRepository
    {

        private static string connectionString = "Data Source=localhost;Initial Catalog=NannyTime;Integrated Security=True";

        public static bool SetState(TimeState state)
        {
            var arthurResult =  SetBabyState("Arthur", state.arthurClockedIn);
            var emiliaResult =  SetBabyState("Emilia", state.emiliaClockedIn);
            return arthurResult && emiliaResult;
        }

        public static TimeState GetState()
        {
            TimeState result = new TimeState();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetMostRecentEntries", conn))
                    {
                        conn.Open();
                        var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            result.arthurClockedIn = reader.GetBoolean(1);
                        }

                        if (reader.Read())
                        {
                            result.emiliaClockedIn = reader.GetBoolean(1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR!", e);
                //log or something?
            }
            return result;
        }

        public static List<ClockEntry> GetClockEntriesBetweenDates(DateTime startDate, DateTime endDate)
        {
            List<ClockEntry> result = new List<ClockEntry>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetEntriesByDates", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var startTime = command.Parameters.Add("@start", System.Data.SqlDbType.DateTime);
                        startTime.Value = startDate;

                        var endTime = command.Parameters.Add("@end", System.Data.SqlDbType.DateTime);
                        endTime.Value = endDate;

                        conn.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new ClockEntry
                            {
                                baby = reader.GetString(0),
                                clockTime = reader.GetDateTime(1),
                                clockedIn = reader.GetBoolean(2)
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR!", e);
                //log or something?
            }
            return result;
            return new List<ClockEntry>();
        }

        private static bool SetBabyState(string baby, bool clockingIn)
        {
            bool result = false;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("SetState", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var timeParam = command.Parameters.Add("@timeClock", System.Data.SqlDbType.DateTime);
                        timeParam.Value = DateTime.Now;

                        var nameParam = command.Parameters.Add("@baby", System.Data.SqlDbType.VarChar, 50);
                        nameParam.Value = $"{baby}-test";

                        var clockParam = command.Parameters.Add("@clockIn", System.Data.SqlDbType.Bit);
                        clockParam.Value = clockingIn ? 1 : 0;

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR!", e);
                //log or something?
            }
            return result;
        }
    }
}
