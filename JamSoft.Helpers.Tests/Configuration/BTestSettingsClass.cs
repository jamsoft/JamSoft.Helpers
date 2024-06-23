using JamSoft.Helpers.Configuration;

namespace JamSoft.Helpers.Tests.Configuration
{
    public sealed class BTestSettingsClass : SettingsBase<BTestSettingsClass>
    {
        public string BSetting { get; set; } = "BDefault";
    }
}