using eStore.Accounting.ViewModels.Entry.Accounting;

namespace eStore.Accounting.Pages.Entry;

public partial class VoucherEntryPage : ContentPage
{
	private VoucherEntryViewModel viewModel;
	public VoucherEntryPage()
	{
		InitializeComponent();
		 viewModel = new VoucherEntryViewModel();
		BindingContext = viewModel;
		dataForm.DataObject =  viewModel.VoucherEntry;
		dataForm.PickerSourceProvider = viewModel;
    }
}
