using eStoreMobileX.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreMobileX.InitApp
{
    public class AppInit
    {
        //Add DefaultStore.
        //public static bool AddDefaultStore()
        //{
        //    //Store store = new Store
        //    //{
        //    //    StoreCode = "JHC0006",
        //    //    Address = "Bhagalpur Road, Dumka",
        //    //    City = "Dumka",
        //    //    GSTNO = "20AJHPA7396P1ZV",
        //    //    IsReadOnly = false,
        //    //    NoOfEmployees = 3,
        //    //    OpeningDate = new DateTime(2016, 02, 17),
        //    //    PanNo = "AJHPA7396P",
        //    //    PhoneNo = "06434-224461",
        //    //    PinCode = "814101",
        //    //    Status = true,
        //    //    StoreManagerName = "Alok Kumar",
        //    //    StoreName = "Aprajita Retails",
        //    //    StoreManagerPhoneNo = "",
        //    //    UserId = "AutoAdmin"
        //    //};
        //    using var db = new AppDBContext();
        //    //db.Stores.Add(store);
        //    return db.SaveChanges() > 0;
        //}


        //public static bool IsStoreActive()
        //{
        //    using var db = new AppDBContext();
        //    if (db.Stores.Any())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return AddDefaultStore();
        //        // No Store Found Localy.
        //        //Search at server . If not found then Create a demo store
        //        // Which can be changed by default , upload at server also.

        //    }
        //}
    }
}
