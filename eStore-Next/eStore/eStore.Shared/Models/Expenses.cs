using eStore.Shared.Models.Payroll;
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
        public string? PartyName { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentMode PayMode { get; set; }

        [Display(Name = "From Account")]
        public int? BankAccountId { get; set; }

        //public virtual BankAccount FromAccount { get; set; }

        [Display(Name = "Payment Details")]
        public string? PaymentDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public string? Remarks { get; set; }

        [Display(Name = "Party")]
        public int? PartyId { get; set; }

        [Display(Name = "Leger")]
        public int? LedgerEnteryId { get; set; }

        [DefaultValue(false), Display(Name = "Cash")]
        public bool? IsCash { get; set; }

        [DefaultValue(false), Display(Name = "ON")]
        public bool? IsOn { get; set; }

        [DefaultValue(true), Display(Name = "Dyn")]
        public bool? IsDyn { get; set; }

        //public virtual Party Party { get; set; }
        //public virtual LedgerEntry LedgerEntry { get; set; }
    }
    public class Expense : BasicVoucher
    {
        public int ExpenseId { get; set; }
        public string Particulars { get; set; }

        [Display(Name = "Paid To")]
        public new string? PartyName { get; set; }

        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }

        public virtual Employee PaidBy { get; set; }
    }
    public class Receipt : BasicVoucher
    {
        public int ReceiptId { get; set; }

        [Display(Name = "Receipt From ")]
        public new string? PartyName { get; set; }

        [Display(Name = "Receipt Slip No ")]
        public string? ReceiptSlipNo { get; set; }
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
        public string? PaidTo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Receipt No")]
        public string? SlipNo { get; set; }

        public string? Remarks { get; set; }
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
        public string? ReceiptFrom { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Receipt No")]
        public string? SlipNo { get; set; }

        public string? Remarks { get; set; }
    }
    public class Payment : BasicVoucher
    {
        public int PaymentId { get; set; }

        [Display(Name = "Paid To")]
        public new string? PartyName { get; set; }

        [Display(Name = "Payment Slip No")]
        public string? PaymentSlipNo { get; set; }
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

    public class Party
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OpenningDate { get; set; }

        [Display(Name = "Opening Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OpenningBalance { get; set; }

        public string? Address { get; set; }
        public string? PANNo { get; set; }
        public string? GSTNo { get; set; }

        [Display(Name = "Ledger Group")]
        // public LedgerCategory LedgerType { get; set; }
        public int LedgerTypeId { get; set; }

       // public virtual LedgerType LedgerType { get; set; }
       // public LedgerMaster LedgerMaster { get; set; }
        //public virtual ICollection<LedgerEntry> Ledgers { get; set; }
    }
    public class LedgerMaster
    {
        public int LedgerMasterId { get; set; }

        [ForeignKey("Parties")]
        public int PartyId { get; set; }

        public Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime CreatingDate { get; set; }

        [Display(Name = "Ledger Type")]
        public int LedgerTypeId { get; set; }

       // public virtual LedgerType LedgerType { get; set; }
    }
    //Ok
    public class LedgerType
    {
        public int LedgerTypeId { get; set; }

        [Display(Name = "Name")]
        public string LedgerNameType { get; set; }

        public LedgerCategory Category { get; set; }
        public string? Remark { get; set; }
    }
    public class LedgerEntry
    {
        public int LedgerEntryId { get; set; }

        [Display(Name = "Party Name")]
        public int PartyId { get; set; }

        public virtual Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }

        [Display(Name = "On Account Off")]
        public LedgerEntryType EntryType { get; set; }

        public int ReferanceId { get; set; }

        public VoucherType VoucherType { get; set; }
        public string Particulars { get; set; }

        [Display(Name = "Amount In")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountIn { get; set; }

        [Display(Name = "Amount Out")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountOut { get; set; }

        //Ref of itself for double entry system.
        public int LedgerEntryRefId { get; set; }
    }
}


