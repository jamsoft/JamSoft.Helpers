using JamSoft.Helpers.Constants;
using JamSoft.Helpers.Tests.xUnitExt;
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
        public void Gets_Where_Am_I()
        {
            var p = EnvEx.WhereAmI();
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

        [FactWin]
        public void Gets_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.OsDrive);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }   

        [FactWin]
        public void Gets_Os_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.Os);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_Computer_Name_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.ComputerName);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_User_Name_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.UserName);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_System_Root_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.SystemRoot);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_AllUsersProfile_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.AllUsersProfile);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_AppData_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.AppData);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_HomePath_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.HomePath);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_HomeDrive_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.HomeDrive);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_LocalAppData_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.LocalAppData);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_ProgramData_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.ProgramData);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_Public_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.Public);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_Temp_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.Temp);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_Tmp_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.Tmp);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactWin]
        public void Gets_UserProfile_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExWinVariableNames.UserProfile);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

#if NETCOREAPP
        [FactOsx]
        public void Osx_Gets_Shell_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.Shell);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_Term_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.Term);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_Display_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.Display);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_Home_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.Home);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_TempDir_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.TempDir);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_User_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.User);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_LogName_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.LogName);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_TermProgram_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.TermProgram);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactOsx]
        public void Osx_Gets_TermProgramVersion_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExOsxVariableNames.TermProgramVersion);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }

        [FactLinux(Skip = "Limited permissions in GitHub prevents this from running in the .net core pipeline")]
        public void Linux_Gets_TermProgramVersion_Environment_Value()
        {
            var p = EnvEx.GetVariable(EnvExLinuxVariableNames.ManPath);
            Assert.NotNull(p);
            _outputHelper.WriteLine(p);
        }
#endif
    }
}
