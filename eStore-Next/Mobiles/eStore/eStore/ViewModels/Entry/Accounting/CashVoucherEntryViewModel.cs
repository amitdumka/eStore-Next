using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore.ViewModels.List.Accounting;
using eStore.MAUILib.DataModels;
using eStore.MAUILib.Helpers;
using eStore.MAUILib.Services;
using eStore.MAUILib.ViewModels.Base;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using eStore.MAUILib.DataModels.Accounting;


namespace eStore.ViewModels.Entry.Accounting
{
    public partial class CashVoucherEntryViewModel : BaseEntryViewModel<CashVoucher, VoucherDataModel>, IPickerSourceProvider
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
        private CashVoucherEntry _voucherEntry;

        [ObservableProperty]
        private int[] _lastCount = new int[9];

        [ObservableProperty]
        private CashVoucherViewModel _voucherViewModel;

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

                if (propertyName == "TranscationId")
                {
                    if (BankList == null)
                        BankList = CommonDataModel.GetTranscation(DataModel.GetContext());
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

        public CashVoucherEntryViewModel()
        {
            IsNew = true;
            VoucherEntry = new CashVoucherEntry
            {
                Amount = 0,
                OnDate = DateTime.Now,
                VoucherType = VoucherType.Payment
            };
            InitViewModel();
        }

        protected void ResetView()
        {
            Dfv.DataObject = VoucherEntry = new CashVoucherEntry { OnDate = DateTime.Now, VoucherType = VoucherEntry.VoucherType };
        }

        public CashVoucherEntryViewModel(CashVoucherViewModel vm)
        {
            IsNew = true;
            VoucherEntry = new CashVoucherEntry
            {
                Amount = 0,
                OnDate = DateTime.Now,
                
                VoucherType = VoucherType.Payment
            };
            DataModel = vm.GetDataModel();
            InitViewModel();
            this.VoucherViewModel = vm;
        }

        public CashVoucherEntryViewModel(VoucherDataModel dm)
        {
            IsNew = true;
            VoucherEntry = new CashVoucherEntry
            {
                Amount = 0,
                OnDate = DateTime.Now,
                VoucherType = VoucherType.Payment
            };
            DataModel = dm;
            InitViewModel();
        }

        public CashVoucherEntryViewModel(VoucherDataModel dm, CashVoucher v)
        {
            IsNew = false;
            //TODO: Use of AutoMapper is required.
            VoucherEntry = new CashVoucherEntry
            {
                Amount = v.Amount,
                OnDate = v.OnDate,
                Particulars = v.Particulars,
                PartyName = v.PartyName,
                Remarks = v.Remarks,
                SlipNumber = v.SlipNumber,
                VoucherType = v.VoucherType,
                TranscationId = v.TranscationId,
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
                           new CashVoucher
                           {
                               TranscationId=VoucherEntry.TranscationId,
                               EmployeeId = VoucherEntry.EmployeeId,
                               OnDate = VoucherEntry.OnDate,
                               EntryStatus = EntryStatus.Added,
                               //AccountId = VoucherEntry.AccountId ?? VoucherEntry.AccountId,
                               Amount = VoucherEntry.Amount,
                               IsReadOnly = false,
                               MarkedDeleted = false,
                               Particulars = VoucherEntry.Particulars,
                               PartyId = VoucherEntry.PartyId ?? VoucherEntry.PartyId,
                               PartyName = VoucherEntry.PartyName,
                               //PaymentDetails = VoucherEntry.PaymentDetails,
                               //PaymentMode = VoucherEntry.PaymentMode,
                               Remarks = VoucherEntry.Remarks,
                               SlipNumber = VoucherEntry.SlipNumber ?? VoucherEntry.SlipNumber,
                               StoreId = CurrentSession.StoreCode,
                               UserId = CurrentSession.UserName,
                               VoucherType = VoucherEntry.VoucherType,
                               VoucherNumber = IsNew ? AutoGen.GenerateVoucherNumber(VoucherEntry.VoucherType, VoucherEntry.OnDate, CurrentSession.StoreCode, Count(VoucherEntry.VoucherType) + 1) : VoucherEntry.VoucherNumber
                           }); ;
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

    public class CashVoucherEntry
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


        [DataFormDisplayOptions(LabelText = "Category")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        public string TranscationId { get; set; }

        [Required(ErrorMessage = "Party name is Required")]
        [DataFormDisplayOptions(LabelText = "Name", GroupName = "Basic")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Particulars is Required")]
        [DataFormDisplayOptions(GroupName = "Voucher Details")]
        public string Particulars { get; set; }

        [Required(ErrorMessage = "Amount is Required")]
        [DataFormDisplayOptions(LabelText = "$", GroupName = "Voucher Details")]
        public decimal Amount { get; set; }

        //[DataFormDisplayOptions(LabelText = "Pay Mode", GroupName = "Payment")]
        //public PaymentMode PaymentMode { get; set; }

        //[DataFormDisplayOptions(LabelText = "Pay Details", GroupName = "Payment")]
        //public string PaymentDetails { get; set; }

        [DataFormDisplayOptions(GroupName = "Voucher Details")]
        public string Remarks { get; set; }

        

        [DataFormDisplayOptions(LabelText = "Issued By", GroupName = "Issued By")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        public string EmployeeId { get; set; }

       
        [DataFormDisplayOptions(LabelText = "Ledger", GroupName = "Ledger")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        public string PartyId { get; set; }

         
    }
}