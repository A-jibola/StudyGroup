using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroup.Service
{
    public class TextHub : Hub
    {
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task LeaveGroup(string group)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public Task SendMessageToGroup(string group, string message, string username)
        {
            return Clients.Group(group).SendAsync("SendMessage", message, username);
        }
    }
}
