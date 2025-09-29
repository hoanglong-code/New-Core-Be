using Application.Contexts.Abstractions;
using Domain.Constants;
using Domain.Enums;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomAuthorize
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _functionCode;
        private readonly ConstantEnums.TypeAction _type;

        public CustomAuthorizeAttribute(string functionCode, ConstantEnums.TypeAction type)
        {
            _functionCode = functionCode;
            _type = type;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Lấy service IUserContext từ DI
            var userContext = context.HttpContext.RequestServices.GetService(typeof(IUserContext)) as IUserContext;

            if (userContext == null || userContext.userClaims == null)
            {
                //context.Result = new ForbidResult();
                //return;
                context.Result = new JsonResult(new { error = MessageErrorConstant.AUTHORIZED })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            // Kiểm tra quyền
            if (!string.IsNullOrEmpty(_functionCode) &&
                !CheckRoleExtension.CheckRoleByCode(userContext.userClaims.access_key, _functionCode, (int)_type))
            {
                string message = _type switch
                {
                    ConstantEnums.TypeAction.VIEW => MessageErrorConstant.NOPERMISION_VIEW_MESSAGE,
                    ConstantEnums.TypeAction.CREATE => MessageErrorConstant.NOPERMISION_CREATE_MESSAGE,
                    ConstantEnums.TypeAction.UPDATE => MessageErrorConstant.NOPERMISION_UPDATE_MESSAGE,
                    ConstantEnums.TypeAction.DELETED => MessageErrorConstant.NOPERMISION_DELETE_MESSAGE,
                    ConstantEnums.TypeAction.IMPORT => MessageErrorConstant.NOPERMISION_ACTION_MESSAGE,
                    ConstantEnums.TypeAction.EXPORT => MessageErrorConstant.NOPERMISION_ACTION_MESSAGE,
                    ConstantEnums.TypeAction.PRINT => MessageErrorConstant.NOPERMISION_ACTION_MESSAGE,
                    ConstantEnums.TypeAction.EDIT_ANOTHER_USER => MessageErrorConstant.NOPERMISION_ACTION_MESSAGE,
                    _ => MessageErrorConstant.NOPERMISION_ACTION_MESSAGE
                };

                // Trả lỗi 403 kèm message
                context.Result = new JsonResult(new { error = message })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}
