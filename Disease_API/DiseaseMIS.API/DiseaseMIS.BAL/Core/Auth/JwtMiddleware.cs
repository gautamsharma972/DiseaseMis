using DiseaseMIS.BAL.Services;
using DiseaseMIS.BAL.Services.User;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMIS.BAL.Core
{
    // Added by Gautam
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context,
            IUserService userService, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, userService, authService, token).ConfigureAwait(false);

            return _next(context);
        }

        private async Task AttachUserToContext(HttpContext context,
            IUserService userService, IAuthService authService, string token)
        {
            try
            {
                var jwtToken = authService.DecodeJwtToken(token);
                var userId = jwtToken.Item1.Identities.
                    Select(a => a.Claims.Select(b => b.Value)).SingleOrDefault();

                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetUser(userId.First());
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
