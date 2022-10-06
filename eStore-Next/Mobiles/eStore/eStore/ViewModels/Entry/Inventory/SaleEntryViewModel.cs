using System;
using AKS.Shared.Commons.Models;
using System.ComponentModel.DataAnnotations;
using AKS.Shared.Commons.Models.Inventory;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using eStore.MAUILib.DataModels.Inventory;
using eStore.MAUILib.ViewModels.Base;
using System.ComponentModel.DataAnnotations.Schema;
using AKS.Shared.Commons.Models.Sales;

namespace eStore.ViewModels.Entry.Inventory
{
    public partial class SaleEntryViewModel : BaseEntryViewModel<ProductSale, SaleDataModel>
    {
        [ObservableProperty]
        private InvoiceType _invouceType;

        partial void OnInvouceTypeChanged(InvoiceType value)
        {
            //Change UI elemnts and Auto ID Generation 
            throw new NotImplementedException();
        }

        public SaleEntryViewModel()
        {
        }

        public SaleEntryViewModel(IMessenger messenger) : base(messenger)
        {
        }

        protected override void Cancle()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            throw new NotImplementedException();
        }

        protected override void Save()
        {
            throw new NotImplementedException();
        }
    }


    public  class SaleEntry
    {
        public DateTime OnDate { get; set; }
        public bool Paid { get; set; }
        public string SalesmanId { get; set; }
    }



    public class ProductSaleEntry {
        [Key]
        public string InvoiceCode { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public string InvoiceNo { get; set; }
        public DateTime OnDate { get; set; }

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
     }

    public class SaleItemEntry {
        public int Id { get; set; }
        // Type of Invoice like Regular or manual  => Sale return 
        public InvoiceType InvoiceType { get; set; }
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

        public bool Adjusted { get; set; }
        public bool LastPcs { get; set; }

    }

    public class SalePaymentDetailEntry
    {
        public int Id { get; set; }
        public string InvoiceCode { get; set; }
        public decimal PaidAmount { get; set; }
        public PayMode PayMode { get; set; }
        public string RefId { get; set; }
    }
    
    public class CardPaymentDetailEntry
    {
        public int Id { get; set; }
        public string InvoiceCode { get; set; }
        public decimal PaidAmount { get; set; }
        public Card Card { get; set; }
        public CardType CardType { get; set; }
        public int CardLastDigit { get; set; }
        public int AuthCode { get; set; }
        public string? EDCTerminalId { get; set; }
    }

    public class ItemList
    {
        public string Barcode { get; set; }

        public decimal CurrentStock { get; set; }
        public decimal CurrentHoldStock { get; set; }

        public decimal MRP { get; set; }
        public decimal BilledQty { get; set; }

        public decimal BasicRate { get { return (MRP-Discount) - ((MRP-Discount) - ((MRP-Discount) * (100 / (100 + TaxRate)))); } }

        public decimal TaxRate { get; set; }
        public decimal Discount { get; set; }

        public decimal LineTotal { get { return ((BasicRate*BilledQty)+(BasicRate*BilledQty*TaxRate)/100); } }

        public static decimal BaiscRate(decimal MRP, decimal TaxRate)
        {
            var BasicRate = MRP - ( MRP-(MRP*(100/(100+TaxRate))) );
            return BasicRate;
        }
    }
     


}

