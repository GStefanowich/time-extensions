namespace TheElm.Time {
    public static partial class Helpers {
        /// <summary>Get the total <see cref="TimeUnit"/> of a <see cref="TimeSpan"/> (1m30s, Seconds = 90)</summary>
        public static double Total( this TimeSpan span, TimeUnit unit )
            => unit switch {
                TimeUnit.Milliseconds => span.TotalMilliseconds,
                TimeUnit.Seconds => span.TotalSeconds,
                TimeUnit.Minutes => span.TotalMinutes,
                TimeUnit.Hours => span.TotalHours,
                TimeUnit.Days => span.TotalDays,
                _ => 0
            };
        
        /// <summary>Get only the <see cref="TimeUnit"/> of a <see cref="TimeSpan"/> (1m30s, Seconds = 30)</summary>
        public static int Of( this TimeSpan span, TimeUnit unit )
            => unit switch {
                TimeUnit.Milliseconds => span.Milliseconds,
                TimeUnit.Seconds => span.Seconds,
                TimeUnit.Minutes => span.Minutes,
                TimeUnit.Hours => span.Hours,
                TimeUnit.Days => span.Days,
                _ => 0
            };
    }
}
