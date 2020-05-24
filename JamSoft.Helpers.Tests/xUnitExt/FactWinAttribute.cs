using System.Runtime.InteropServices;
using Xunit;

namespace JamSoft.Helpers.Tests.xUnitExt
{
    public class FactWinAttribute : FactAttribute
    {
        public FactWinAttribute()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Skip = $"Not running on Windows";
            }
        }
    }
}