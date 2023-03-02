using System;

namespace JamSoft.Helpers.Tests.Configuration
{
    public class SettingsBaseWrapper : MarshalByRefObject
    {
        public void CallSave()
        {
            ATestSettingsClass.Save();
        }
    }
}