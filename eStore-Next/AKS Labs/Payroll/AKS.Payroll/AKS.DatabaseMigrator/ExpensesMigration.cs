using System;
using AKS.Shared.Commons.Models.Accounts;

namespace AKS.DatabaseMigrator
{
	public class ExpensesMigration
	{
		public static void  MigrateExpenses()
        {

			using AKSDbContext aKSDb = new AKSDbContext();
			using eStoreDbContext eStoreDb = new eStoreDbContext();
			var datalist = eStoreDb.Expenses.OrderBy(c => c.OnDate).ToList();
            foreach (var exp in datalist)
            {
				Voucher v = new Voucher {
				VoucherType=VoucherType.Expense, OnDate=exp.OnDate, Amount= exp.Amount,
				EmployeeId=exp.EmployeeId.ToString(), EntryStatus=EntryStatus.Approved, IsReadOnly=true,
				MarkedDeleted=false, PartyId=exp.PartyId.ToString(), PartyName=exp.PartyName,
				PaymentDetails=exp.PaymentDetails, PaymentMode=exp.PayMode,
				Remarks=exp.Remarks, AccountId=exp.BankAccountId.ToString(),
				

				};

            }

        }
	}
}

