using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string Version = "v1";
        public const string Rule = root+"/"+Version+"/";

        public static class PositionRouting
        {
            public const string Prefix = Rule+ "Positions";
            public const string List = Prefix+"";
            public const string Paginated = Prefix+ "";
            public const string ById = Prefix + ""+ "/{id}";
            public const string Create = Prefix + "";
            public const string Update = Prefix + "/{id}";
            public const string Delete = Prefix + "/{id}";
        }
        public static class LeaveTypeRouting
        {
            public const string Prefix = Rule+ "LeaveTypes";
            public const string List = Prefix + "";
            public const string Paginated = Prefix + "";
            public const string ById = Prefix + "" + "/{id}";
            public const string Create = Prefix + "";
            public const string Update = Prefix + "/{id}";
            public const string Delete = Prefix + "/{id}";
        }
  
        public static class DepartmentRouting
        {
            public const string Prefix = Rule+ "Departments";
            public const string List = Prefix + "";
            public const string Paginated = Prefix + "";
            public const string ById = Prefix + "" + "/{id}";
            public const string Create = Prefix + "";
            public const string Update = Prefix + "/{id}";
            public const string Delete = Prefix + "/{id}";
        }
        //public static class DepartmentRouting
        //{
        //    public const string Prefix = Rule+ "Department";
        //    public const string List = Prefix+"/List";
        //public const string Paginated = Prefix + "/Paginated";
        //    public const string ById = Prefix + "/ById"+ "/{id}";
        //    public const string Create = Prefix + "/Create";
        //    public const string Update = Prefix + "/Update";
        //    public const string Delete = Prefix + "/Delete"+ "/{id}";
        //}

        public static class UserRouting
        {
            public const string Prefix = Rule+ "User";
            public const string List = Prefix+"/List";
            public const string Paginated = Prefix + "/Paginated";
            public const string ById = Prefix + "/ById"+ "/{id}";
            public const string Create = Prefix + "/Create";
            public const string CurrentUser = Prefix + "/CurrentUser";
            public const string Update = Prefix + "/Update";
            public const string ChangePassword = Prefix + "/Change-Password";
            public const string LockUser = Prefix + "/Lock-User" + "/{id}";
            public const string UnLockUser = Prefix + "/UnLock-User" + "/{id}";
            public const string Delete = Prefix + "/Delete"+ "/{id}";
        }
        public static class AccountRouting
        {
            public const string Prefix = Rule+ "Account";
            public const string List = Prefix+"/List";
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/Refresh-Token";
            public const string ValidateToken = Prefix + "/Validate-Token";
            public const string ConfirmEmail = Prefix + "/ConfirmEmail";
            public const string SendResetPasswordCode = Prefix + "/SendResetPasswordCode";
            public const string ConfirmResetPassword = Prefix + "/ConfirmResetPassword";
            public const string SendResetPasswordLink = Prefix + "/SendResetPasswordLink";
            public const string ResetPasswordLink = Prefix + "/ResetPasswordLink";
            public const string ResetPassword = Prefix + "/ResetPassword";
            public const string ById = Prefix + "/ById"+ "/{id}";
            public const string Create = Prefix + "/Create";
            public const string Update = Prefix + "/Update";
            public const string ChangePassword = Prefix + "/Change-Password";
            public const string LockUser = Prefix + "/Lock-User" + "/{id}";
            public const string UnLockUser = Prefix + "/UnLock-User" + "/{id}";
            public const string Delete = Prefix + "/Delete"+ "/{id}";
        }
        public static class RoleRouting
        {
            public const string Prefix = Rule+ "Role";
            public const string PrefixClaims = Rule+ "Claims";
            public const string List = Prefix+"/List";
            public const string ById = Prefix + "/ById"+ "/{id}";
            public const string Create = Prefix + "/Create";
            public const string Update = Prefix + "/Update";
            public const string Delete = Prefix + "/Delete"+ "/{id}";
            public const string ManageUserClaims = PrefixClaims + "/Manage-User-Claims" + "/{id}";
            public const string UpdateUserClaims = PrefixClaims + "/Update-User-Claims";
        }
        public static class EmailRouting
        {
            public const string Prefix = Rule + "Email";
            public const string SendEmail = Prefix + "/SendEmail";
          
        }
    }
}
