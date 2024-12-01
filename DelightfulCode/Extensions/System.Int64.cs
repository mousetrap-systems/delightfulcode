namespace DelightfulCode
{
    public static partial class Extensions
    {
        /// <summary>
        /// Calculates the percentage of a value from the total value, represented as an unsigned long integer.
        /// </summary>
        public static int PercentOf(this ulong value, ulong total)
        {
            return total == 0 ? 0 : (int)((value / (double)total) * 100);
        }
    }
}