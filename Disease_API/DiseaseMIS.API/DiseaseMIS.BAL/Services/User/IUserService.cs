/*Author: Gautam Sharma
 * Created Date: 15 July 2021
 *Desc: Interface for User Management*/
// Modified by Gautam -- Added Claims modification

using DiseaseMIS.BAL.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Gets the loggedUser
        /// Gautam Edit
        /// </summary>
        ApplicationUser User { get; }

        // Added by Gautam 
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Gets User by Username
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="_userManager">Identity UserManager Service</param>
        /// <returns></returns>
        Task<ApplicationUser> GetUser(string userId, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Returns the list of users
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetUsers(CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Get User by PhoneNumber
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Change User Status - IsActive; Toggles True/False
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_userManager"></param>
        /// <returns></returns>
        Task<ServiceResult> ToggleStatus(string id, CancellationToken ct = default);
    }
}
