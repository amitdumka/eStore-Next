using AKS.Shared.Commons.Models.Accounts;
using eStore.ViewModels.List.Accounting;
using Syncfusion.Maui.DataGrid;

namespace eStore.Pages.Accounting;

public partial class VoucherPage : ContentPage
{
    public VoucherViewModel viewModel;
    public static ColumnCollection gridColumns;

    public VoucherPage(VoucherViewModel vm)
    {
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }

    public VoucherPage()
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.Setup(this, RLV);
    }
    
}