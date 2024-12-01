namespace DelightfulCode
{
    public static partial class Extensions
    {
        /// <summary>
        /// Rounds the input number to a specified number of decimal places.
        /// This method uses the standard mathematical rounding rules: if the number to the right of the last significant digit is 5 or more, the last significant digit is increased by one.
        /// </summary>
        /// <param name="number">The double-precision floating-point number to be rounded.</param>
        /// <param name="numberOfDecimalPlaces">The number of significant decimal places to which the input number should be rounded.</param>
        /// <returns>The input number rounded to the specified number of decimal places.</returns>
        public static double ToDecimalPlaces(this double number, int numberOfDecimalPlaces)
        {
            return Math.Round(number, numberOfDecimalPlaces);
        }

        /// <summary>
        /// Calculates the percentage of a value from the total value, represented as a double-precision floating-point number.
        /// </summary>
        public static double PercentOf(this double value, double total)
        {
            return total == 0 ? 0 : (value / total) * 100;
        }
    }
}