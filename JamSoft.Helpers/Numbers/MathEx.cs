namespace JamSoft.Helpers.Numbers
{
	/// <summary>
	/// A collection of basic Maths operations
	/// </summary>
	public static class MathEx
	{
		/// <summary>
		/// Determines whether [is even number].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if [is even number] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsEvenNumber(this int value)
		{
			return value % 2 == 0;
		}

		/// <summary>
		/// Determines whether [is even number].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if [is even number] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsEvenNumber(this decimal value)
		{
			return value % 2 == 0.0M;
		}
	}
}