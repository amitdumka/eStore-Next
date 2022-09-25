using CommunityToolkit.Maui;
using DevExpress.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.DataGrid.Hosting;

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
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            }).ConfigureEffects((effects) =>
            {
                effects.AddCompatibilityEffects(AppDomain.CurrentDomain.GetAssemblies());
            });
            builder.ConfigureSyncfusionCore();
            //builder.ConfigureSyncfusionSignaturePad();
            builder.ConfigureSyncfusionDataGrid();
            var services = builder.Services;
            return builder.Build();
        }
    }
}