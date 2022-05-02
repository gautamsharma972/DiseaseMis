namespace DiseaseMIS.BAL.Core
{
    // Added by Gautam
    public class CustomClaimTypes
    {
        public const string Permission = "Application.Permission";
    }
    public static class Permissions
    {
        // Roles
        public const string SuperAdmin = "SibinAdmin";
        public const string Admin = "Admin";
        public const string Billers = "Billers";

        // Modules Permission
        // Add Other Modules here. 
        public const string UserManagementRead = "UserManagement.View";
        public const string UserManagementWrite = "UserManagement.Write";
        public const string UserManagementExecute = "UserManagement.Execute";
    }
}
