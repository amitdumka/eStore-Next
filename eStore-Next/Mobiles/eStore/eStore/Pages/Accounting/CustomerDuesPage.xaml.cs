using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting;

public partial class CustomerDuesPage : ContentPage
{
    CustomerDueViewModel viewModel;
	public CustomerDuesPage(CustomerDueViewModel vm)
	{
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}