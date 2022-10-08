using eStore.ViewModels.List.Inventory;

namespace eStore.Pages.Inventory;

public partial class InvoicePage : ContentPage
{
    private SaleViewModel viewModel;
    public InvoicePage()
    {
        InitializeComponent();
        BindingContext = viewModel = new SaleViewModel();
        viewModel.Setup(this, RLV);
    }
    public InvoicePage(SaleViewModel vm)
    {
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}