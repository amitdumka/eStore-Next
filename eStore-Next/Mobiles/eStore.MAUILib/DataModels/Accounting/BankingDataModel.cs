using System;
using AKS.Shared.Commons.Models.Banking;
using eStore.MAUILib.DataModels.Base;

namespace eStore.MAUILib.DataModels.Accounting
{
    public class BankingDataModel : BaseDataModel<Bank, BankAccount, BankTranscation>
    {
        public BankingDataModel(ConType conType) : base(conType)
        {
        }

        public BankingDataModel(ConType conType, Permission role) : base(conType, role)
        {
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateYID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateZID()
        {
            throw new NotImplementedException();
        }

        public override List<Bank> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Bank>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY()
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListZ(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListZ()
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccount>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccount>> GetYItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankTranscation>> GetZFiltered(QueryParam query)
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

        public BankInfoDataModel(ConType conType, Permission role) : base(conType, role)
        {
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenrateYID()
        {
            throw new NotImplementedException();
        }

        public override List<BankAccountList> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<BankAccountList>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<int>> GetYearListY()
        {
            throw new NotImplementedException();
        }

        public override Task<List<VendorBankAccount>> GetYFiltered(QueryParam query)
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

