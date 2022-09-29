using eStore.ViewModels.List.Accounting.Banking;

namespace eStore.Pages.Accounting;

public partial class BankAccountPage : ContentPage
{
    BankAccountViewModel viewModel;
	public BankAccountPage(BankAccountViewModel vm)
	{
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}