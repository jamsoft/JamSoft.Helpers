using System;
using System.Collections.Generic;
using System.Linq;
using JamSoft.Helpers.Collections;
using Moq;
using Xunit;
// ReSharper disable PossibleMultipleEnumeration

namespace JamSoft.Helpers.Tests.Collections
{
	public class CollectionExtensionMethodTests
	{
		[Fact]
		public void Shuffle_Returns_Same_Size_Array()
		{
			var data = CreateTestList();
			var shuffledData = data.Shuffle();
			
			Assert.True(data.Count() == shuffledData.Count());
		}
		
		[Fact]
		public void Shuffle_Returns_Shuffled_Data()
		{
			var data = CreateTestList();
			var shuffledData = data.Shuffle();
			
			Assert.NotEqual(data, shuffledData);
		}
		
		[Fact]
		public void Shuffle_Returns_Shuffled_Data_Recursive()
		{
			var data = CreateTestList();
			var shuffledData = data.Shuffle();
			
			Assert.NotEqual(data, shuffledData);
			
			var reShuffledData = shuffledData.Shuffle();
			
			Assert.NotEqual(shuffledData, reShuffledData);
		}
		
		[Fact]
		public void Shuffle_Uses_Provided_Randomiser()
		{
			var mockRandom = new Mock<Random>();
			Assert.True(mockRandom.Invocations.Count == 0);
			
			var data = CreateTestList();
			var shuffledData = data.Shuffle(mockRandom.Object);
			
			Assert.NotEqual(data, shuffledData);
			Assert.True(mockRandom.Invocations.Count > 0);
		}

		private IEnumerable<int> CreateTestList()
		{
			return new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
		}
	}
}