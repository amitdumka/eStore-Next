﻿using CommunityToolkit.Maui;
using eStore_Maui;
using Microsoft.Maui.LifecycleEvents;

namespace eStore_Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });

            return builder.Build();
        }
    }
}

//namespace WeatherTwentyOne;

//public static class MauiProgram
//{
//    public static MauiApp CreateMauiApp()
//    {
//        var builder = MauiApp.CreateBuilder();
//        builder
//            .UseMauiApp<App>()
//            .ConfigureFonts(fonts => {
//                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
//                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
//            });
//        builder.ConfigureLifecycleEvents(lifecycle => {
//#if WINDOWS
//        //lifecycle
//        //    .AddWindows(windows =>
//        //        windows.OnNativeMessage((app, args) => {
//        //            if (WindowExtensions.Hwnd == IntPtr.Zero)
//        //            {
//        //                WindowExtensions.Hwnd = args.Hwnd;
//        //                WindowExtensions.SetIcon("Platforms/Windows/trayicon.ico");
//        //            }
//        //        }));

//            lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
//                del.ExtendsContentIntoTitleBar = true;
//            }));
//#endif
//        });

//        var services = builder.Services;
//#if WINDOWS
//            services.AddSingleton<ITrayService, WinUI.TrayService>();
//            services.AddSingleton<INotificationService, WinUI.NotificationService>();
//#elif MACCATALYST
//            services.AddSingleton<ITrayService, MacCatalyst.TrayService>();
//            services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
//#endif
//        services.AddSingleton<HomeViewModel>();
//        services.AddSingleton<HomePage>();




//        return builder.Build();
//    }
//}