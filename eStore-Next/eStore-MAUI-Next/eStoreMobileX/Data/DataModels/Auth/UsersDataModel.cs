using AKS.Shared.Commons.Models.Auth;
using eStoreMobileX.Core.Database;
using eStoreMobileX.Data.DataModels.Base;
using eStoreMobileX.Data.RemoteServer;
using Microsoft.EntityFrameworkCore;

namespace eStoreMobileX.Data.DataModels.Auth
{
    public class UsersDataModel : LocalDataModel<User>
    {
        public UsersDataModel(): base(ConType.Local)
        {
            
        }
        public UsersDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<User>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<User>> GetItems(int storeid)
        {
            using (_context = new AppDBContext()) return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Verify Login with password If successful, return user else return null;
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User SigIn(string userName, string password)
        {
            //TODO: based on Connection Type it should query database.

            using (_context = new AppDBContext())
            {
                var user = _context.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
                // TODO: Set Logged In User Info so it can be used across the app. 

                if (user != null) return user; else return null;
            }
        }

        /// <summary>
        /// Verify Login with password
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool DoLogin(string UserName, string password)
        {
            using (_context = new AppDBContext())
            {
                var user = _context.Users.Where(c => c.UserName == UserName && c.Password == password).FirstOrDefault();
                // TODO: Set Logged In User Info so it can be used across the app. 

                if (user != null) return true; else return false;
            }
        }

        internal Task<List<User>> GetItems()
        {
            throw new NotImplementedException();
        }

        public bool PasswordChange(string userName, string oldPassword, string newPassword)
        {
            var user = _context.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();
            if (user != null)
            {
                // TODO: Send info that old password is not matched or user not found!.
                user.Password = newPassword;
                _context.Update(user);
                return _context.SaveChanges() > 0;
            }
            else return false;

        }
        public bool SignUp(User user)
        {
            return Save(user, true).Result;
        }


        public async Task<bool> SyncWithServer()
        {
            RemoteSingleServer server = new RemoteSingleServer("", "User");
            var users = await server.GetByUrl<List<User>>(WebAPI.APIBase + WebAPI.Users);
            foreach (var user in users)
            {
                user.UserId = 0; //TODO: remove is leagace.

            }
            _context.Users.AddRange(users);
            return _context.SaveChanges() > 0;

        }

        public override async Task<List<User>> GetItems(string storeid)
        {
            using (_context = new AppDBContext()) return await _context.Users.ToListAsync();
        }
    }

}
