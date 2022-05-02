using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core
{
    public class RolesViewModel
    {
        public IdentityRole Roles { get; set; }
        public int UsersCount { get; set; }
        public List<ModulePermission> Permissions { get; set; }
    }
}
