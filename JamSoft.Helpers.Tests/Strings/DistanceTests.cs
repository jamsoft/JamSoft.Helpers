using JamSoft.Helpers.Strings;
using Xunit;

namespace JamSoft.Helpers.Tests.Strings
{
	public class DistanceTests
	{
		[Fact]
		public void GetHammingDistance_Distance_One()
		{
			var inputOne = "InputString1";
			var inputTwo = "InputString2";

			var distance = Distance.GetHammingDistance(inputOne, inputTwo);
			
			Assert.Equal(1, distance);
		}
		
		[Fact]
		public void GetHammingDistance_Distance_One_Ext()
		{
			var inputOne = "InputString1";
			var inputTwo = "InputString2";
	
			Assert.Equal(1, inputOne.HammingDistanceTo(inputTwo));
		}
		
		[Fact]
		public void GetHammingDistance_Same()
		{
			var inputOne = "InputString1";
			var inputTwo = "InputString1";

			var distance = Distance.GetHammingDistance(inputOne, inputTwo);
			
			Assert.Equal(0, distance);
		}
		
		[Fact]
		public void GetHammingDistance_Different_Length_Strings()
		{
			var inputOne = "InputString";
			var inputTwo = "InputString1";

			var distance = Distance.GetHammingDistance(inputOne, inputTwo);
			
			Assert.Equal(-1, distance);
		}
		
		[Fact]
		public void GetLevenshteinDistance_One()
		{
			var inputOne = "InputString1";
			var inputTwo = "InputString2";

			var distance = Distance.GetLevenshteinDistance(inputOne, inputTwo);
			
			Assert.Equal(1, distance);
		}
		
		[Fact]
		public void GetLevenshteinDistance_Same()
		{
			var inputOne = "InputString1";
			var inputTwo = "InputString1";

			var distance = Distance.GetLevenshteinDistance(inputOne, inputTwo);
			
			Assert.Equal(0, distance);
		}
		
		[Fact]
		public void GetLevenshteinDistance_Different_Lengths()
		{
			var inputOne = "InputString1";
			var inputTwo = "InputString";

			var distance = Distance.GetLevenshteinDistance(inputOne, inputTwo);
			
			Assert.Equal(1, distance);
		}
		
		[Fact]
		public void GetLevenshteinDistance_Distance_One_Ext()
		{
			var inputOne = "InputString1";
			var inputTwo = "InputString2";
	
			Assert.Equal(1, inputOne.LevenshteinDistanceTo(inputTwo));
		}
	}
}