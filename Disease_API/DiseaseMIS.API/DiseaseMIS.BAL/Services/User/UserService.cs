using DiseaseMIS.BAL.Configurations;
using DiseaseMIS.BAL.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services.User
{
    public class UserService : IUserService, IDisposable
    {
        private readonly ApplicationDbContext _db;
        private IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
            _userManager = userManager;
        }

        private ApplicationUser _user;

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Gets the loggedUser
        /// Gautam Edit
        /// </summary>
        public ApplicationUser User
        {
            get
            {
                var currentUser = _user ?? _db.Users.SingleOrDefault(a => a.Id ==
              MetaDataHelper.GetLoggedInUserId<ApplicationUser>(ClaimsPrincipal.Current).Id.ToString());
                if (currentUser == null)
                    currentUser = new ApplicationUser() { UserName = "Need to Login" };
                return currentUser;
            }
        }



        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Get User details by Username
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="_userManager"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetUser(string userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserRoles = (List<string>)await _userManager.GetRolesAsync(user);
            return user;
        }

        public async Task<List<ApplicationUser>> GetUsers(CancellationToken ct = default)
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                user.UserRoles = (List<string>)await _userManager.GetRolesAsync(user);
            }
            return users;

        }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Get User details by Username
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="_userManager"></param>
        /// <returns>User's object by Phone Number</returns>
        public Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber, CancellationToken ct = default)
        {
            return _db.Users.SingleOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

        }

        public async Task<ServiceResult> ToggleStatus(string userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResult("No user found")
                {
                    Errors = new List<string>
                    {
                        $"No user found with Email => {userId}"
                    }
                };
            }
            user.IsActive = !user.IsActive;
            var result = await _userManager.UpdateAsync(user);
            return new ServiceResult
            {
                Errors = result.Succeeded ? null : result.Errors.Select(a => a.Description).ToList(),
                Data = result.Succeeded ? user : null
            };
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
