﻿using CommunityToolkit.Maui;
using DevExpress.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace eStore.Accounting
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                 .UseMauiCompatibility()
                .UseDevExpress().UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).ConfigureEffects((effects) =>
            {
                effects.AddCompatibilityEffects(AppDomain.CurrentDomain.GetAssemblies());
            }); ;
            return builder.Build();
        }
    }
}