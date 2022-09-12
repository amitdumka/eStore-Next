using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore_MauiLib.Printers.Thermals
{
    public class InvoicePrint : ThermalPrinter
    {
        public override MemoryStream PrintPdf(bool duplicate, bool print = false)
        {
            throw new NotImplementedException();
        }

        protected override void Content()
        {
            throw new NotImplementedException();
        }

        protected override void DuplicateFooter()
        {
            throw new NotImplementedException();
        }

        protected override void Footer()
        {
            throw new NotImplementedException();
        }

        protected override void QRBarcode()
        {
            throw new NotImplementedException();
        }
    }
}
