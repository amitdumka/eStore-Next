using System;
using AKS.Shared.Commons.Models.Accounts;

namespace eStore_MauiLib.DataModels.Accounting
{
	public class VoucherDataModel:BaseDataModel<Voucher,CashVoucher>
	{
        public VoucherDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<Voucher>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<CashVoucher>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Voucher>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Voucher>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

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

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

