using System;
namespace AKS.Shared.Commons.Data.Helpers.Auth
{
    public sealed class AuthHelper
    {

        public static Permission GetPermission(UserType type)
        {
            switch (type)
            {
                case UserType.Admin: return Permission.All;
                case UserType.Owner: return Permission.All;
                case UserType.Accountant: return Permission.ReadWriteModify;
                case UserType.StoreManager: return Permission.ReadWrite;
                case UserType.PowerUser: return Permission.ReadWriteModify;
                case UserType.CA: return Permission.Read;
                case UserType.Sales: return Permission.Self;
                case UserType.Employees: return Permission.Self;
                case UserType.Guest: return Permission.None;
                default:
                    return Permission.None;

            }
        }
    }
}

