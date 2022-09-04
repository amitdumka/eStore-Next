using AKS.Shared.Commons.Models.Auth;
using eStoreMobileX.Core.Database;
using eStoreMobileX.Data.DataModels.Base;
using eStoreMobileX.Data.RemoteServer;
using Microsoft.EntityFrameworkCore;
//TODO: remove
namespace eStoreMobileX.Data.DataModels.Auth
{
    /// <summary>
    ///  User Data Model. It is not completed and error free
    /// </summary>
    public class UsersDataModel : HybridDataMode<User>
    {
        //TODO: need to check and verify each operations
        public UsersDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<User>> FindAsync(QueryParam query)
        {

            switch (Mode)
            {
                case DbType.Local:

                    break;
                case DbType.Azure:
                    break;
                case DbType.API:
                    break;
                default:
                    break;
            }
            return null;
        }


        public override async Task<List<User>> GetItems(int storeid)
        {
            switch (Mode)
            {
                case DbType.Local:
                    using (_localDb = new AppDBContext()) return await _localDb.Users.ToListAsync();
                    break;
                case DbType.Azure:
                    using (_localDb = new AppDBContext()) return await _localDb.Users.ToListAsync();
                    break;
                case DbType.API:
                    return null;
                    break;
                default:
                    return null;
                    break;
            }

        }

        /// <summary>
        /// Verify Login with password If successful, return user else return null;
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User SigIn(string userName, string password)
        {
            User user = null;
            switch (Mode)
            {
                case DbType.Local:
                    user = _localDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
                    break;
                case DbType.Azure:
                    user = _azureDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
                    break;
                case DbType.API:
                    break;
                default:
                    break;
            }
            if (user != null) { 
                using(_localDb=new AppDBContext())
                {
                  if(  !_localDb.Users.Any(c=>c.UserName == userName))
                    {
                        _localDb.Users.Add(user);
                        _localDb.SaveChangesAsync();
                    }
                }
                return user; } else return null;
        }

        /// <summary>
        /// Verify Login with password
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool DoLogin(string UserName, string password)
        {
            User user = null;

            switch (Mode)
            {
                case DbType.Local:
                    user = _localDb.Users.Where(c => c.UserName == UserName && c.Password == password).FirstOrDefault();
                    break;
                case DbType.Azure:
                    user = _azureDb.Users.Where(c => c.UserName == UserName && c.Password == password).FirstOrDefault();
                    break;
                case DbType.API:
                    break;
                default:
                    break;
            }

            if (user != null) return true; else return false;

        }

        internal Task<List<User>> GetItems()
        {
            throw new NotImplementedException();
        }

        public bool PasswordChange(string userName, string oldPassword, string newPassword)
        {
            var user = _localDb.Users.Where(c => c.UserName == userName && c.Password == oldPassword).FirstOrDefault();
            if (user != null)
            {
                // TODO: Send info that old password is not matched or user not found!.
                user.Password = newPassword;
                _localDb.Update(user);
                return _localDb.SaveChanges() > 0;
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
            _localDb.Users.AddRange(users);
            return _localDb.SaveChanges() > 0;

        }

        public override async Task<List<User>> GetItems(string storeid)
        {
            using (_localDb = new AppDBContext()) return await _localDb.Users.ToListAsync();
        }
    }

}
