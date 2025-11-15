using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomAuthorize
{
    public class PrivilegeRequirement : IAuthorizationRequirement
    {
        public string FunctionCode { get; }
        public ConstantEnums.TypeAction Type { get; }

        public PrivilegeRequirement(string functionCode, ConstantEnums.TypeAction type)
        {
            FunctionCode = functionCode;
            Type = type;
        }
    }
}
