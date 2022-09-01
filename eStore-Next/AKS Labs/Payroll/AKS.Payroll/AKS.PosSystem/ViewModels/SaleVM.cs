/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

using AKS.PosSystem.DataModels;
using AKS.Shared.Commons.Models.Inventory;

namespace AKS.PosSystem.ViewModels
{
    #region NotImplemented

    public class SaleVM
    {
        //TODO: To be implemented
    }

    public class ProductSaleViewModel : ViewModel<ProductSale, ProductSaleDataModel>
    {
        public override bool InitViewModel()
        {
            throw new NotImplementedException();
        }
    }

    public class SaleItemViewModel : ViewModel<SaleItem, SaleItemDataModel>
    {
        public override bool InitViewModel()
        {
            throw new NotImplementedException();
        }
    }

    public class ProductSaleItemViewModel : ViewModel<ProductSale, SaleItem>
    {
        public override bool InitViewModel()
        {
            throw new NotImplementedException();
        }
    }

    #endregion NotImplemented
}