/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.PosSystem.Models.VM;
using AKS.Shared.Commons.Models.Inventory;

namespace AKS.PosSystem.DataModels
{
    public class SaleDataModel : DataModel<ProductSale, SaleItem, SalePaymentDetail>
    {
        public bool CustomerExists(string mobileNo)
        {
            return azureDb.Customers.Any(C => C.MobileNo == mobileNo);
        }

        /// <summary>
        /// Get Customer List
        /// </summary>
        /// <returns></returns>
        public List<CustomerListVM> GetCustomerList()
        {
            return azureDb.Customers.Select(c => new CustomerListVM { MobileNo = c.MobileNo, CustomerName = c.CustomerName }).OrderBy(c => c.MobileNo).ToList();
        }

        /// <summary>
        /// Count of invoice for Particular type of particular month
        /// </summary>
        /// <param name="storeCode"></param>
        /// <param name="iType"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int GetRecordCount(string storeCode, InvoiceType iType, DateTime date)
        {
            return azureDb.ProductSales.Where(c => c.StoreId == storeCode && c.InvoiceType == iType
          && c.OnDate.Year == date.Year && c.OnDate.Month == date.Month).Count();
        }

        public List<int> GetYearList(string storeCode)
        {
            return azureDb.ProductSales.Where(c => c.StoreId == storeCode).Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();
        }

        public List<int> GetYearList(string storeCode, InvoiceType iType)
        {
            return azureDb.ProductSales.Where(c => c.StoreId == storeCode && c.InvoiceType == iType)
                .Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();
        }

        public List<ProductSale> Filters(System.Linq.Expressions.Expression<Func<ProductSale, bool>> filter, System.Linq.Expressions.Expression<Func<ProductSale, DateTime>> order, bool desc = false)
        {
            if (desc)
                return azureDb.ProductSales.Where(filter).OrderByDescending(order)
                    .ToList();
            return azureDb.ProductSales.Where(filter).OrderBy(order)
                .ToList();
        }

        public override SalePaymentDetail GetSeconday(string id)
        {
            throw new NotImplementedException();
        }

        public override SalePaymentDetail GetSeconday(int id)
        {
            throw new NotImplementedException();
        }

        public override List<SalePaymentDetail> GetSecondayList()
        {
            throw new NotImplementedException();
        }

        public override SaleItem GetY(string id)
        {
            throw new NotImplementedException();
        }

        public override SaleItem GetY(int id)
        {
            throw new NotImplementedException();
        }

        public override List<SaleItem> GetYList()
        {
            throw new NotImplementedException();
        }

        public override ProductSale Get(string id)
        {
            throw new NotImplementedException();
        }

        public override ProductSale Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ProductSale> GetList()
        {
            throw new NotImplementedException();
        }

        public List<SaleReportVM> SaleReports(string storeCode, int year, int month, InvoiceType iType)
        {
            var x = azureDb.ProductSales.Where(c => c.StoreId == storeCode
            && c.InvoiceType == iType
            && c.MarkedDeleted == false && !c.Adjusted
            && c.OnDate.Year == year && c.OnDate.Month == month)
           .GroupBy(c => new { c.OnDate.Year, c.InvoiceType, c.Tailoring })
            .Select(c => new SaleReportVM
            {
                Year = year,
                Month = month,
                InvoiceType = c.Key.InvoiceType,
                Tailoing = c.Key.Tailoring,
                BillQty = c.Sum(x => x.BilledQty),
                FreeQty = c.Sum(x => x.FreeQty),
                TotalMRP = c.Sum(x => x.TotalMRP),
                TotalDiscount = c.Sum(x => x.TotalDiscountAmount),
                TotalTax = c.Sum(x => x.TotalTaxAmount),
                TotalPrice = c.Sum(x => x.TotalPrice),
            })
               .ToList();
            return x;
        }
    }
}