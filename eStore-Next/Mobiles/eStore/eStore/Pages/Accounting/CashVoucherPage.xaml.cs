namespace eStore.Pages.Accounting;
using eStore.ViewModels.List.Accounting;

public partial class CashVoucherPage : ContentPage
{
    public CashVoucherViewModel viewModel;

    public CashVoucherPage(CashVoucherViewModel vm)
    {
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }

    public CashVoucherPage()
    {
        InitializeComponent();
        BindingContext = viewModel = new CashVoucherViewModel();
        viewModel.Setup(this, RLV);
    }
}