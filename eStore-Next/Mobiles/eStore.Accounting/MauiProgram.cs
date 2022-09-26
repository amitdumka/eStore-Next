using CommunityToolkit.Maui;
using DevExpress.Maui;
using eStore.Accounting.Pages;
using eStore.Accounting.Pages.Entry;
using eStore.Accounting.ViewModels.List.Accounting;
using eStore.Accounting.ViewModels.List.Dashboard;
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

            builder.UseMauiApp<App>() .UseMauiCompatibility() .UseDevExpress().UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
            }).ConfigureEffects((effects) =>
            {
                effects.AddCompatibilityEffects(AppDomain.CurrentDomain.GetAssemblies());
            });
            builder.ConfigureSyncfusionCore();
            builder.ConfigureSyncfusionDataGrid();
            
            var services = builder.Services;
            builder.Services.AddSingleton<VoucherViewModel>();
            builder.Services.AddSingleton<VoucherPage>();
            builder.Services.AddSingleton<VoucherEntryPage>();
            builder.Services.AddSingleton<CashVoucherViewModel>();
            builder.Services.AddSingleton<CashVoucherPage>();
            builder.Services.AddSingleton<CashVoucherEntryPage>();
            builder.Services.AddSingleton<AccountingDashboardViewModel>();
            builder.Services.AddSingleton<AccountingDashboardPage>();

            return builder.Build();
        }
    }
}