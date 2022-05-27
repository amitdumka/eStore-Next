using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobileX.Config
{
    /// <summary>
    /// Application Config 
    /// </summary>
    public static class AppConfig
    {
        public static bool  LoggedIn=false;
        public static string StoreId = "ARD";
        public static DbType DbMode = DbType.Azure;
        public static string UserName=String.Empty;
        public static string Name=String.Empty;
        public static string EmployeeId = String.Empty;
        public static UserType UserType=UserType.Guest;
        public static LoginRole Role=LoginRole.Member;
        public static DateTime LogInTime=DateTime.Now;

    }
}
