﻿namespace eStore;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using eStore.MAUILib.DataModels.Accounting;
using eStore.Pages;
using eStore.Pages.Accounting;
using eStore.Pages.Accounting.Entry;
using eStore.ViewModels.List.Accounting;
using eStore.ViewModels.List.Accounting.Banking;
using eStore.ViewModels.List.Dashboard;
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
        builder.Services.AddSingleton<DashboardPage>();
        //PettyCash 
        builder.Services.AddSingleton<PettyCashViewMoldel>();
        builder.Services.AddSingleton<PettyCashSheetPage>();
        builder.Services.AddSingleton<CashDetailPage>();
        //DailySale 
        builder.Services.AddSingleton<DailySaleViewMoldel>();
        //builder.Services.AddSingleton <DailySalePage>();
        //CashDetails
        builder.Services.AddSingleton<CashDetailViewModel>();
        builder.Services.AddSingleton<CashDetailPage>();
        //Notes 
        builder.Services.AddSingleton<NotesViewModel>();
        builder.Services.AddSingleton<NotesPage>();

        //Banking 
        builder.Services.AddSingleton<BankViewModel>();
        builder.Services.AddSingleton<BankPage>();

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



        return builder.Build();
	}
}