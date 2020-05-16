namespace JamSoft.Helpers.Numbers
{
	public static class MathEx
	{
		public static bool IsEvenNumber(this int value)
		{
			return value % 2 == 0;
		}
		
		public static bool IsEvenNumber(this decimal value)
		{
			return value % 2 == 0.0M;
		}
	}
}