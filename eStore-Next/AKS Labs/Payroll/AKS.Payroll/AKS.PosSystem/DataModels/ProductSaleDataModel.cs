/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.Payroll.Database;
using AKS.Shared.Commons.Models.Inventory;

namespace AKS.PosSystem.DataModels
{
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
}