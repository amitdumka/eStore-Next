using AKS.Shared.Commons.Models.Base;
using AKS.Shared.Payroll.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Shared.Commons.Models.Accounts
{

    //public static string GenerateVocherNumber(CashCVoucherType vocher, int StoreId)
    //{
    //    var StoreCode = " JHC0006"; // Fetch from DataBase;
    //    string TypeName = vocher.ToString();
    //    string VoucherNumber = $"{StoreCode}/{TypeName}/{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}";
    //    int count = 0;
    //    //count=(_context.Vochers.Where(c=>StoreId==StoreId && c.OnDate.Date==DateTime.Today.Date && c.VoucherType==vocher).Count())+1;
    //    string countName = "";
    //    if (count > 0 && count < 10) countName = "000" + count;
    //    else if (count > 9 && count < 100) countName = "00" + count;
    //    else if (count > 99 && count < 1000) countName = "0" + count;
    //    else countName = "" + count;

    //    return (VoucherNumber + countName);
    //}



    public class Voucher : BaseST
    {
        [Key]
        public string VoucherNumber { get; set; }
        public VoucherType VoucherType { get; set; }
        public DateTime OnDate { get; set; }
        public string SlipNumber { get; set; }

        public string PartyName { get; set; }

        public string Particulars { get; set; }

        public decimal Amount { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string PaymentDetails { get; set; }
        public string Remarks { get; set; }
        public string AccountId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string PartyId { get; set; }
        public virtual Party Partys { get; set; }
    }

    [Table("V1_TranscationModes")]
    public class TranscationMode
    {
        [Key]
        public string TranscationId { get; set; }
        public string TranscationName { get; set; }
    }

    public class CashVoucher : BaseST
    {

        [Key]
        public string VoucherNumber { get; set; }
        public VoucherType VoucherType { get; set; }
        public DateTime OnDate { get; set; }


        public string TranscationId { get; set; }

        [ForeignKey("TranscationId")]
        public virtual TranscationMode TranscationMode { get; set; }

        public string SlipNumber { get; set; }
        public string PartyName { get; set; }
        public string Particulars { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string PartyId { get; set; }
        public virtual Party Partys { get; set; }

    }
    [Table("V1_Notes")]
    public class Note : BaseST
    {
        [Key]
        public string NoteNumber { get; set; }
        public NotesType NotesType { get; set; }
        public DateTime OnDate { get; set; }

        public string PartyName { get; set; }
        public bool WithGST { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get { return (Amount * (TaxRate / 100)); } }
        public decimal NetAmount { get { return Amount + TaxAmount; } }
        public string Reason { get; set; }
        public string Remarks { get; set; }

        public string PartyId { get; set; }
        public virtual Party Party { get; set; }
    }

    public class LedgerGroup
    {
        [Key]
        public string LedgerGroupId { get; set; }
        public string GroupName { get; set; }
        public LedgerCategory Category { get; set; }
        public string Remark { get; set; }


    }
    [Table("V1_Parties")]
    public class Party : BaseST
    {
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public LedgerCategory Category { get; set; }
        public string GSTIN { get; set; }
        public string PANNo { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public string LedgerGroupId { get; set; }
    }
    [Table("V1_LedgerMasters")]
    public class LedgerMaster
    {
        [Key]
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public DateTime OpeningDate { get; set; }
    }

    [Table("V1_PettyCashSheets")]
    public class PettyCashSheet
    {
        public string Id { get; set; }
        public DateTime OnDate { get; set; }
        //Balance
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }

        //Bank
        public decimal BankDeposit { get; set; }
        public decimal BankWithdrawal { get; set; }

        //Sale
        public decimal DailySale { get; set; }

        public decimal TailoringSale { get; set; }
        public decimal TailoringPayment { get; set; }

        //Different Sale
        public decimal ManualSale { get; set; }
        public decimal CardSale { get; set; }
        public decimal NonCashSale { get; set; }

        public decimal CustomerDue { get; set; }
        public string DueList { get; set; }
        public decimal CustomerRecovery { get; set; }
        public string RecoveryList { get; set; }

        public string ReceiptsNaration { get; set; }
        public decimal ReceiptsTotal { get; set; }

        public string PaymentNaration { get; set; }
        public decimal PaymentTotal { get; set; }

    }
}
