using eStore.Accounting.ViewModels.Entry.Accounting;
using eStore.Accounting.ViewModels.List.Accounting;

namespace eStore.Accounting.Pages.Entry;

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