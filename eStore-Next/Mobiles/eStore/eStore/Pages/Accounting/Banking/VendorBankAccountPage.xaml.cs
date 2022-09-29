
using eStore.ViewModels.List.Accounting.Banking;

namespace eStore.Pages.Accounting;

public partial class VendorBankAccountPage : ContentPage
{
    VendorAccountViewModel viewModel;
	public VendorBankAccountPage(VendorAccountViewModel vm)
	{
		
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}