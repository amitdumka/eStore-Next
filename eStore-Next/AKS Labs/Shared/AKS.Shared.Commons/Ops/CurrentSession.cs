﻿

namespace AKS.Shared.Commons.Ops
{
    //TODO: permission , READ, READ WrITE, READ WRITE Delte , Full
    public static class CurrentSession
    {
        public static string StoreCode { get; set; } = "ARD";
        public static string StoreName { get; set; } = "Aprajita Retails";
        public static string CityName { get; set; } = "Dumka";
        public static string Address { get; set; } = "Bhagalpur Road, Dumka";
        public static string TaxNumber { get; set; } = "20AJHP7396P1ZV";
        public static string PhoneNo { get; set; } = "06434-224461";


        public static string UserName { get; set; } = "AuotAdmin";
        public static string GuestName { get; set; } = "Admin";
        public static UserType UserType { get; set; } = UserType.StoreManager;

        public static DateTime LoggedTime { get; set; } = DateTime.Now;

        public static bool IsLoggedIn { get; set; } = false;


    }
    public static class AppConfig
    {
        public static DBType DBType { get; set; } = DBType.Local;

    }

}
