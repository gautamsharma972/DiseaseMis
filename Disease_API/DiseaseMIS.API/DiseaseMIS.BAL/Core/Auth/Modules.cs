/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: Modules class model
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiseaseMIS.BAL.Core
{
    public class Modules
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Unique ID for Modules
        /// </summary>
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Module Name
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds link type like internal, external etc.
        /// </summary>
        public string LinkType { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds link 
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds icon name 
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds display order of the module in menu
        /// </summary>
        public string DisplayOrder { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Not Mapped Property to toggle Checked value but not mapped
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// List of Operations Modules holds
        /// </summary>
        public virtual ICollection<ModuleOperations> ModuleOperations { get; set; }
    }

    public class ModuleOperations
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Unique ID for Modules
        /// </summary>
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Operation Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Module Name of Operation
        /// </summary>
        public virtual Modules Modules { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 30-08-2021
        /// Property to hold the description for the Operation.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds link type like internal, external etc.
        /// </summary>
        public string LinkType { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds link 
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds icon name 
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// Author : Somnath Roy
        /// Date : 30-Dec-2021
        /// Desc : This property holds display order of the module in menu
        /// </summary>
        public string DisplayOrder { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Property to toggle Checked value but not mapped
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }

        [NotMapped]
        public string ModuleId { get; set; }
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// List of Permissions Operation holds
        /// </summary>
        public virtual ICollection<ModulePermission> Permissions { get; set; }
    }

    public class ModulePermission
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Unique ID for Modules
        /// </summary>
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Permission Defined
        /// </summary>
        public string Permissions { get; set; }

        public string DisplayName { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Description for the Permission
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Operation this Permission holds
        /// </summary>
        public ModuleOperations ModuleOperations { get; set; }

        [NotMapped]
        public string OperationId { get; set; }
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 15-05-2021
        /// Property to toggle Checked value but not mapped
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }
    }
}
