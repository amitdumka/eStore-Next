/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */
using System.ComponentModel.DataAnnotations;

namespace AKS.PosSystem.Models.VM
{
    //List of ViewModels
    public class CustomerListVM
    {
        public string CustomerName { get; set; }

        [Key]
        public string MobileNo { get; set; }
    }
    public class SaleReportVM
    {
        public decimal BillQty { get; set; }
        public decimal FreeQty { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int Month { get; set; }
        public bool Tailoing { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalMRP { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalTax { get; set; }
        public int Year { get; set; }
    }
    public class SaleItemVM
    {
        public string Barcode { get; set; }
        public string ProductItem { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
    }
    public class PaymentDetail
    {
        public string Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public PayMode Mode { get; set; }
        public string RefNumber { get; set; }
        public Card? Card { get; set; }
        public CardType? CardType { get; set; }
        public string AuthCode { get; set; }
        public int LastFour { get; set; }
        public string? PosMachineId { get; set; }
    }
    public class StockInfo
    {
        public string Barcode { get; set; }
        public ProductCategory Category { get; set; }
        public decimal HoldQty { get; set; }
        public string ProductItem { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxRate { get; set; }
        public TaxType TaxType { get; set; }
        public Unit Unit { get; set; }
    }
}
