using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace NewCommunity.Hubs
{
    //[HubName("chathub")]
    public class ChatHub : Hub<IChatHub>
    {
        private static string Now => DateTime.Now.ToString("HH:mm:ss", System.Threading.Thread.CurrentThread.CurrentCulture);
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ServerMessage($"[{Now}] {Context.ConnectionId} joined").ConfigureAwait(false);
            //return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            await Clients.All.ServerMessage($"[{Now}] {Context.ConnectionId} left").ConfigureAwait(false);
            //return base.OnDisconnectedAsync(exception);
        }
        public Task ClientMessage(MessageInfo data)
        {
            if (data == null)
            {
                return default;
            }
            string name = data.Name;
            string message = data.Message;
            return Clients.All.ServerMessage($"[{Now}] {name}: {message}");
        }
    }
    public class MessageInfo
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
