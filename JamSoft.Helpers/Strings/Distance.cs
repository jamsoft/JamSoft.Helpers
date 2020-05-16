using System;
using System.Linq;

namespace JamSoft.Helpers.Strings
{
	/// <summary>
	/// A class containing distance algorithms for string comparisons
	/// </summary>
	public static class Distance
	{
		/// <summary>
		/// Returns 0 -> positive integer for distance, or -1 for different length strings
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static int HammingDistanceTo(this string s, string t)
		{
			return GetHammingDistance(s, t);
		}
		
		/// <summary>
		/// Returns 0 -> positive integer for distance, or -1 for different length strings
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static int GetHammingDistance(string s, string t)
		{
			if (s.Length != t.Length)
			{
				return -1;
			}
 
			var distance =
				s.ToCharArray()
					.Zip(t.ToCharArray(), (c1, c2) => new { c1, c2 })
					.Count(m => m.c1 != m.c2);
 
			return distance;
		}

		/// <summary>
		/// Returns the number of edits required to mutate one string into another
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static int LevenshteinDistanceTo(this string s, string t)
		{
			return GetLevenshteinDistance(s, t);
		}
		
		/// <summary>
		/// Returns the number of edits required to mutate one string into another
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static int GetLevenshteinDistance(string s, string t)
		{
			int n = s.Length;
			int m = t.Length;
			int[,] d = new int[n + 1, m + 1];
 
			// Step 1
			if (n == 0)
			{
				return m;
			}
 
			if (m == 0)
			{
				return n;
			}
 
			// Step 2
			for (int i = 0; i <= n; d[i, 0] = i++)
			{
			}
 
			for (int j = 0; j <= m; d[0, j] = j++)
			{
			}
 
			// Step 3
			for (int i = 1; i <= n; i++)
			{
				//Step 4
				for (int j = 1; j <= m; j++)
				{
					// Step 5
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
 
					// Step 6
					d[i, j] = Math.Min(
						Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						d[i - 1, j - 1] + cost);
				}
			}
			// Step 7
			return d[n, m];
		}
	}
}