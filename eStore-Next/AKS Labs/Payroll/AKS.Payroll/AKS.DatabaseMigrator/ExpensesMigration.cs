using System;
using AKS.Shared.Commons.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AKS.DatabaseMigrator
{
    public class ExpensesMigration
    {

        public static void MigrateParty()
        {
            using AKSDbContext aKSDb = new AKSDbContext();
            using eStoreDbContext eStoreDb = new eStoreDbContext();
            var ledgerTypes = eStoreDb.LedgerTypes.ToList();
            var parties = eStoreDb.Parties.ToList();

            foreach (var ledgerType in ledgerTypes)
            {
                LedgerGroup ledgerGroup = new LedgerGroup
                {
                    Remark = ledgerType.Remark,
                    Category = ledgerType.Category,
                    GroupName = ledgerType.LedgerNameType,
                    LedgerGroupId = $"ARD/LG/{ledgerType.LedgerTypeId}"
                };
                aKSDb.LedgerGroups.Add(ledgerGroup);
            }
            int saved = aKSDb.SaveChanges();

            foreach (var p in parties)
            {
                Party party = new Party
                {
                    IsReadOnly = true,
                    EntryStatus = EntryStatus.Approved,
                    PartyId = $"ARD/PTY/{p.PartyId}",
                    MarkedDeleted = false,
                    StoreId = "ARD",
                    PartyName = p.PartyName,
                    OpeningDate = p.OpenningDate,
                    OpeningBalance = p.OpenningBalance,
                    ClosingBalance = 0,
                    ClosingDate = null,
                    UserId = "AUTOADMIN",
                    GSTIN = p.GSTNo,
                    PANNo = p.PANNo,
                    Address = p.Address,
                    LedgerGroupId = $"ARD/LG/{p.LedgerTypeId}",
                    Remarks = ledgerTypes.Where(c => c.LedgerTypeId == p.LedgerTypeId).First().Remark,
                    Category = ledgerTypes.Where(c => c.LedgerTypeId == p.LedgerTypeId).First().Category,

                };
                LedgerMaster ledger = new LedgerMaster
                {
                    OpeningDate = p.OpenningDate.Date,
                    PartyId = party.PartyId,
                    PartyName = p.PartyName
                };
                aKSDb.Parties.Add(party); aKSDb.LedgerMasters.Add(ledger);
            }
            saved = aKSDb.SaveChanges() / 2;


        }

        public static void MigrateExpenses()
        {

            using AKSDbContext aKSDb = new AKSDbContext();
            using eStoreDbContext eStoreDb = new eStoreDbContext();
            var datalist = eStoreDb.Expenses.OrderBy(c => c.OnDate).ToList();
            int count = 0;
            List<bool> Flag = new List<bool>();
            foreach (var exp in datalist)
            {
                Voucher v = new Voucher
                {
                    VoucherType = VoucherType.Expense,
                    OnDate = exp.OnDate,
                    Amount = exp.Amount,
                    EmployeeId = exp.EmployeeId.ToString(),
                    EntryStatus = EntryStatus.Approved,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    PartyId = exp.PartyId > 0 ? $"ARD/PTY/{exp.PartyId}" : "",

                    PartyName = exp.PartyName,
                    PaymentDetails = exp.PaymentDetails,
                    PaymentMode = exp.PayMode,
                    Remarks = exp.Remarks,
                    AccountId = exp.BankAccountId.ToString(),
                    UserId = exp.UserId,
                    StoreId = "ARD",
                    VoucherNumber = $"ARD/EXP/{exp.OnDate.Year}/{exp.OnDate.Month}/{exp.OnDate.Day}/{exp.ExpenseId}",
                    SlipNumber = "NA",
                    Particulars = exp.Particulars
                };
                count++;
                aKSDb.Vouchers.Add(v);
            }
            int saved = aKSDb.SaveChanges();

            if (saved != count)
            {
                Flag.Add(false);
            }
            else { Flag.Add(true); }
            datalist.Clear();
            // Payment
            var payments = eStoreDb.Payments.OrderBy(c => c.OnDate).ToList();
            count = 0;
            foreach (var exp in payments)
            {
                Voucher v = new Voucher
                {
                    VoucherType = VoucherType.Expense,
                    OnDate = exp.OnDate,
                    Amount = exp.Amount,
                    EntryStatus = EntryStatus.Approved,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    PartyId = exp.PartyId > 0 ? $"ARD/PTY/{exp.PartyId}" : "",
                    PartyName = exp.PartyName,
                    PaymentDetails = exp.PaymentDetails,
                    PaymentMode = exp.PayMode,
                    Remarks = exp.Remarks,
                    AccountId = exp.BankAccountId.ToString(),
                    UserId = exp.UserId,
                    StoreId = "ARD",
                    VoucherNumber = $"ARD/PYM/{exp.OnDate.Year}/{exp.OnDate.Month}/{exp.OnDate.Day}/{exp.PaymentId}",
                    SlipNumber = exp.PaymentSlipNo,
                    Particulars = "NA",
                    EmployeeId = ""

                };
                count++;
                aKSDb.Vouchers.Add(v);
            }
            saved = aKSDb.SaveChanges();

            if (saved != count)
            {
                Flag.Add(false);
            }
            else { Flag.Add(true); }
            payments.Clear();

            //receipts

            var receipts = eStoreDb.Receipts.OrderBy(c => c.OnDate).ToList();
            count = 0;
            foreach (var exp in receipts)
            {
                Voucher v = new Voucher
                {
                    VoucherType = VoucherType.Expense,
                    OnDate = exp.OnDate,
                    Amount = exp.Amount,
                    EntryStatus = EntryStatus.Approved,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    PartyId = exp.PartyId>0?$"ARD/PTY/{exp.PartyId}":"",
                    PartyName = exp.PartyName,
                    PaymentDetails = exp.PaymentDetails,
                    PaymentMode = exp.PayMode,
                    Remarks = exp.Remarks,
                    AccountId = exp.BankAccountId.ToString(),
                    UserId = exp.UserId,
                    StoreId = "ARD",
                    VoucherNumber = $"ARD/RCT/{exp.OnDate.Year}/{exp.OnDate.Month}/{exp.OnDate.Day}/{exp.ReceiptId}",
                    SlipNumber = exp.ReceiptSlipNo,
                    Particulars = exp.PartyName + "\t #" + exp.Remarks,
                    EmployeeId = ""

                };
                count++;
                aKSDb.Vouchers.Add(v);
            }
            saved = aKSDb.SaveChanges();

            if (saved != count)
            {
                Flag.Add(false);
            }
            else { Flag.Add(true); }
            receipts.Clear();


        }
    
    
        public static bool MigrarteCashExpense()
        {
            using AKSDbContext aKSDb = new AKSDbContext();
            using eStoreDbContext eStoreDb = new eStoreDbContext();
            
            var tm= eStoreDb.TranscationModes.ToList();
            foreach (var t in tm)
            {
                TranscationMode m = new TranscationMode {
                TranscationName=t.Transcation, TranscationId=$"TM/{t.TranscationModeId}"
               
                }; 
                aKSDb.TranscationModes.Add(m);
            }
            aKSDb.SaveChanges();

            var cashPayments = eStoreDb.CashPayments.OrderBy(c => c.PaymentDate).ToList();
            var cashrecpt = eStoreDb.CashReceipts.OrderBy(c => c.InwardDate).ToList();
            int count = 0;
            foreach (var cash in cashPayments)
            {
                CashVoucher voucher = new CashVoucher { 
                Amount = cash.Amount, EmployeeId="", EntryStatus=EntryStatus.Approved, 
                 IsReadOnly=true, MarkedDeleted=false, OnDate=cash.PaymentDate,
                 PartyName=cash.PaidTo, Remarks=cash.Remarks, SlipNumber=cash.SlipNo, 
                 StoreId="ARD", PartyId="", TranscationId=$"TM/{cash.TranscationModeId}",
                 UserId=cash.UserId,VoucherType=VoucherType.CashPayment, 
                 VoucherNumber=$"ARD/CPT/{cash.PaymentDate.Year}/{cash.PaymentDate.Month}/{cash.PaymentDate.Day}/{cash.CashPaymentId}", 
                 Particulars=tm.Where(c=>c.TranscationModeId==cash.TranscationModeId).First().Transcation,
                }; 
                aKSDb.CashVouchers.Add(voucher);
                count++;
            }

            int saved= aKSDb.SaveChanges();
             
            foreach (var cash in cashrecpt)
            {
                CashVoucher voucher = new CashVoucher
                {
                    Amount = cash.Amount,
                    EmployeeId = "",
                    EntryStatus = EntryStatus.Approved,
                    IsReadOnly = true,
                    MarkedDeleted = false,
                    OnDate = cash.InwardDate,
                    PartyName = cash.ReceiptFrom,
                    Remarks = cash.Remarks,
                    SlipNumber = cash.SlipNo,
                    StoreId = "ARD",
                    PartyId = "",
                    TranscationId = $"TM/{cash.TranscationModeId}",
                    UserId = cash.UserId,
                    VoucherType = VoucherType.CashReceipt,
                    VoucherNumber = $"ARD/CPT/{cash.InwardDate.Year}/{cash.InwardDate.Month}/{cash.InwardDate.Day}/{cash.CashReceiptId}",
                    Particulars = tm.Where(c => c.TranscationModeId == cash.TranscationModeId).First().Transcation,
                };
                aKSDb.CashVouchers.Add(voucher);
                count++;
            }

              saved+= aKSDb.SaveChanges();
            if (saved == count) return true; else return false;
        }
    
    }
}

