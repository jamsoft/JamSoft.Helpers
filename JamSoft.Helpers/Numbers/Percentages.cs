using System;

namespace JamSoft.Helpers.Numbers
{
	public static class Percentages
	{
		public static int IsWhatPercentageOf(this int value, int total)
		{
			if (total < value) return -1;
			
			return Convert.ToInt32(Math.Round(((decimal) value / total) * 100, 0));
		}

		public static double IsWhatPercentageOf(this double value, double total, int precision = 0)
		{
			if (total < value) return -1;

			if (precision > 0)
			{
				return Math.Round(value / total * 100, precision);
			}
			
			return value / total * 100;
		}
		
		public static double IsWhatPercentageOf(this float value, float total, int precision = 0)
		{
			if (total < value) return -1;

			if (precision > 0)
			{
				return Math.Round(value / total * 100, precision);
			}
			
			return value / total * 100;
		}
		
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