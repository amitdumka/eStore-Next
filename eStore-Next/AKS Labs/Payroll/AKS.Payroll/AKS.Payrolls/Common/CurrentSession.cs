using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Payrolls.Common
{
    public static class CurrentSession
    {
        public static string StoreCode { get; set; }
        public static string StoreName { get; set; }

        public static string UserName { get; set; }
        public static string GuestName { get; set; }
        public static UserType UserType { get; set; }

        public static DateTime LoggedTime { get; set; }


    }
}
