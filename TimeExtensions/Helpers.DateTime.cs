using System.Globalization;

namespace TheElm.Time {
    public static partial class Helpers {
        /// <summary>Converts the <see cref="DateTimeOffset"/> to a <see cref="DateOnly"/></summary>
        public static DateOnly GetDate( this DateTime dateTime )
            => DateOnly.FromDateTime(dateTime);
        
        #region Time Comparison relative to NOW
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &gt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsAhead( this DateTime? time )
            => time is DateTime value && value.IsAhead();
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &gt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsAhead( this DateTime time )
            => !time.CompareToNow(TimeComparer.LESS_THAN_EQUALS);
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &lt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsBehind( this DateTime? time )
            => time is DateTime value && value.IsBehind();
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &lt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsBehind( this DateTime time )
            => time.CompareToNow(TimeComparer.LESS_THAN);
        
        /// <summary>Checks if the <see cref="DateTime"/> is more than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAgo( this DateTime? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTime value && value.MoreThanAgo(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTime"/> is more than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAgo( this DateTime time, TimeSpan timeSpan, bool inclusive = true )
            => time.AddInternal(timeSpan).CompareToNow(inclusive ? TimeComparer.LESS_THAN_EQUALS : TimeComparer.LESS_THAN);
        
        /// <summary>Checks if the <see cref="DateTime"/> is less than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAgo( this DateTime? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTime value && value.LessThanAgo(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTime"/> is less than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAgo( this DateTime time, TimeSpan timeSpan, bool inclusive = true )
            => time.SubtractInternal(timeSpan).CompareToNow(inclusive ? TimeComparer.LESS_THAN_EQUALS : TimeComparer.LESS_THAN);
        
        /// <summary>Checks if the <see cref="DateTime"/> is more than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAhead( this DateTime? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTime value && value.MoreThanAhead(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTime"/> is more than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAhead( this DateTime time, TimeSpan timeSpan, bool inclusive = true )
            => time.SubtractInternal(timeSpan).CompareToNow(inclusive ? TimeComparer.GREATER_THAN_EQUALS : TimeComparer.GREATER_THAN);
        
        /// <summary>Checks if the <see cref="DateTime"/> is less than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAhead( this DateTime? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTime value && value.LessThanAhead(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTime"/> is less than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAhead(this DateTime time, TimeSpan timeSpan, bool inclusive = true)
            => !time.MoreThanAhead(timeSpan, !inclusive);
        
        internal static bool CompareToNow(this DateTime time, TimeComparer comparer) {
            int result = time.RoundToSeconds()
                .CompareTo(DateTime.UtcNow.RoundToSeconds());
            
            return comparer.Compare(result);
        }
        
        internal static DateTime AddInternal( this DateTime time, TimeSpan timeSpan ) {
            if (timeSpan.TotalSeconds < 1 && timeSpan.TotalMicroseconds > 0)
                throw new ArgumentOutOfRangeException(nameof(timeSpan), $"{nameof(TimeSpan)} comparison does not support seconds > 0 and < 1");
            
            return time.Add(timeSpan);
        }
        
        internal static DateTime SubtractInternal( this DateTime time, TimeSpan timeSpan ) {
            if (timeSpan.TotalSeconds < 1 && timeSpan.TotalMicroseconds > 0)
                throw new ArgumentOutOfRangeException(nameof(timeSpan), $"{nameof(TimeSpan)} comparison does not support seconds > 0 and < 1");
            
            return time.Subtract(timeSpan);
        }
        
        #endregion
        #region Math
        
        public static DateTime Add( this DateTime time, double length, TimeUnit unit = TimeUnit.Seconds )
            => time.Add(unit.ToSpan(length));
        
        public static DateTime Subtract( this DateTime time, double length, TimeUnit unit = TimeUnit.Seconds )
            => time.Subtract(unit.ToSpan(length));
        
        #endregion
        #region Time
        
        public static DateTime WithTime( this DateTime dateTime, TimeOnly time )
            => new(dateTime.GetDate(), time);
        
