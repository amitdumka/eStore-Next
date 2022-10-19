using AKS.Shared.Commons.Models.Base;
using AKS.Shared.Commons.Models.Sales;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Shared.Commons.Models.Inventory
{
    [Table("V1_Products")]
    public class ProductItem
    {
        [Key]
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StyleCode { get; set; }
        public TaxType TaxType { get; set; }
        public decimal MRP { get; set; }
        public Size Size { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string SubCategory { get; set; }

        public string? ProductTypeId { get; set; }
        public string HSNCode { get; set; }
        public Unit Unit { get; set; }

        public string BrandCode { get; set; }

        [ForeignKey("BrandCode")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("SubCategory")]
        public virtual ProductSubCategory ProductSubCategory { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
    public class Category
    {
        [Key]
        public string Name { get; set; }
    }
    public class ProductType
    {
        [Key]
        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
    }
    public class ProductSubCategory
    {
        [Key]
        public string SubCategory { get; set; }
        //public string CategoryName { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }

    public class ProductStock
    {
        [Key]
        public string Barcode { get; set; }
        [Key]
        public string StoreId { get; set; }
        [Key]
        public decimal CostPrice { get; set; }

        public decimal PurhcaseQty { get; set; }
        public decimal SoldQty { get; set; }
        public decimal HoldQty { get; set; }

        public decimal MRP { get; set; }

        public Unit Unit { get; set; }

        [DefaultValue(false)]
        public bool MultiPrice { get; set; }

        public decimal CurrentQty { get { return (PurhcaseQty - SoldQty - HoldQty); } }
        public decimal CurrentQtyWH { get { return (PurhcaseQty - SoldQty); } }
        public decimal StockValue { get { return (CurrentQty * CostPrice); } }
        public decimal StockValueWH { get { return (CurrentQtyWH * CostPrice); } }

        [ForeignKey("Barcode")]
        public virtual ProductItem Product { get; set; }
        public virtual Store Store { get; set; }
    }

    [Table("V1_Stocks")]
    public class Stock : BaseST
    {
        [Key]
        public string Barcode { get; set; }

        public decimal PurhcaseQty { get; set; }
        public decimal SoldQty { get; set; }
        public decimal HoldQty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MRP { get; set; }
        public Unit Unit { get; set; }

        [DefaultValue(false)]
        public bool MultiPrice { get; set; }

        public decimal CurrentQty { get { return (PurhcaseQty - SoldQty - HoldQty); } }
        public decimal CurrentQtyWH { get { return (PurhcaseQty - SoldQty); } }
        public decimal StockValue { get { return (CurrentQty * CostPrice); } }
        public decimal StockValueWH { get { return (CurrentQtyWH * CostPrice); } }

        [ForeignKey("Barcode")]
        public virtual ProductItem Product { get; set; }
    }

    [Table("V1_PurchaseProducts")]
    public class PurchaseProduct : BaseST
    {
        [Key]
        public string InwardNumber { get; set; }
        public DateTime InwardDate { get; set; }

        public string InvoiceNo { get; set; }
        public string VendorId { get; set; }

        public PurchaseInvoiceType InvoiceType { get; set; }
        public TaxType TaxType { get; set; }
        public DateTime OnDate { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public int Count { get; set; }
        public decimal BillQty { get; set; }
        public decimal FreeQty { get; set; }
        public decimal TotalQty { get; set; }
        public bool Paid { get; set; }

        public string Warehouse { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchaseItem> Items { get; set; }
    }
    [Table("V1_PurchaseItems")]
    public class PurchaseItem
    {
        public int Id { get; set; }

        [ForeignKey("PurchaseProduct")]
        public string InwardNumber { get; set; }
        [ForeignKey("Product")]
        public string Barcode { get; set; }
        public decimal Qty { get; set; }
        public decimal FreeQty { get; set; }
        public decimal CostPrice { get; set; }
        public Unit Unit { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CostValue { get; set; }

        [ForeignKey("InwardNumber")]
        public virtual PurchaseProduct PurchaseProduct { get; set; }
        [ForeignKey("Barcode")]
        public virtual ProductItem ProductItem { get; set; }

    }

    [Table("V1_Vendors")]
    public class Vendor : BaseST
    {
        [Key]
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public VendorType VendorType { get; set; }
        public DateTime OnDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
    }

    [Table("V1_ProductSales")]
    public class ProductSale : BaseST
    {
        [Key]
        public string InvoiceCode { get; set; }

        public string InvoiceNo { get; set; }
        public DateTime OnDate { get; set; }
        public InvoiceType InvoiceType { get; set; }

        public decimal BilledQty { get; set; }
        public decimal FreeQty { get; set; }
        public decimal TotalQty { get { return BilledQty + FreeQty; } }

        public decimal TotalMRP { get; set; }
        public decimal TotalDiscountAmount { get; set; }

        public decimal TotalBasicAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }

        public decimal RoundOff { get; set; }
        public decimal TotalPrice { get; set; }
        // Manual Bill which is taxed or it become regular bill
        public bool Taxed { get; set; }
        public bool Adjusted { get; set; }

        public virtual ICollection<SaleItem> Items { get; set; }
        public bool Paid { get; set; }

        public string SalesmanId { get; set; }
        public bool Tailoring { get; set; }
        public virtual Salesman Salesman { get; set; }

    }
    [Table("V1_SaleItems")]
    public class SaleItem
    {
        public int Id { get; set; }

        public string InvoiceCode { get; set; }

        public string Barcode { get; set; }

        public decimal BilledQty { get; set; }
        public decimal FreeQty { get; set; }
        public Unit Unit { get; set; }

        public decimal DiscountAmount { get; set; }

        //Amount Without Tax
        public decimal BasicAmount { get; set; }
        //Tax on Total Amount(Total Tax {Vat/IGST/CGST+SGST})
        public decimal TaxAmount { get; set; }
        // Total Amount With Basic+total Tax
        public decimal Value { get; set; }
        // Type of Tax , Vat/IGST/ GST in case of local
        public TaxType TaxType { get; set; }

        // Type of Invoice like Regular or manual  => Sale return 
        public InvoiceType InvoiceType { get; set; }

        public bool Adjusted { get; set; }
        public bool LastPcs { get; set; }

        [ForeignKey("InvoiceCode")]
        public virtual ProductSale ProductSale { get; set; }
        [ForeignKey("Barcode")]
        public virtual ProductItem ProductItem { get; set; }
    }

    [Table("V1_SalePaymentDetails")]
    public class SalePaymentDetail
    {
        public int Id { get; set; }
        public string InvoiceCode { get; set; }
        public decimal PaidAmount { get; set; }
        public PayMode PayMode { get; set; }
        public string RefId { get; set; }
        public virtual ProductSale ProductSale { get; set; }
    }
    [Table("V1_CardPaymentDetails")]
    public class CardPaymentDetail
    {
        public int Id { get; set; }
        public string InvoiceCode { get; set; }
        public decimal PaidAmount { get; set; }
        public Card Card { get; set; }
        public CardType CardType { get; set; }
        public int CardLastDigit { get; set; }
        public int AuthCode { get; set; }
        public string? EDCTerminalId { get; set; }
        public virtual EDCTerminal PosMachine { get; set; }
    }

    [Table("V1_Brands")]
    public class Brand
    {
        [Key]
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
    }

    [Table("V2_Suppliers")]
    public class Supplier
    {
        [Key]
        public string SupplierName { get; set; }
        public string Warehouse { get; set; }
    }

    public class CustomerSale
    {
        [Key]
        public string InvoiceCode { get; set; }
        public string MobileNo { get; set; }
        [ForeignKey("MobileNo")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("InvoiceCode")]
        public virtual ProductSale Sale { get; set; }

    }

}
