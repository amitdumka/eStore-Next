using eStore.Accounting.ViewModels.Entry.Accounting;
using eStore.Accounting.ViewModels.List.Accounting;

namespace eStore.Accounting.Pages.Entry;

public partial class VoucherEntryPage : ContentPage
{
    private VoucherEntryViewModel viewModel;

    public VoucherEntryPage(VoucherViewModel vm)
    {
        InitializeComponent();
        viewModel = new VoucherEntryViewModel(vm);
        BindingContext = viewModel;
        dataForm.DataObject = viewModel.VoucherEntry;
        dataForm.PickerSourceProvider = viewModel;
        viewModel.Dfv = dataForm;
    }
}