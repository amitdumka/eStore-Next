using System;
using System.IO;

namespace eStore_Maui.Test
{
    public interface ISave
    {
        Task SaveAndView(string filename, string contentType, MemoryStream stream);

    }
    //public class Helper
    //{
    //    public static async Task SaveFileAsync(string targetFileName, MemoryStream stream)
    //    {
    //        string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);

    //        using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
    //        using StreamWriter streamWriter = new StreamWriter(outputStream);
    //        using StreamReader reader = new StreamReader(stream);


    //        await streamWriter.WriteAsync(content);

    //}
    //}
    public static class ServiceHelper
    {

        public static T GetService<T>() => Current.GetService<T>();

        public static IServiceProvider Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
    MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
    MauiUIApplicationDelegate.Current.Services;
#else
    null;
#endif
    }
}

