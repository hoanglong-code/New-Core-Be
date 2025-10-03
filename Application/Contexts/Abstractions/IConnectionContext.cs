using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contexts.Abstractions
{
    public interface IConnectionContext
    {
        Task AddConnectionAsync(string connectionId, string userId);
        Task RemoveConnectionAsync(string connectionId);
        Task<string?> GetUserIdAsync(string connectionId);
        Task<IEnumerable<string>> GetConnectionsAsync(string userId);
        Task<bool> CheckConnectionsAsync(string userId);
    }
}
