using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore.Shared.Models
{
    public partial class BasicVoucher : BaseST
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OnDate { get; set; }

        [Display(Name = "Party Name")]
        public string PartyName { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentMode PayMode { get; set; }

        [Display(Name = "From Account")]
        public int? BankAccountId { get; set; }

        //public virtual BankAccount FromAccount { get; set; }

        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Party")]
        public int? PartyId { get; set; }

        [Display(Name = "Leger")]
        public int? LedgerEnteryId { get; set; }

        [DefaultValue(false), Display(Name = "Cash")]
        public bool IsCash { get; set; }

        [DefaultValue(false), Display(Name = "ON")]
        public bool? IsOn { get; set; }

        [DefaultValue(true), Display(Name = "Dyn")]
        public bool IsDyn { get; set; }

        //public virtual Party Party { get; set; }
        //public virtual LedgerEntry LedgerEntry { get; set; }
    }
    public class Expense : BasicVoucher
    {
        public int ExpenseId { get; set; }
        public string Particulars { get; set; }

        [Display(Name = "Paid To")]
        public new string PartyName { get; set; }

        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }

        //public virtual Employee PaidBy { get; set; }
    }
    public class Receipt : BasicVoucher
    {
        public int ReceiptId { get; set; }

        [Display(Name = "Receipt From ")]
        public new string PartyName { get; set; }

        [Display(Name = "Receipt Slip No ")]
        public string ReceiptSlipNo { get; set; }
    }
    /// <summary>
    /// @Version: 5.0
    /// </summary>
    // Expenses
    public class CashPayment : BaseST
    {
        public int CashPaymentId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }

        public TranscationMode Mode { get; set; }

        [Display(Name = "Paid To"), Required]
        public string PaidTo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Receipt No")]
        public string SlipNo { get; set; }

        public string Remarks { get; set; }
    }
    /// <summary>
    /// @Version: 5.0
    /// </summary>
    public class CashReceipt : BaseST
    {
        public int CashReceiptId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime InwardDate { get; set; }

        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }

        public virtual TranscationMode Mode { get; set; }

        [Display(Name = "Receipt From"), Required]
        public string ReceiptFrom { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Receipt No")]
        public string SlipNo { get; set; }

        public string Remarks { get; set; }
    }
    public class Payment : BasicVoucher
    {
        public int PaymentId { get; set; }

        [Display(Name = "Paid To")]
        public new string PartyName { get; set; }

        [Display(Name = "Payment Slip No")]
        public string PaymentSlipNo { get; set; }
    }
    public class TranscationMode
    {
        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }

        //[Index(IsUnique = true)]
        [Display(Name = "Transaction Mode")]
        public string Transcation { get; set; }

        //public virtual ICollection<CashReceipt> CashReceipts { get; set; }
        //public virtual ICollection<CashPayment> CashPayments { get; set; }
    }
}


