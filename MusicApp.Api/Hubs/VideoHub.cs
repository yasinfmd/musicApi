using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Hubs
{
    public class VideoHub : Hub
    {


        public async Task UserTest(string username)
        {
            var userInfo = new UserInfo() { userName = username, connectionId = Context.ConnectionId };
            await Clients.Others.SendAsync("NewUserArrived", userInfo);
        }
        public async Task HelloUser(string userName, string user)
        {
            var userInfo = new UserInfo() { userName = userName, connectionId = Context.ConnectionId };
            await Clients.Client(user).SendAsync("UserSaidHello", userInfo);
        }
        public async Task SendSignal(string signal, string user)
        {
            await Clients.Client(user).SendAsync("SendSignal", Context.ConnectionId, signal);
        }

        public async Task offer (string connectionId,object desc)
        {
            await Clients.Client(connectionId).SendAsync("offer", connectionId, desc);
        }
        public async Task answer(string connectionId, object desc)
        {
            await Clients.Client(connectionId).SendAsync("answer", connectionId, desc);
            // socket.to(socketId).emit('answer', description);
        }
        public async Task candidate(string connectionId , object cand)
        {
          //  await Clients.Use
            await Clients.Client(connectionId).SendAsync("candidate",cand);
            // socket.to(socketId).emit('candidate', candidate);
            //socket.on('offer', (socketId, description) => {
            //    socket.to(socketId).emit('offer', socket.id, description);
            //});
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("onConnectChannel", Context.ConnectionId);
           // return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnect", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);

            //public async Task JoinRoom(string roomid, string userid)
            //{
            //  //  await Groups.AddToGroupAsync(Context.ConnectionId, roomid);
            //  //await  Clients.Group(roomid).SendCoreAsync(Context.User.Identity.Name + " joined.");
            //    //Clients.Group(roomid);
            //    //  await Clients.Client(user).SendAsync("SendSignal", Context.ConnectionId, signal);
            //}

        }
        public async Task JoinRoom(string roomName)
        {
            await   Groups.AddToGroupAsync(Context.ConnectionId, roomName);
           await Clients.Group(roomName).SendAsync("addChatMessage",Context.ConnectionId + " joined.");
        }

        public class UserInfo
        {
            public string userName { get; set; }
            public string connectionId { get; set; }
        }
    }
}
