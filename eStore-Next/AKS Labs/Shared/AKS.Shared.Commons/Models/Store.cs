
using AKS.Shared.Commons.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Shared.Commons.Models
{
    [Table("V1_Stores")]
    public class Store
    {
        [Key]
        public string StoreId { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

        public string StoreManager { get; set; }
        public string StoreManagerContactNo { get; set; }

        public string StorePhoneNumber { get; set; }
        public string StoreEmailId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public string PanNo { get; set; }
        public string GSTIN { get; set; }
        public string VatNo { get; set; }
        public bool MarkedDeleted { get; set; }
    }

    [Table("V1_Salesmen")]
    public class Salesman : BaseST
    {
        [Key]
        public string SalesmanId { get; set; }
        public string Name { get; set; }

        public string StoreId { get; set; }
        public string EmployeeId { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("V1_Customers")]
    public class Customer
    {
        [Key]
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerName { get { return (FirstName + " " + LastName); } }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public int NoOfBills { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OnDate { get; set; }
    }
    [Table("V1_CashDetails")]
    public class CashDetail : BaseST
    {
        [Key]
        public string CashDetailId { get; set; }
        public DateTime OnDate { get; set; }
        public int Count { get; set; }
        public int TotalAmount { get; set; }

        public int N2000 { get; set; }
        public int N1000 { get; set; }
        public int N500 { get; set; }
        public int N200 { get; set; }
        public int N100 { get; set; }
        public int N50 { get; set; }
        public int N20 { get; set; }
        public int N10 { get; set; }

        public int C10 { get; set; }
        public int C5 { get; set; }
        public int C2 { get; set; }
        public int C1 { get; set; }

    }
}