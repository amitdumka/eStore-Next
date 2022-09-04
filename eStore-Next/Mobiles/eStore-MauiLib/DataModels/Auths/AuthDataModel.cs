using System;
using AKS.Shared.Commons.Models.Auth;

namespace eStore_MauiLib.DataModels.Auths
{
	public class AuthDataModel:BaseDataModel<User>
	{
        //public AuthDataModel() => throw NotSupportedException;

        public AuthDataModel(ConType dBType):base(dBType)
        {

        }

        public override Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public override Task<List<User>> FindAsync(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<User> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public override Task<User> GetById(int id)
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

        public override Task<List<User>> GetItems()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }

        public override bool IsExists(string id)
        {
            throw new NotImplementedException();
        }

        public override bool IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<User> Save(User item, bool isNew = true)
        {
            throw new NotImplementedException();
        }
    }
}

