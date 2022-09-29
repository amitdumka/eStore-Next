using System;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Sales;
using eStore.MAUILib.DataModels.Base;
using Microsoft.EntityFrameworkCore;

namespace eStore.MAUILib.DataModels.Accounting
{
    public class DailySaleDataModel : BaseDataModel<DailySale, CustomerDue, DueRecovery>
    {
        public DailySaleDataModel(ConType conType) : base(conType)
        {
        }

        public DailySaleDataModel(ConType conType, Permission role) : base(conType, role)
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

        public override List<DailySale> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<DailySale>> GetItemsAsync(string storeid)
        {
            var db = GetContext();
            return await db.DailySales.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                 .OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override List<int> GetYearList(string storeid)
        {
            var db = GetContext();
           return db.DailySales.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override List<int> GetYearList()
        {
            var db = GetContext();
            return db.DailySales.Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override async Task<List<int>> GetYearListY(string storeid)
        {
            var db = GetContext();
            return await db.CustomerDues.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override async Task<List<int>> GetYearListY()
        {
            var db = GetContext();
            return await db.CustomerDues.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override async Task<List<int>> GetYearListZ(string storeid)
        {
            var db = GetContext();
            return await db.DueRecovery.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override async Task<List<int>> GetYearListZ()
        {
            var db = GetContext();
            return await db.DueRecovery.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<CustomerDue>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<CustomerDue>> GetYItems(string storeid)
        {
            var db = GetContext();
            return await db.CustomerDues.Where(c => c.StoreId == storeid && !c.Paid && c.OnDate.Year == DateTime.Today.Year)
                 .OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override Task<List<DueRecovery>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<DueRecovery>> GetZItems(string storeid)
        {
            var db = GetContext();
            return await db.DueRecovery.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                 .OrderByDescending(c => c.OnDate).ToListAsync();
        }

        public override async Task<bool> InitContext()
        {
            return Connect();
        }
    }
}

