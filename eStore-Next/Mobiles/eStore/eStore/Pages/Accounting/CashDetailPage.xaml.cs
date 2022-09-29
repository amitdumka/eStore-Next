
using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting;

public partial class CashDetailPage : ContentPage
{
    CashDetailViewModel viewModel;
	public CashDetailPage(CashDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}