using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using JamSoft.Helpers.Configuration;

namespace JamSoft.Helpers.Tests.Configuration
{
    public sealed class ATestSettingsClass : SettingsBase<ATestSettingsClass>
    {
        public string ASetting { get; set; } = "ADefault";
        public override JsonTypeInfo GetTypeInfo(Type type)
        {
            return JsonTypeInfo.CreateJsonTypeInfo<ATestSettingsClass>(new JsonSerializerOptions());
        }

        protected override JsonSerializerOptions GeneratedSerializerOptions { get; }
    }
}