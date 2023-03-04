using System.Globalization;
using Avalonia.Media;
using JamSoft.Helpers.AvaloniaUI.Converters;
using Xunit;
using Color = System.Drawing.Color;

namespace JamSoft.Helpers.Tests.Converters;

public class IsDirtyColorConverterTests
{
    [Fact]
    public void Default_Values_Null()
    {
        var sut = CreateSut();
        SolidColorBrush val = (SolidColorBrush)sut.Convert(null, typeof(IBrush), null, new CultureInfo("en-GB"));
        Assert.NotNull(val);
        Assert.Equal("#ffb8ffb8", val.ToString());
    }
    
    [Fact]
    public void Default_Values_True()
    {
        var sut = CreateSut();
        SolidColorBrush val = (SolidColorBrush)sut.Convert(true, typeof(IBrush), null, new CultureInfo("en-GB"));
        Assert.NotNull(val);
        Assert.Equal("#ffff8d8d", val.ToString());
    }
    
    [Fact]
    public void Default_Values_False()
    {
        var sut = CreateSut();
        SolidColorBrush val = (SolidColorBrush)sut.Convert(false, typeof(IBrush), null, new CultureInfo("en-GB"));
        Assert.NotNull(val);
        Assert.Equal("#ffb8ffb8", val.ToString());
    }
    
    [Fact]
    public void Custom_Values_False()
    {
        var sut = CreateSut();
        sut.FalseColor = new SolidColorBrush(Avalonia.Media.Color.FromArgb(255, 0, 0, 0));
        SolidColorBrush val = (SolidColorBrush)sut.Convert(false, typeof(IBrush), null, new CultureInfo("en-GB"));
        Assert.NotNull(val);
        Assert.Equal("Black", val.ToString());
    }

    [Fact]
    public void Custom_Values_True()
    {
        var sut = CreateSut();
        sut.TrueColor = new SolidColorBrush(Avalonia.Media.Color.FromArgb(255, 0, 0, 0));
        SolidColorBrush val = (SolidColorBrush)sut.Convert(true, typeof(IBrush), null, new CultureInfo("en-GB"));
        Assert.NotNull(val);
        Assert.Equal("Black", val.ToString());
    }
    
    [Fact]
    public void Default_Convert_Back_Null()
    {
        var sut = CreateSut();
        Assert.Null(sut.ConvertBack(true, typeof(IBrush), null, new CultureInfo("en-GB")));
    }
    
    private IsDirtyColorConverter CreateSut()
    {
        return new IsDirtyColorConverter();
    }
}