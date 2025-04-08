namespace TheElm.Time {
    public static partial class Helpers {
        /// <summary>Returns <see langref="true"/> if the Date is &gt; the current system Date</summary>
        public static bool IsAhead( this DateOnly? date )
            => date is DateOnly value && value.IsAhead();
        
        /// <summary>Returns <see langref="true"/> if the Date is &gt; the current system Date</summary>
        public static bool IsAhead( this DateOnly date )
            => !date.CompareToNow(TimeComparer.LESS_THAN_EQUALS);
        
        /// <summary>Returns <see langref="true"/> if the Date is &lt; the current system Date</summary>
        public static bool IsBehind( this DateOnly? date )
            => date is DateOnly value && value.IsBehind();
        
        /// <summary>Returns <see langref="true"/> if the Date is &lt; the current system Date</summary>
        public static bool IsBehind( this DateOnly date )
            => date.CompareToNow(TimeComparer.LESS_THAN);
        
        internal static bool CompareToNow( this DateOnly time, TimeComparer comparer ) {
            int result = time
                .CompareTo(DateTimeOffset.UtcNow.GetDate());
            return comparer.Compare(result);
        }
    }
}
