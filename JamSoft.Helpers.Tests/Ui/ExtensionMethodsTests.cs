using Xunit;
using JamSoft.Helpers.Ui;

namespace JamSoft.Helpers.Tests.Ui
{
	public class ExtensionMethodsTests
	{
		[Fact]
		public void ConvertLongToBytes()
		{
			long input = 10;
			Assert.Equal("10 b", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertLongToKiloBytes()
		{
			long input = 10000;
			Assert.Equal("9.77 Kb", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertLongToMegaBytes()
		{
			long input = 10000000;
			Assert.Equal("9.54 Mb", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertLongToGigaBytes()
		{
			long input = 10000000000;
			Assert.Equal("9.31 Gb", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertLongToTeraBytes()
		{
			long input = 10000000000000;
			Assert.Equal("9.09 Tb", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertLongToPetaBytes()
		{
			long input = 10000000000000000;
			Assert.Equal("8.88 Pb", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertLongToExaBytes()
		{
			long input = 2000000000000000000;
			Assert.Equal("1.73 Eb", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertIntToBytes()
		{
			int input = 10;
			Assert.Equal("10 b", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertIntToKiloBytes()
		{
			int input = 10000;
			Assert.Equal("9.77 Kb", input.ToHumanReadable());
		}
		
		[Fact]
		public void ConvertIntToMegaBytes()
		{
			int input = 10000000;
			Assert.Equal("9.54 Mb", input.ToHumanReadable());
		}
	}
}