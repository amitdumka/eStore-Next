using eStore.ViewModels.List.Accounting.Banking;

namespace eStore.Pages.Accounting;

public partial class BankPage : ContentPage
{
    BankViewModel viewModel;
	public BankPage(BankViewModel vm)
	{
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}