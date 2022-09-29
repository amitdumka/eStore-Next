namespace eStore.MAUILib.Helpers
{
    public static class AutoGen
    {
        /// <summary>
        /// Voucher/CashVoucher ID Generation based on Date, Store and Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="onDate"></param>
        /// <param name="StoreCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GenerateVoucherNumber(VoucherType type, DateTime onDate, string StoreCode, int count)
        {
            string vNumber = "";
            int c = count + 1;

            switch (type)
            {
                case VoucherType.Payment:
                    vNumber = $"{StoreCode}/PYM/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.Receipt:
                    vNumber = $"{StoreCode}/RCT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.Contra:
                    vNumber = $"{StoreCode}/CNT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.DebitNote:
                    vNumber = $"{StoreCode}/DBN/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.CreditNote:
                    vNumber = $"{StoreCode}/CRN/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.JV:
                    vNumber = $"{StoreCode}/JNV/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.Expense:
                    vNumber = $"{StoreCode}/EXP/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.CashReceipt:
                    vNumber = $"{StoreCode}/PCT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                case VoucherType.CashPayment:
                    vNumber = $"{StoreCode}/CPT/{onDate.Year}/{onDate.Month}/{onDate.Day}/{c}";
                    break;

                default:
                    break;
            }

            return vNumber;
        }

        /// <summary>
        /// Generate Generic ID for Today/Now
        /// </summary>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        public static string GenerateIDNow(string storeCode)
        {
            return $"{storeCode}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
        }

        /// <summary>
        /// Generate Genric ID based on Date
        /// </summary>
        /// <param name="storeCode"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GenerateID(string storeCode, DateTime dateTime)
        {
            return $"{storeCode}/{dateTime.Year}/{dateTime.Month}/{dateTime.Day}";
        }

        /// <summary>
        /// Generate Genric ID based on Date and Count
        /// </summary>
        /// <param name="storeCode"></param>
        /// <param name="dateTime"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GenerateID(string storeCode, DateTime dateTime, int count)
        {
            return $"{storeCode}/{dateTime.Year}/{dateTime.Month}/{dateTime.Day}/{count}";
        }
    }
 
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