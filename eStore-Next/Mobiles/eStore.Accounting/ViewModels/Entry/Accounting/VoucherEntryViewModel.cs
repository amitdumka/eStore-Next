using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Payroll.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;
using eStore_MauiLib.DataModels;
using eStore_MauiLib.DataModels.Accounting;
using eStore_MauiLib.ViewModels;

namespace eStore.Accounting.ViewModels.Entry.Accounting
{
    public partial class VoucherEntryViewModel : BaseEntryViewModel<Voucher, VoucherDataModel>, IPickerSourceProvider
    {
        #region Field

        [DataFormDisplayOptions(IsVisible = false)]
        [ObservableProperty]
        private string _voucherNumber;

        [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 1)]
        [DataFormDisplayOptions(LabelIcon = "editors_name", GroupName = "Basic")]
        [ObservableProperty]
        private VoucherType _voucherType;

        [DataFormItemPosition(RowOrder = 1, ItemOrderInRow = 2)]
        [DataFormDisplayOptions(LabelIcon = "editors_name", GroupName = "Basic")]
        [ObservableProperty]
        private DateTime _onDate;

        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Basic")]
        [ObservableProperty]
        private string _slipNumber;

        [Required(ErrorMessage = "Party name is Required")]
        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Basic")]
        [ObservableProperty]
        private string _partyName;

        [Required(ErrorMessage = "Particulars is Required")]
        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Voucher Details")]
        [ObservableProperty]
        private string _particulars;

        
        [Required(ErrorMessage = "Amount is Required")]
        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Voucher Details")]
        [ObservableProperty]
        private decimal _amount;

        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Voucher Details")]
        [ObservableProperty]
        private string _remarks;

        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Payment")]
        [ObservableProperty]
        private PaymentMode _paymentMode;

        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Payment")]
        [ObservableProperty]
        private string _paymentDetails;


        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Payment")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        [ObservableProperty]
        private string _accountId;

        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Issued By")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        [ObservableProperty]
        private string _employeeId;

        [DataFormDisplayOptions(LabelIcon = "editors_email", GroupName = "Ledger")]
        [DataFormComboBoxEditor(ValueMember = "ValueData", DisplayMember = "DisplayData")]
        [ObservableProperty]
        private string _partyId;


        public IEnumerable GetSource(string propertyName)
        {
            if (propertyName == "EmployeeId")
            {
                return CommonDataModel.GetEmployeeList(DataModel.GetContext());
            }
            if (propertyName == "PartyId")
            {
                return CommonDataModel.GetParty(DataModel.GetContext());
            }

            if (propertyName == "AccountId")
            {
                return CommonDataModel.GetBankAccount(DataModel.GetContext());
            }
            return null;
        }


        #endregion

        public VoucherEntryViewModel()
        {
            OnDate = DateTime.Now;
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

