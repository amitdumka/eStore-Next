using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AKS.POSBilling.Commons
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
    public class InternetStatus
    {
        //Creating the extern function...  
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        //Creating a function that uses the API function...  
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
    }
}
