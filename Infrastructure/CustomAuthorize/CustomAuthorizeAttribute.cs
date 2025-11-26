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
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _functionCode;
        private readonly ConstantEnums.TypeAction _type;

        public CustomAuthorizeAttribute(string functionCode, ConstantEnums.TypeAction type)
        {
            _functionCode = functionCode;
            _type = type;
            Policy = $"permission={_functionCode}:{(int)_type}";
        }
    }
}
