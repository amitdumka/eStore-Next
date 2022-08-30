/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */
using IronBarCode;

namespace AKS.PosSystem.Helpers
{
    /// <summary>
    /// Sale Utils: Rename to better 
    /// </summary>
    public class SaleUtils
    {
        public static GeneratedBarcode GenerateBarCode(string invNo)
        {
            try
            {
                Directory.CreateDirectory(@"d:\arp\barcode\");
                GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(invNo, BarcodeEncoding.Code93);
                barcode.SaveAsPng($@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
                return barcode;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public static GeneratedBarcode GenerateQRCode(string invNo, DateTime onDate, decimal value)
        {
            Directory.CreateDirectory(@"d:\arp\QRCode\");
            GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter
                .CreateQrCode($"InvNo:{invNo} On {onDate.ToString()} of Rs. {value}/-");
            Qrcode.SaveAsPng($@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
            return Qrcode;
        }
        public static string BarCodePNG(string invNo)
        {
            Directory.CreateDirectory(@"d:\arp\barcode\");
            GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(invNo, BarcodeEncoding.Code128);
            barcode.SaveAsPng($@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
            return $@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png";
        }
        public static string QRCodePng(string invNo, DateTime onDate, decimal value)
        {
            Directory.CreateDirectory(@"d:\arp\QRCode\");
            GeneratedBarcode Qrcode = IronBarCode.QRCodeWriter
                .CreateQrCode($"InvNo:{invNo} On {onDate.ToString()} of Rs. {value}/-");
            Qrcode.SaveAsPng($@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");

            return $@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png";
        }
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
            return Math.Round((100 * mrp / (100 + taxRate)), 2);
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
            return Math.Round(mrp - (100 * mrp / (100 + taxRate)), 2);
        }
    }

}