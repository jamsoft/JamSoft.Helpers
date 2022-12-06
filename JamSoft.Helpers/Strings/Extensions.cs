using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;

namespace JamSoft.Helpers.Strings
{
    /// <summary>
    /// Various string helper methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Removes all instances of multi white space substrings.
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="replacePattern">Optional replace pattern</param>
        /// <param name="trim">set to true to trim leading and trailing spaces</param>
        /// <returns>the sanitised result</returns>
        public static string RemoveAllMultiSpace(this string input, string replacePattern = " ", bool trim = false)
        {
            return trim ? Regex.Replace(input, @"\s+", replacePattern).Trim(replacePattern.ToCharArray()) 
                        : Regex.Replace(input, @"\s+", replacePattern);
        }

        /// <summary>
        /// Takes a complete file path and shortens it to the value provided in totalLength filling in with "...".
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="endPartLength"></param>
        /// <param name="maxLength">The total length.</param>
        /// <param name="midPattern">the string pattern to use as the middle pattern</param>
        /// <returns>The shortened string using either the standard dot notation or a pattern if provided</returns>
        public static string DotShortenString(this string input, int endPartLength, int maxLength, string midPattern = "...")
        {
            if (input.Length < maxLength || input.Length < endPartLength) return input;
            if (endPartLength > maxLength) return input;
            
            var firstPart = input.Substring(0, (maxLength - endPartLength) - midPattern.Length);
            var secondPart = input.Substring(input.Length - endPartLength, endPartLength);

            return $"{firstPart}{midPattern}{secondPart}";
        }

        /// <summary>
        /// Compares strings whilst ignoring case sensitivity and culture
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="input"></param>
        /// <returns><c>true</c> if is equal to the specified input; otherwise, <c>false</c>.</returns>
        public static bool IsExactlySameAs(this string theString, string input)
        {
            return string.Compare(theString, input, StringComparison.Ordinal) == 0;
        }
    }
}
