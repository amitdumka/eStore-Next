namespace AKS.Payroll.Forms.Inventory.Functions
{
    public class SaleUtils
    {
        /// <summary>
        /// Get Count for id
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string INCode(int count)
        {
            string a = "";
            if (count < 10) a = $"000{count}";
            else if (count >= 10 && count < 100) a = $"00{count}";
            else if (count >= 100 && count < 1000) a = $"0{count}";
            else a = $"{count}";
            return a;
        }
        public static decimal BasicRateCalucaltion(decimal mrp, decimal taxRate)
        {
            return (100 * mrp / (100 + taxRate));
        }

        /// <summary>
        /// Helper function if missing
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string[] EnumList(Type t)
        {
            return Enum.GetNames(t);
        }

        public static int SetTaxRate(ProductCategory category, decimal Price)
        {
            int rate = 0;
            switch (category)
            {
                case ProductCategory.Fabric:
                    rate = 5;
                    break;

                case ProductCategory.Apparel:
                    rate = Price > 999 ? 12 : 5;
                    break;

                case ProductCategory.Accessiories:
                    rate = 12;
                    break;

                case ProductCategory.Tailoring:
                    rate = 5;
                    break;

                case ProductCategory.Trims:
                    rate = 5;
                    break;

                case ProductCategory.PromoItems:
                    rate = 0;
                    break;

                case ProductCategory.Coupons:
                    rate = 0;
                    break;

                case ProductCategory.GiftVouchers:
                    rate = 0;
                    break;

                case ProductCategory.Others:
                    rate = 18;
                    break;

                default:
                    rate = 5;
                    break;
            }
            return rate;
        }

        public static decimal TaxCalculation(decimal mrp, decimal taxRate)
        {
            return mrp - (100 * mrp / (100 + taxRate));
        }
    }


}