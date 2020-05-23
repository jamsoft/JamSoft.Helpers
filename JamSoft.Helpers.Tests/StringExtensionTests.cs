using JamSoft.Helpers.Strings;
using Xunit;

namespace JamSoft.Helpers.Tests
{
    public class StringExtensionTests
    {
        [Fact]
        public void Shortens_Correctly()
        {
            string input = "Thisismylongstringthatneedsshortening";
            Assert.Equal("Thisism...shortening", input.DotShortenString(10, 20));
        }

        [Fact]
        public void Shortens_Correctly_Is_Exactly_Max()
        {
            string input = "Thisismylongstringthatneedsshortening";
            Assert.Equal(20, input.DotShortenString(10, 20).Length);
        }

        [Fact]
        public void When_Input_Length_Less_Than_Max_Returns_Input()
        {
            string input = "aaaaaa";
            Assert.Equal("aaaaaa", input.DotShortenString(5, 10));
        }

        [Fact]
        public void When_Input_Length_Less_Than_End_Returns_Input()
        {
            string input = "aaaaaa";
            Assert.Equal("aaaaaa", input.DotShortenString(10, 5));
        }

        [Fact]
        public void When_Specifying_Max_Less_Than()
        {
            string input = "Thisismylongstringthatneedsshortening";
            Assert.Equal("Thisismylongstringthatneedsshortening", input.DotShortenString(10, 5));
        }

        [Fact]
        public void When_Specifying_Custom_Mid_Pattern()
        {
            string input = "Thisismylongstringthatneedsshortening";
            Assert.Equal("Thisism;;;shortening", input.DotShortenString(10, 20, ";;;"));
        }
    }
}