        public static DateTime WithTime( this DateTime dateTime, int hour, int minute = 0, int second = 0, int millis = 0, int micro = 0 )
            => dateTime.WithTime(new TimeOnly(hour, minute, second, millis, micro));
        
        /// <summary>
        /// Converts the given <see cref="DateTimeOffset"/> to 00:00 if it is currently 00:00
        /// Otherwise returns 00:00 of the next day
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset WithNextStartOfDay( this DateTime dateTime )
            => dateTime.WithNextTime(new TimeOnly());
        
        /// <summary>
        /// Converts the given <see cref="DateTimeOffset"/> to the next given set of Time
        /// If the current time is 14:00 and 12:00 is given as a time, skip to the next day
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime WithNextTime( this DateTime dateTime, TimeOnly time ) {
            DateTime timed = dateTime.WithTime(time);
            return timed < dateTime ? timed.AddDays(1) : timed;
        }
        
        /// <summary>
        /// Converts the given <see cref="DateTimeOffset"/> to the next given set of Time
        /// If the current time is 14:00 and 12:00 is given as a time, skip to the next day
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millis"></param>
        /// <param name="micro"></param>
        /// <returns></returns>
        public static DateTime WithNextTime( this DateTime dateTime, int hour, int minute = 0, int second = 0, int millis = 0, int micro = 0 )
            => dateTime.WithNextTime(new TimeOnly(hour, minute, second, millis, micro));
        
        #endregion
        #region Relative Times
        
        /// <summary>
        /// Get the next <see cref="DayOfWeek"/> from the given <see cref="DateTimeOffset"/>
        /// When inclusive, if the <see cref="DayOfWeek"/> is the same day as the given time, that time is returned
        /// If the given time has passed the day of week, the next day of week is returned
        /// If the given time has not passed the day of week, that day of week is returned
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="inclusive"></param>
        /// <returns></returns>
        public static DateTime WithNextDay( this DateTime dateTime, DayOfWeek dayOfWeek, bool inclusive = true ) {
            if ( inclusive && dateTime.DayOfWeek == dayOfWeek ) {
                return dateTime;
            }
            
            int diff = (int)dayOfWeek - (int)dateTime.DayOfWeek;
            
            if ( diff <= 0 ) {
                diff += 7;
            }
            
            // Shift the time
            return dateTime.AddDays(diff);
        }
        
        public static DateTime WithNthNextDay( this DateTime dateTime, DayOfWeek dayOfWeek, int ahead, bool inclusive = true )
            => dateTime.WithNextDay(dayOfWeek, inclusive)
                .AddDays(7 * ((int)dayOfWeek - (int)dateTime.DayOfWeek <= 0 ? ahead - 1 : ahead));
        
        #endregion
        #region TimeZones
        
        public static DateTimeOffset InZone( this DateTime dateTime, TimeZoneInfo info )
            => new(dateTime.Ticks, info.BaseUtcOffset);
        
        public static DateTimeOffset InZone( this DateTime dateTime, string name )
            => dateTime.InZone(TimeZoneInfo.FindSystemTimeZoneById(name));
        
        #endregion
        
        private static DateTime RoundToSeconds( this DateTime time )
            => new(
                year: time.Year,
                month: time.Month,
                day: time.Day,
                hour: time.Hour,
                minute: time.Minute,
                second: time.Second,
                millisecond: 0,
                microsecond: 0,
                time.Kind
            );
        
        /// <summary>Calculate the difference of <paramref name="unit"/>s between two Times</summary>
        public static double Difference( this DateTime first, DateTime second, TimeUnit unit )
            => (first - second).Total(unit);
        
        /// <summary>Convert the <see cref="DateTime"/> into a Unix Timestamp</summary>
        public static long AsUnixTimestamp( this DateTime time )
            => (long)(time - DateTime.UnixEpoch).TotalSeconds;
        
        /// <summary>Convert the <see cref="DateTimeOffset"/> into an ISO-8601 Timestamp</summary>
        public static string ToISO8601( this DateTime time, int decimals = 3 )
            => time.ToString($"yyyy-MM-dd'T'HH:mm:ss.{new string('f', decimals)}K", CultureInfo.InvariantCulture);
    }
}
