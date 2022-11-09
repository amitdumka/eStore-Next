using System.IO.Compression;
using System.Text.Json;

namespace eStore.SetUp.Import
{
    public class ImportBasic
    {
        public static string ConfigFile = "eStoreConfig.json";
        public static SortedDictionary<string, string> Settings = new SortedDictionary<string, string>();

        public static bool AddOrUpdateSetting(string key, string value)
        {
            if (Settings.TryAdd(key, value)) return true;
            else
            {
                Settings.Remove(key);
                return Settings.TryAdd(key, value);
            }
        }

        public static bool AddSetting(string key, string value)
        {
            return Settings.TryAdd(key, value);
        }

        public static bool BackupJSon(string filename, string path)
        {
            try
            {
                ZipFile.CreateFromDirectory(path, filename, CompressionLevel.Fastest, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ClenJson(string itemName)
        {
            try
            {
                var path = Settings.GetValueOrDefault(itemName, "");
                if (string.IsNullOrEmpty(path) == false)
                {
                    Directory.Delete(Path.GetDirectoryName(path), true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteSetting(string key)
        { return Settings.Remove(key); }

        public static string GetSetting(string key, string defaultvalue = "")
        {
            return Settings.GetValueOrDefault(key, defaultvalue);
        }

        public static async Task InitSettingAsync(string basepath, string storeCode)
        {
            var fn = Path.Combine(basepath + $@"\{storeCode}", "Configs");
            Directory.CreateDirectory(fn);
            ConfigFile = Path.Combine(fn, ConfigFile);

            if (!File.Exists(ConfigFile))
            {
                var config = new SortedDictionary<string, string>();
                config.Add("BasePath", basepath + $@"\{storeCode}");
                config.Add("Store", storeCode);
                using FileStream createStream = File.OpenWrite(ConfigFile);
                await JsonSerializer.SerializeAsync(createStream, config);
                await createStream.DisposeAsync();
            }else
            {
                ReadSetting();
            }
        }

        public static void ReadSetting()
        {
            StreamReader reader = new StreamReader(ConfigFile);
            var json = reader.ReadToEnd();
            Settings = JsonSerializer.Deserialize<SortedDictionary<string, string>>(json);
            reader.Close();
        }

        public static async Task<bool> SaveSettingsAsync()
        {
            try
            {
                if (Settings != null && Settings.Count > 0)
                {
                    using FileStream createStream = File.OpenWrite(ConfigFile);
                    createStream.Flush();
                    await JsonSerializer.SerializeAsync(createStream, Settings);
                    await createStream.DisposeAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}