/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.Shared.Commons.Models.Inventory;

namespace AKS.PosSystem.DataModels
{
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
}