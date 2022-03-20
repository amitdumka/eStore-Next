using System;
namespace eStore.Shared.Models.Stores
{
    /// <summary>
    /// Version:7.0
    /// </summary>
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
    public class Store
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
    }

    /// <summary>
    /// @Version: 7.0
    /// </summary>
    public class Salesman : BaseModel
    {
        public int SalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public string SalesmanName { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }

    /// <summary>
    /// Version: 7.0
    /// </summary>
    public class Customer
    {
        public int CustomerId { set; get; }

        [Display(Name = "First Name")]
        public string FirstName { set; get; }

        [Display(Name = " Last Name")]
        public string LastName { set; get; }

        public int Age { set; get; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string City { set; get; }

        [Display(Name = "Contact No")]
        public string MobileNo { set; get; }

        public Gender Gender { set; get; }

        [Display(Name = "Bill Count")]
        public int NoOfBills { set; get; }

        [Display(Name = "Purchase Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalAmount { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual ICollection<RegularInvoice> Invoices { get; set; }
    }

    public class Contact
    {
        public int ContactId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [EmailAddress]
        [Display(Name = "eMail")]
        public string? EMailAddress { get; set; }

        [Display(Name = "Notes")]
        public string Remarks { get; set; }
    }
}

