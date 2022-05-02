/*
 * Author: Gautam Sharma
 * Date: 05-05-2021
 * Desc: Authentication service
 */
using DiseaseMIS.BAL.Core;
using DiseaseMIS.BAL.Core.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Dictionary to hold the refresh Token for the logged user.
        /// </summary>
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Generates the token using claims containing permission by Roles of a user.
        /// </summary>
        /// <param name="username">logged in user's username</param>
        /// <param name="claims">logged in user's claims</param>
        /// <param name="now">Current DateTime, used for Token Expiration</param>
        /// <returns>Bearer JWT Auth Access Token</returns>
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Get refresh Token using the expired access Token for a User.
        /// </summary>
        /// <param name="refreshToken">Refresh Token that was generated while generating the Access Tokemn</param>
        /// <param name="accessToken">Access Token of User</param>
        /// <param name="now">Current DateTime, used for Expiration</param>
        /// <returns>Bearer JWT Auth Access Token</returns>
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Remove the all Expired Refresh Token of user by Time
        /// </summary>
        /// <param name="now">Time of Token generated</param>
        void RemoveExpiredRefreshTokens(DateTime now, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Remove refresh Token of a User.
        /// </summary>
        /// <param name="userName"></param>
        void RemoveRefreshToken(string userName, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Verify the JWT Token received from Header of current logged user.
        /// </summary>
        /// <param name="token">JWT Bearer Auth Token</param>
        /// <returns></returns>
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Generate Random OTP for OTP auth.
        /// </summary>
        /// <param name="phoneNumber">Phone number to send OTP on</param>
        /// <param name="userManager">Logged User</param>
        /// <returns></returns>
        Task<ServiceResult> GenerateOTP(string phoneNumber, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Verify OTP, generate JWT access token, get roles, permission and claims for user.
        /// </summary>
        /// <param name="values">OTP value</param>
        /// <param name="headers">Header that contains the OTP session</param>
        /// <param name="userManager">User logged</param>
        /// <param name="roleManager">Role Manager to get the Roles</param>
        /// <returns>JWT Access Token, Roles, Claims and permission of user</returns>
        Task<object> Verify(LoginInputModel values, IHeaderDictionary headers, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Method to return all Roles from Db
        /// </summary>
        /// <param name="role"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<ServiceResult> GetRoles(string role, CancellationToken ct = default);

        /// <summary>
        /// Register a new user, add to roles
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        Task<ServiceResult> Register(UserEditInput model, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Creates Roles
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        Task<ServiceResult> CreateRole(RolesInputModel model, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Edit Role and its permissions
        /// </summary>
        /// <param name="model">Role Input details from Client</param>
        /// <param name="roleManager">Identity RoleManager Service</param>
        /// <returns>Service Result containing the process result.</returns>
        Task<ServiceResult> UpdateRole(RolesInputModel model, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Awaitable method to return all roles with their permissions
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        Task<ServiceResult> GetRoles(CancellationToken ct = default);
        Task<ServiceResult> UpdateUser(UserEditInput userInput, CancellationToken ct = default);
        void Logout(string userId);
        Task ResetPassword(PasswordInputModal passwordInput);
    }
}
