using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests
{
    public class EnvExTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public EnvExTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Gets_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.OsDrive);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }   

        [Fact]
        public void Gets_Path_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExVariableNames.Path);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [Fact]
        public void Gets_Os_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExVariableNames.Os);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [Fact]
        public void Gets_Computer_Name_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExVariableNames.ComputerName);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [Fact]
        public void Gets_User_Name_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExVariableNames.UserName);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [Fact]
        public void Gets_System_Root_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExVariableNames.SystemRoot);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }
    }
}
