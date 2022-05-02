/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: User Controller for User controls
 */
using DiseaseMIS.BAL.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DiseaseMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [AuthorizePermission("User Management")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Method to get logged user
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("loggedUser")]
        public IActionResult GetLoggedUser()
        {
            return Ok(_userService.User);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Method to get user by username
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <returns></returns>

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            return Ok(await _userService.GetUser(userId));
        }

        [HttpPost("toggleStatus/{userId}")]
        public async Task<IActionResult> ToggleUserStatus(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));
            return Ok(await _userService.ToggleStatus(userId));
        }

    }
}
