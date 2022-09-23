using System;
using AKS.Shared.Commons.Models.Banking;

namespace eStore_MauiLib.DataModels.Accounting
{
    public class BankingDataModel : BaseDataModel<Bank, BankAccount, BankTranscation>
    {
        public BankingDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<Bank>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccount>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankTranscation>> FindZAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Bank>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Bank>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccount>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccount>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankTranscation>> GetZItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankTranscation>> GetZItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }


    public class BankInfoDataModel : BaseDataModel<BankAccountList, VendorBankAccount>
    {
        public BankInfoDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<BankAccountList>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<VendorBankAccount>> FindYAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccountList>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccountList>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<VendorBankAccount>> GetYItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<VendorBankAccount>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

