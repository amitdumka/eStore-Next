using System;
namespace AKS.Shared.Commons.Data.Helpers.Auth
{
    public sealed class AuthHelper
    {

        public static Permission GetPermission(UserType type)
        {
            switch (type)
            {
                case UserType.Admin: return Permission.RWMD;
                case UserType.Owner: return Permission.RWMD;
                case UserType.Accountant: return Permission.RWM;
                case UserType.StoreManager: return Permission.RW;
                case UserType.PowerUser: return Permission.RWM;
                case UserType.CA: return Permission.R;
                case UserType.Sales: return Permission.S;
                case UserType.Employees: return Permission.S;
                case UserType.Guest: return Permission.N;
                default:
                    return Permission.N;

            }
        }
        public static string GetPermissionString(UserType type)
        {
            switch (type)
            {
                case UserType.Admin: return "RWMD";
                case UserType.Owner: return "RWMD";
                case UserType.Accountant: return "RWM";
                case UserType.StoreManager: return "RW";
                case UserType.PowerUser: return "RWM";
                case UserType.CA: return "R";
                case UserType.Sales: return "S";
                case UserType.Employees: return "S";
                case UserType.Guest: return "N";
                default:
                    return "N";

            }
        }

        public static string GetPermission(Permission perm)
        {
            return Enum.GetName(typeof(Permission), perm); 
        }
    }
}

