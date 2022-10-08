namespace eStore;

using CommunityToolkit.Maui;
using DevExpress.Maui;
using eStore.Pages.Accounting;
using eStore.Pages.Accounting.Entry;
using eStore.Pages.Accounting.Entry.Banking;
using eStore.Pages.Dashboard.StoreManager;
using eStore.Pages.Inventory;
using eStore.Pages.Payrol;
using eStore.ViewModels.Entry.Inventory;
using eStore.ViewModels.List.Accounting;
using eStore.ViewModels.List.Accounting.Banking;
using eStore.ViewModels.List.Dashboard;
using eStore.ViewModels.List.Inventory;
using eStore.ViewModels.List.Payroll;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.DataGrid.Hosting;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>().UseMauiCommunityToolkit().UseDevExpress()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
            });
        builder.ConfigureSyncfusionCore();
        builder.ConfigureSyncfusionDataGrid();

        //Accounting
        //Voucher
        builder.Services.AddSingleton<VoucherViewModel>();
        builder.Services.AddSingleton<VoucherPage>();
        builder.Services.AddSingleton<VoucherEntryPage>();
        //CashVoucher
        builder.Services.AddSingleton<CashVoucherViewModel>();
        builder.Services.AddSingleton<CashVoucherPage>();
        builder.Services.AddSingleton<CashVoucherEntryPage>();
        //Dashboardpage
        builder.Services.AddSingleton<AccountingDashboardViewModel>();
        //builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<StoreManagerDashboardPage>();

        //PettyCash
        builder.Services.AddSingleton<PettyCashViewMoldel>();
        builder.Services.AddSingleton<PettyCashSheetPage>();
        builder.Services.AddSingleton<CashDetailPage>();
        //DailySale
        builder.Services.AddSingleton<DailySaleViewMoldel>();
        builder.Services.AddSingleton<DailySalePage>();
        //CashDetails
        builder.Services.AddSingleton<CashDetailViewModel>();
        builder.Services.AddSingleton<CashDetailPage>();
        //Notes
        builder.Services.AddSingleton<NotesViewModel>();
        builder.Services.AddSingleton<NotesPage>();

        //Banking
        builder.Services.AddSingleton<BankViewModel>();
        builder.Services.AddSingleton<BankPage>();
        builder.Services.AddSingleton<BankEntryPage>();

        builder.Services.AddSingleton<BankAccountViewModel>();
        builder.Services.AddSingleton<BankAccountPage>();

        builder.Services.AddSingleton<VendorAccountViewModel>();
        builder.Services.AddSingleton<VendorBankAccountPage>();

        //due
        builder.Services.AddSingleton<CustomerDueViewModel>();
        builder.Services.AddSingleton<CustomerDuesPage>();

        //Due rec
        builder.Services.AddSingleton<DueRecoveryViewModel>();
        builder.Services.AddSingleton<DueRecoveryPage>();

        //Bankfino
        builder.Services.AddSingleton<BankTranscationViewModel>();
        builder.Services.AddSingleton<BankTranscationPage>();

        //Attendance
        builder.Services.AddSingleton<AttendanceViewModel>();
        builder.Services.AddSingleton<AttendancePage>();
        //Monthly Attendance
        builder.Services.AddSingleton<MonthlyAttendanceViewModel>();
        builder.Services.AddSingleton<MonthlyAttendancePage>();
        //Attendance
        builder.Services.AddSingleton<EmployeeViewModel>();
        builder.Services.AddSingleton<EmployeePage>();
        //Inventory Sale
        builder.Services.AddSingleton<SaleViewModel>();
        builder.Services.AddSingleton<StockViewModel>();
        builder.Services.AddSingleton<PurchaseViewModel>();

        //Inventory Sale Entry
        builder.Services.AddSingleton<SaleEntryViewModel>();

        builder.Services.AddSingleton<InvoicePage>();

        return builder.Build();
    }
}