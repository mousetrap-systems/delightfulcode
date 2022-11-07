using System;

public static partial class ExtendInt32
{
    public static int PercentOf(this ulong value, ulong total)
    {
        return (int)((value / (double)total) * 100);
    }

    public static int PercentOf(this int value, int total)
    {
        ulong v = value > 0 ? (ulong)value : 0;
        return total == 0 ? 0 : v.PercentOf((ulong)total);
    }

    public static bool IsOdd(this int value)
    {
        return value % 2 != 0;
    }
}