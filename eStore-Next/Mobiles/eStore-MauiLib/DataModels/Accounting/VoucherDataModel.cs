using System;
using AKS.Shared.Commons.Models.Accounts;

namespace eStore_MauiLib.DataModels.Accounting
{
	public class VoucherDataModel:BaseDataModel<Voucher,CashVoucher,Note>
	{
        public VoucherDataModel(ConType conType) : base(conType)
        {
        }

        /// <summary>
        /// Vouchers 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<Voucher>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashVoucher>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notes
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<Note>> FindZAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vouchers
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Obsolete]
        public override Task<List<Voucher>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vouchers
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<Voucher>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vouchers
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashVoucher>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashVoucher>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notes
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Obsolete]
        public override Task<List<Note>> GetZItems(int storeid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notes
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<Note>> GetZItems(string storeid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Init DB Context
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

