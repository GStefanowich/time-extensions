namespace TheElm.Time {
    public static partial class Helpers {
        internal static bool Compare( this TimeComparer comparer, int value )
            => comparer switch {
                TimeComparer.GREATER_THAN => value > 0,
                TimeComparer.GREATER_THAN_EQUALS => value >= 0,
                TimeComparer.LESS_THAN => value < 0,
                TimeComparer.LESS_THAN_EQUALS => value <= 0,
                TimeComparer.EQUALS => value == 0,
                _ => throw new ArgumentOutOfRangeException(nameof(comparer), comparer, null)
            };
    }
}
