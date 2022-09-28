using eStore.ViewModels.Entry.Accounting;
using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting.Entry;

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