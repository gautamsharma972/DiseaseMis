/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: Permission Mapper for User
 */
using System.ComponentModel.DataAnnotations;

namespace DiseaseMIS.BAL.Core
{
    public class UserPermissionsMapper
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
        /// Permission Mapping
        /// </summary>
        public ModulePermission Permissions { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// User Mapping
        /// </summary>
        public ApplicationUser AppUser { get; set; }
    }
}
