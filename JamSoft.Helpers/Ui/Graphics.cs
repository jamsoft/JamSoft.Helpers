using System;
using System.Drawing;
using System.Globalization;

namespace JamSoft.Helpers.Ui
{
    /// <summary>
    /// Provides helper methods for UI tasks
    /// </summary>
    public static class Graphics
    {
        /// <summary>
        /// Provides helper methods to work with color values
        /// </summary>
        public static class Colors
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
            /// Converts to hex.
            /// </summary>
            /// <param name="a">a.</param>
            /// <param name="r">The r.</param>
            /// <param name="g">The g.</param>
            /// <param name="b">The b.</param>
            /// <param name="prependHash">if set to <c>true</c> [prepend hash].</param>
            /// <returns>The hex representation or black when passed any component value greater than 255 or less than 0</returns>
            public static string ToHex(int a, int r, int g, int b, bool prependHash = true)
            {
                string retVal;
                if (a > 255 || r > 255 || g > 255 || b > 255 || a < 0 || r < 0 || g < 0 || b < 0)
                {
                    retVal = "FF000000";
                }
                else
                {
                    retVal = $"{a:X2}{r:X2}{g:X2}{b:X2}";
                }

                return prependHash ? $"#{retVal}" : retVal;
            }

            /// <summary>
            /// Converts RGB integers to HEX format color string.
            /// </summary>
            /// <param name="values">The RGB values array</param>
            /// <param name="prependHash">if set to <c>true</c> [prepend hash].</param>
            /// <returns>The hex representation or black when passed any component value greater than 255 or less than 0</returns>
            /// <exception cref="ArgumentException">Thrown if insufficient number of integer values are provided</exception>
            public static string ToHex(int[] values, bool prependHash = true)
            {
                if (values.Length < 3) throw new ArgumentException("Insufficient values provided", nameof(values));
                if (values.Length < 4)
                {
                    return ToHex(values[0], values[1], values[2], prependHash);
                }

                return ToHex(values[0], values[1], values[2], values[3], prependHash);
            }

            /// <summary>
            /// Converts a standard hex string to <seealso cref="Color"/> struct
            /// </summary>
            /// <param name="hex">The string to convert to a color</param>
            /// <returns> Color struct based on the provided hex string</returns>
            /// <exception cref="ArgumentException">Thrown when string is invalid length</exception>
            public static Color ToRgb(string hex)
            {
                if (hex.Length < 6) throw new ArgumentException("Input string invalid length");
                if (hex.IndexOf('#') != -1)
                {
                    hex = hex.Replace("#", "");
                }

                var r = int.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                var g = int.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                var b = int.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);

                return Color.FromArgb(r, g, b);
            }

            /// <summary>
            /// Converts a alpha hex string to <seealso cref="Color"/> struct
            /// </summary>
            /// <param name="hex"></param>
            /// <returns> Color struct based on the provided hex string</returns>
            /// <exception cref="ArgumentException">Thrown when string is invalid length</exception>
            public static Color ToArgb(string hex)
            {
                if (hex.Length < 8) throw new ArgumentException("Input string invalid length");
                if (hex.IndexOf('#') != -1)
                {
                    hex = hex.Replace("#", "");
                }

                var a = int.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                var r = int.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                var g = int.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                var b = int.Parse(hex.Substring(6, 2), NumberStyles.AllowHexSpecifier);

                return Color.FromArgb(a,r, g, b);
            }
        }
    }
}
