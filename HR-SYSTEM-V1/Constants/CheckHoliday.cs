using HR_SYSTEM_V1.Data;
using HR_SYSTEM_V1.Models;
using HR_SYSTEM_V1.Repository.InterfaceRepository;

namespace HR_SYSTEM_V1.Constants
{
    public static class CheckHoliday
    {
        public static List<string> checkDay(Holiday dayHoliday)
        {

            List<string> checkDays = new List<string>();

            if (dayHoliday.Saturday == true)
                checkDays.Add("Saturday");
            if (dayHoliday.Sunday == true)
                checkDays.Add("Sunday");
            if (dayHoliday.Monday == true)
                checkDays.Add("Monday");
            if (dayHoliday.Tuesday == true)
                checkDays.Add("Tuesday");
            if (dayHoliday.Thursday == true)
                checkDays.Add("Thursday");
            if (dayHoliday.Wednesday == true)
                checkDays.Add("Wednesday");
            if (dayHoliday.Friday == true)
                checkDays.Add("Friday");

            return checkDays;
        }

        public static string getMonth(int month, int year)
        {
            DateTime date = new DateTime(year, month, 1);

            return date.ToString("MMMM");
        }


        public static double getHourPrice(int id, Holiday holiday, Attendance attendance, double empSalary, int subtractionTime)
        {

            var holidayDays = CheckHoliday.checkDay(holiday);

            int month = int.Parse(attendance.Day_Date.ToString("MM"));
            int year = int.Parse(attendance.Day_Date.ToString("yyyy"));

            var numberOfDayds = DateTime.DaysInMonth(year, month);

            List<string> daysInMonth = new List<string>();
            List<string> daysInMonthWithNoHolidays = new List<string>();

            for (int i = 1; i <= numberOfDayds; i++)
            {
                DateTime day = new DateTime(year, month, i, 0, 0, 0);
                daysInMonth.Add(day.DayOfWeek.ToString());
            }

            foreach (var day in daysInMonth)
                daysInMonthWithNoHolidays.Add(day);

            foreach (var holida in holidayDays)
            {
                foreach (var day in daysInMonth)
                {
                    if (holida == day)
                    {
                        daysInMonthWithNoHolidays.Remove(day);
                    }
                }
            }
            double hours = subtractionTime * daysInMonthWithNoHolidays.Count();
            
            return empSalary / hours;
        }


        public static int getDaysOfMonthWithNoHoliday(Holiday holiday, Attendance attendance)
        {
            var holidayDays = CheckHoliday.checkDay(holiday);

            int month = int.Parse(attendance.Day_Date.ToString("MM"));
            int year = int.Parse(attendance.Day_Date.ToString("yyyy"));

            var numberOfDayds = DateTime.DaysInMonth(year, month);

            List<string> daysInMonth = new List<string>();
            List<string> daysInMonthWithNoHolidays = new List<string>();

            for (int i = 1; i <= numberOfDayds; i++)
            {
                DateTime day = new DateTime(year, month, i, 0, 0, 0);
                daysInMonth.Add(day.DayOfWeek.ToString());
            }

            foreach (var day in daysInMonth)
                daysInMonthWithNoHolidays.Add(day);

            foreach (var holida in holidayDays)
            {
                foreach (var day in daysInMonth)
                {
                    if (holida == day)
                    {
                        daysInMonthWithNoHolidays.Remove(day);
                    }
                }
            }

            return daysInMonthWithNoHolidays.Count;
        }
    }
}
