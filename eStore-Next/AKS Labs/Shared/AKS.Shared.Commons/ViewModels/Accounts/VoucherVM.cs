using System.ComponentModel.DataAnnotations;

namespace AKS.Shared.Commons.ViewModels.Accounts
{
    public class VoucherVM  
    {
        [Key]
        public string VoucherNumber { get; set; }
        public VoucherType VoucherType { get; set; }
        public DateTime OnDate { get; set; }
        public string SlipNumber { get; set; }
        public string PartyName { get; set; }
        public decimal Amount { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string PaymentDetails { get; set; }
        public string Remarks { get; set; }
        
        public string AccountId { get; set; }

        public string EmployeeId { get; set; }
        public string StaffName { get; set; }

        public string PartyId { get; set; }
        public string Party { get; set; }
    }


    public class CashVoucherVM  
    {

        [Key]
        public string VoucherNumber { get; set; }
        public VoucherType VoucherType { get; set; }
        public DateTime OnDate { get; set; }
        public string SlipNumber { get; set; }
        public string PartyName { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }

        public string EmployeeId { get; set; }
        public string StaffName { get; set; }

        public string PartyId { get; set; }
        public string Party { get; set; }

    }

    public class NoteVM  
    {
        [Key]
        public string NoteNumber { get; set; }
        public NotesType NotesType { get; set; }
        public DateTime dateTime { get; set; }

        public string PartyName { get; set; }
        public bool WithGST { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get { return (Amount * (TaxRate / 100)); } }
        public decimal NetAmount { get { return Amount + TaxAmount; } }
        public string Reason { get; set; }
        public string Remarks { get; set; }

        public string PartyId { get; set; }
        public string Party { get; set; }
        public string StoreName { get; set; }
        
    }

    public class PartyVM  
    {
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public LedgerCategory Category { get; set; }
        public string StoreName { get; set; }
    }

    
}
