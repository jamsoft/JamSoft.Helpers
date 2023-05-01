using System.Linq;
using JamSoft.Helpers.Ui;
using Xunit;

namespace JamSoft.Helpers.Tests.Ui;

public class CorrectNumberOfHashesTest
{
    [Fact]
    public void ValidatePropertiesAndFields_Correct_Number_Of_Hashes()
    {
        var isDirtyValidator = DirtyValidatorFactory.Create();
		    
        var p1 = new PersonViewModel
        {
            Name = "Person1",
            DisplayName = "Person1-DisplayName",
            Field = "Person1-Field"
        };
	        
        var p2 = new PersonViewModel
        {
            Name = "Person2",
            DisplayName = "Person2-DisplayName",
            Field = "Person2-Field"
        };
	        
        Assert.False(isDirtyValidator.Validate(p1, true).IsDirty);
        Assert.False(isDirtyValidator.Validate(p2, true).IsDirty);
	        
        Assert.NotNull(isDirtyValidator.ObjectValueHashStore);
        Assert.Equal(2, isDirtyValidator.ObjectValueHashStore.Count);
        Assert.Equal(5, isDirtyValidator.ObjectValueHashStore.First().Value.Count);
        Assert.Equal(5, isDirtyValidator.ObjectValueHashStore.Last().Value.Count);
    }	    
}