using eStore.Shared.Models.Payroll;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore.Shared.Models
{
    [Table("Stores")]
    public class Store : BaseGT
    {
        public int StoreId { get; set; }

        [Display(Name = "Code")]
        public string StoreCode { get; set; }

        [Display(Name = "Store")]
        public string StoreName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }

        [Display(Name = "Contact")]
        public string PhoneNo { get; set; }

        [Display(Name = "Store Manager")]
        public string StoreManagerName { get; set; }

        [Display(Name = "SM Contact No")]
        public string StoreManagerPhoneNo { get; set; }

        [Display(Name = "PAN No")]
        public string PanNo { get; set; }

        [Display(Name = "GSTIN ")]
        public string GSTNO { get; set; }

        [Display(Name = "Employees Count")]
        public int NoOfEmployees { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Closing Date")]
        public DateTime? ClosingDate { get; set; }

        [Display(Name = "Operative")]
        public bool Status { get; set; }



        public int? CompanyId { get; set; }
        ///public virtual Company Company { get; set; }
    }

    /// <summary>
    /// @Version: 5.0
    /// </summary>

    [Table("Salesmen")]
    public class Salesman : BaseST
    {
        public int SalesmanId { get; set; }

        [Display(Name = "Salesman")]
        public string SalesmanName { get; set; }

        //public virtual ICollection<DailySale> DailySales { get; set; }
        //public virtual ICollection<RegularSaleItem> SaleItems { get; set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
