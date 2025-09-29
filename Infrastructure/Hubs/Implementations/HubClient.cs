using Application.Contexts.Abstractions;
using Infrastructure.Hubs.Abstractions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Hubs.Implementations
{
    public class HubClient : Hub<IHubClient>
    {
        private readonly IUserContext _clientConnectionService;
        public HubClient(IUserContext clientConnectionService)
        {
            _clientConnectionService = clientConnectionService;
        }
        // Gửi thông báo
        public async Task SendErrorMessage(string connectionId, string message)
        {
            await Clients.Clients(connectionId).SendMessage(message);
        }
        // Dùng cách truyền tham số từ FE
        public Task SuscribeToUser(string userId)
        {
            return this.Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
        // Client kết nối
        public override async Task OnConnectedAsync()
        {
            // await PrintTime();

            string connectionId = Context.ConnectionId;
            string userName = Context.User?.Identity?.Name ?? "Anonymous"; // Lấy tên người dùng hoặc đặt mặc định

            Console.WriteLine($"Client connected: {connectionId} - {userName}");

            var userId = Context.User?.FindFirst("Id")?.Value;

            if (_clientConnectionService.ConnectedClients.Values.ToList().Contains(userId))
            {
                await SendErrorMessage(connectionId, "Tài khoản này đã đăng nhập ở trên 1 thiết bị khác!");
                //await Clients.Clients(connectionId).LogoutClient("");
            }
            else
            {
                _clientConnectionService.ConnectedClients.TryAdd(connectionId, userId);

                await Groups.AddToGroupAsync(connectionId, userId);
            }
            _clientConnectionService.ConnectedClients.TryAdd(connectionId, userId);
            await base.OnConnectedAsync();
        }
        // Client ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;

            _clientConnectionService.ConnectedClients.TryRemove(connectionId, out _);

            var userId = Context.User?.FindFirst("Id")?.Value;

            await Groups.RemoveFromGroupAsync(connectionId, userId);

            Console.WriteLine($"Client disconnected: {connectionId}");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
