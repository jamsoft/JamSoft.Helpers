using System.Runtime.InteropServices;
using Xunit;

namespace JamSoft.Helpers.Tests.xUnitExt
{
    public class FactLinuxAttribute : FactAttribute
    {
        public FactLinuxAttribute()
        {
#if NETCOREAPP
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Skip = "Not running on Linux";
            }
#endif
        }
    }
}