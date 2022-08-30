/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace AKS.PosSystem.DataModels
{
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
}