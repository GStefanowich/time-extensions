namespace TheElm.Time {
    public static partial class Helpers {
        /// <summary>Create a <see cref="TimeSpan"/> from a <see cref="TimeUnit"/></summary>
        public static TimeSpan ToSpan( this TimeUnit unit, double length )
            => unit switch {
                TimeUnit.Milliseconds => TimeSpan.FromMilliseconds(length),
                TimeUnit.Seconds => TimeSpan.FromSeconds(length),
                TimeUnit.Minutes => TimeSpan.FromMinutes(length),
                TimeUnit.Hours => TimeSpan.FromHours(length),
                TimeUnit.Days => TimeSpan.FromDays(length),
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };
        
        /// <summary>Convert one <see cref="TimeUnit"/> amount to another <see cref="TimeUnit"/> amount</summary>
        public static double From( this TimeUnit unit, double sourceLength, TimeUnit sourceUnit )
            => sourceUnit.ToSpan(sourceLength)
                .Total(unit);
    }
}
