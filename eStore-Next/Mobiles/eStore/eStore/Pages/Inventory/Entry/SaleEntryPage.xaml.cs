using eStore.ViewModels.Entry.Inventory;
using eStore.ViewModels.List.Inventory;

namespace eStore.Pages.Inventory.Entry;

public partial class SaleEntryPage : ContentPage
{

    private SaleEntryViewModel viewModel;
	public SaleEntryPage()
	{
		InitializeComponent();
        BindingContext = viewModel = new SaleEntryViewModel();
    }
    public SaleEntryPage(SaleViewModel vm)
    {
        InitializeComponent();
        BindingContext = viewModel = new SaleEntryViewModel(vm);

        // dataForm.DataObject = viewModel.VoucherEntry;
       // dataForm.PickerSourceProvider = viewModel;
       // viewModel.Dfv = dataForm;
    }
}
