﻿using AKS.Shared.Commons.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Platform;

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

        #endregion IDGen

        public override async Task<bool> InitContext()
        {
            return Connect();
        }

        #region Vouchers

        public override List<Voucher> GetFiltered(QueryParam query)
        {
            
             //public int Id { get; set; }
             //public string Ids { get; set; }
             //public List<string> Command { get; set; }
             //public List<string> Query { get; set; }
             //public int Order { get; set; }
             //public List<string> Filters { get; set; }
             //public int StoreId { get; set; }


            throw new NotImplementedException();
        }

        public IQueryable<Voucher> WhereO(System.Linq.Expressions.Expression<Func<Voucher,bool>> predict) 
        {
            return GetContext().Vouchers.Where(predict);
            
        }
        public IQueryable<CashVoucher> WhereO(System.Linq.Expressions.Expression<Func<CashVoucher, bool>> predict)
        {
            return GetContext().CashVouchers.Where(predict);

        }
        public IQueryable<Note> WhereO(System.Linq.Expressions.Expression<Func<Note, bool>> predict)
        {
            return GetContext().Notes.Where(predict);

        }

        public override async Task<List<Voucher>> GetItemsAsync(string storeid)
        {
            if (Permissions.Contains("RW"))
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

        #endregion Vouchers

        #region YearList

        public override List<int> GetYearList(string storeid)
        {
            var db = GetContext();
            return db.Vouchers.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override List<int> GetYearList()
        {
            var db = GetContext();
            return db.Vouchers.Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override Task<List<int>> GetYearListY(string storeid)
        {
            var db = GetContext();
            return db.CashVouchers.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<int>> GetYearListY()
        {
            var db = GetContext();
            return db.CashVouchers.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<int>> GetYearListZ(string storeid)
        {
            var db = GetContext();
            return db.Notes.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        public override Task<List<int>> GetYearListZ()
        {
            var db = GetContext();
            return db.Notes.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        }

        #endregion YearList

        #region CashVouchers

        public override Task<List<CashVoucher>> GetYFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<CashVoucher>> GetYItems(string storeid)
        {
            if (Permissions.Contains("RW"))
            {
                var db = GetContext();
                return await db.CashVouchers.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                      .OrderByDescending(c => c.OnDate).ToListAsync();
            }
            IsError = true;
            ErrorMsg = "Access Deninde";
            return null;
        }

        #endregion CashVouchers

        #region Notes

        public override Task<List<Note>> GetZFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Note>> GetZItems(string storeid)
        {
            if (Permissions.Contains("RW"))
            {
                var db = GetContext();
                return   db.Notes.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year)
                      .OrderByDescending(c => c.OnDate).ToListAsync();
            }
            IsError = true;
            ErrorMsg = "Access Deninde";
            return null;
        }

        #endregion Notes
    }
    public class Filter
    {
        public string PropertyName { get; set; }
       // public Op Operation { get; set; }
        public object Value { get; set; }
    }
}