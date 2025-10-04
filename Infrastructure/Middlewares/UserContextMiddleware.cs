using Application.Contexts.Abstractions;
using Domain.Commons;
using Domain.Constants;
using Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserContextMiddleware> _logger;
        public UserContextMiddleware(RequestDelegate next, ILogger<UserContextMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                await HandleUnauthorized(context);
            }
            else
            {
                var identity = context.User.Identity as ClaimsIdentity;
                UserClaims userClaims = new UserClaims
                {
                    User = context.User,
                    Identity = identity,
                    access_key = identity.Claims.Where(c => c.Type == "AccessKey").Select(c => c.Value).SingleOrDefault() ?? string.Empty,
                    fullName = identity.Claims.Where(c => c.Type == "FullName").Select(c => c.Value).SingleOrDefault() ?? string.Empty,
                    userName = identity.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault() ?? string.Empty,
                    userId = int.Parse(identity.Claims.Where(c => c.Type == "UserId").Select(c => c.Value).SingleOrDefault() ?? "0"),
                    userMapId = int.Parse(identity.Claims.Where(c => c.Type == "UserMapId").Select(c => c.Value).SingleOrDefault() ?? "0"),
                    roleMax = int.Parse(identity.Claims.Where(c => c.Type == "RoleMax").Select(c => c.Value).SingleOrDefault() ?? "9999"),
                    type = int.Parse(identity.Claims.Where(c => c.Type == "Type").Select(c => c.Value).SingleOrDefault() ?? "9999"),
                };

                if (userClaims.userId != null)
                {
                    await HandleCheckRoleUser(context, userClaims);
                }
                else
                {
                    await HandleUnauthorized(context);
                }
            }

            await _next(context);
        }
        private async Task HandleUnauthorized(HttpContext context)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(MessageErrorConstant.AUTHORIZED);
        }
        private async Task HandleCheckRoleUser(HttpContext context, UserClaims userClaims)
        {
            _logger.LogInformation(userClaims.fullName);

            var userManager = context.RequestServices.GetService(typeof(ILoginService)) as ILoginService;
            // only get from cache, no need to set because of the user-service will put into cache.
            var userInfo = await userManager.GetUserByUserName(userClaims.userName);

            if (userInfo is null)
            {
                await HandleUnauthorized(context);
            }
            var userContext = context.RequestServices.GetService(typeof(IUserContext)) as IUserContext;
            if (userInfo is not null && userContext is not null)
            {
                userContext.SetUserClaims(userClaims);
            }
        }
    }
}
