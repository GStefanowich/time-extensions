namespace TheElm.Time {
    public static partial class Helpers {
        /// <summary>Converts the <see cref="DateTimeOffset"/> to a <see cref="DateOnly"/></summary>
        public static DateOnly GetDate( this DateTimeOffset offset )
            => offset.UtcDateTime.GetDate();
        
        #region Time Comparison relative to NOW
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &gt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsAhead( this DateTimeOffset? time )
            => time is DateTimeOffset value && value.IsAhead();
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &gt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsAhead( this DateTimeOffset time )
            => !time.CompareToNow(TimeComparer.LESS_THAN_EQUALS);
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &lt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsBehind( this DateTimeOffset? time )
            => time is DateTimeOffset value && value.IsBehind();
        
        /// <summary>Returns <see langref="true"/> if the Date and Time are &lt; the current system Date and Time (Compares in <see cref="TimeUnit.Seconds"/>)</summary>
        public static bool IsBehind( this DateTimeOffset time )
            => time.CompareToNow(TimeComparer.LESS_THAN);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is more than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAgo( this DateTimeOffset? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTimeOffset value && value.MoreThanAgo(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is more than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAgo( this DateTimeOffset time, TimeSpan timeSpan, bool inclusive = true )
            => time.AddInternal(timeSpan).CompareToNow(inclusive ? TimeComparer.LESS_THAN_EQUALS : TimeComparer.LESS_THAN);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is less than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAgo( this DateTimeOffset? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTimeOffset value && value.LessThanAgo(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is less than <see cref="TimeSpan"/> into the past than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAgo( this DateTimeOffset time, TimeSpan timeSpan, bool inclusive = true )
            => time.SubtractInternal(timeSpan).CompareToNow(inclusive ? TimeComparer.LESS_THAN_EQUALS : TimeComparer.LESS_THAN);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is more than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAhead( this DateTimeOffset? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTimeOffset value && value.MoreThanAhead(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is more than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool MoreThanAhead( this DateTimeOffset time, TimeSpan timeSpan, bool inclusive = true )
            => time.SubtractInternal(timeSpan).CompareToNow(inclusive ? TimeComparer.GREATER_THAN_EQUALS : TimeComparer.GREATER_THAN);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is less than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAhead( this DateTimeOffset? time, TimeSpan timeSpan, bool inclusive = true )
            => time is DateTimeOffset value && value.LessThanAhead(timeSpan, inclusive);
        
        /// <summary>Checks if the <see cref="DateTimeOffset"/> is less than <see cref="TimeSpan"/> into the future than the current system time</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the <see cref="TimeSpan"/> given is fewer than 1 full second, but great than 0 seconds (Time comparison is in Seconds, so Millis and Micro are unsupported and can lead to bugs)</exception>
        public static bool LessThanAhead( this DateTimeOffset time, TimeSpan timeSpan, bool inclusive = true )
            => time.SubtractInternal(timeSpan).CompareToNow(inclusive ? TimeComparer.LESS_THAN_EQUALS : TimeComparer.LESS_THAN);
        
        internal static bool CompareToNow( this DateTimeOffset time, TimeComparer comparer ) {
            int result = time.RoundToSeconds()
                .CompareTo(DateTimeOffset.UtcNow.RoundToSeconds());
            return comparer.Compare(result);
        }
        
        internal static DateTimeOffset AddInternal( this DateTimeOffset time, TimeSpan timeSpan ) {
            if (timeSpan.TotalSeconds < 1 && timeSpan.TotalMicroseconds > 0)
                throw new ArgumentOutOfRangeException(nameof(timeSpan), $"{nameof(TimeSpan)} comparison does not support seconds > 0 and < 1");
            return time.Add(timeSpan);
        }
        
        internal static DateTimeOffset SubtractInternal( this DateTimeOffset time, TimeSpan timeSpan ) {
            if (timeSpan.TotalSeconds < 1 && timeSpan.TotalMicroseconds > 0)
                throw new ArgumentOutOfRangeException(nameof(timeSpan), $"{nameof(TimeSpan)} comparison does not support seconds > 0 and < 1");
            return time.Subtract(timeSpan);
        }
        
        #endregion
        #region Math
        
        public static DateTimeOffset Add( this DateTimeOffset time, double length, TimeUnit unit = TimeUnit.Seconds )
            => time.Add(unit.ToSpan(length));
        
        public static DateTimeOffset Subtract( this DateTimeOffset time, double length, TimeUnit unit = TimeUnit.Seconds )
            => time.Subtract(unit.ToSpan(length));
        
        #endregion
        #region Time
        
        public static DateTimeOffset WithTime( this DateTimeOffset dateTime, TimeOnly time )
            => new(dateTime.GetDate(), time, dateTime.Offset);
        
        public static DateTimeOffset WithTime( this DateTimeOffset dateTime, int hour, int minute = 0, int second = 0, int millis = 0, int micro = 0 )
            => dateTime.WithTime(new TimeOnly(hour, minute, second, millis, micro));
        
        /// <summary>
        /// Converts the given <see cref="DateTimeOffset"/> to 00:00 if it is currently 00:00
        /// Otherwise returns 00:00 of the next day
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset WithNextStartOfDay( this DateTimeOffset dateTime )
            => dateTime.WithNextTime(new TimeOnly());
        
        /// <summary>
        /// Converts the given <see cref="DateTimeOffset"/> to the next given set of Time
        /// If the current time is 14:00 and 12:00 is given as a time, skip to the next day
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTimeOffset WithNextTime( this DateTimeOffset dateTime, TimeOnly time ) {
            DateTimeOffset timed = dateTime.WithTime(time);
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
        public static DateTimeOffset WithNextTime( this DateTimeOffset dateTime, int hour, int minute = 0, int second = 0, int millis = 0, int micro = 0 )
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
        public static DateTimeOffset WithNextDay( this DateTimeOffset dateTime, DayOfWeek dayOfWeek, bool inclusive = true ) {
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
        
        public static DateTimeOffset WithNthNextDay( this DateTimeOffset dateTime, DayOfWeek dayOfWeek, int ahead, bool inclusive = true )
            => dateTime.WithNextDay(dayOfWeek, inclusive)
                .AddDays(7 * ((int)dayOfWeek - (int)dateTime.DayOfWeek <= 0 ? ahead - 1 : ahead));
        
        #endregion
        #region TimeZones
        
        public static DateTimeOffset InZone( this DateTimeOffset dateTime, TimeZoneInfo info )
            => new(dateTime.Ticks, info.BaseUtcOffset);
        
        public static DateTimeOffset InZone( this DateTimeOffset dateTime, string name )
            => dateTime.InZone(TimeZoneInfo.FindSystemTimeZoneById(name));
        
        #endregion
        
        private static DateTimeOffset RoundToSeconds( this DateTimeOffset time )
            => new(
                year: time.Year,
                month: time.Month,
                day: time.Day,
                hour: time.Hour,
                minute: time.Minute,
                second: time.Second,
                millisecond: 0,
                microsecond: 0,
                offset: time.Offset
            );
        
        /// <summary>Convert the <see cref="DateTimeOffset"/> to the nearest <see cref="TimeSpan"/> tick (as a <see cref="DateTimeOffset"/>)</summary>
        public static DateTimeOffset RoundToNearest( this DateTimeOffset time, TimeSpan nearest )
            => new(time.RoundToNearestTick(nearest), time.Offset);
        
        /// <summary>Convert the <see cref="DateTimeOffset"/> to the nearest <see cref="TimeSpan"/> tick (in Ticks)</summary>
        public static long RoundToNearestTick( this DateTimeOffset time, TimeSpan nearest )
            => (time.Ticks + nearest.Ticks - 1) / nearest.Ticks * nearest.Ticks;
        
        /// <summary>Calculate the difference of <paramref name="unit"/> between two Times</summary>
        public static double Difference( this DateTimeOffset first, DateTimeOffset second, TimeUnit unit )
            => (first - second).Total(unit);
        
        /// <summary>Gets the <see cref="TimeUnit"/>s ago of the <see cref="DateTimeOffset"/> compared to the System Time, removes decimal places</summary>
        public static double Ago( this DateTimeOffset time, TimeUnit unit )
            => time.Difference(DateTimeOffset.UtcNow, unit);
        
        /// <summary>Gets the <see cref="TimeUnit"/>s ago of the <see cref="DateTimeOffset"/> compared to the System Time, without removing decimal places</summary>
        public static double AgoWithPrecision( this DateTimeOffset time, TimeUnit unit )
            => time.WithPrecision(unit).Difference(DateTimeOffset.UtcNow.WithPrecision(unit), unit);
        
        /// <summary>Get the <see cref="TimeSpan"/> of how long ago the <see cref="DateTimeOffset"/> is from the current system time</summary>
        public static TimeSpan UnitsAgo( this DateTimeOffset time, TimeUnit unit )
            => unit.ToSpan(time.AgoWithPrecision(unit));
        
        /// <summary>Remove <see cref="TimeUnit"/>s from the <see cref="DateTimeOffset"/> to be less precise</summary>
        public static DateTimeOffset WithPrecision( this DateTimeOffset time, TimeUnit unit )
            => unit switch {
                TimeUnit.Milliseconds => time,
                TimeUnit.Seconds => new DateTimeOffset(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Offset),
                TimeUnit.Minutes => new DateTimeOffset(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, time.Offset),
                TimeUnit.Hours => new DateTimeOffset(time.Year, time.Month, time.Day, time.Hour, 0, 0, time.Offset),
                TimeUnit.Days => new DateTimeOffset(time.Year, time.Month, time.Day, 0, 0, 0, time.Offset),
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };
        
        /// <summary>Convert the <see cref="DateTimeOffset"/> into a Unix Timestamp</summary>
        public static long AsUnixTimestamp( this DateTimeOffset time )
            => (long)(time - DateTimeOffset.UnixEpoch).TotalSeconds;
        
        /// <summary>Convert the <see cref="DateTimeOffset"/> into an ISO-8601 Timestamp</summary>
        public static string ToISO8601( this DateTimeOffset time, int decimals = 3 )
            => time.UtcDateTime.ToISO8601();
    }
}
