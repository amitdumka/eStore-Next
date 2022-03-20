using System;
namespace eStore.Shared.Models.Accounting.Vouchers
{
	public class Voucher:BaseModel
	{
		public int VoucherId { get; set; }

		[Key]
		public string VoucherNumber { get; set; }
		public VoucherType VoucherType { get; set; }
		public DateTime OnDate { get; set; }
		public decimal Amount { get; set; }
		public string PartyName { get; set; }
		public PayMode PayMode { get; set; }
		public string Remarks { get; set; }

		public bool IsPrinted { get; set; }
		
	}

	
}

