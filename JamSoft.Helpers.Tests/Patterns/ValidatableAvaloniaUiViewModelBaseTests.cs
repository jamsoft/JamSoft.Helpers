using Xunit;

namespace JamSoft.Helpers.Tests.Patterns;

public class ValidatableAvaloniaUiViewModelBaseTests
{
    [Fact]
    public void Property_Changed_IsBusy()
    {
        var sut = CreateSut();
        Assert.PropertyChanged(sut, nameof(sut.IsBusy), () => sut.IsBusy = true);
    }
    
    [Fact]
    public void IsDirty()
    {
        var sut = CreateSut();
        sut.MonitoredValue = "Original";
        sut.Validate();
        sut.MonitoredValue = "Changed";
        sut.Validate();
        
        Assert.True(sut.IsDirty);
        Assert.NotNull(sut.Hash);
    }
    
    [Fact]
    public void Changes_Populated()
    {
        var sut = CreateSut();
        sut.MonitoredValue = "Original";
        sut.StartValidateAndTrack();
        sut.MonitoredValue = "Changed";
        sut.GetChanged();
        
        Assert.NotNull(sut.ChangedFields);
        Assert.Empty(sut.ChangedFields);
        Assert.NotNull(sut.ChangedProperties);
        Assert.NotEmpty(sut.ChangedProperties);
    }
    
    [Fact]
    public void StopTracking()
    {
        var sut = CreateSut();
        sut.MonitoredValue = "Original";
        sut.StartValidateAndTrack();
        sut.MonitoredValue = "Changed";
        sut.GetChanged();
        
        Assert.NotNull(sut.ChangedFields);
        Assert.Empty(sut.ChangedFields);
        Assert.NotNull(sut.ChangedProperties);
        Assert.NotEmpty(sut.ChangedProperties);
        
        sut.StopTracking();
        Assert.Null(sut.ChangedProperties);
        Assert.Null(sut.ChangedFields);
    }
    
    private MyValidatableAvaloniaTestViewModel CreateSut()
    {
        return new MyValidatableAvaloniaTestViewModel();
    }
}