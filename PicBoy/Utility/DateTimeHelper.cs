using System;

namespace PicBoy.Utility
{
    /// <summary>
    /// Represents extension methods for DateTime.
    /// </summary>
    static class DateTimeHelper
    {
        /// <summary>
        /// Returns end of the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>End of the day.</returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// Returns start of the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Start of the day.</returns>
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
    }
}