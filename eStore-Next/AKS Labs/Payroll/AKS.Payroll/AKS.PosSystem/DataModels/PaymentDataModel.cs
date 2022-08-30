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
}