using Application.Contexts.Abstractions;
using Domain.Commons;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contexts.Implementations
{
    public class UserContext : IUserContext
    {
        private UserClaims _userClaims;
        public UserClaims userClaims => _userClaims;

        public IUserContext SetUserClaims(UserClaims userClaims)
        {
            _userClaims = userClaims;
            return this;
        }

        public ConcurrentDictionary<string, string> ConnectedClients { get; } = new ConcurrentDictionary<string, string>();
    }
}
