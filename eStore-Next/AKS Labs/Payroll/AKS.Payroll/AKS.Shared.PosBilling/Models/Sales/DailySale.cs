using AKS.Shared.Commons.Models.Base;
using AKS.Shared.Payroll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AKS.Shared.Commons.Models.Sales
{
    [Table("V1_DailySales")]
    public class DailySale:BaseST
    {
        [Key]
        public string InvoiceNumber { get; set; }
        public DateTime OnDate { get; set; }
        public decimal Amount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal NonCashAmount { get; set; }
        public PayMode PayMode { get; set; }
        public string SalesmanId { get; set; }
        
        public bool IsDue { get; set; }

        public bool ManualBill { get; set; }
        public bool SalesReturn { get; set; }
        public bool TailoringBill { get; set; }
        public string Remarks { get; set; }
        
        public string? EDCTerminalId { get; set; }
        public virtual EDCTerminal EDC { get; set; }
        public virtual Salesman Saleman { get; set; }

    }
    //TODO: Move to VM
    public class DailySaleVM : BaseST
    {
        [Key]
        public string InvoiceNumber { get; set; }
        public DateTime OnDate { get; set; }
        public decimal Amount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal NonCashAmount { get; set; }
        public PayMode PayMode { get; set; }
        public string SalemanName { get; set; }
        
        public bool IsDue { get; set; }
        public bool ManualBill { get; set; }
        public bool SalesReturn { get; set; }
        public bool TailoringBill { get; set; }
        public string Remarks { get; set; }
        public string TerminalName { get; set; }
        public string StoreName { get; set; }

        public EntryStatus EntryStatus { get; set; }
        public string StoreId { get; set; }
        public string SalesmanId { get; set; }       
        public string? EDCTerminalId { get; set; }
        

    }

    [Table("V1_EDCMachine")]
    public class EDCTerminal:BaseST
    {
        public string EDCTerminalId { get; set; }
        public string Name { get; set; }
        public DateTime OnDate { get; set; }
        public string TID { get; set; }
        public string MID { get; set; }
        public string BankId { get; set; }
        public string ProviderName { get; set; }
        public DateTime? CloseDate { get; set; }
        public  bool Active { get; set; }
    }

    [Table("V1_CustomerDues")]
    public class CustomerDue:BaseST
    {
        [Key]
        public string InvoiceNumber { get; set; }
        public DateTime OnDate { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
        public DateTime? ClearingDate { get; set; }
    }
    public class DueRecovery : BaseST
    {
        public string Id { get; set; }
        public DateTime OnDate { set; get; }
        public string InvoiceNumber { set; get; }
        public decimal Amount { set; get; }
        public PayMode PayMode { get; set; }
        public string Remarks { get; set; }
        public bool ParticialPayment { get; set; }
        public virtual CustomerDue Due { get; set; }
        
        public static string GenerateId(string inv,DateTime onDate)
        {
            return $"DR/{onDate.Year}/{onDate.Month}/{onDate.Day}/{inv}/";
        }

    }

    //public class Saleman
    //{
    //    public string SalemanId { get; set; }
    //    public string EmployeeId { get; set; }
    //    public string Name { get; set; }
    //    public bool Active { get; set; }
    //    public string StoreId { get; set; }
    //    public bool MarkedDeleted { get; set; }
    //    public virtual Store Store { get; set; }
    //    public virtual Employee Employee { get; set; }

    //}

    
}
