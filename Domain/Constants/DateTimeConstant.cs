using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public class DateTimeConstant
    {
        public static string GetCurrentYear() => DateTime.Now.Year.ToString();
        public static string GetCurrentMonth() => DateTime.Now.Month.ToString();
        public static string GetCurrentDay() => DateTime.Now.Day.ToString();
        public static string GetCurrentDateString() => DateTime.Now.ToString("dd/MM/yyyy");
        public static string GetCurrentDateUtcString() => DateTime.UtcNow.ToString("dd/MM/yyyy");
    }
}
