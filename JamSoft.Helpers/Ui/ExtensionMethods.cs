using System;

namespace JamSoft.Helpers.Ui
{
	/// <summary>
	/// General UI Extension Methods
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// Converts an input long value and converts to a human readable representation
		/// </summary>
		/// <param name="i">the input value</param>
		/// <returns>a human readable representation of the input</returns>
		public static string ToHumanReadable(this int i)
		{
			return ToHumanReadable(Convert.ToInt64(i));
		}
		
		/// <summary>
		/// Converts an input long value and converts to a human readable representation
		/// </summary>
		/// <param name="i">the input value</param>
		/// <returns>a human readable representation of the input</returns>
		public static string ToHumanReadable(this long i)
		{
			long absoluteI = (i < 0 ? -i : i);
			string suffix;
			double readable;
			if (absoluteI >= 0x1000000000000000) // Exabyte
			{
				suffix = "Eb";
				readable = (i >> 50);
			}
			else if (absoluteI >= 0x4000000000000) // Petabyte
			{
				suffix = "Pb";
				readable = (i >> 40);
			}
			else if (absoluteI >= 0x10000000000) // Terabyte
			{
				suffix = "Tb";
				readable = (i >> 30);
			}
			else if (absoluteI >= 0x40000000) // Gigabyte
			{
				suffix = "Gb";
				readable = (i >> 20);
			}
			else if (absoluteI >= 0x100000) // Megabyte
			{
				suffix = "Mb";
				readable = (i >> 10);
			}
			else if (absoluteI >= 0x400) // Kilobyte
			{
				suffix = "Kb";
				readable = i;
			}
			else
			{
				return i.ToString("0 b"); // Byte
			}
			
			readable = (readable / 1024);
			return readable.ToString("0.## ") + suffix;
		}
	}
}