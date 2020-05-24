using System;
using System.Runtime.InteropServices;

namespace JamSoft.Helpers
{
    /// <summary>
    /// Core environment names
    /// </summary>
    public static class EnvExVariableNames
    {
        /// <summary>
        /// Get the PATH variable name
        /// </summary>
        public const string Path = "PATH";
    }

    /// <summary>
    /// A collection of standard windows environment variable names
    /// </summary>
    public static class EnvExWinVariableNames
    {
        /// <summary>
        /// Get the OS variable name
        /// </summary>
        public const string Os = "OS";

        /// <summary>
        /// Gets the system root variable name
        /// </summary>
        public const string SystemRoot = "SYSTEMROOT";

        /// <summary>
        /// Get the user name variable name
        /// </summary>
        public const string UserName = "USERNAME";

        /// <summary>
        /// The computer name variable name
        /// </summary>
        public const string ComputerName = "COMPUTERNAME";

        /// <summary>
        /// 
        /// </summary>
        public const string AllUsersProfile = "ALLUSERSPROFILE";

        /// <summary>
        /// App Data
        /// </summary>
        public const string AppData = "APPDATA";

        /// <summary>
        /// Home Path
        /// </summary>
        public const string HomePath = "HOMEPATH";

        /// <summary>
        /// The home drive
        /// </summary>
        public const string HomeDrive = "HOMEDRIVE";

        /// <summary>
        /// Local App Data
        /// </summary>
        public const string LocalAppData = "LOCALAPPDATA";

        /// <summary>
        /// Program data
        /// </summary>
        public const string ProgramData = "PROGRAMDATA";

        /// <summary>
        /// Public
        /// </summary>
        public const string Public = "PUBLIC";

        /// <summary>
        /// Temp
        /// </summary>
        public const string Temp = "TEMP";

        /// <summary>
        /// Tmp
        /// </summary>
        public const string Tmp = "TMP";

        /// <summary>
        /// User Profile
        /// </summary>
        public const string UserProfile = "USERPROFILE";

        /// <summary>
        /// 
        /// </summary>
        public const string OsDrive = "SYSTEMDRIVE";
    }

    /// <summary>
    /// Standard variable names for Mac OSX
    /// </summary>
    public static class EnvExOsxVariableNames
    {
        /// <summary>
        /// The shell
        /// </summary>
        public const string Shell = "SHELL";

        /// <summary>
        /// The term
        /// </summary>
        public const string Term = "TERM";

        /// <summary>
        /// The display
        /// </summary>
        public const string Display = "DISPLAY";

        /// <summary>
        /// The home
        /// </summary>
        public const string Home = "HOME";

        /// <summary>
        /// The home
        /// </summary>
        public const string TempDir = "TMPDIR";

        /// <summary>
        /// The home
        /// </summary>
        public const string User = "USER";

        /// <summary>
        /// The home
        /// </summary>
        public const string LogName = "LOGNAME";
    }

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
