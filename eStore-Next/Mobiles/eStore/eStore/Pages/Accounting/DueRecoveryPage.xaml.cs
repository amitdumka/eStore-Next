 
using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting;

public partial class DueRecoveryPage : ContentPage
{
    DueRecoveryViewModel viewModel;
	public DueRecoveryPage(DueRecoveryViewModel vm)
	{
		 
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}