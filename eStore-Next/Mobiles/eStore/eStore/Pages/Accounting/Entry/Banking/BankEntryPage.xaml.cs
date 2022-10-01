using AKS.Shared.Commons.Models.Banking;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.ViewModels.Base;

using eStore.ViewModels.List.Accounting.Banking;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace eStore.Pages.Accounting.Entry.Banking;

public partial class BankEntryPage : ContentPage
{

    public BankEntryPage(BankViewModel vm)
    {
        InitializeComponent();
        var viewModel = new BankEntryViewModel(vm);
        BindingContext = viewModel;
        dataForm.DataObject = viewModel.BankEntry;
        viewModel.Dfv = dataForm;
    }
    public BankEntryPage(BankViewModel vm, Bank bank)
    {
        InitializeComponent();
        var viewModel = new BankEntryViewModel(vm, bank);
        BindingContext = viewModel;
        dataForm.DataObject = viewModel.BankEntry;
        viewModel.Dfv = dataForm;
    }
}

public class BankEntry
{
    [DataFormDisplayOptions(LabelText = "ID", IsVisible = false)]
    
    public string BankId { get; set; }

    [StringLength(64, MinimumLength = 5,
           ErrorMessage = "The Bank name should contain at least 5 characters.")]
    [Required(ErrorMessage = "Bank is Required")]
    [DataFormDisplayOptions(LabelText = "Bank Name", GroupName = "Bank")]
    public string Name { get; set; }
}

public class BankTranscationEntry
{
    [Key]
    [DataFormDisplayOptions(LabelText = "ID", IsVisible = false)]
    public int BankTranscationId { get; set; }

    [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public string AccountNumber { get; set; }

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public DateTime OnDate { get; set; }

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public string Naration { get; set; }

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public string RefNumber { get; set; }

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public decimal Amount { get; set; }//In is postive and Out is Negative.

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public decimal Balance { get; set; }

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public DebitCredit DebitCredit { get; set; }

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public DateTime? BankDate { get; set; }

    //public bool Verified { get; set; }
}

public class BankAccountEntry
{
    [Key]
    [DataFormDisplayOptions(LabelText = "ID", IsVisible = false)]
    public string AccountNumber { get; set; }

    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public string AccountHolderName { get; set; }

    [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
    [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 1)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public string BankId { get; set; }

    [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 2)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public string IFSCCode { get; set; }

    [DataFormItemPosition(RowOrder = 2, ItemOrderInRow = 1)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public string BranchName { get; set; }

    [DataFormItemPosition(RowOrder = 2, ItemOrderInRow = 2)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public AccountType AccountType { get; set; }

    [DataFormItemPosition(RowOrder = 3, ItemOrderInRow = 1)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public bool IsActive { get; set; }

    [DataFormItemPosition(RowOrder = 3, ItemOrderInRow = 2)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public bool DefaultBank { get; set; }

    [DataFormItemPosition(RowOrder = 3, ItemOrderInRow = 3)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public bool SharedAccount { get; set; }

    [DataFormItemPosition(RowOrder = 4, ItemOrderInRow = 1)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public decimal OpenningBalance { get; set; }

    [DataFormItemPosition(RowOrder = 4, ItemOrderInRow = 2)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public decimal CurrentBalance { get; set; }

    [DataFormItemPosition(RowOrder = 5, ItemOrderInRow = 1)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public DateTime OpenningDate { get; set; }

    [DataFormItemPosition(RowOrder = 5, ItemOrderInRow = 2)]
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
    public DateTime? ClosingDate { get; set; }
}

public partial class BankAccountEntryViewModel : BaseEntryViewModel<BankAccount, BankingDataModel>, IPickerSourceProvider
{
    [ObservableProperty]
    private DataFormView _dfv;

    [ObservableProperty]
    private BankAccount _bankAccountEntry;

    public IEnumerable GetSource(string propertyName)
    {
        throw new NotImplementedException();
    }

    protected override void Cancle()
    {
        throw new NotImplementedException();
    }

    protected override void InitViewModel()
    {
        throw new NotImplementedException();
    }

    protected override void Save()
    {
        throw new NotImplementedException();
    }
}

public partial class BankEntryViewModel : BaseEntryViewModel<Bank, BankingDataModel>
{
    [ObservableProperty]
    private DataFormView _dfv;

    [ObservableProperty]
    private BankEntry _bankEntry;

    [ObservableProperty]
    private BankViewModel _viewModel;

    public BankEntryViewModel(BankViewModel vm)
    {
        BankEntry = new BankEntry();
        IsNew = true;

        DataModel = vm.GetDataModel();
        InitViewModel();
    }

    public BankEntryViewModel(BankViewModel vm, Bank bank)
    {
        if (bank != null)
        {
            BankEntry = new BankEntry
            {
                BankId = bank.BankId,
                Name = bank.Name
            };
            IsNew = false;
        }
        else
        {
            BankEntry = new BankEntry();
            IsNew = true;
        }
        DataModel = vm.GetDataModel();
        InitViewModel();
    }

    private void ResetView()
    {
        Dfv.DataObject = BankEntry = new BankEntry();
    }

    protected override void Cancle()
    {
        ResetView();
    }

    protected override void InitViewModel()
    {
        if (DataModel == null)
        {
            DataModel = new BankingDataModel(ConType.Hybrid, CurrentSession.Role);
            DataModel.InitContext();
        }
    }

    protected override async void Save()
    {
        try
        {
            if (!this.Dfv.Validate())
            {
                Notify.NotifyVShort("Kindly Check Fields for error, and try again!!");
                return;
            }
            var v = await DataModel.SaveAsync(new Bank { 
                BankId = DataModel.GenerateBankId(BankEntry.Name), 
                Name = BankEntry.Name }, IsNew);

            if (v != null)
            {
                Notify.NotifyVLong($"{v.Name} is added");

                //Update view model on add/update.
                if (this._viewModel != null)
                {
                    if (!IsNew)
                        ViewModel.Entities
                            .Remove(ViewModel.Entities.FirstOrDefault(c => c.BankId == v.BankId));
                    ViewModel.Entities.Add(v);
                }

                DataModel.SyncUp(v, IsNew, false);
                ResetView();
            }
            else
            {
                Notify.NotifyVLong($"Error on saving Bank :{BankEntry.Name}");
                
            }
        }
        catch (NullReferenceException e)
        {
            Notify.NotifyVLong($"Error  :{e.Message}");
            DataModel.GetContext().Banks.Local.Clear();

        }
        catch (Exception e)
        {
            Notify.NotifyVLong($"Error  :{e.Message}");
            DataModel.GetContext().Banks.Local.Clear();
        }
    }

    public partial class BankTranscationEntryViewModel : BaseEntryViewModel<BankTranscation, BankingDataModel>, IPickerSourceProvider
    {
        [ObservableProperty]
        private DataFormView _dfv;

        [ObservableProperty]
        private BankTranscationEntry _BankTranscationEntry;

        public IEnumerable GetSource(string propertyName)
        {
            throw new NotImplementedException();
        }

        protected override void Cancle()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            throw new NotImplementedException();
        }

        protected override void Save()
        {
            throw new NotImplementedException();
        }
    }
}