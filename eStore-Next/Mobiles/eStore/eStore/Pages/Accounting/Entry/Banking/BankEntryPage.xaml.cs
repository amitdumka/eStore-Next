using AKS.Shared.Commons.Models.Banking;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.ViewModels.Base;
 
using eStore.ViewModels.List.Accounting.Banking;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace eStore.Pages.Accounting.Entry.Banking;

public partial class BankEntryPage : ContentPage
{
	public BankEntryPage(BankViewModel vm , Bank bank)
	{
		InitializeComponent();
		var viewModel = new BankEntryViewModel(vm, bank);
        BindingContext = viewModel;
        dataForm.DataObject = viewModel.BankEntry;
        dataForm.PickerSourceProvider = viewModel;
        viewModel.Dfv = dataForm;
    }
}


public class BankEntry
{
    [DataFormDisplayOptions(LabelText = "ID", IsVisible = false)]
    public string BankId { get; set; }
    [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
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



public partial class BankEntryViewModel : BaseEntryViewModel<Bank, BankingDataModel>, IPickerSourceProvider
{
    [ObservableProperty]
    private DataFormView _dfv;
	[ObservableProperty]
	private BankEntry _bankEntry; 

    public BankEntryViewModel(BankViewModel vm, Bank bank)
	{
		//this.ViewModel = vm; 
	}

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