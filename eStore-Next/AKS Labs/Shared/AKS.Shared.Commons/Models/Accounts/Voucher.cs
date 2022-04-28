using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

    public class Voucher
    {
        public string VoucherId { get; set; }
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
    }


    public class CashVoucher
    {

        [Key]
        public string VoucherId { get; set; }
        public VoucherType VoucherType { get; set; }
        public DateTime OnDate { get; set; }
        public string SlipNumber { get; set; }
        public string PartyName { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string EmployeeId { get; set; }


    }

    public class Note
    {
        public string NoteId { get; set; }
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



    }
}
