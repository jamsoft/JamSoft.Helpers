using System;
using System.Runtime.InteropServices;

namespace JamSoft.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnvExLinuxVariableNames
    {
        /// <summary>
        /// The man path
        /// </summary>
        public const string ManPath = "MANPATH";
    }

    /// <summary>
    /// Provides a wrapper around generalized environment methods
    /// </summary>
    public static class EnvEx
    {
        /// <summary>
        /// Get the value of an environment variables
        /// </summary>
        /// <remarks>see EnvExWinVariableNames for string values</remarks>
        /// <param name="name">The name of the parameter to get</param>
        /// <param name="target">Only supported on Windows</param>
        /// <returns></returns>
        public static string GetVariable(string name, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
        {
            if (target != EnvironmentVariableTarget.Process &&
                (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                 RuntimeInformation.IsOSPlatform(OSPlatform.Linux)))
            {
                // target is only supported on Windows
                target = EnvironmentVariableTarget.Process;
            }

            return Environment.GetEnvironmentVariable(name, target);
        }

        /// <summary>
        /// Returns the app domain base directory
        /// </summary>
        /// <returns></returns>
        public static string WhereAmI()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
