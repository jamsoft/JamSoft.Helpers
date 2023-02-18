using JamSoft.Helpers.Ui;
using Xunit;

namespace JamSoft.Helpers.Tests.Ui
{
    public class DirtyMonitoringTests
    {
        [Fact]
        public void IsDirty_Initial_False()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom"
	        };

	        Assert.Null(p.Hash);
	        Assert.False(IsDirtyValidator.Validate(p).IsDirty);
	        Assert.NotNull(p.Hash);
        }
        
        [Fact]
        public void IsDirty_Unmonitored_Change_False()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom"
	        };

	        IsDirtyValidator.Validate(p);
	        p.Name = "Dick";
	        
	        Assert.False(IsDirtyValidator.Validate(p).IsDirty);
        }
        
        [Fact]
        public void IsDirty_Monitored_Change_True()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom"
	        };

	        IsDirtyValidator.Validate(p);
	        p.DisplayName = "New";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);
        }
        
        [Fact]
        public void IsDirty_Monitored_Changed_Back_To_Original_False()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom",
		        DisplayName = "Original"
	        };

	        IsDirtyValidator.Validate(p);
	        p.DisplayName = "New";
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);
	        p.DisplayName = "Original";
	        Assert.False(IsDirtyValidator.Validate(p).IsDirty);
        }
        
        [Fact]
        public void IsDirty_Monitored_Fields_Changed_Back_To_Original_False()
        {
	        var p = new PersonViewModel
	        {
		        Field = "Original",
	        };

	        IsDirtyValidator.Validate(p);
	        p.Field = "New";
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);
	        p.Field = "Original";
	        Assert.False(IsDirtyValidator.Validate(p).IsDirty);
        }
        
        [Fact]
        public void IsDirty_Monitored_Reset_False()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom",
		        DisplayName = "Original"
	        };

	        IsDirtyValidator.Validate(p);
	        p.DisplayName = "New";
	        Assert.False(IsDirtyValidator.Validate(p, true).IsDirty);
	        Assert.NotNull(p.Hash);
        }
    }
    
    public class PersonViewModel : IDirtyMonitoring
    {
        public string Name { get; set; }

        [IsDirtyMonitoring]
        public string Field;
    
        [IsDirtyMonitoring]
        public string DisplayName { get; set; }

        public bool IsDirty { get; set; }
        
        public string Hash { get; set; }
    }
}