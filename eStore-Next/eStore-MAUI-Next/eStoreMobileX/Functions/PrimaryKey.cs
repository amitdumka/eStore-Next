using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobileX.Functions
{
    //public class PrimaryKey
    //{
    //    /// <summary>
    //    /// Auto Generate Voucher Number
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="vocher"></param>
    //    /// <param name="StoreId"></param>
    //    /// <returns></returns>
    //    public static string GenerateVocherNumber(eStoreDbContext db, VoucherTypes vocher, int StoreId)
    //    {
    //        var StoreCode = " JHC0006"; // Fetch from DataBase;
    //        string TypeName = vocher.ToString();
    //        string VoucherNumber = $"{StoreCode}/{TypeName}/{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}";
    //        int count = 0;
    //        //count=(_context.Vochers.Where(c=>StoreId==StoreId && c.OnDate.Date==DateTime.Today.Date && c.VoucherType==vocher).Count())+1;
    //        string countName = "";
    //        if (count > 0 && count < 10) countName = "000" + count;
    //        else if (count > 9 && count < 100) countName = "00" + count;
    //        else if (count > 99 && count < 1000) countName = "0" + count;
    //        else countName = "" + count;

    //        return (VoucherNumber + countName);
    //    }

    //    /// <summary>
    //    /// Auto Generate Debit/Credit Note Number
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="note"></param>
    //    /// <param name="StoreId"></param>
    //    /// <returns></returns>
    //    public static string GenerateNoteNumber(eStoreDbContext db, NotesType note, int StoreId)
    //    {
    //        var StoreCode = " JHC0006"; // Fetch from DataBase;
    //        string TypeName = note.ToString();
    //        string NoteNumber = $"{StoreCode}/{TypeName}/{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}";
    //        int count = 0;
    //        //count=(_context.Notes.Where(c=>StoreId==StoreId && c.OnDate.Date==DateTime.Today.Date && c.VoucherType==vocher).Count())+1;
    //        string countName = "";
    //        if (count > 0 && count < 10) countName = "000" + count;
    //        else if (count > 9 && count < 100) countName = "00" + count;
    //        else if (count > 99 && count < 1000) countName = "0" + count;
    //        else countName = "" + count;

    //        return (NoteNumber + countName);
    //    }
    //    /// <summary>
    //    /// Auto Generate Invoice Number
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <returns></returns>
    //    public string GenerateInvoiceNumber(InvoiceType type)
    //    {
    //        using var db = new eStoreDbContext();
    //        int count = db.Invoices.Where(c => c.OnDate.Date == DateTime.Today.Date && c.InvoiceType == type).Count();
    //        string zeros = "";
    //        if (count < 10) zeros = "00";
    //        else if (count < 100 && count >= 10) zeros = "0";
    //        string InvoiceNumber = $"JH006IN{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}{zeros}{++count}";
    //        return InvoiceNumber;
    //    }
    //    /// <summary>
    //    /// AutoGenEmpCode
    //    /// </summary>
    //    /// <param name="emp"></param>
    //    /// <param name="count"></param>
    //    /// <returns></returns>
    //    public string GenerateEmployeeId(Employee emp, int count)
    //    {

    //        return ($"{GetEmpShortCode(emp.Category)}{emp.JoiningDate.Year}{GetCountString(count)}");

    //    }
    //    /// <summary>
    //    /// Max Len value should be 5 and Min will 2
    //    /// </summary>
    //    /// <param name="count"></param>
    //    /// <param name="len"></param>
    //    /// <returns></returns>
    //    private string GetCountString(int count, int len = 3)
    //    {
    //        if (len > 5) len = 5;
    //        else if (len < 2) len = 2;
    //        string data = $"{count}";
    //        for (int z = 0; z < len - ("" + count).Length; z++) data = "0" + data;
    //        return data;


    //    }

    //    private string GetEmpShortCode(EmpType cat)
    //    {
    //        switch (cat)
    //        {
    //            case EmpType.Accounts: return "ACC";
    //            case EmpType.HouseKeeping: return "HKP";
    //            case EmpType.Owner: return "OWN";
    //            case EmpType.Salesman: return "SLM";
    //            case EmpType.StoreManager: return "SMN";
    //            case EmpType.Tailors: return "TLR";
    //            case EmpType.TailorMaster: return "TLM";
    //            case EmpType.TailoringAssistance: return "TLA";
    //            case EmpType.Others:
    //            default:
    //                return "EMP";
    //        }
    //    }


    //}
}

