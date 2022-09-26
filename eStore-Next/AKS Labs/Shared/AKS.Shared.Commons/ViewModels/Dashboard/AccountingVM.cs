using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Shared.Commons.ViewModels.Dashboard
{
    public class AccountingVM
    {
        public decimal CashReceipt { get; set; }
        public decimal CashPayment { get; set; }
        public decimal Payment { get; set; }
    }
}
