using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using JamSoft.Helpers.Configuration;

namespace JamSoft.Helpers.Tests.Configuration
{
    public sealed class BTestSettingsClass : SettingsBase<BTestSettingsClass>
    {
        public string BSetting { get; set; } = "BDefault";
        public override JsonTypeInfo GetTypeInfo(Type type)
        {
            return JsonTypeInfo.CreateJsonTypeInfo<BTestSettingsClass>(new JsonSerializerOptions());
        }

        protected override JsonSerializerOptions GeneratedSerializerOptions { get; }
    }
}