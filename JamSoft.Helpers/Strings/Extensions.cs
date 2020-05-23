namespace JamSoft.Helpers.Strings
{
    /// <summary>
    /// Various string helpers
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Takes a complete file path and shortens it to the value provided in totalLength filling in with "...".
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="endPartLength"></param>
        /// <param name="maxLength">The total length.</param>
        /// <param name="midPattern">the string pattern to use as the middle pattern</param>
        /// <returns></returns>
        public static string DotShortenString(this string input, int endPartLength, int maxLength, string midPattern = "...")
        {
            if (input == null) return null;
            if (input.Length < maxLength || input.Length < endPartLength) return input;
            if (endPartLength > maxLength) return input;
            
            var firstPart = input.Substring(0, (maxLength - endPartLength) - midPattern.Length);
            var secondPart = input.Substring(input.Length - endPartLength, endPartLength);

            return $"{firstPart}{midPattern}{secondPart}";
        }
    }
}
