using Bug.API.Services;
using Bug.Data.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.SignalR
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;
        
        public ChatHub(ILogger<ChatHub> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async Task GetConnectionId(string accountId, string connectionId)
        {
            _logger.LogInformation(connectionId);
            var user = await _accountService
                .GetDetailAccountById(accountId);
            if (user == null || user.WatchIssues.Count == 0)
                return;
            foreach(var i in user.WatchIssues)
            {
                await Groups.AddToGroupAsync(connectionId, i.Id);
            }
        }
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Hello SignalR");
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
