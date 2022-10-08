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
        viewModel.InvoiceType = InvoiceType.Sales;
        
    }
    public SaleEntryPage(SaleViewModel vm, InvoiceType invType)
    {
        InitializeComponent();
        BindingContext = viewModel = new SaleEntryViewModel(vm);
        viewModel.InvoiceType= invType;

         //dataForm.DataObject = viewModel.VoucherEntry;
       // dataForm.PickerSourceProvider = viewModel;
       // viewModel.Dfv = dataForm;
    }
}
