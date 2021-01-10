using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Hubs
{


    public class WebRtcHub : Hub
    {
        public async Task Join(object user)
        {
            await Clients.All.SendAsync("newUser", user);
        }

        public async Task SendMessage(string connectionId, string msg,string user)
        {
            await Clients.Client(connectionId).SendAsync("newMsg", msg,user);
        }

    }


}
