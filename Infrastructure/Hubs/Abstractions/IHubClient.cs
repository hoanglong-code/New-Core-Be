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
        Task BroadcastMessage(SignalRNotify signalRNotify);
        Task SendMessage(string message);
        Task PrintTime(DateTime time);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync();
    }
}
