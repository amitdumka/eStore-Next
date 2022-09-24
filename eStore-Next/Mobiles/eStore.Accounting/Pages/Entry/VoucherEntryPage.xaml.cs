using eStore.Accounting.ViewModels.Entry.Accounting;

namespace eStore.Accounting.Pages.Entry;

public partial class VoucherEntryPage : ContentPage
{
	private VoucherEntryViewModel viewModel;
	public VoucherEntryPage()
	{
		InitializeComponent();
		BindingContext = viewModel = new VoucherEntryViewModel();
		dataForm.DataObject = viewModel;
		dataForm.PickerSourceProvider = viewModel;
    }
}
