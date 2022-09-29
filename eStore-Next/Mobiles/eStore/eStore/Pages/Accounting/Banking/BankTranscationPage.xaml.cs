
using eStore.ViewModels.List.Accounting.Banking;

namespace eStore.Pages.Accounting;

public partial class BankTranscationPage : ContentPage
{
    BankTranscationViewModel viewModel;
	public BankTranscationPage(BankTranscationViewModel vm)
	{
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}