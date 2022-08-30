﻿using AKS.Shared.Commons.Models.Accounts;
using AKS.Shared.Commons.ViewModels.Accounts;
using AKS.Shared.Templets.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AKS.AccountingSystem.DataModels
{
    public class VoucherCashDataModel : DataModel<Voucher, CashVoucher>
    {

        public List<Voucher> GetVouchers(VoucherType type, int year, string storeCode)
        {
            return azureDb.Vouchers.Include(c => c.Employee).Include(c => c.Partys).Where(c => c.StoreId == storeCode && c.VoucherType == type && c.OnDate.Year == year).OrderBy(c => c.OnDate).ToList();

        }
        public List<CashVoucher> GetCashVouchers(VoucherType type, int year, string storeCode)
        {

            return azureDb.CashVouchers
                        .Include(c => c.Employee).Include(c => c.Partys).Include(c => c.TranscationMode)
                        .Where(c => c.VoucherType == type && c.OnDate.Year == year && c.StoreId == storeCode).OrderBy(c => c.OnDate).ToList();

        }
        public TranscationMode? GetTranscationMode(string id)
        {
            return azureDb.TranscationModes.Find(id);
        }
        public Party? GetParty(string id)
        {
            return azureDb.Parties.Find(id);
        }
        public Shared.Payroll.Models.Employee? GetEmployee(string id)
        {
            return azureDb.Employees.Find(id);
        }
        public override Voucher Get(string id)
        {
            return azureDb.Vouchers.Find(id);
        }

        public override Voucher Get(int id)
        {
            return azureDb.Vouchers.Find(id);
        }


        public List<Voucher> GetList(VoucherType type)
        {
            return azureDb.Vouchers.Where(c => c.VoucherType == type).ToList();
        }

        public override List<Voucher> GetList()
        {
            return azureDb.Vouchers.ToList();
        }

        public List<int> GetYearList(string storeCode)
        {
            var years = azureDb.Vouchers.Where(c => c.StoreId == storeCode).Select(c => c.OnDate.Year).Distinct().ToList();
            years.AddRange(azureDb.CashVouchers.Where(c => c.StoreId == storeCode).Select(c => c.OnDate.Year).Distinct().ToList());
            years = years.Distinct().OrderBy(c => c).ToList();
            return years;
        }

        public override CashVoucher GetY(string id)
        {
            return azureDb.CashVouchers.Find(id);
        }

        public override CashVoucher GetY(int id)
        {
            return azureDb.CashVouchers.Find(id);
        }

        public List<CashVoucher> GetYList(VoucherType type)
        {
            return azureDb.CashVouchers.Where(c => c.VoucherType == type).ToList();
        }

        public override List<CashVoucher> GetYList()
        {
            return azureDb.CashVouchers.ToList();
        }
    }
}