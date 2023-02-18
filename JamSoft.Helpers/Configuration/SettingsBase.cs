using System.Globalization;
using System.IO;

namespace JamSoft.Helpers.Configuration
{
    /// <summary>
    /// Provides loading and saving of JSON settings and configuration data at runtime
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SettingsBase<T> where T : SettingsBase<T>, new()
    {
        private static string _filePath;

        private static string _basePath;

        /// <summary>
        /// The class instance containing the values
        /// </summary>
        public static T Instance { get; private set; } = new T();

        private static string GetLocalFilePath(string fileName)
        {
            return Path.Combine(_basePath, fileName);
        }

        /// <summary>
        /// Loads the settings file for the class type found at the provided base path. If no file exists, the defaults are loaded
        /// </summary>
        /// <param name="basePath">The base settings directory</param>
        /// <param name="filename">The settings filename (including extension) (optional: the type name will be used if this is null, empty or white-space. Creates a file with .json extension)</param>
        public static void Load(string basePath, string filename = null)
        {
            _basePath = basePath;
            _filePath = GetLocalFilePath(string.IsNullOrWhiteSpace(filename) ? $"{typeof(T).Name.ToLower(CultureInfo.CurrentCulture)}.json" : filename);
            Instance = File.Exists(_filePath) ? System.Text.Json.JsonSerializer.Deserialize<T>(File.ReadAllText(_filePath)) : new T();
        }

        /// <summary>
        /// Returns all properties to their defaults and optionally saves to disk
        /// </summary>
        /// <param name="saveToDisk">A flag to optionally save to disk (default: True)</param>
        public static void ResetToDefaults(bool saveToDisk = true)
        {
            Instance = new T();

            if (saveToDisk)
            {
                Save();
            }
        }

        /// <summary>
        /// Save the current setting to the file they were originally loaded from
        /// </summary>
        public static void Save()
        {
            string json = System.Text.Json.JsonSerializer.Serialize(Instance);
            var settingsPath = Path.GetDirectoryName(_filePath);
            if (settingsPath != null)
            {
                Directory.CreateDirectory(settingsPath);
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
