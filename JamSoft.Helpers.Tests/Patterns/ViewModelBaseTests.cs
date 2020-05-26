using Xunit;

namespace JamSoft.Helpers.Tests.Patterns
{
    public class ViewModelBaseTests
    {
        [Fact]
        public void PropertyChanged_Is_Fired_SetProperty()
        {
            var sut = new MyTestViewModel();
            Assert.PropertyChanged(sut, nameof(sut.CallsSetProperty), () => sut.CallsSetProperty = "value");
        }

        [Fact]
        public void PropertyChanged_Is_Fired_SetProperty_With_Name()
        {
            var sut = new MyTestViewModel();
            Assert.PropertyChanged(sut, "DifferentName", () => sut.CallsSetPropertyWithName = "value");
        }

        [Fact]
        public void PropertyChanged_Is_Fired_SetProperty_IsEditable()
        {
            var sut = new MyTestViewModel();
            Assert.PropertyChanged(sut, nameof(sut.IsEditable), () => sut.IsEditable = true);
        }

        [Fact]
        public void PropertyChanged_Is_Fired_SetProperty_IsEditable_Returns_True()
        {
            var sut = new MyTestViewModel();
            Assert.PropertyChanged(sut, nameof(sut.IsEditable), () => sut.IsEditable = true);
            Assert.True(sut.IsEditable);
        }

        [Fact]
        public void PropertyChanged_Is_Fired_OnPropertyChanged()
        {
            var sut = new MyTestViewModel();
            Assert.PropertyChanged(sut, nameof(sut.CallsOnPropertyChanged), () => sut.CallsOnPropertyChanged = "value");
        }

        [Fact]
        public void PropertyChanged_Is_Fired_OnPropertyChanged_With_Name()
        {
            var sut = new MyTestViewModel();
            Assert.PropertyChanged(sut, "DifferentName", () => sut.CallsOnPropertyChangedWithName = "value");
        }

        [Fact]
        public void PropertyChanged_Is_Fired_OnPropertyChanged_With_Event_Args()
        {
            var sut = new MyTestViewModel();
            Assert.PropertyChanged(sut, nameof(sut.CallsOnPropertyChangedWithEventArgs), () => sut.CallsOnPropertyChangedWithEventArgs = "value");
        }

        [Fact]
        public void PropertyChanged_Is_Only_Fired_When_Value_Actually_Changes()
        {
            int count = 0;
            var sut = new MyTestViewModel();
            sut.PropertyChanged += (sender, args) =>
            {
                count++;
            };

            sut.CallsSetProperty = "value1";
            sut.CallsSetProperty = "value1";

            Assert.Equal(1, count);
        }
    }
}
