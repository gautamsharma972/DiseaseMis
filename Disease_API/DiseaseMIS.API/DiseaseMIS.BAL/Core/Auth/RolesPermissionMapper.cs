/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: Role Class to map roles from Db.
 */

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DiseaseMIS.BAL.Core
{
    public class RolesPermissionMapper
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Mapper Id
        /// </summary>
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary> 
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Role Mapping
        /// </summary>
        public IdentityRole Roles { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Permission Mapping
        /// </summary>
        public ModulePermission Permissions { get; set; }
    }
}
