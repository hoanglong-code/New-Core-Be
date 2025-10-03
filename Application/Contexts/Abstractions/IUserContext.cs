using Domain.Commons;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contexts.Abstractions
{
    public interface IUserContext
    {
        IUserContext SetUserClaims(UserClaims userClaims);
        UserClaims userClaims { get; }
    }
}
