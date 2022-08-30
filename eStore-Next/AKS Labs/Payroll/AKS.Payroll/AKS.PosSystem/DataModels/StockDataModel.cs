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
}