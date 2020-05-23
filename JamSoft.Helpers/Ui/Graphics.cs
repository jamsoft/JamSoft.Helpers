using System;

namespace JamSoft.Helpers.Ui
{
    /// <summary>
    /// Provides helper methods for UI tasks
    /// </summary>
    public static class Graphics
    {
        /// <summary>
        /// Converts RGB integers to HEX format color string.
        /// </summary>
        /// <param name="r">The RED component</param>
        /// <param name="g">The GREEN component</param>
        /// <param name="b">The BLUE component</param>
        /// <param name="prependHash">if set to <c>true</c> [prepend hash].</param>
        /// <returns>The hex representation or black when passed any component value greater than 255 or less than 0</returns>
        public static string ToHex(int r, int g, int b, bool prependHash = true)
        {
            string retVal;
            if (r > 255 || g > 255 || b > 255 || r < 0 || g < 0 || b < 0)
            {
                retVal = "000000";
            }
            else
            {
                retVal = $"{r:X2}{g:X2}{b:X2}";
            }

            return prependHash ? $"#{retVal}" : retVal;
        }

        /// <summary>
        /// Converts RGB integers to HEX format color string.
        /// </summary>
        /// <param name="rgb">The RGB values array</param>
        /// <param name="prependHash">if set to <c>true</c> [prepend hash].</param>
        /// <returns>The hex representation or black when passed any component value greater than 255 or less than 0</returns>
        /// <exception cref="ArgumentException">Thrown if insufficient number of integer values are provided</exception>
        public static string ToHex(int[] rgb, bool prependHash = true)
        {
            if (rgb.Length < 3) throw new ArgumentException("Insufficient RGB values provided", nameof(rgb));
            return ToHex(rgb[0], rgb[1], rgb[2], prependHash);
        }
    }
}
