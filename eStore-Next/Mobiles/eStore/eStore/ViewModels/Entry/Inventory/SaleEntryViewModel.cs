using System.ComponentModel.DataAnnotations;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Inventory;
using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Editors;
using eStore.MAUILib.DataModels.Inventory;
using eStore.MAUILib.ViewModels.Base;
using eStore.ViewModels.List.Inventory;

namespace eStore.ViewModels.Entry.Inventory
{
    public partial class SaleEntryViewModel : BaseEntryViewModel<ProductSale, SaleDataModel>
    {
        [ObservableProperty]
        private InvoiceType _invoiceType;

        [ObservableProperty]
        private SaleViewModel _viewModel;

        [ObservableProperty]
        private List<SaleItem> _itemList;

        #region InvocieFields

        [ObservableProperty]
        private string _mobileNo;
        [ObservableProperty]
        private string _customerName;
        [ObservableProperty]
        private DateTime _invoiceDate;

        [ObservableProperty]
        private decimal _totalBasicAmount;
        [ObservableProperty]
        private decimal _totalMRPAmount;
        [ObservableProperty]
        private decimal _totalTaxAmount;
        [ObservableProperty]
        private decimal _totalDiscountAmount;
        [ObservableProperty]
        private decimal _billAmount;
        [ObservableProperty]
        private decimal _totalBilledQty;
        [ObservableProperty]
        private decimal _totalFeeQty;

        [ObservableProperty]
        private decimal _totalQty;

        partial void OnTotalBilledQtyChanged(decimal value)
        {
            TotalQty = value + TotalFeeQty;
        }
        partial void OnTotalFeeQtyChanged(decimal value)
        {
            TotalQty = TotalBilledQty+value;
        }
        #endregion

        #region SaleItemEntryFields

        [ObservableProperty]
        private string _barcode = "";

        [ObservableProperty]
        private decimal _qty = 0;

        [ObservableProperty]
        private decimal _rate = 0;

        

        [ObservableProperty]
        private string _discount = "0";

        [ObservableProperty]
        private decimal _discountAmount = 0;

        [ObservableProperty]
        private decimal _lineTotal = 0;

        private decimal _basicRate = 0;
        private decimal _taxRate = 0;
        private decimal _taxAmount = 0;
        private Unit _unit=Unit.Meters;

        private TaxType _taxType=TaxType.GST;

        #endregion

        #region SaleItemEntryOnChanged

        partial void OnBarcodeChanged(string value)
        {
            if (value.Length > 6)
                FetchProductItem(value);
        }
        partial void OnDiscountChanged(string value)
        {
            OnSaleItemDetailChanged();
        }
        partial void OnLineTotalChanged(decimal value)
        {
            // throw new NotImplementedException();
        }
        partial void OnQtyChanged(decimal value)
        {
            OnSaleItemDetailChanged();
        }
        partial void OnRateChanged(decimal value)
        {
            OnSaleItemDetailChanged();
        }


        private void OnSaleItemDetailChanged()
        {
            if (Discount.Contains('%'))
            {
                _discountAmount = decimal.Parse(Discount.Remove('%', ' ').Trim());
                _discountAmount = (Rate * Qty) / (_discountAmount / 100);
            }
            else
            {
                _discountAmount = decimal.Parse(Discount.Remove('%', ' ').Trim());
            }

            LineTotal = ((Rate * Qty) - _discountAmount);
        }


        private void FetchProductItem(string barcode)
        {
            // No Record Found . It shoud show error near Barcode Help/Error Text
        }
        #endregion

        [RelayCommand]
        private void OnDelegateRequested( SuggestionsRequestEventArgs e)
        {
            if (e.Text.Length > 4)
            {
                string t = e.Text;
                if (!e.Text.StartsWith("91"))
                {
                    t = "91" + e.Text;
                }
                e.Request = () =>
                {
                    return DataModel.CustomerWhere(i => i.MobileNo.StartsWith(t, StringComparison.CurrentCultureIgnoreCase)).ToList();
                };
            }
        }
        partial void OnInvoiceTypeChanged(InvoiceType value)
        {
            //Change UI elemnts and Auto ID Generation 

        }

        public SaleEntryViewModel()
        {
        }

        public SaleEntryViewModel(SaleViewModel vm)
        {
            ViewModel = vm;
        }

        protected override void Cancle()
        {
            throw new NotImplementedException();
        }

        protected override void InitViewModel()
        {
            _itemList = new List<SaleItem>();
        }

        protected override void Save()
        {
            throw new NotImplementedException();
        }


        protected void AddCustomer() {

            Customer cust = new Customer
            {
                Age = 30,
                City = CurrentSession.CityName,
                DateOfBirth = DateTime.Today.AddYears(-30),
                FirstName = CustomerName,
                LastName = "",
                Gender = Gender.Male,
                MobileNo = MobileNo,
                NoOfBills = 0,
                OnDate = DateTime.Now,
                TotalAmount = 0
            };
            DataModel.GetContext().Customers.Add(cust);
        }

        [RelayCommand]
        protected void AddSaleItem() {

            SaleItem item = new SaleItem {
                Adjusted=false, Barcode=Barcode, BilledQty=Qty,
               FreeQty=0, DiscountAmount=DiscountAmount, InvoiceType=InvoiceType,
               BasicAmount=_basicRate, TaxType=_taxType, LastPcs=false, TaxAmount=_taxAmount,
               Value=LineTotal, Unit=_unit
            };
            _itemList.Add(item);

        }



    }


    //public class SaleEntry
    //{
    //    public DateTime OnDate { get; set; }
    //    public bool Paid { get; set; }
    //    public string SalesmanId { get; set; }
    //}



    public class ProductSaleEntry
    {
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

    public class SaleItemEntry
    {
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

        public decimal BasicRate { get { return (MRP - Discount) - ((MRP - Discount) - ((MRP - Discount) * (100 / (100 + TaxRate)))); } }

        public decimal TaxRate { get; set; }
        public decimal Discount { get; set; }

        public decimal LineTotal { get { return ((BasicRate * BilledQty) + (BasicRate * BilledQty * TaxRate) / 100); } }

        public static decimal BaiscRate(decimal MRP, decimal TaxRate)
        {
            var BasicRate = MRP - (MRP - (MRP * (100 / (100 + TaxRate))));
            return BasicRate;
        }
    }



}

