using System;
using AKS.Shared.Commons.Models.Auth;
using eStore.MAUILib.DataModels.Base;

namespace eStore.MAUILib.DataModels.Auth
{
    public class AuthDataModel : BaseDataModel<User>
    {
        public AuthDataModel(ConType conType) : base(conType)
        {
        }

        public AuthDataModel(ConType conType, Permission role) : base(conType, role)
        {
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        public override List<User> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }

        public override Task<List<User>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList(string storeid)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetYearList()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> InitContext()
        {
            throw new NotImplementedException();
        }
    }
}

