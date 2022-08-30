/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.Payroll.Database;
using AKS.PosSystem.Helpers;
using AKS.PosSystem.Models.VM;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace AKS.PosSystem.DataModels
{

    public class ProductStockDataModel : DataModel<ProductItem, Stock>
    {
        public StockInfo? GetItemDetail(string barcode, bool Tailoring)
        {
            if (barcode.Length < 7) return null;
            if (Tailoring)
            {
                var item = azureDb.ProductItems.Where(c => c.Barcode == barcode)
                    .Select(c => new StockInfo { Barcode = c.Barcode, HoldQty = 1, Qty = 1, Unit = Unit.Nos, TaxRate = 5, TaxType = c.TaxType, Rate = c.MRP, Category = c.ProductCategory })
                    .FirstOrDefault();
                return item;
            }
            else return GetItemDetail(barcode);
        }

        /// <summary>
        /// return stock info. Add to API/DataModel
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public StockInfo? GetItemDetail(string barcode)
        {
            if (barcode.Length < 7)
            {
                return null;
            }
            var item = azureDb.Stocks.Include(c => c.Product).Where(c => c.Barcode == barcode)
                .Select(item =>
           new StockInfo()
           {
               Barcode = item.Barcode,
               HoldQty = item.CurrentQtyWH,
               Qty = item.CurrentQty,
               Rate = item.Product.MRP,
               ProductItem = item.Product.Name,
               TaxType = item.Product.TaxType,
               Unit = item.Product.Unit,
               Category = item.Product.ProductCategory,
               TaxRate = SaleUtils.SetTaxRate(item.Product.ProductCategory, item.Product.MRP)
           }).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Init View Model
        /// </summary>
        /// <returns></returns>

        public override ProductItem Get(string id)
        {
            throw new NotImplementedException();
        }

        public override ProductItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetBarcodeList()
        {
            return azureDb.Stocks.Where(c => c.PurhcaseQty > 0).Select(c => new { c.Barcode, c.CurrentQty, c.CurrentQtyWH })
                    .Where(c => c.CurrentQty > 0).Select(c => c.Barcode).ToList();
        }

        public override List<ProductItem> GetList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Return Stock for barcode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Stock GetY(string id)
        {
            return azureDb.Stocks.Find(id);
        }

        public override Stock GetY(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Stock> GetYList()
        {
            return azureDb.Stocks.ToList();
        }
    }

    public class StockDataModel : DataModel<Stock>
    {
        public override Stock Get(string id)
        {
            throw new NotImplementedException();
        }

        public override Stock Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Stock> GetList()
        {
            throw new NotImplementedException();
        }
    }

    public class ProductItemDataModel : DataModel<ProductItem>
    {
        public override ProductItem Get(string id)
        {
            throw new NotImplementedException();
        }

        public override ProductItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ProductItem> GetList()
        {
            throw new NotImplementedException();
        }
    }

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
            return azureDb.ProductSales.Where(c=>c.StoreId==storeCode).Select(c => c.OnDate.Year).Distinct().OrderByDescending(c => c).ToList();
        }
        public List<int> GetYearList(string storeCode,InvoiceType iType)
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

    public class PaymentDataModel : DataModel<SalePaymentDetail, CardPaymentDetail>
    {
        public override SalePaymentDetail Get(string id)
        {
            throw new NotImplementedException();
        }

        public override SalePaymentDetail Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<SalePaymentDetail> GetList()
        {
            throw new NotImplementedException();
        }

        public override CardPaymentDetail GetY(string id)
        {
            throw new NotImplementedException();
        }

        public override CardPaymentDetail GetY(int id)
        {
            throw new NotImplementedException();
        }

        public override List<CardPaymentDetail> GetYList()
        {
            throw new NotImplementedException();
        }
    }

    public class SaleItemDataModel : DataModel<SaleItem>
    {
        public SaleItemDataModel()
        { }

        public SaleItemDataModel(AzurePayrollDbContext db)
        { }

        public SaleItemDataModel(LocalPayrollDbContext ldb)
        { }

        public SaleItemDataModel(AzurePayrollDbContext db, LocalPayrollDbContext ldb)
        { }

        public override SaleItem Get(string id)
        {
            return azureDb.SaleItems.Find(id);
        }

        public override SaleItem Get(int id)
        {
            return azureDb.SaleItems.Find(id);
        }

        public override List<SaleItem> GetList()
        {
            return azureDb.SaleItems.ToList();
        }

        public SaleItem Get(string id, bool included)
        {
            return azureDb.SaleItems.Include(c => c.ProductSale).Include(c => c.ProductItem).Where(c => c.InvoiceCode == id).FirstOrDefault();
        }

        public SaleItem Get(int id, bool included)
        {
            return azureDb.SaleItems.Include(c => c.ProductSale).Include(c => c.ProductItem).Where(c => c.Id == id).FirstOrDefault();
        }

        public List<SaleItem> GetList(bool included)
        {
            return azureDb.SaleItems.Include(c => c.ProductSale).Include(c => c.ProductItem).ToList();
        }
    }

    public class ProductSaleDataModel : DataModel<ProductSale>
    {
        //TODO: all construstor to suport all kind of Database model;
        public ProductSaleDataModel()
        { }

        public ProductSaleDataModel(AzurePayrollDbContext db)
        { }

        public ProductSaleDataModel(LocalPayrollDbContext ldb)
        { }

        public ProductSaleDataModel(AzurePayrollDbContext db, LocalPayrollDbContext ldb)
        { }

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
    }

    public abstract class DataModel<T, Y, Z> : DataModel<T, Y>
    {
        //View
        public abstract Z GetSeconday(string id);

        public abstract Z GetSeconday(int id);

        public abstract List<Z> GetSecondayList();

        //Create and Update
        public void AddOrUpdate(Z record, bool isNew = true)
        {
            if (isNew)
                azureDb.Add(record);
            else
                azureDb.Update(record);
        }
        public Z? Save(Z record, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.Add(record);
                else
                    azureDb.Update(record);
                int count = azureDb.SaveChanges();
                if (count > 0) return record;
                else return default(Z);
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return default(Z);
            }
        }

        public List<Z> SaveRange(List<Z> records, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.AddRange(records);
                else
                    azureDb.UpdateRange(records);
                int count = azureDb.SaveChanges();
                if (count > 0) return records;
                else return null;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return null;
            }
        }

        //Delete
        public bool Delete(Z record)
        {
            try
            {
                azureDb.Remove(record);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteRange(List<Z> records)
        {
            try
            {
                azureDb.RemoveRange(records);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }
    }

    public abstract class DataModel<T, Y> : DataModel<T>
    {
        //View
        public abstract Y GetY(string id);

        public abstract Y GetY(int id);

        public abstract List<Y> GetYList();

        //Create and Update
        public void AddOrUpdate(Y record, bool isNew = true)
        {
            if (isNew)
                azureDb.Add(record);
            else
                azureDb.Update(record);
        }
        public Y? Save(Y record, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.Add(record);
                else
                    azureDb.Update(record);
                int count = azureDb.SaveChanges();
                if (count > 0) return record;
                else return default(Y);
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return default(Y);
            }
        }

        public List<Y> SaveRange(List<Y> records, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.AddRange(records);
                else
                    azureDb.UpdateRange(records);
                int count = azureDb.SaveChanges();
                if (count > 0) return records;
                else return null;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return null;
            }
        }

        //Delete
        public bool Delete(Y record)
        {
            try
            {
                azureDb.Remove(record);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteRange(List<Y> records)
        {
            try
            {
                azureDb.RemoveRange(records);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }
    }

    public abstract class DataModel<T>
    {
        protected AzurePayrollDbContext azureDb;
        protected LocalPayrollDbContext localDb;

        public DataModel()
        { }

        public DataModel(AzurePayrollDbContext db)
        { }

        public DataModel(LocalPayrollDbContext ldb)
        { }

        public DataModel(AzurePayrollDbContext db, LocalPayrollDbContext ldb)
        { }
        public int SaveChanges() { return azureDb.SaveChanges(); }
        //View
        public abstract T Get(string id);

        public abstract T Get(int id);

        public abstract List<T> GetList();
        public void AddOrUpdate(T record, bool isNew = true)
        {
            if (isNew)
                azureDb.Add(record);
            else
                azureDb.Update(record);
        }
        //Create and Update
        public T? Save(T record, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.Add(record);
                else
                    azureDb.Update(record);
                int count = azureDb.SaveChanges();
                if (count > 0) return record;
                else return default(T);
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return default(T);
            }
        }

        public List<T> SaveRange(List<T> records, bool isNew = true)
        {
            try
            {
                if (isNew)
                    azureDb.AddRange(records);
                else
                    azureDb.UpdateRange(records);
                int count = azureDb.SaveChanges();
                if (count > 0) return records;
                else return null;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return null;
            }
        }

        //Delete
        public bool Delete(T record)
        {
            try
            {
                azureDb.Remove(record);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteRange(List<T> records)
        {
            try
            {
                azureDb.RemoveRange(records);
                return azureDb.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogWrite.LogError(ex.Message);
                return false;
            }
        }

        public int AddorUpdateRecord<TK>(TK obj, bool isNew, bool save = false)
        {
            if (isNew)
                azureDb.Add(obj);
            else azureDb.Update(obj);
            if (save)
                return azureDb.SaveChanges();
            return 0;
        }


    }
}