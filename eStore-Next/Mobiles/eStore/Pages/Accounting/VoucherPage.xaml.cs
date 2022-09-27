﻿using AKS.Shared.Commons.Models.Accounts;
using eStore.Accounting.ViewModels.List.Accounting;
using Syncfusion.Maui.DataGrid;

namespace eStore.Accounting.Pages;

public partial class VoucherPage : ContentPage
{
    public VoucherViewModel viewModel;
    public static ColumnCollection gridColumns;

    public VoucherPage(VoucherViewModel vm)
    {
        InitializeComponent();
        viewModel = vm;
        BindingContext = vm;
        RLV.BindingContext = vm;
        viewModel.Icon = eStore.Resources.Styles.IconFont.MoneyBillWave;
        RLV.Cols = SetGridCols();
        viewModel.CurrentPage = this;
    }

    public VoucherPage()
    {
        InitializeComponent();
        viewModel = new VoucherViewModel();
        BindingContext = viewModel;
        RLV.BindingContext = viewModel;
        viewModel.Icon = eStore.Resources.Styles.IconFont.MoneyBillWave;
        RLV.Cols = SetGridCols();
        viewModel.CurrentPage = this;
    }

    protected ColumnCollection SetGridCols()
    {
        gridColumns = new();
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.VoucherNumber), MappingName = nameof(Voucher.VoucherNumber) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.OnDate), MappingName = nameof(Voucher.OnDate), Format = "dd/MMM/yyyy" });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.VoucherType), MappingName = nameof(Voucher.VoucherType) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.PartyName), MappingName = nameof(Voucher.PartyName) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Particulars), MappingName = nameof(Voucher.Particulars) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Remarks), MappingName = nameof(Voucher.Remarks) });
        gridColumns.Add(new DataGridTextColumn() { HeaderText = nameof(Voucher.Amount), MappingName = nameof(Voucher.Amount) });

        return gridColumns;
    }
}