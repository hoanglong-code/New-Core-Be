using Application.Contexts.Abstractions;
using Domain.Constants;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomAuthorize
{
    public class PrivilegeHandler : AuthorizationHandler<PrivilegeRequirement>
    {
        protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PrivilegeRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;
            if (httpContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var userContext = httpContext.RequestServices.GetService(typeof(IUserContext)) as IUserContext;

            if (userContext == null || userContext.userClaims == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var accessKey = userContext.userClaims.accessKey;
            if (string.IsNullOrEmpty(accessKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (CheckRoleExtension.CheckRoleByCode(accessKey, requirement.FunctionCode, requirement.Type))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }

    }
}
