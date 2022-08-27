using System.IO;
using JamSoft.Helpers.Configuration;
using Xunit;

namespace JamSoft.Helpers.Tests.Configuration
{
    public sealed class ATestSettingsClass : SettingsBase<ATestSettingsClass>
    {
        public string ASetting { get; set; } = "ADefault";
    }

    public sealed class BTestSettingsClass : SettingsBase<BTestSettingsClass>
    {
        public string BSetting { get; set; } = "BDefault";
    }

    public class SettingsBaseTests
    {
        [Fact]
        public void Init()
        {
            DeleteSettingsFiles();

            var s = new ATestSettingsClass();
            Assert.Equal("ADefault", s.ASetting);

            DeleteSettingsFiles();
        }

        [Fact]
        public void Set_Save()
        {
            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);
            
            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            var randomString = TestHelpers.RandomString(20);

            ATestSettingsClass.Instance.ASetting = randomString;

            ATestSettingsClass.Save();

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            Assert.True(File.Exists(Path.Combine(TestHelpers.AssemblyDirectory, "ATestSettingsClass.json")));

            DeleteSettingsFiles();
        }

        [Fact]
        public void Set_Save_Recall()
        {
            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            var randomString = TestHelpers.RandomString(20);

            ATestSettingsClass.Instance.ASetting = randomString;

            ATestSettingsClass.Save();

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            DeleteSettingsFiles();
        }

        [Fact]
        public void Multi_Settings_Files_Do_Not_Collide_Dirs()
        {
            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);
            BTestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);
            Assert.Equal("BDefault", BTestSettingsClass.Instance.BSetting);

            ATestSettingsClass.Save();
            BTestSettingsClass.Save();

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);
            BTestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);
            Assert.Equal("BDefault", BTestSettingsClass.Instance.BSetting);

            DeleteSettingsFiles();
        }

        [Fact]
        public void Custom_File_Name_Used()
        {
            string testFileName = "test-file.json";

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory, testFileName);

            var randomString = TestHelpers.RandomString(20);

            ATestSettingsClass.Instance.ASetting = randomString;

            ATestSettingsClass.Save();

            Assert.True(File.Exists(Path.Combine(TestHelpers.AssemblyDirectory, testFileName)));

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            DeleteSettingsFiles();
        }

        [Fact]
        public void Custom_File_Name_Used_Load_Default_Has_Default_Values()
        {
            string testFileName = "test-file.json";

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory, testFileName);

            var randomString = TestHelpers.RandomString(20);

            ATestSettingsClass.Instance.ASetting = randomString;

            ATestSettingsClass.Save();

            Assert.True(File.Exists(Path.Combine(TestHelpers.AssemblyDirectory, testFileName)));

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            DeleteSettingsFiles();
        }

        [Fact]
        public void Custom_File_Name_Used_Load_Has_Stored_Values()
        {
            string testFileName = "test-file.json";

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory, testFileName);

            var randomString = TestHelpers.RandomString(20);

            ATestSettingsClass.Instance.ASetting = randomString;

            ATestSettingsClass.Save();

            Assert.True(File.Exists(Path.Combine(TestHelpers.AssemblyDirectory, testFileName)));

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory, testFileName);

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            DeleteSettingsFiles();
        }

        [Fact]
        public void Set_Save_Reset_Auto_Save()
        {
            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            var randomString = TestHelpers.RandomString(20);

            ATestSettingsClass.Instance.ASetting = randomString;

            ATestSettingsClass.Save();

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            ATestSettingsClass.ResetToDefaults();

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            DeleteSettingsFiles();
        }

        [Fact]
        public void Set_Save_Reset_No_Auto_Save()
        {
            DeleteSettingsFiles();

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal("ADefault", ATestSettingsClass.Instance.ASetting);

            var randomString = TestHelpers.RandomString(20);

            ATestSettingsClass.Instance.ASetting = randomString;

            ATestSettingsClass.Save();

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            ATestSettingsClass.ResetToDefaults(false);

            ATestSettingsClass.Load(TestHelpers.AssemblyDirectory);

            Assert.Equal(randomString, ATestSettingsClass.Instance.ASetting);

            DeleteSettingsFiles();
        }

        private void DeleteSettingsFiles()
        {
            foreach (string sFile in Directory.GetFiles(TestHelpers.AssemblyDirectory, "*.json"))
            {
                File.Delete(sFile);
            }
        }
    }
}
