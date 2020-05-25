using System;

namespace JamSoft.Helpers.Numbers
{
	/// <summary>
	/// Provides a collection of methods for calculating percentages
	/// </summary>
	public static class Percentages
	{
		/// <summary>
		/// Determines what percentage of the total the provided value is.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="total">The total.</param>
		/// <returns>the percentage</returns>
		public static int IsWhatPercentageOf(this int value, int total)
		{
			if (total < value) return -1;
			
			return Convert.ToInt32(Math.Round(((decimal) value / total) * 100, 0));
		}

		/// <summary>
		/// Determines what percentage of the total the provided value is.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="total">The total.</param>
        /// <param name="precision">The precision of the result.</param>
		/// <returns>the percentage</returns>
		public static double IsWhatPercentageOf(this double value, double total, int precision = 0)
		{
			if (total < value) return -1;

			if (precision > 0)
			{
				return Math.Round(value / total * 100, precision);
			}
			
			return value / total * 100;
		}

		/// <summary>
		/// Determines what percentage of the total the provided value is.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="total">The total.</param>
        /// <param name="precision">The precision of the result.</param>
		/// <returns>the percentage</returns>
		public static double IsWhatPercentageOf(this float value, float total, int precision = 0)
		{
			if (total < value) return -1;

			if (precision > 0)
			{
				return Math.Round(value / total * 100, precision);
			}
			
			return value / total * 100;
		}

		/// <summary>
        /// Determines what percentage of the total the provided value is.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="total">The total.</param>
		/// <param name="precision">The precision of the result.</param>
		/// <returns>the percentage</returns>
		public static decimal IsWhatPercentageOf(this decimal value, decimal total, int precision = 0)
		{
			if (total < value) return -1;

			if (precision > 0)
			{
				return Math.Round(value / total * 100, precision);
			}
			
			return value / total * 100;
		}
	}
}