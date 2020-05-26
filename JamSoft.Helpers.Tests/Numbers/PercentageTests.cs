using System.Collections.Generic;
using JamSoft.Helpers.Numbers;
using Xunit;

namespace JamSoft.Helpers.Tests.Numbers
{
	public class PercentageTests
	{
		[Theory]
		[InlineData(7900, 12000, 66)]
		[InlineData(7900, 2000, -1)]
		[InlineData(500, 2000, 25)]
		[InlineData(100, 2000, 5)]
		[InlineData(50, 2000, 2)]
		public void Gets_Correct_Integer_Percentage_Of_Total(int value, int total, int expected)
		{
			Assert.Equal(expected, value.IsWhatPercentageOf(total));
		}
		
		[Theory]
		[InlineData(7900, 12000, 65.83333333333333)]
		[InlineData(7900, 2000, -1)]
		[InlineData(500, 2000, 25)]
		[InlineData(100, 2000, 5)]
		[InlineData(50, 2000, 2.5)]
		[InlineData(723123, 12314141, 5.872297547997867)]
		public void Gets_Correct_Double_Percentage_Of_Total(double value, double total, double expected)
		{
			Assert.Equal(expected, value.IsWhatPercentageOf(total));
		}
		
		[Theory]
		[InlineData(7900, 12000, 65.833, 3)]
		[InlineData(723123, 12314141, 5.872, 3)]
		public void Gets_Correct_Double_Percentage_Of_Total_Three_Decimals_Places(double value, double total, double expected, int places)
		{
			Assert.Equal(expected, value.IsWhatPercentageOf(total, places));
		}
		
		[Theory]
		[InlineData(7900, 12000, 65.83, 2)]
		[InlineData(723123, 12314141, 5.87, 2)]
		public void Gets_Correct_Double_Percentage_Of_Total_Two_Decimals_Places(double value, double total, double expected, int places)
		{
			Assert.Equal(expected, value.IsWhatPercentageOf(total, places));
		}
		
		[Theory]
		[InlineData(7900, 12000, 65.83333587646484)]
		[InlineData(7900, 2000, -1)]
		[InlineData(500, 2000, 25)]
		[InlineData(100, 2000, 5)]
		[InlineData(50, 2000, 2.5)]
		[InlineData(723123, 12314141, 5.872297763824463)]
		public void Gets_Correct_Float_Percentage_Of_Total(float value, float total, double expected)
		{
			Assert.Equal(expected, value.IsWhatPercentageOf(total));
		}

        [Theory]
        [InlineData(7900, 12000, 65.83334, 5)]
        [InlineData(7900, 2000.00000, -1, 5)]
        [InlineData(500, 2000.00000, 25, 5)]
        [InlineData(100, 2000.00000, 5, 5)]
        [InlineData(50, 2000.00000, 2.5, 5)]
        [InlineData(723123, 12314141, 5.8723, 5)]
        public void Gets_Correct_Float_Percentage_Of_Total_With_Precision(float value, float total, double expected, int places)
        {
            Assert.Equal(expected, value.IsWhatPercentageOf(total, places));
        }

		public static IEnumerable<object[]> Data =>
			new List<object[]>
			{
				new object[] { 7900M, 12000M, 65.833333333333333333333333330M },
				new object[] { 7900M, 2000M, -1M },
				new object[] { 500M, 2000, 25M },
				new object[] { 100M, 2000M, 5M },
				new object[] { 50M, 2000M, 2.5M },
				new object[] { 723123M, 12314141M, 5.8722975479978668426811094700M },
			};

		[Theory]
		[MemberData(nameof(Data))]
		public void Gets_Correct_Decimal_Percentage_Of_Total(decimal value, decimal total, decimal expected)
		{
			Assert.Equal(expected, value.IsWhatPercentageOf(total));
		}
		
		public static IEnumerable<object[]> DataWithPrecision =>
			new List<object[]>
			{
				new object[] { 7900M, 12000M, 65.83M, 2 },
				new object[] { 7900M, 2000M, -1M, 2 },
				new object[] { 500M, 2000, 25M, 2 },
				new object[] { 100M, 2000M, 5M, 2 },
				new object[] { 50M, 2000M, 2.5M, 2 },
				new object[] { 723123M, 12314141M, 5.87M, 2 },
			};

		[Theory]
		[MemberData(nameof(DataWithPrecision))]
		public void Gets_Correct_Decimal_Percentage_Of_Total_Precision(decimal value, decimal total, decimal expected, int places)
		{
			Assert.Equal(expected, value.IsWhatPercentageOf(total, places));
		}
	}
}