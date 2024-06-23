using JamSoft.Helpers.Configuration;

namespace JamSoft.Helpers.Tests.Configuration
{
    public sealed class ATestSettingsClass : SettingsBase<ATestSettingsClass>
    {
        public string ASetting { get; set; } = "ADefault";
    }
}