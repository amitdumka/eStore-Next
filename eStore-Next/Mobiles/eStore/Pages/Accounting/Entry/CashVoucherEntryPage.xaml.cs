using eStore.ViewModels.Entry.Accounting;
using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting.Entry;

public partial class CashVoucherEntryPage : ContentPage
{
    private CashVoucherEntryViewModel viewModel;
    public CashVoucherEntryPage(CashVoucherViewModel vm)
    {
        InitializeComponent();
        viewModel = new CashVoucherEntryViewModel(vm);
        BindingContext = viewModel;
        dataForm.DataObject = viewModel.VoucherEntry;
        dataForm.PickerSourceProvider = viewModel;
        viewModel.Dfv = dataForm;
    }
}