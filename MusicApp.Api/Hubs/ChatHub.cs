using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendConnectionId(string connectionId)
        {
            await Clients.All.SendAsync("setClientMessage", "A connection with ID '" + connectionId + "' has just connected");
        }




    }
}
