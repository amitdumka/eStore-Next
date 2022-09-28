using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting;

public partial class PettyCashSheetPage : ContentPage
{
	public PettyCashViewMoldel viewModel;
	public PettyCashSheetPage(PettyCashViewMoldel vm)
	{
		InitializeComponent();
		BindingContext= viewModel = vm;
		viewModel.Setup(this, RLV);
	}
}