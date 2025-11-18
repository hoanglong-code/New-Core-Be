using Application.Contexts.Abstractions;
using Domain.Commons;
using Domain.Constants;
using Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
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
            var endpoint = context.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null;
            if (allowAnonymous)
            {
                await _next(context);
                return;
            }

            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await HandleUnauthorized(context);
                return;
            }

            var identity = context.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                await HandleUnauthorized(context);
                return;
            }

            var userClaims = BuildUserClaims(context.User, identity);
            if (userClaims.userId <= 0)
            {
                await HandleUnauthorized(context);
                return;
            }

            var isAuthorized = await HandleCheckUser(context, userClaims);
            if (!isAuthorized)
            {
                return;
            }

            await _next(context);
        }

        private UserClaims BuildUserClaims(ClaimsPrincipal user, ClaimsIdentity identity)
        {
            return new UserClaims
            {
                User = user,
                Identity = identity,
                userId = ParseIntClaim(identity, "UserId", 0),
                userName = GetClaimValue(identity, "UserName"),
                fullName = GetClaimValue(identity, "FullName"),
                phone = GetClaimValue(identity, "Phone"),
                email = GetClaimValue(identity, "Email"),
                gender = GetClaimValue(identity, "Gender"),
                address = GetClaimValue(identity, "Address"),
                avatar = GetClaimValue(identity, "Avatar"),
                birthday = GetClaimValue(identity, "Birthday"),
                accessKey = GetClaimValue(identity, "AccessKey"),
            };
        }

        private string GetClaimValue(ClaimsIdentity identity, string claimType)
        {
            return identity.Claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? string.Empty;
        }

        private int ParseIntClaim(ClaimsIdentity identity, string claimType, int defaultValue)
        {
            var value = GetClaimValue(identity, claimType);
            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        private async Task HandleUnauthorized(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(MessageErrorConstant.AUTHORIZED);
        }

        private async Task<bool> HandleCheckUser(HttpContext context, UserClaims userClaims)
        {
            _logger.LogInformation("Processing user: {FullName}", userClaims.fullName);

            var userManager = context.RequestServices.GetService(typeof(ILoginService)) as ILoginService;
            if (userManager == null)
            {
                _logger.LogWarning("ILoginService not found in service provider");
                await HandleUnauthorized(context);
                return false;
            }

            // Only get from cache, no need to set because the user-service will put into cache
            var userInfo = await userManager.GetUserByUserName(userClaims.userName);
            if (userInfo == null)
            {
                await HandleUnauthorized(context);
                return false;
            }

            var userContext = context.RequestServices.GetService(typeof(IUserContext)) as IUserContext;
            if (userContext != null)
            {
                userContext.SetUserClaims(userClaims);
            }
            else
            {
                _logger.LogWarning("IUserContext not found in service provider");
            }

            return true;
        }
    }
}
