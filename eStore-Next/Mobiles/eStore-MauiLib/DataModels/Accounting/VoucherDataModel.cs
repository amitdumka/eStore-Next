using System;
using AKS.MAUI.Databases;
using AKS.Shared.Commons.Models;
using AKS.Shared.Commons.Models.Accounts;
using eStore_MauiLib.DataModels.Base;
using Microsoft.EntityFrameworkCore;

namespace eStore_MauiLib.DataModels.Accounting
{
    public class VoucherDataModel : Base.BaseDataModel<Voucher, CashVoucher, Note>
    {
        public VoucherDataModel(ConType conType) : base(conType)
        {
        }

        public VoucherDataModel(ConType conType, Permission role) : base(conType, role)
        {
        }

        #region IDGen
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
        #endregion

        public override async Task<bool> InitContext()
        {
            return Connect();
        }


        #region Vouchers
        protected override List<Voucher> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        protected override async Task<List<Voucher>> GetItemsAsync(string storeid)
        {
            if (Permissions.Contains("R"))
            {
                var db = GetContext();
                return await db.Vouchers.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                                  .OrderByDescending(c => c.OnDate)
                                  .ToListAsync();

            }
            IsError = true;
            ErrorMsg = "Access Deninde";
            return null;
        }
        #endregion
        #region YearList
        protected override List<int> GetYearList(string storeid)
        {
            var db = GetContext();
            return db.Vouchers.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        protected override List<int> GetYearList()
        {
            var db = GetContext();
            return db.Vouchers.Select(c => c.OnDate.Year).Distinct().ToList();

        }
        protected override Task<List<int>> GetYearListY(string storeid)
        {
            var db = GetContext();
            return db.CashVouchers.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();

        }

        protected override Task<List<int> >GetYearListY()
        {
            var db = GetContext();
            return db.CashVouchers.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        protected override Task<List<int>> GetYearListZ(string storeid)
        {
            var db = GetContext();
            return db.Notes.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        protected override Task<List<int>> GetYearListZ()
        {
            var db = GetContext();
            return db.Notes.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }
        #endregion


        #region CashVouchers
        protected override Task<List<CashVoucher>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        protected override async Task<List<CashVoucher>> GetYItems(string storeid)
        {
            if (Permissions.Contains("R"))
            {
                var db = GetContext();
              return await db.CashVouchers.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                    .OrderByDescending(c => c.OnDate).ToListAsync();

            }
            IsError = true;
            ErrorMsg = "Access Deninde";
            return null;
        }

        #endregion
        #region Notes
        protected override Task<List<Note>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Note>> GetZItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}

