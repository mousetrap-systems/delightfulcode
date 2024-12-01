namespace DelightfulCode
{
    public static partial class Extensions
    {
        /// <summary>
        /// Calculates the percentage that the provided integer 'value' represents out of the 'total'.
        /// The percentage is returned as a whole number (integer). If the 'total' is zero, the method returns zero.
        /// </summary>
        /// <param name="value">The integer value to calculate the percentage of.</param>
        /// <param name="total">The total integer value that the 'value' parameter is a part of.</param>
        /// <returns>An integer representing the percentage that the 'value' parameter is of the 'total' parameter.</returns>
        public static int PercentOf(this int value, int total)
        {
            return total == 0 ? 0 : (int)(((ulong)value / (double)total) * 100);
        }

        /// <summary>
        /// Determines whether an integer is odd (very basic check)
        /// </summary>
        /// <param name="value">The integer to check.</param>
        /// <returns>true if the integer is odd; otherwise, false.</returns>
        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }
    }
}