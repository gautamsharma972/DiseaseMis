/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: Role Class to map roles from Db.
 */
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiseaseMIS.BAL.Core
{
    public class RolesInputModel
    {
        /// <summary> 
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Role Id Property
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Role Name Property
        /// </summary>
        [Required(ErrorMessage = "RoleName is required")]
        public string Name { get; set; }

        /// <summary> 
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// List of Modules Role holds
        /// </summary>
        public List<SelectedModules> Modules { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Property to render Check
        /// </summary>
        public bool Checked { get; set; }

        public List<ModulePermission> Permissions { get; set; }
    }

}
