using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore_MauiLib.DataModels;
using eStore_MauiLib.DataModels.Accounting;
using eStore_MauiLib.Helpers;
using eStore_MauiLib.Services;
using eStore_MauiLib.ViewModels;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace eStore.Accounting.ViewModels.Entry.Accounting
{
    public partial class VoucherEntryViewModel : BaseEntryViewModel<Voucher, VoucherDataModel>, IPickerSourceProvider
    {
        #region Field

        [ObservableProperty]
        private List<DynVM> _employeeList;

        [ObservableProperty]
        private List<DynVM> _partyList;

        [ObservableProperty]
        private List<DynVM> _bankList;

        [ObservableProperty]
        private VoucherEntry _voucherEntry;

        [ObservableProperty]
        private int[] _lastCount= new int[8];

        public int Count(VoucherType type)
        {
            var x = _lastCount[(int)type];
            if (x > 0) { _lastCount[(int)type] = ++x+10; return x+10; }
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
                Toast.Make(e.Message, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return null;
            }
            catch (Exception e)
            {
                Toast.Make(e.Message, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return null;
            }
        }

        #endregion Field

        public VoucherEntryViewModel()
        {
            IsNew = true;
            VoucherEntry = new VoucherEntry
            {
                Amount = 100,
                OnDate = DateTime.Now,
                Particulars = "das",
                PartyName = "dasdasd",
                PaymentDetails = "dasdas",
                PaymentMode = PaymentMode.Cash,
                Remarks = "dasd12313",
                SlipNumber = "ddddaaa",
                VoucherType = VoucherType.Payment
            };
            VoucherEntry.OnDate = DateTime.Now;
            InitViewModel();
        }

        public VoucherEntryViewModel(VoucherDataModel dm)
        {
            IsNew = true;
            VoucherEntry = new VoucherEntry
            {
                Amount = 100,
                OnDate = DateTime.Now,
                Particulars = "das",
                PartyName = "dasdasd",
                PaymentDetails = "dasdas",
                PaymentMode = PaymentMode.Cash,
                Remarks = "dasd12313",
                SlipNumber = "ddddaaa",
                VoucherType = VoucherType.Payment
            };
            // VoucherEntry.OnDate = DateTime.Now;
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
            // VoucherEntry.OnDate = DateTime.Now;
            DataModel = dm;
            InitViewModel();
        }

        protected override void Cancle()
        {
            Toast.Make("Cancel", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            ASpeak.Speak("Cancel Button is pressed");
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
                           }); ;
                if (v != null)
                {
                    await Toast.Make($"Save Voucher :{v.VoucherNumber}", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                    ASpeak.Speak($"Save Voucher :{v.VoucherNumber}");
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
        [DataFormDisplayOptions(IsVisible = false)]
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