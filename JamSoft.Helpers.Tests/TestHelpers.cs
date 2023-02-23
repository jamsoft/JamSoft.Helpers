using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JamSoft.Helpers.Tests
{
    public static class TestHelpers
    {
        private static readonly Random Random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string AssemblyDirectory
        {
            get
            {
                return EnvEx.WhereAmI();
            }
        }
    }
}
