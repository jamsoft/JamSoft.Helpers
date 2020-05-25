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
        /// Removes all instances of multi-whitespace substrings.
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
            if (input == null) return null;
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

        /// <summary>
        /// Determines whether secureStringOne [is equal to] [the specified secureStringTwo].
        /// </summary>
        /// <param name="secureStringOne">The secureStringOne.</param>
        /// <param name="secureStringTwo">The secureStringTwo.</param>
        /// <returns>
        ///   <c>true</c> if is equal to the specified secureStringTwo; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsExactlySameAs(this SecureString secureStringOne, SecureString secureStringTwo)
        {
            var bstr1 = IntPtr.Zero;
            var bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(secureStringOne);
                bstr2 = Marshal.SecureStringToBSTR(secureStringTwo);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                }
                else return false;
                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }
    }
}
