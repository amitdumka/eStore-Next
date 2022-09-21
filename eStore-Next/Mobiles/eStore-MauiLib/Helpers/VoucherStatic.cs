namespace eStore_MauiLib.Helpers
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
                    vNumber = $"{StoreCode}/CON/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                case VoucherType.DebitNote:
                    vNumber = $"{StoreCode}/DBN/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                case VoucherType.CreditNote:
                    vNumber = $"{StoreCode}/CRN/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;

                case VoucherType.JV:
                    vNumber = $"{StoreCode}/JNV/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
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
                    vNumber = $"{StoreCode}/GNV/{onDate.Year}/{onDate.Month}/{onDate.Day}/{count}";
                    break;
            }

            return vNumber;
        }
    }
}