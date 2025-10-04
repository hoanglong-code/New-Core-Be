using Application.Contexts.Abstractions;
using Domain.Constants;
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
        private readonly IConnectionContext _connectionContext;
        public HubClient(IUserContext clientConnectionService, IConnectionContext connectionContext)
        {
            _clientConnectionService = clientConnectionService;
            _connectionContext = connectionContext;
        }
        // Gửi thông báo
        public async Task SendMessageToUserAsync(string connectionId, string message)
        {
            await Clients.Clients(connectionId).SendMessage(message);
        }
        // Client kết nối
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string userName = Context.User?.Identity?.Name ?? "Anonymous";
            string userId = Context.User?.FindFirst("Id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                await SendMessageToUserAsync(connectionId, MessageErrorConstant.INVALID_USERID);
                Context.Abort(); // Ngắt kết nối
                return;
            }

            // Kiểm tra nếu user đã có kết nối cũ
            var existingConnection = await _connectionContext.GetConnectionsAsync(userId);

            if (existingConnection.Any())
            {
                // Gửi thông báo logout tới connection mới
                await Clients.Client(connectionId).SendAsync("LogoutClient", MessageErrorConstant.INVALID_LOGIN);
                Context.Abort(); // Ngắt kết nối
                return;
            }

            // Thêm connection mới
            await _connectionContext.AddConnectionAsync(connectionId, userId);

            // Thêm user vào group theo userId
            await Groups.AddToGroupAsync(connectionId, userId);

            await base.OnConnectedAsync();
        }
        // Client ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;

            await _connectionContext.RemoveConnectionAsync(connectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
