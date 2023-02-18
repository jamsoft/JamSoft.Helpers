using System.Security;
using JamSoft.Helpers.Strings;
using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Strings
{
    public class StringExtensionTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public StringExtensionTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

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

        [Fact]
        public void String_Compare_When_Different_Case_Returns_False()
        {
            string input = "string1";
            string pattern = "strinG1";

            Assert.False(input.IsExactlySameAs(pattern));
        }

        [Fact]
        public void String_Compare_When_Same_Dif_Culture_Returns_False()
        {
            string input = "encyclopedia";
            string pattern = "encyclopædia";

            Assert.False(input.IsExactlySameAs(pattern));
        }

        [Fact]
        public void String_Compare_When_Same_Returns_True()
        {
            string input1 = "QqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!#Xzo4nUk0bEOxI1x$!FWrC0Sf71RpN7Y@TSYWA16Q@cxZpOY5RUR!1IhHiBlHM6cW7";
            string input2 = "QqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!#Xzo4nUk0bEOxI1x$!FWrC0Sf71RpN7Y@TSYWA16Q@cxZpOY5RUR!1IhHiBlHM6cW7";

            Assert.True(input1.IsExactlySameAs(input2));
        }

        [Fact]
        public void String_Removes_All_MultiSpaces()
        {
            string expected = " QqN#c1rFz9 ^Xl8rAO! #Xzo4nUk0bEOxI1x$ !FWrY@ TSYWA16Q@ cxZpOY5RUR! 1IhHiBlHM6 cW7";
            string input = "    QqN#c1rFz9  ^Xl8rAO!  #Xzo4nUk0bEOxI1x$   !FWrY@   TSYWA16Q@     cxZpOY5RUR!       1IhHiBlHM6      cW7";

            Assert.Equal(expected, input.RemoveAllMultiSpace());
        }

        [Fact]
        public void String_Removes_All_MultiSpaces_Using_Pattern()
        {
            string expected = "--QqNK8f#X4t7lZYomTC#c1rFz9--^Xl8rAO!--#Xzo4nUk0bEOxI1x$--!FWrC0Sf71RpN7Y@--TSYWA16Q@--cxZpOY5RUR!--1IhHiBlHM6--cW7";
            string input = "    QqNK8f#X4t7lZYomTC#c1rFz9  ^Xl8rAO!  #Xzo4nUk0bEOxI1x$   !FWrC0Sf71RpN7Y@   TSYWA16Q@     cxZpOY5RUR!       1IhHiBlHM6      cW7";

            Assert.Equal(expected, input.RemoveAllMultiSpace("--"));
            _outputHelper.WriteLine(input.RemoveAllMultiSpace("--"));
        }

        [Fact]
        public void String_Removes_All_MultiSpaces_And_Trim()
        {
            string expected = "This has too many spaces";
            string input = "  This  has    too  many  spaces   ";

            Assert.Equal(expected, input.RemoveAllMultiSpace(trim:true));
            _outputHelper.WriteLine(input.RemoveAllMultiSpace(trim: true));
        }

        [Fact]
        public void String_Removes_All_MultiSpaces_And_Pattern_And_Trim()
        {
            string expected = "This--has--too--many--spaces";
            string input = "  This  has    too  many  spaces   ";

            Assert.Equal(expected, input.RemoveAllMultiSpace("--", true));
            _outputHelper.WriteLine(input.RemoveAllMultiSpace("--", true));
        }
    }
}
