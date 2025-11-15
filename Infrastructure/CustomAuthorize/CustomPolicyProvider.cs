using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomAuthorize
{
    public class CustomPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public CustomPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("permission="))
            {
                // Parse: permission=EMPLOYEE:3
                string data = policyName.Replace("permission=", "");
                var parts = data.Split(':');

                var functionCode = parts[0];
                ConstantEnums.TypeAction type = (ConstantEnums.TypeAction)int.Parse(parts[1]);

                var requirement = new PrivilegeRequirement(functionCode, type);

                var policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(requirement)
                    .Build();

                return Task.FromResult(policy);
            }

            return base.GetPolicyAsync(policyName);
        }
    }
}
