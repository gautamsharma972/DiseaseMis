/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: User Input class to hold ui values
 */
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core
{
    public class UserEditInput
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
    }

    public class SelectedModules
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Module Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Name of Module
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Status holding Selected?
        /// </summary>
        public bool Checked { get; set; }

        public virtual ICollection<SelectedOperations> ModuleOperations { get; set; }
    }

    public class SelectedOperations
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Operation ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Operation Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Operation Checked 
        /// </summary>
        public bool Checked { get; set; }

        /// <summary> 
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// List of permissions user holds
        /// </summary>
        public virtual ICollection<SelectedPermissions> Permissions { get; set; }
    }

    public class SelectedPermissions
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Status Check
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Permimssion Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Actual Permission
        /// </summary>
        public string Permissions { get; set; }
    }
}
