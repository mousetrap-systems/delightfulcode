using System;

public static partial class Extensions
{
    /// <summary>
    /// Returns the same number but to specific requested decimal places.
    /// </summary>
    public static double ToDecimalPlaces(this double number, int numberOfDecimalPlaces)
    {
        return Math.Round(number, numberOfDecimalPlaces);
    }

    public static double PercentOf(this double value, double total)
    {
        ulong v = value > 0 ? (ulong)value : 0;
        return total == 0 ? 0 : v.PercentOf((ulong)total);
    }
}