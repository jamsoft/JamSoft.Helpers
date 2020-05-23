﻿using System;
using JamSoft.Helpers.Ui;
using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests
{
    public class UiGraphicsTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public UiGraphicsTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Returns_Not_Null()
        {
            int red = 121;
            int green = 155;
            int blue = 56;

            var hex = Graphics.ToHex(red, green, blue);
            Assert.Equal("#799B38", hex);
            _outputHelper.WriteLine($"R:{red},G:{green},B:{blue} HEX:{hex}");
        }

        [Fact]
        public void Converts_To_Black_For_Invalid()
        {
            int red = 121;
            int green = 155;
            int blue = 256;
            
            var hex = Graphics.ToHex(red, green, blue);
            Assert.Equal("#000000", hex);
            _outputHelper.WriteLine($"R:{red},G:{green},B:{blue} HEX:{hex}");
        }

        [Fact]
        public void Converts_To_Correct_Hex_Value() 
        {
            int red = 255;
            int green = 169;
            int blue = 104;

            var hex = Graphics.ToHex(red, green, blue);
            Assert.Equal("#FFA968", hex);
            _outputHelper.WriteLine($"R:{red},G:{green},B:{blue} HEX:{hex}");
        }

        [Fact]
        public void Converts_To_Correct_Hex_Value_From_Array()
        {
            int red = 255;
            int green = 169;
            int blue = 104;

            var hex = Graphics.ToHex(new[] {red, green, blue});
            Assert.Equal("#FFA968", hex);
            _outputHelper.WriteLine($"R:{red},G:{green},B:{blue} HEX:{hex}");
        }


        [Fact]
        public void Throws_Argument_Exception_When_Array_Too_Small()
        {
            int red = 255;
            int green = 169;

            Assert.Throws<ArgumentException>(() => Graphics.ToHex(new[] { red, green }));
        }

        [Fact]
        public void Hex_Value_Contains_Hash()
        {
            int red = 255;
            int green = 169;
            int blue = 104;

            var hex = Graphics.ToHex(red, green, blue);
            Assert.Contains("#", hex);
        }

        [Fact]
        public void Hex_Value_Does_Not_Contain_Hash()
        {
            int red = 255;
            int green = 169;
            int blue = 104;

            var hex = Graphics.ToHex(red, green, blue, false);
            Assert.DoesNotContain("#", hex);
        }
    }
}