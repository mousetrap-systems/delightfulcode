using System;
using System.Text.RegularExpressions;

public static partial class Extensions
{
    /// <summary>
    /// Inverse of 'IsEmpty', just a bit more readable.
    /// Indicates has at least one character (and whitespace is allowed in this version)
    /// </summary>
    public static bool HasSomeValue(this string? s)
    {
        return !string.IsNullOrEmpty(s);
    }

    /// <summary>
    /// Determines whether the string contained whitespace or not (as its only content).
    /// This is useful shorthand for combining with some tests in the case where the string
    /// is expected to contain whitespace but nothing else. For example, if the string value
    /// is "    " (i.e. contains spaces) then this method returns a more correct result:
    ///  - IsNullOrEmpty = false
    ///  - IsBlank = true
    /// </summary>
    /// <remarks>
    /// I know this seems overkill for a common-enough routine, but it's a neat shortcut and reads really well in code
    /// </remarks>
    [Health(CodeStability.Stable)]
    public static bool IsBlank(this string? s)
    {
        return string.IsNullOrEmpty(s?.Trim());
    }

    /// <summary>
    /// Simple version of truncate, with no frills ...
    /// Truncates the string to a specified length and replace the truncated to a ...
    /// </summary>
    /// <param name="text">string that will be truncated</param>
    /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
    /// <returns>truncated string</returns>
    public static string Truncate(this string text, int maxLength)
    {
        const string suffix = "...";
        string truncatedString = text;

        if (maxLength <= 0) return truncatedString;
        int strLength = maxLength - suffix.Length;

        if (strLength <= 0) return truncatedString;

        if (text == null || text.Length <= maxLength) return truncatedString;

        truncatedString = text.Substring(0, strLength);
        truncatedString = truncatedString.TrimEnd();
        truncatedString += suffix;
        return truncatedString;
    }

    /// <summary>
    /// Very simple test for whether a string might be a number, like the method from VB.NET.
    /// </summary>
    /// <remarks>
    /// As Scott Hanselman points out, http://www.hanselman.com/blog/ExploringIsNumericForC.aspx
    /// this is not a true implementation, but it works for us because it's really, really simple.
    /// </remarks>
    public static bool IsNumeric(this string s)
    {
        int i = 0;
        return Int32.TryParse(s, out i); // this is maximized for performance
    }

    /// <summary>
    /// Uses RegEx for removal of any whitespace patterns.
    /// Great shortcut for where you don't want to remember the complex RegEx things.
    /// </summary>
    public static string TrimWhitespace(this string original)
    {
        return Regex.Replace(original, @"^\s*$\n", string.Empty, RegexOptions.Multiline);
    }

    /// <summary>
    /// Returns empty if no valid match.
    /// IS CASE SENSITIVE!
    /// </summary>
    /// <param name="input">the string you want to check</param>
    /// <param name="firstOccurenceText">first match occurence (i.e. from the start of the string)</param>
    /// <param name="lastOccurenceText">last match - this starts looking from the back, i.e. 'LastIndexOf'</param>
    public static string GetTextBetween(this string input, string firstOccurenceText, string lastOccurenceText)
    {
        int startPos = input.IndexOf(firstOccurenceText) + firstOccurenceText.Length;
        int finishPos = input.LastIndexOf(lastOccurenceText);
        int length = finishPos - startPos;
        string textBetweenMarkers = string.Empty;

        if (startPos > 0 && finishPos > 0)
        {
            textBetweenMarkers = input.Substring(startPos, length);
        }

        return textBetweenMarkers;
    }
}