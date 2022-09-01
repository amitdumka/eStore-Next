namespace AKS.AccountingSystem.Helpers
{
    public static class VoucherStatic
    {
        public static string GenerateVoucherNumber(VoucherType type, DateTime onDate, string StoreCode, int count)
        {
            string vNumber = "";

            switch (type)
            {
                case VoucherType.Payment:
                    vNumber = $"{StoreCode}/PYM/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                case VoucherType.Receipt:
                    vNumber = $"{StoreCode}/RCT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                case VoucherType.Contra:
                    break;

                case VoucherType.DebitNote:
                    break;

                case VoucherType.CreditNote:
                    break;

                case VoucherType.JV:
                    break;

                case VoucherType.Expense:
                    vNumber = $"{StoreCode}/EXP/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                case VoucherType.CashReceipt:
                    vNumber = $"{StoreCode}/PCT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                case VoucherType.CashPayment:
                    vNumber = $"{StoreCode}/CPT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                default:
                    break;
            }

            return vNumber;
        }
    }
}