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
			var isDirtyValidator = CreateSut();
			
			PersonViewModel p = null;
			// ReSharper disable once ExpressionIsAlwaysNull
			var obj = isDirtyValidator.Validate(p);
			Assert.Null(obj);
		}

		[Fact]
		public void Validate_Null_Object_Properties()
		{
			var isDirtyValidator = CreateSut();
			
			PersonViewModel p = null;
			// ReSharper disable once ExpressionIsAlwaysNull
			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);
			Assert.Empty(props);
			Assert.Empty(fields);
		}

		[Fact]
		public void ValidateProperties_UnValidated_Object()
		{
			var isDirtyValidator = CreateSut();
			
			PersonViewModel p = new PersonViewModel
			{
				DisplayName = "DisplayName"
			};

			// ReSharper disable once ExpressionIsAlwaysNull
			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);
			Assert.Empty(props);
			Assert.Empty(fields);
		}

		[Fact]
		public void ValidateProperties_UnValidated_Object_NonNull_Hash_String_Property()
		{
			var isDirtyValidator = CreateSut();
			
			PersonViewModel p = new PersonViewModel
			{
				Hash = "DisplayName"
			};

			// ReSharper disable once ExpressionIsAlwaysNull
			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);
			Assert.Empty(props);
			Assert.Empty(fields);
		}

		[Fact]
		public void IsDirty_Initial_False()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom"
			};

			Assert.Null(p.Hash);
			Assert.False(isDirtyValidator.Validate(p).IsDirty);
			Assert.NotNull(p.Hash);
		}

		[Fact]
		public void IsDirty_Unmonitored_Change_False()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom"
			};

			isDirtyValidator.Validate(p);
			p.Name = "Dick";

			Assert.False(isDirtyValidator.Validate(p).IsDirty);
		}

		[Fact]
		public void IsDirty_Monitored_Change_True()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom"
			};

			isDirtyValidator.Validate(p);
			p.DisplayName = "New";

			Assert.True(isDirtyValidator.Validate(p).IsDirty);
		}

		[Fact]
		public void IsDirty_Field_Only()
		{
			var isDirtyValidator = CreateSut();
			var p = new FieldOnlyViewModel()
			{
				Field = "Tom"
			};

			isDirtyValidator.Validate(p, true);
			p.Field = "New";

			Assert.True(isDirtyValidator.Validate(p).IsDirty);

			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);
			Assert.True(props.Length == 0);
			Assert.True(fields.Length == 1);
		}

		[Fact]
		public void IsDirty_Monitored_Changed_Back_To_Original_False()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom",
				DisplayName = "Original"
			};

			isDirtyValidator.Validate(p);
			p.DisplayName = "New";
			Assert.True(isDirtyValidator.Validate(p).IsDirty);
			p.DisplayName = "Original";
			Assert.False(isDirtyValidator.Validate(p).IsDirty);
		}

		[Fact]
		public void IsDirty_Monitored_Fields_Changed_Back_To_Original_False()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Field = "Original",
			};

			isDirtyValidator.Validate(p);
			p.Field = "New";
			Assert.True(isDirtyValidator.Validate(p).IsDirty);
			p.Field = "Original";
			Assert.False(isDirtyValidator.Validate(p).IsDirty);
		}

		[Fact]
		public void IsDirty_Monitored_Reset_False()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom",
				DisplayName = "Original"
			};

			isDirtyValidator.Validate(p);
			p.DisplayName = "New";
			Assert.False(isDirtyValidator.Validate(p, true).IsDirty);
			Assert.NotNull(p.Hash);
		}

		[Fact]
		public void IsDirty_Stop_Tracking_Reset_False()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom",
				DisplayName = "Original"
			};

			isDirtyValidator.Validate(p, true);
			Assert.False(isDirtyValidator.Validate(p).IsDirty);

			p.DisplayName = "New";

			Assert.True(isDirtyValidator.Validate(p).IsDirty);
			Assert.NotNull(p.Hash);

			isDirtyValidator.StopTrackingObject(p);
			Assert.False(p.IsDirty);
			Assert.Null(p.Hash);

			Assert.False(isDirtyValidator.Validate(p).IsDirty);
			Assert.NotNull(p.Hash);
		}

		[Fact]
		public void IsDirty_Stop_Tracking_Null()
		{
			var isDirtyValidator = CreateSut();
			PersonViewModel p = null;
			// ReSharper disable once ExpressionIsAlwaysNull
			isDirtyValidator.StopTrackingObject(p);
			Assert.Null(p);
		}

		[Fact]
		public void ValidatePropertiesAndFields_SingleInstance_Props()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom",
				DisplayName = "Original",
				Field = "test"
			};

			isDirtyValidator.Validate(p, true);
			p.DisplayName = "New";

			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);

			Assert.True(p.IsDirty);

			Assert.True(props.Length == 1);
			Assert.True(fields.Length == 0);
			Assert.Equal("DisplayName", props[0].Name);
		}

		[Fact]
		public void ValidatePropertiesAndFields_SingleInstance_Tracked_Multiple_Property_Changes()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom",
				DisplayName = "Original",
				Field = "test"
			};

			isDirtyValidator.Validate(p, true);

			p.DisplayName = "New";
			p.Address = "Some New value";

			Assert.True(isDirtyValidator.Validate(p).IsDirty);

			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);

			Assert.True(props.Length == 2);
			Assert.True(fields.Length == 0);
			Assert.Equal("DisplayName", props[0].Name);
			Assert.Equal("Address", props[1].Name);
		}

		[Fact]
		public void ValidatePropertiesAndFields_SingleInstance_Tracked_Field_Changes()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Tom",
				DisplayName = "Original",
				Field = "test"
			};

			Assert.False(isDirtyValidator.Validate(p, true).IsDirty);

			p.Field = "new";

			Assert.True(isDirtyValidator.Validate(p).IsDirty);

			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);

			Assert.True(props.Length == 0);
			Assert.True(fields.Length == 1);
			Assert.Equal("Field", fields[0].Name);
		}

		[Fact]
		public void ValidatePropertiesAndFields_MultipleInstance_Tracked_Property_Changes()
		{
			var isDirtyValidator = CreateSut();
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

			p2.DisplayName = "Person2-DisplayName-new";

			var (person1Props, person1Fields) = isDirtyValidator.ValidatePropertiesAndFields(p1);
			var (person2Props, person2Fields) = isDirtyValidator.ValidatePropertiesAndFields(p2);

			Assert.True(person1Props.Length == 0);
			Assert.True(person1Fields.Length == 0);

			Assert.True(person2Props.Length == 1);
			Assert.True(person2Fields.Length == 0);
		}

		[Fact]
		public void ValidatePropertiesAndFields_MultipleInstance_Tracked_Field_Changes()
		{
			var isDirtyValidator = CreateSut();
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
			Assert.NotNull(p1.Hash);
			Assert.False(isDirtyValidator.Validate(p2, true).IsDirty);
			Assert.NotNull(p2.Hash);

			p1.Field = "Person1-Field-new";

			var (person1Props, person1Fields) = isDirtyValidator.ValidatePropertiesAndFields(p1);
			var (person2Props, person2Fields) = isDirtyValidator.ValidatePropertiesAndFields(p2);

			Assert.True(person1Props.Length == 0);
			Assert.True(person1Fields.Length == 1);

			Assert.True(person2Props.Length == 0);
			Assert.True(person2Fields.Length == 0);
		}

		[Fact]
		public void ValidatePropertiesAndFields_MultipleInstance_Tracked_ComplexObject_Property_Changes()
		{
			var isDirtyValidator = CreateSut();
			var p1 = new PersonViewModel
			{
				Name = "Person1",
				DisplayName = "Person1-DisplayName",
				Field = "Person1-Field",
				Complex = new MyComplexObject
					{ ComplexProperty1 = "Person1-Complex1", ComplexProperty2 = "Person1-Complex2" }
			};

			var p2 = new PersonViewModel
			{
				Name = "Person2",
				DisplayName = "Person2-DisplayName",
				Field = "Person2-Field",
				Complex = new MyComplexObject
					{ ComplexProperty1 = "Person2-Complex1", ComplexProperty2 = "Person2-Complex2" }
			};

			Assert.False(isDirtyValidator.Validate(p1, true).IsDirty);
			Assert.NotNull(p1.Hash);
			Assert.False(isDirtyValidator.Validate(p2, true).IsDirty);
			Assert.NotNull(p2.Hash);

			var (person1Props, person1Fields) = isDirtyValidator.ValidatePropertiesAndFields(p1);
			var (person2Props, person2Fields) = isDirtyValidator.ValidatePropertiesAndFields(p2);

			Assert.True(person1Props.Length == 0);
			Assert.True(person1Fields.Length == 0);

			Assert.True(person2Props.Length == 0);
			Assert.True(person2Fields.Length == 0);

			p2.Complex.ComplexProperty1 = "new";

			Assert.True(isDirtyValidator.Validate(p2).IsDirty);

			var (person2PropsRevalidated, person2Fields2Revalidated) = isDirtyValidator.ValidatePropertiesAndFields(p2);

			Assert.True(person2PropsRevalidated.Length == 1);
			Assert.True(person2Fields2Revalidated.Length == 0);

			Assert.Equal("Complex", person2PropsRevalidated[0].Name);
		}

		[Fact]
		public void ValidatePropertiesAndFields_MultipleInstance_Tracked_ComplexObject_Field_Changes()
		{
			var isDirtyValidator = CreateSut();
			var p1 = new PersonViewModel
			{
				Name = "Person1",
				DisplayName = "Person1-DisplayName",
				Field = "Person1-Field",
				ComplexField = new MyComplexObject
					{ ComplexProperty1 = "Person1-Complex1", ComplexProperty2 = "Person1-Complex2" }
			};

			var p2 = new PersonViewModel
			{
				Name = "Person2",
				DisplayName = "Person2-DisplayName",
				Field = "Person2-Field",
				ComplexField = new MyComplexObject
					{ ComplexProperty1 = "Person2-Complex1", ComplexProperty2 = "Person2-Complex2" }
			};

			Assert.False(isDirtyValidator.Validate(p1, true).IsDirty);
			Assert.NotNull(p1.Hash);
			Assert.False(isDirtyValidator.Validate(p2, true).IsDirty);
			Assert.NotNull(p2.Hash);

			var (person1Props, person1Fields) = isDirtyValidator.ValidatePropertiesAndFields(p1);
			var (person2Props, person2Fields) = isDirtyValidator.ValidatePropertiesAndFields(p2);

			Assert.True(person1Props.Length == 0);
			Assert.True(person1Fields.Length == 0);

			Assert.True(person2Props.Length == 0);
			Assert.True(person2Fields.Length == 0);

			p2.ComplexField.ComplexProperty1 = "new";

			Assert.False(isDirtyValidator.Validate(p1).IsDirty);
			Assert.True(isDirtyValidator.Validate(p2).IsDirty);

			var (person2PropsRevalidated, person2Fields2Revalidated) = isDirtyValidator.ValidatePropertiesAndFields(p2);

			Assert.True(person2PropsRevalidated.Length == 0);
			Assert.True(person2Fields2Revalidated.Length == 1);

			Assert.Equal("ComplexField", person2Fields2Revalidated[0].Name);
		}

		[Fact]
		public void ValidatePropertiesAndFields_SingleInstance_Reset_Tracking_Changes()
		{
			var isDirtyValidator = CreateSut();
			var p = new PersonViewModel
			{
				Name = "Name",
				DisplayName = "DisplayName",
				Field = "Field"
			};

			Assert.False(isDirtyValidator.Validate(p, true).IsDirty);

			p.DisplayName = "DisplayName-new";

			Assert.True(isDirtyValidator.Validate(p).IsDirty);

			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);

			Assert.True(props.Length == 1);
			Assert.True(fields.Length == 0);
			Assert.Equal("DisplayName", props[0].Name);

			Assert.False(isDirtyValidator.Validate(p, true).IsDirty);
		}

		[Fact]
		public void IsDirty_Multi_Iterations()
		{
			var isDirtyValidator = CreateSut();
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
					Complex = new MyComplexObject
						{ ComplexProperty1 = "SomeValue", ComplexProperty2 = "SomeOther Value" }
				};

				Assert.False(isDirtyValidator.Validate(p, true).IsDirty);
			}

			stopWatch.Stop();

			_outputHelper.WriteLine($"Validated {iterations} in {stopWatch.Elapsed}");
		}

		[Fact]
		public void Multi_Threading_Test()
		{
			var isDirtyValidator = CreateSut();
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
				isDirtyValidator.Validate(p1, true);
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
				isDirtyValidator.Validate(p2, true);
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
			var isDirtyValidator = CreateSut();
			var p = new HashPropertyMonitoringViewModel()
			{
				MyProp = "MyProp"
			};

			isDirtyValidator.Validate(p, true);
			p.MyProp = "New";

			Assert.True(isDirtyValidator.Validate(p).IsDirty);

			var (props, fields) = isDirtyValidator.ValidatePropertiesAndFields(p);

			Assert.True(props.Length == 1);
			Assert.True(fields.Length == 0);
			Assert.Equal("MyProp", props[0].Name);
		}

		[Fact]
		public void ValidatePropertiesAndFields_Stop_Tracking_Removes_Hashes()
		{
			var isDirtyValidator = CreateSut();
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

			isDirtyValidator.StopTrackingObject(p1);

			Assert.Single(isDirtyValidator.ObjectValueHashStore);
		}

		private IDirtyValidator CreateSut()
		{
			return DirtyValidatorFactory.Create();
		}
	}
}