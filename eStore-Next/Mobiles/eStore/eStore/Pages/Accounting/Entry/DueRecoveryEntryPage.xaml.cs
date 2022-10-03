using AKS.Shared.Commons.Models.Sales;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;
using eStore.ViewModels.List.Accounting;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace eStore.Pages.Accounting.Entry;

public partial class DueRecoveryEntryPage : ContentPage
{
    public DueRecoveryEntryPage(DueRecoveryViewModel vm)
    {
        InitializeComponent();
        var viewModel = new DueRecoveryEntryViewModel(vm, null);
        BindingContext = viewModel;
        dataForm.DataObject = viewModel.Entry;
        viewModel.Dfv = dataForm;
    }

    public DueRecoveryEntryPage(DueRecoveryViewModel vm, DueRecovery recovery)
    {
        InitializeComponent();
        var viewModel = new DueRecoveryEntryViewModel(vm, recovery);
        BindingContext = viewModel;
        dataForm.DataObject = viewModel.Entry;
        viewModel.Dfv = dataForm;
    }
}

public class DueRecoveryEntry
{
    [Key]
    [DataFormDisplayOptions(LabelText = "ID", IsVisible = false)]
    public string Id { get; set; }

    [Required]
    [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
    [DataFormDisplayOptions(LabelText = "Invoice No", GroupName = "Invoice")]
    public string InvoiceNumber { set; get; }

    [Required]
    [DataFormDisplayOptions(LabelText = "Date", GroupName = "Recovery")]
    public DateTime OnDate { set; get; }

    [Required]
    [DataFormDisplayOptions(LabelText = "Amount", GroupName = "Recovery")]
    public decimal Amount { set; get; }

    [Required]
    [DataFormDisplayOptions(LabelText = "Mode", GroupName = "Recovery")]
    public PayMode PayMode { get; set; }

    [DataFormDisplayOptions(LabelText = "Remarks", GroupName = "Remarks")]
    public string Remarks { get; set; }

    [DataFormDisplayOptions(LabelText = "Particial Payment", GroupName = "Remarks")]
    public bool ParticialPayment { get; set; }
}

public partial class DueRecoveryEntryViewModel : BaseEntryViewModel<DueRecovery, DailySaleDataModel>, IPickerSourceProvider
{
    [ObservableProperty]
    private DataFormView _dfv;

    [ObservableProperty]
    private DueRecoveryEntry _entry;

    [ObservableProperty]
    private DueRecoveryViewModel _viewModel;

    [ObservableProperty]
    private List<string> _invoiceList;

    public DueRecoveryEntryViewModel(DueRecoveryViewModel vm, DueRecovery recovery)
    {
        ViewModel = vm;
        if (recovery != null)
        {
            Entry = new DueRecoveryEntry
            {
                OnDate = recovery.OnDate,
                Amount = recovery.Amount,
                PayMode = recovery.PayMode,
                Id = recovery.Id,
                InvoiceNumber = recovery.InvoiceNumber,
                ParticialPayment = recovery.ParticialPayment,
                Remarks = recovery.Remarks
            };
            IsNew = false;
        }
        else
        {
            Entry = new DueRecoveryEntry { OnDate = DateTime.Now, PayMode = PayMode.Cash };
            IsNew = true;
        }
    }

    private void ResetView()
    {
        Dfv.DataObject = Entry = new DueRecoveryEntry();
    }

    protected override void Cancle()
    {
        ResetView();
    }

    protected override async void InitViewModel()
    {
        if (DataModel == null)
        {
            DataModel = new DailySaleDataModel(ConType.Hybrid, CurrentSession.Role);
            DataModel.InitContext();
        }
        InvoiceList = await DataModel.GetContext().CustomerDues.Where(c => c.StoreId == DataModel.StoreCode && !c.Paid).Select(c => c.InvoiceNumber).ToListAsync();
    }

    protected override async void Save()
    {
        try
        {
            var save = await DataModel.SaveAsync(new DueRecovery
            {
                Amount = Entry.Amount,
                EntryStatus = IsNew ? EntryStatus.Added : EntryStatus.Updated,
                InvoiceNumber = Entry.InvoiceNumber,
                IsReadOnly = false,
                MarkedDeleted = false,
                ParticialPayment = Entry.ParticialPayment,
                OnDate = Entry.OnDate,
                PayMode = Entry.PayMode,
                Remarks = Entry.Remarks,
                StoreId = CurrentSession.StoreCode,
                UserId = CurrentSession.UserName,
                Id = IsNew ? DueRecovery.GenerateId(Entry.InvoiceNumber, Entry.OnDate) + (DataModel.CountZ() + 1) : Entry.Id
            }, IsNew);
            if (save != null)
            {
                Notify.NotifyVLong($"Recovery of Rs.{save.Amount} is saved for Invocie No {save.InvoiceNumber}");
                ResetView();
                if (_viewModel != null)
                {
                    if (!IsNew)
                        _viewModel.Entities.Remove(_viewModel.Entities.Where(c => c.InvoiceNumber == save.InvoiceNumber && c.Id == save.Id).FirstOrDefault());

                    _viewModel.Entities.Add(save);
                }
            }
            else
            {
                Notify.NotifyVShort("It failed to save Recovery!, Kindly try again!");
            }
        }
        catch (NullReferenceException e)
        {
            Notify.NotifyVLong($"Error  :{e.Message}");
            DataModel.GetContext().DueRecovery.Local.Clear();
        }
        catch (Exception e)
        {
            Notify.NotifyVLong($"Error  :{e.Message}");
            DataModel.GetContext().DueRecovery.Local.Clear();
        }
    }

    public IEnumerable GetSource(string propertyName)
    {
        if (propertyName == "InvoiceNumber")
        {
            return _invoiceList;
        }
        return null;
    }
}