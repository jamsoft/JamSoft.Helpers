using System.Security;
using JamSoft.Helpers.Strings;
using Xunit;

namespace JamSoft.Helpers.Tests.Strings
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
        public void Secure_String_Compare_When_Same_Returns_True()
        {
            // #dontdothisinproduction
            var input1 = new SecureString();
            foreach (char c in "QqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!#Xzo4nUk0bEOxI1x$!FWrC0Sf71RpN7Y@TSYWA16Q@cxZpOY5RUR!1IhHiBlHM6cW7")
            {
                input1.AppendChar(c);
            }

            // #dontdothisinproduction
            var input2 = new SecureString();
            foreach (char c in "QqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!#Xzo4nUk0bEOxI1x$!FWrC0Sf71RpN7Y@TSYWA16Q@cxZpOY5RUR!1IhHiBlHM6cW7")
            {
                input2.AppendChar(c);
            }

            Assert.True(input1.IsExactlySameAs(input2));
        }

        [Fact]
        public void Secure_String_Compare_When_Different_Returns_False()
        {
            // #dontdothisinproduction
            var input1 = new SecureString();
            foreach (char c in "QqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!#Xzo4nUk0bEOxI1x$!FWrC0Sf71RpN7Y@TSYWA16Q@cxZpOY5RUR!1IhHiBlHM6cW7".ToCharArray())
            {
                input1.AppendChar(c);
            }

            // #dontdothisinproduction
            var input2 = new SecureString();
            foreach (char c in "qqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!#Xzo4nUk0bEOxI1x$!FWrC0Sf71RpN7Y@TSYWA16Q@cxZpOY5RUR!1IhHiBlHM6cW7".ToCharArray())
            {
                input2.AppendChar(c);
            }

            Assert.False(input1.IsExactlySameAs(input2));
        }
    }
}
