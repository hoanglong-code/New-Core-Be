using Application.Contexts.Abstractions;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contexts.Implementations
{
    public class ConnectionContext : IConnectionContext
    {
        private readonly ConcurrentDictionary<string, string> _connections = new();
        private readonly IDatabase _db;

        public ConnectionContext(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task AddConnectionAsync(string connectionId, string userId)
        {
            _connections[connectionId] = userId;
            await _db.HashSetAsync("connections", connectionId, userId);
        }

        public async Task RemoveConnectionAsync(string connectionId)
        {
            _connections.TryRemove(connectionId, out _);
            await _db.HashDeleteAsync("connections", connectionId);
        }

        public async Task<string?> GetUserIdAsync(string connectionId)
        {
            if (_connections.TryGetValue(connectionId, out var userId))
                return userId;

            var value = await _db.HashGetAsync("connections", connectionId);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task<IEnumerable<string>> GetConnectionsAsync(string userId)
        {
            var local = _connections
                .Where(x => x.Value == userId)
                .Select(x => x.Key);

            var remote = await _db.HashGetAllAsync("connections");
            var redisConnections = remote
                .Where(x => x.Value == userId)
                .Select(x => x.Name.ToString());

            return local.Concat(redisConnections).Distinct();
        }

        public async Task<bool> CheckConnectionsAsync(string userId)
        {
            var connections = await GetConnectionsAsync(userId);
            return connections.Any();
        }
    }

}
