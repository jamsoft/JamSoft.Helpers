using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JamSoft.Helpers.Ui;
using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Ui
{
    public class DirtyMonitoringTests
    {
	    private readonly ITestOutputHelper _outputHelper;

	    public DirtyMonitoringTests(ITestOutputHelper outputHelper)
	    {
		    _outputHelper = outputHelper;
	    }
	    
	    [Fact]
	    public void Validate_Null_Object()
	    {
		    PersonViewModel p = null;
		    // ReSharper disable once ExpressionIsAlwaysNull
		    var obj = IsDirtyValidator.Validate(p);
		    Assert.Null(obj);
	    }
	    
	    [Fact]
	    public void Validate_Null_Object_Properties()
	    {
		    PersonViewModel p = null;
		    // ReSharper disable once ExpressionIsAlwaysNull
		    var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
		    Assert.Empty(props);
		    Assert.Empty(fields);
	    }
	    
	    [Fact]
	    public void ValidateProperties_UnValidated_Object()
	    {
		    PersonViewModel p = new PersonViewModel
		    {
			    DisplayName = "DisplayName"
		    };

		    // ReSharper disable once ExpressionIsAlwaysNull
		    var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
		    Assert.Empty(props);
		    Assert.Empty(fields);
	    }
	    
	    [Fact]
	    public void ValidateProperties_UnValidated_Object_NonNull_Hash_String_Property()
	    {
		    PersonViewModel p = new PersonViewModel
		    {
			    Hash = "DisplayName"
		    };

		    // ReSharper disable once ExpressionIsAlwaysNull
		    var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
		    Assert.Empty(props);
		    Assert.Empty(fields);
	    }
	    
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
        public void IsDirty_Field_Only()
        {
	        var p = new FieldOnlyViewModel()
	        {
		        Field = "Tom"
	        };

	        IsDirtyValidator.Validate(p, true);
	        p.Field = "New";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);

	        var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
	        Assert.True(props.Length == 0);
	        Assert.True(fields.Length == 1);
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
        
        [Fact]
        public void IsDirty_Stop_Tracking_Reset_False()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom",
		        DisplayName = "Original"
	        };

	        IsDirtyValidator.Validate(p, true);
	        Assert.False(IsDirtyValidator.Validate(p).IsDirty);
	        
	        p.DisplayName = "New";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);
	        Assert.NotNull(p.Hash);
	        
	        IsDirtyValidator.StopTrackingObject(p);
	        Assert.False(p.IsDirty);
	        Assert.Null(p.Hash);
	        
	        Assert.False(IsDirtyValidator.Validate(p).IsDirty);
	        Assert.NotNull(p.Hash);
        }
        
        [Fact]
        public void IsDirty_Stop_Tracking_Null()
        {
	        PersonViewModel p = null;
	        // ReSharper disable once ExpressionIsAlwaysNull
	        IsDirtyValidator.StopTrackingObject(p);
	        Assert.Null(p);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_SingleInstance_Props()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom",
		        DisplayName = "Original",
		        Field = "test"
	        };

	        IsDirtyValidator.Validate(p, true);
	        p.DisplayName = "New";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);

	        var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
	        
	        Assert.True(props.Length == 1);
	        Assert.True(fields.Length == 0);
	        Assert.Equal("DisplayName", props[0].Name);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_SingleInstance_Tracked_Multiple_Property_Changes()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom",
		        DisplayName = "Original",
		        Field = "test"
	        };

	        IsDirtyValidator.Validate(p, true);
	        
	        p.DisplayName = "New";
	        p.Address = "Some New value";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);

	        var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
	        
	        Assert.True(props.Length == 2);
	        Assert.True(fields.Length == 0);
	        Assert.Equal("DisplayName", props[0].Name);
	        Assert.Equal("Address", props[1].Name);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_SingleInstance_Tracked_Field_Changes()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Tom",
		        DisplayName = "Original",
		        Field = "test"
	        };
	        
	        Assert.False(IsDirtyValidator.Validate(p, true).IsDirty);

	        p.Field = "new";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);
	        
	        var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
	        
	        Assert.True(props.Length == 0);
	        Assert.True(fields.Length == 1);
	        Assert.Equal("Field", fields[0].Name);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_MultipleInstance_Tracked_Property_Changes()
        {
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
	        
	        Assert.False(IsDirtyValidator.Validate(p1, true).IsDirty);
	        Assert.False(IsDirtyValidator.Validate(p2, true).IsDirty);

	        p2.DisplayName = "Person2-DisplayName-new";
       
	        var (person1Props, person1Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p1);
	        var (person2Props, person2Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p2);
	        
	        Assert.True(person1Props.Length == 0);
	        Assert.True(person1Fields.Length == 0);
	        
	        Assert.True(person2Props.Length == 1);
	        Assert.True(person2Fields.Length == 0);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_MultipleInstance_Tracked_Field_Changes()
        {
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
	        
	        Assert.False(IsDirtyValidator.Validate(p1, true).IsDirty);
	        Assert.NotNull(p1.Hash);
	        Assert.False(IsDirtyValidator.Validate(p2, true).IsDirty);
	        Assert.NotNull(p2.Hash);

	        p1.Field = "Person1-Field-new";
       
	        var (person1Props, person1Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p1);
	        var (person2Props, person2Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p2);
	        
	        Assert.True(person1Props.Length == 0);
	        Assert.True(person1Fields.Length == 1);
	        
	        Assert.True(person2Props.Length == 0);
	        Assert.True(person2Fields.Length == 0);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_MultipleInstance_Tracked_ComplexObject_Property_Changes()
        {
	        var p1 = new PersonViewModel
	        {
		        Name = "Person1",
		        DisplayName = "Person1-DisplayName",
		        Field = "Person1-Field",
		        Complex = new MyComplexObject { ComplexProperty1 = "Person1-Complex1", ComplexProperty2 = "Person1-Complex2" }
	        };
	        
	        var p2 = new PersonViewModel
	        {
		        Name = "Person2",
		        DisplayName = "Person2-DisplayName",
		        Field = "Person2-Field",
		        Complex = new MyComplexObject { ComplexProperty1 = "Person2-Complex1", ComplexProperty2 = "Person2-Complex2" }
	        };
	        
	        Assert.False(IsDirtyValidator.Validate(p1, true).IsDirty);
	        Assert.NotNull(p1.Hash);
	        Assert.False(IsDirtyValidator.Validate(p2, true).IsDirty);
	        Assert.NotNull(p2.Hash);
     
	        var (person1Props, person1Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p1);
	        var (person2Props, person2Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p2);
	        
	        Assert.True(person1Props.Length == 0);
	        Assert.True(person1Fields.Length == 0);
	        
	        Assert.True(person2Props.Length == 0);
	        Assert.True(person2Fields.Length == 0);

	        p2.Complex.ComplexProperty1 = "new";
	        
	        Assert.True(IsDirtyValidator.Validate(p2).IsDirty);
	        
	        var (person2PropsRevalidated, person2Fields2Revalidated) = IsDirtyValidator.ValidatePropertiesAndFields(p2);
	        
	        Assert.True(person2PropsRevalidated.Length == 1);
	        Assert.True(person2Fields2Revalidated.Length == 0);
	        
	        Assert.Equal("Complex", person2PropsRevalidated[0].Name);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_MultipleInstance_Tracked_ComplexObject_Field_Changes()
        {
	        var p1 = new PersonViewModel
	        {
		        Name = "Person1",
		        DisplayName = "Person1-DisplayName",
		        Field = "Person1-Field",
		        ComplexField = new MyComplexObject { ComplexProperty1 = "Person1-Complex1", ComplexProperty2 = "Person1-Complex2" }
	        };
	        
	        var p2 = new PersonViewModel
	        {
		        Name = "Person2",
		        DisplayName = "Person2-DisplayName",
		        Field = "Person2-Field",
		        ComplexField = new MyComplexObject { ComplexProperty1 = "Person2-Complex1", ComplexProperty2 = "Person2-Complex2" }
	        };
	        
	        Assert.False(IsDirtyValidator.Validate(p1, true).IsDirty);
	        Assert.NotNull(p1.Hash);
	        Assert.False(IsDirtyValidator.Validate(p2, true).IsDirty);
	        Assert.NotNull(p2.Hash);
     
	        var (person1Props, person1Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p1);
	        var (person2Props, person2Fields) = IsDirtyValidator.ValidatePropertiesAndFields(p2);
	        
	        Assert.True(person1Props.Length == 0);
	        Assert.True(person1Fields.Length == 0);
	        
	        Assert.True(person2Props.Length == 0);
	        Assert.True(person2Fields.Length == 0);

	        p2.ComplexField.ComplexProperty1 = "new";
	        
	        Assert.False(IsDirtyValidator.Validate(p1).IsDirty);
	        Assert.True(IsDirtyValidator.Validate(p2).IsDirty);
	        
	        var (person2PropsRevalidated, person2Fields2Revalidated) = IsDirtyValidator.ValidatePropertiesAndFields(p2);
	        
	        Assert.True(person2PropsRevalidated.Length == 0);
	        Assert.True(person2Fields2Revalidated.Length == 1);
	        
	        Assert.Equal("ComplexField", person2Fields2Revalidated[0].Name);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_SingleInstance_Reset_Tracking_Changes()
        {
	        var p = new PersonViewModel
	        {
		        Name = "Name",
		        DisplayName = "DisplayName",
		        Field = "Field"
	        };
	        
	        Assert.False(IsDirtyValidator.Validate(p, true).IsDirty);

	        p.DisplayName = "DisplayName-new";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);
	        
	        var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
	        
	        Assert.True(props.Length == 1);
	        Assert.True(fields.Length == 0);
	        Assert.Equal("DisplayName", props[0].Name);
	        
	        Assert.False(IsDirtyValidator.Validate(p, true).IsDirty);
        }
        
        [Fact]
        public void IsDirty_Multi_Iterations()
        {
	        int iterations = 100000;
	        var stopWatch = new Stopwatch();
	        stopWatch.Start();
	        
	        for (int i = 0; i < iterations; i++)
	        {
		        var p = new PersonViewModel
		        {
			        Name = "Tom",
			        DisplayName = "DisplayName",
			        Field = "Field",
			        Complex = new MyComplexObject { ComplexProperty1 = "SomeValue", ComplexProperty2 = "SomeOther Value" }
		        };
		        
		        Assert.False(IsDirtyValidator.Validate(p, true).IsDirty);
	        }
	        
	        stopWatch.Stop();

	        _outputHelper.WriteLine($"Validated {iterations} in {stopWatch.Elapsed}");
        }

        [Fact]
        public void Multi_Threading_Test()
        {
	        string hash1 = String.Empty;
	        string hash2 = String.Empty;

	        var t1 = Task.Run(() =>
	        {
		        var p1 = new PersonViewModel
		        {
			        Name = "Name",
			        DisplayName = "DisplayName",
			        Field = "Field"
		        };
		        IsDirtyValidator.Validate(p1, true);
		        hash1 = p1.Hash.Split('|')[1];
	        });
	        
	        Task t2 = Task.Run(() =>
	        {
		        var p2 = new PersonViewModel
		        {
			        Name = "Name",
			        DisplayName = "DisplayName",
			        Field = "Field"
		        };
		        IsDirtyValidator.Validate(p2, true);
		        hash2 = p2.Hash.Split('|')[1];
	        });
	        
	        Task t = Task.WhenAll(t1, t2);
	        t.Wait();

	         _outputHelper.WriteLine($"hash1: {hash1}");
	         _outputHelper.WriteLine($"hash2: {hash2}");
	         Assert.Equal(hash1, hash2);
        }
        
        [Fact]
        public void ValidatePropertiesAndFields_Ignores_Hash_Props_Even_When_Attributed()
        {
	        var p = new HashPropertyMonitoringViewModel()
	        {
		        MyProp = "MyProp"
	        };

	        IsDirtyValidator.Validate(p, true);
	        p.MyProp = "New";
	        
	        Assert.True(IsDirtyValidator.Validate(p).IsDirty);

	        var (props, fields) = IsDirtyValidator.ValidatePropertiesAndFields(p);
	        
	        Assert.True(props.Length == 1);
	        Assert.True(fields.Length == 0);
	        Assert.Equal("MyProp", props[0].Name);
        }
    }
}