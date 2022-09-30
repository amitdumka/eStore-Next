﻿using System;
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
    public class Meeting
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsAllDay { get; set; }
        public string EventName { get; set; }
        public TimeZoneInfo StartTimeZone { get; set; }
        public TimeZoneInfo EndTimeZone { get; set; }
        public Brush Background { get; set; }
    }
}
