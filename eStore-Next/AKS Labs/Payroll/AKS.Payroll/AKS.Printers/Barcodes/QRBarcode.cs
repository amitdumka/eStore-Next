using IronBarCode;

//using PDFtoPrinter;

namespace AKS.Printers.Barcodes
{
    public class QRBarcode
    {
        public static GeneratedBarcode GenerateBarCode(string invNo)
        {
            try
            {
                Directory.CreateDirectory(@"d:\arp\barcode\");
                GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(invNo, BarcodeEncoding.Code93);
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
            GeneratedBarcode Qrcode = QRCodeWriter
                .CreateQrCode($"InvNo:{invNo} On {onDate.ToString()} of Rs. {value}/-");
            Qrcode.SaveAsPng($@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
            return Qrcode;
        }
        public static string BarCodePNG(string invNo)
        {
            Directory.CreateDirectory(@"d:\arp\barcode\");
            GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(invNo, BarcodeEncoding.Code128);
            barcode.SaveAsPng($@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");
            return $@"d:\arp\barcode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png";
        }
        public static string QRCodePng(string invNo, DateTime onDate, decimal value)
        {
            Directory.CreateDirectory(@"d:\arp\QRCode\");
            GeneratedBarcode Qrcode = QRCodeWriter
                .CreateQrCode($"InvNo:{invNo} On {onDate.ToString()} of Rs. {value}/-");
            Qrcode.SaveAsPng($@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png");

            return $@"d:\arp\QRCode\{invNo.Replace("\\", "-").Replace("/", "-").ToString()}.png";
        }
    }
}
