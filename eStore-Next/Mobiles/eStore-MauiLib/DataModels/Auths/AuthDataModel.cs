using System;
using System.Data;
using AKS.Shared.Commons.Models.Auth;
using eStore_MauiLib.DataModels;

namespace eStore_MauiLib.DataModels.Auths
{
    public class AuthDataModel : BaseDataModel<User>
    {
        public AuthDataModel(ConType conType) : base(conType)
        {
        }

        public override Task<List<User>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<User>> GetItems(int storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<List<User>> GetItems(string storeid)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }

        public User SignIn(string userName, string password)
        {
            User user = null;
            switch (Mode)
            {
                case DBType.Local:
                    user = _localDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
                    break;
                case DBType.Azure:
                    user = _azureDb.Users.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
                    break;
                case DBType.API:
                    break;
                default:
                    break;
            }

            if (user != null) return true; else return false;
        }
    }
}