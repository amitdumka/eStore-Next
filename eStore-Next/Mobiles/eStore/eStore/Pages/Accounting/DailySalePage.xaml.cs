using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting;

public partial class DailySalePage : ContentPage
{
    DailySaleViewMoldel viewModel;
	public DailySalePage(DailySaleViewMoldel vm)
	{
		InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}