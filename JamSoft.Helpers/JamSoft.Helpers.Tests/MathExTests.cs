using JamSoft.Helpers.Numbers;
using Xunit;

namespace JamSoft.Helpers.Tests
{
	public class MathExTests
	{
		[Theory]
		[InlineData(2, true)]
		[InlineData(3, false)]
		[InlineData(9999, false)]
		[InlineData(1000, true)]
		public void Integer_IsEven(int value, bool expected)
		{
			Assert.Equal(expected, value.IsEvenNumber());
		}
		
		[Theory]
		[InlineData(2, true)]
		[InlineData(3, false)]
		[InlineData(9999, false)]
		[InlineData(1000, true)]
		[InlineData(1000.1, false)]
		[InlineData(1000.2, false)]
		public void Decimal_IsEven(decimal value, bool expected)
		{
			Assert.Equal(expected, value.IsEvenNumber());
		}
	}
}