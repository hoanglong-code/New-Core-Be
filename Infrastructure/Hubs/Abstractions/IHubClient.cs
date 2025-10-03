using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hubs.Abstractions
{
    public interface IHubClient
    {
        Task SendMessage(string message);
        Task SendMessageToUserAsync(string connectionId, string message);
        Task PrintTime(DateTime time);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync();
        Task SendAsync(string method, params object[] args);
    }
}
