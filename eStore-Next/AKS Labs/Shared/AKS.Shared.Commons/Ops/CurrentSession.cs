

namespace AKS.Shared.Commons.Ops
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
