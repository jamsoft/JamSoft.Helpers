using Xunit;

namespace JamSoft.Helpers.Tests.Patterns
{
    public class AvaloniaUiViewModelBaseTests
    {
        [Fact]
        public void PropertyChanged_IsBusy_Fired_SetProperty()
        {
            var sut = CreateSut();
            Assert.PropertyChanged(sut, nameof(sut.IsBusy), () => sut.IsBusy = true);
        }

        [Fact]
        public void PropertyChanged_IsEditable_Fired_SetProperty()
        {
            var sut = CreateSut();
            Assert.PropertyChanged(sut, nameof(sut.IsEditable), () => sut.IsEditable = true);
        }

        private MyAvaloniaTestViewModel CreateSut()
        {
            return new MyAvaloniaTestViewModel();
        }
    }
}
