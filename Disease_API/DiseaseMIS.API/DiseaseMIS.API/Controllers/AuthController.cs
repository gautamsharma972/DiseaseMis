using DiseaseMIS.BAL.Core;
using DiseaseMIS.BAL.Core.Auth;
using DiseaseMIS.BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DiseaseMIS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Login Method to log using Phone Number
        /// </summary>
        /// <param name="phoneNumber">Phone Number (10 digit)</param>
        /// <returns></returns>
        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> Login(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException(phoneNumber);

            return Ok(await _authService.GenerateOTP(phoneNumber));
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Verify OTP using Phone Number and OTP sent
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost("Verify")]
        public async Task<IActionResult> VerifyAsync(LoginInputModel values)
        {
            var _result = await _authService.Verify(values, HttpContext.Request.Headers);

            return Ok(_result);
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Logouts the user using their Phone Number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpPost("Logout/{phoneNumber}")]
        public IActionResult Logout(string phoneNumber)
        {
            _authService.Logout(phoneNumber);
            return Ok();
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Registers user using the param define
        /// </summary>
        /// <param name="model">User Registeration Model including user basic details
        /// and permissions</param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserEditInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authService.Register(model));
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Method to add new roles to the site
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("Roles/Create")]
        public async Task<IActionResult> CreateRoles(RolesInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authService.CreateRole(model));
        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Method to Edit Role details using RoleId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPut("Roles/Edit/{id}")]
        public async Task<IActionResult> UpdateRoles(string id, RolesInputModel model)
        {
            if (id == null)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authService.UpdateRole(model));
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _authService.GetRoles());
        }

        [HttpGet("Roles/{role}")]
        public async Task<IActionResult> GetRoles(string role)
        {
            return Ok(await _authService.GetRoles(role));
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserEditInput model)
        {
            if (id == null)
                throw new ArgumentNullException(id);
            return Ok(await _authService.UpdateUser(model));
        }

        [HttpPut("users/{id}/resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string id, PasswordInputModal passwordInput)
        {
            passwordInput.UserId = id;
            await _authService.ResetPassword(passwordInput);
            return Ok();
        }
    }
}
