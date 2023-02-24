using JamSoft.Helpers.Configuration;

namespace JamSoft.Helpers.Sample.Models;

public class SampleAppSettings : SettingsBase<SampleAppSettings>
{
    public string AStringValue { get; set; } = "Default String Value";
}