﻿using System.Runtime.InteropServices;
using Xunit;

namespace JamSoft.Helpers.Tests.xUnitExt
{
    public class FactOsxAttribute : FactAttribute
    {
        public FactOsxAttribute()
        { 
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Skip = $"Not running on Osx";
            }
        }
    }
}
