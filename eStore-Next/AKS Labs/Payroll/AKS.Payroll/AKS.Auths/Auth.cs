using AKS.Payroll.Database;
using AKS.Shared.Commons.Ops;

namespace AKS.Auths
{
    public class Auth
    {
        public static bool IsLoggedIn() { return CurrentSession.IsLoggedIn; }
        public static bool DoLogin(string username, string password, AzurePayrollDbContext db)
        {
            var f = db.Users.Where(c => c.UserName == username && c.Password == password && c.Enabled).FirstOrDefault();
            if (f != null)
            {
                var st = db.Stores.Find(f.StoreId);
                CurrentSession.StoreName = "Aprajita Retails";
                CurrentSession.StoreCode = f.StoreId;
                CurrentSession.UserName = username;
                CurrentSession.GuestName = f.GuestName;
                CurrentSession.UserType = f.UserType;
                CurrentSession.LoggedTime = DateTime.Now;
                CurrentSession.Address = st.City + ", " + st.State;
                CurrentSession.TaxNumber = st.GSTIN;
                CurrentSession.CityName = st.City;
                CurrentSession.PhoneNo = st.StorePhoneNumber;
                CurrentSession.IsLoggedIn = true;
                return true;
            }
            return false;
        }

        public static void DoLogout()
        {

            CurrentSession.IsLoggedIn = false;
            CurrentSession.StoreName = "";
            CurrentSession.StoreCode = "";
            CurrentSession.UserName = "";
            CurrentSession.GuestName = "";
            CurrentSession.UserType = UserType.Guest;
            CurrentSession.LoggedTime = DateTime.Now;
            CurrentSession.Address = "";
            CurrentSession.TaxNumber = "";
            CurrentSession.CityName = "";
            CurrentSession.PhoneNo = "";

        }

        public string Login(string username, string password) { return "OK"; }
        public void Logout() { }
        public static string GetLogin() { return "OK"; }

        public void AddUser(string username, string password) { }
        public void DeleteUser(string username, string password) { }
        public void ChangePassword(string username, string password) { }


        public void GetUserInfo(string username, string password) { }
    }
    public class RoleManager
    {
        public static void SetRole() { }
        public static void GetRole() { }
        public static void IsAuthorized() { }
        public static bool IsAuthenticated() { return true; }
    }
}