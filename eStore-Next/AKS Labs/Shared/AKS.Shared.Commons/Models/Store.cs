
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
}