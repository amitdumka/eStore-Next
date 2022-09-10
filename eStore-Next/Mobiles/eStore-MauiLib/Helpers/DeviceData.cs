using System;
namespace eStore_MauiLib.Helpers
{

    public class FileHelper
    {
        public async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        var image = ImageSource.FromStream(() => stream);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }

        public FilePickerFileType customFileType = new FilePickerFileType(
                  new Dictionary<DevicePlatform, IEnumerable<string>>
                  {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
                    { DevicePlatform.Android, new[] { "application/comics" } },
                    { DevicePlatform.WinUI, new[] { ".cbr", ".cbz" } },
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
                  });

        public PickOptions options = new()
        {
            PickerTitle = "Please select a comic file",
            FileTypes = new FilePickerFileType(
                 new Dictionary<DevicePlatform, IEnumerable<string>>
                 {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
                    { DevicePlatform.Android, new[] { "application/comics" } },
                    { DevicePlatform.WinUI, new[] { ".cbr", ".cbz" } },
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
                 }),
        };

        public async Task<string> ReadTextFile(string filePath)
        {
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(filePath);
            using StreamReader reader = new StreamReader(fileStream);

            return await reader.ReadToEndAsync();
        }
        public async Task ConvertFileToUpperCase(string sourceFile, string targetFileName)
        {
            // Read the source file
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(sourceFile);
            using StreamReader reader = new StreamReader(fileStream);

            string content = await reader.ReadToEndAsync();

            // Transform file content to upper case text
            content = content.ToUpperInvariant();

            // Write the file content to the app data directory
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);

            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter streamWriter = new StreamWriter(outputStream);

            await streamWriter.WriteAsync(content);
        }
    }
    public class DeviceData
    {
        public string ReadDeviceInfo()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine($"Model: {DeviceInfo.Current.Model}");
            sb.AppendLine($"Manufacturer: {DeviceInfo.Current.Manufacturer}");
            sb.AppendLine($"Name: {DeviceInfo.Name}");
            sb.AppendLine($"OS Version: {DeviceInfo.VersionString}");
            sb.AppendLine($"Refresh Rate: {DeviceInfo.Current}");
            sb.AppendLine($"Idiom: {DeviceInfo.Current.Idiom}");
            sb.AppendLine($"Platform: {DeviceInfo.Current.Platform}");

            bool isVirtual = DeviceInfo.Current.DeviceType switch
            {
                DeviceType.Physical => false,
                DeviceType.Virtual => true,
                _ => false
            };

            sb.AppendLine($"Virtual device? {isVirtual}");

            return sb.ToString();
        }

        public bool IsAndroid() => DeviceInfo.Current.Platform == DevicePlatform.Android;

        public string PrintIdiom()
        {
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
                return ("The current device is a desktop");
            else if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
                return ("The current device is a phone");
            else if (DeviceInfo.Current.Idiom == DeviceIdiom.Tablet)
                return ("The current device is a Tablet");
            return "Not Supported";
        }

        public bool isVirtual = DeviceInfo.Current.DeviceType switch
        {
            DeviceType.Physical => false,
            DeviceType.Virtual => true,
            _ => false
        };

        public string ThemeInfoLabel_Text = AppInfo.Current.RequestedTheme switch
        {
            AppTheme.Dark => "Dark theme",
            AppTheme.Light => "Light theme",
            _ => "Unknown"
        };
    }
}

