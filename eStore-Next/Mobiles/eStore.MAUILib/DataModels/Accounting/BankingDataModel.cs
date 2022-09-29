using System;
using AKS.Shared.Commons.Models.Banking;
using eStore.MAUILib.DataModels.Base;
using Microsoft.EntityFrameworkCore;

namespace eStore.MAUILib.DataModels.Accounting
{
    public partial class BankingDataModel : BaseDataModel<Bank, BankAccount, BankTranscation>
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

        public override async Task<List<Bank>> GetItemsAsync(string storeid)
        {
            var db = GetContext();
            return await db.Banks
                .ToListAsync();
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

        public override async Task<List<BankAccount>> GetYItems(string storeid)
        {
            var db = GetContext();
            return await db.BankAccounts.Where(c => c.StoreId == storeid && c.IsActive)
                .ToListAsync();
        }

        public override Task<List<BankTranscation>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<BankTranscation>> GetZItems(string storeid)
        {
            var db = GetContext();
            return await db.BankTranscations.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                .OrderByDescending(c => c.OnDate)
                .ToListAsync();
        }

        public override async Task<bool> InitContext()
        {
            return Connect();
        }
    }
}

