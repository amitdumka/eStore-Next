using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore.MAUILib.DataModels;
using eStore.MAUILib.DataModels.Accounting;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.Services;
using eStore.MAUILib.ViewModels.Base;
using eStore.ViewModels.List.Accounting;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace eStore.ViewModels.Entry.Accounting
{
    public partial class VoucherEntryViewModel : BaseEntryViewModel<Voucher, VoucherDataModel>, IPickerSourceProvider
    {
        #region Field

        [ObservableProperty]
        private DataFormView _dfv;

        [ObservableProperty]
        private List<DynVM> _employeeList;

        [ObservableProperty]
        private List<DynVM> _partyList;

        [ObservableProperty]
        private List<DynVM> _bankList;

        [ObservableProperty]
        private VoucherEntry _voucherEntry;

        [ObservableProperty]
        private int[] _lastCount = new int[9];

        [ObservableProperty]
        private VoucherViewModel _voucherViewModel;

        [ObservableProperty]
        private string _lastvoucherNumber;

        public int Count(VoucherType type)
        {
            var x = _lastCount[(int)type];
            if (x > 0) { _lastCount[(int)type] = ++x; return x; }
            else { return _lastCount[(int)type] = DataModel.Count(type); }
        }

        public IEnumerable GetSource(string propertyName)
        {
            try
            {
                if (propertyName == "EmployeeId")
                {
                    if (EmployeeList == null)
                        EmployeeList = CommonDataModel.GetEmployeeList(DataModel.GetContext());
                    return EmployeeList;
                }
                if (propertyName == "PartyId")
                {
                    if (PartyList == null)
                        PartyList = CommonDataModel.GetParty(DataModel.GetContext());
                    return PartyList;
                }

                if (propertyName == "AccountId")
                {
                    if (BankList == null)
                        BankList = CommonDataModel.GetBankAccount(DataModel.GetContext());
                    return BankList;
                }
                return null;
            }
            catch (NullReferenceException e)
            {
                Notify.NotifyShort(e.Message);//, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return null;
            }
            catch (Exception e)
            {
                Notify.NotifyShort(e.Message); //, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return null;
            }
        }

        #endregion Field

        public VoucherEntryViewModel()
        {
            IsNew = true;
            VoucherEntry = new VoucherEntry
            {
                Amount = 0,
                OnDate = DateTime.Now,
                PaymentMode = PaymentMode.Cash,
                VoucherType = VoucherType.Payment
            };
            VoucherEntry.OnDate = DateTime.Now;
            InitViewModel();
        }

        protected void ResetView()
        {
            Dfv.DataObject = VoucherEntry = new VoucherEntry { OnDate = DateTime.Now, VoucherType = VoucherEntry.VoucherType };
        }

        public VoucherEntryViewModel(VoucherViewModel vm)
        {
            IsNew = true;
            VoucherEntry = new VoucherEntry
            {
                Amount = 0,
                OnDate = DateTime.Now,

                PaymentMode = PaymentMode.Cash,
                VoucherType = VoucherType.Payment
            };
            DataModel = vm.GetDataModel();
            InitViewModel();
            this.VoucherViewModel = vm;
        }

        public VoucherEntryViewModel(VoucherViewModel vm, Voucher v)
        {
            IsNew = true;
            VoucherEntry = new VoucherEntry
            {
                Amount = v.Amount,
                OnDate = v.OnDate,
                Particulars = v.Particulars,
                PartyName = v.PartyName,
                PaymentDetails = v.PaymentDetails,
                PaymentMode = v.PaymentMode,
                Remarks = v.Remarks,
                SlipNumber = v.SlipNumber,
                VoucherType = v.VoucherType,
                AccountId = v.AccountId,
                EmployeeId = v.EmployeeId,
                PartyId = v.PartyId,
                VoucherNumber = v.VoucherNumber
            };
            DataModel = vm.GetDataModel();
            InitViewModel();
            this.VoucherViewModel = vm;
        }

        public VoucherEntryViewModel(VoucherDataModel dm)
        {
            IsNew = true;
            VoucherEntry = new VoucherEntry
            {
                Amount = 0,
                OnDate = DateTime.Now,
                PaymentMode = PaymentMode.Cash,
                VoucherType = VoucherType.Payment
            };
            DataModel = dm;
            InitViewModel();
        }

        public VoucherEntryViewModel(VoucherDataModel dm, Voucher v)
        {
            IsNew = false;
            //TODO: Use of AutoMapper is required.
            VoucherEntry = new VoucherEntry
            {
                Amount = v.Amount,
                OnDate = v.OnDate,
                Particulars = v.Particulars,
                PartyName = v.PartyName,
                PaymentDetails = v.PaymentDetails,
                PaymentMode = v.PaymentMode,
                Remarks = v.Remarks,
                SlipNumber = v.SlipNumber,
                VoucherType = v.VoucherType,
                AccountId = v.AccountId,
                EmployeeId = v.EmployeeId,
                PartyId = v.PartyId,
                VoucherNumber = v.VoucherNumber
            };
            DataModel = dm;
            InitViewModel();
        }

        protected override void Cancle()
        {
            ResetView();
        }

        protected override void InitViewModel()
        {
            if (DataModel == null)
                DataModel = new VoucherDataModel(ConType.Hybrid, CurrentSession.Role);
            DataModel.InitContext();
        }

        protected override async void Save()
        {
            try
            {
                //TODO: need automapper here.
                var v = await DataModel.SaveAsync(
                           new Voucher
                           {
                               EmployeeId = VoucherEntry.EmployeeId,
                               OnDate = VoucherEntry.OnDate,
                               EntryStatus = EntryStatus.Added,
                               AccountId = VoucherEntry.AccountId ?? VoucherEntry.AccountId,
                               Amount = VoucherEntry.Amount,
                               IsReadOnly = false,
                               MarkedDeleted = false,
                               Particulars = VoucherEntry.Particulars,
                               PartyId = VoucherEntry.PartyId ?? VoucherEntry.PartyId,
                               PartyName = VoucherEntry.PartyName,
                               PaymentDetails = VoucherEntry.PaymentDetails,
                               PaymentMode = VoucherEntry.PaymentMode,
                               Remarks = VoucherEntry.Remarks,
                               SlipNumber = VoucherEntry.SlipNumber ?? VoucherEntry.SlipNumber,
                               StoreId = CurrentSession.StoreCode,
                               UserId = CurrentSession.UserName,
                               VoucherType = VoucherEntry.VoucherType,
                               VoucherNumber = IsNew ? AutoGen.GenerateVoucherNumber(VoucherEntry.VoucherType, VoucherEntry.OnDate, CurrentSession.StoreCode, Count(VoucherEntry.VoucherType) + 1) : VoucherEntry.VoucherNumber
                           }, IsNew);
                if (v != null)
                {
                    Notify.NotifyVLong($"Save Voucher :{v.VoucherNumber}");
                    LastvoucherNumber = v.VoucherNumber;
                    //Update view model on add/update.
                    if (this._voucherViewModel != null)
                    {
                        if (!IsNew)
                            _voucherViewModel.Entities
                                .Remove(_voucherViewModel.Entities.FirstOrDefault(c => c.VoucherNumber == v.VoucherNumber));
                        _voucherViewModel.Entities.Add(v);
                    }

                    DataModel.SyncUp(v, IsNew, false);
                    ResetView();
                }
                else
                {
                    await Toast.Make($"Error on save Voucher :{VoucherEntry.SlipNumber}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                    ASpeak.Speak($"Error on save Voucher :{VoucherEntry.SlipNumber}");
                }
            }
            catch (NullReferenceException e)
            {
                await Toast.Make($"Error  :{e.Message}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
            catch (Exception e)
            {
                await Toast.Make($"Error  :{e.Message}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
        }
    }

    public class VoucherEntry
    {
        [Key]
        [DataFormDisplayOptions(LabelText = "ID", IsVisible = false)]
        public string VoucherNumber { get; set; }

        [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 1)]
        [DataFormDisplayOptions(LabelText = "Type", GroupName = "Basic")]
        public VoucherType VoucherType { get; set; }

        [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 2)]
        [DataFormDisplayOptions(LabelText = "Date", GroupName = "Basic")]
        public DateTime OnDate { get; set; }

        [DataFormDisplayOptions(GroupName = "Basic")]
        public string SlipNumber { get; set; }

        [Required(ErrorMessage = "Party name is Required")]
        [DataFormDisplayOptions(LabelText = "Name", GroupName = "Basic")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Particulars is Required")]
        [DataFormDisplayOptions(GroupName = "Voucher Details")]
        public string Particulars { get; set; }

        [Required(ErrorMessage = "Amount is Required")]
        [DataFormDisplayOptions(LabelText = "$", GroupName = "Voucher Details")]
        public decimal Amount { get; set; }

        [DataFormDisplayOptions(LabelText = "Pay Mode", GroupName = "Payment")]
        public PaymentMode PaymentMode { get; set; }

        [DataFormDisplayOptions(LabelText = "Pay Details", GroupName = "Payment")]
        public string PaymentDetails { get; set; }

        [DataFormDisplayOptions(GroupName = "Voucher Details")]
        public string Remarks { get; set; }

        [DataFormDisplayOptions(LabelText = "Bank", GroupName = "Payment")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        public string AccountId { get; set; }

        [DataFormDisplayOptions(LabelText = "Issued By", GroupName = "Issued By")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        public string EmployeeId { get; set; }

        // public virtual Employee Employee { get; set; }

        [DataFormDisplayOptions(LabelText = "Ledger", GroupName = "Ledger")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        public string PartyId { get; set; }

        //public virtual Party Partys { get; set; }
    }
}