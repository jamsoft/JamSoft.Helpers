using System;
using System.Collections.Generic;
using System.Linq;

namespace JamSoft.Helpers.Collections
{
	/// <summary>
	/// Collection extension methods 
	/// </summary>
	public static class CollectionExtensions
	{
		private static readonly Random Random = new Random();
		
		/// <summary>
		/// Randomising a collection of objects within a collection
		/// </summary>
		/// <param name="source">the source collection</param>
		/// <param name="rng">a provided random number generator</param>
		/// <typeparam name="T">the collection object type</typeparam>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng = null)
		{
			T[] elements = source.ToArray();
			for (int i = elements.Length - 1; i >= 0; i--)
			{
				int swapIndex = rng?.Next(i + 1) ?? Random.Next(i + 1);
				yield return elements[swapIndex];
				elements[swapIndex] = elements[i];
			}
		}
	}
}