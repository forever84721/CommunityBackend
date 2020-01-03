using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NewCommunity.Common;

namespace NewCommunity.Hubs
{
    //[HubName("chathub")]
    public class ChatHub : Hub<IChatHub>
    {
        private static string Now => DateTime.Now.ToString("HH:mm:ss", System.Threading.Thread.CurrentThread.CurrentCulture);
        public override async Task OnConnectedAsync()
        {
            //Context.User.Claims
            //var identity = (ClaimsIdentity)Context.User.Identity;
            //var tmp = identity.FindFirst(ClaimTypes.NameIdentifier);
            var msg = new MessageInfo() { IssuerId = 0, Name = Context.ConnectionId, PostTime = Utils.GetDateTimeWithoutMillisecond(), Content = "joined" };
            await Clients.All.ServerMessage(msg).ConfigureAwait(false);
            //return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var msg = new MessageInfo() { IssuerId = 0, Name = Context.ConnectionId, PostTime = Utils.GetDateTimeWithoutMillisecond(), Content = "left" };
            await Clients.All.ServerMessage(msg).ConfigureAwait(false);
            //return base.OnDisconnectedAsync(exception);
        }
        public Task ClientMessage(MessageInfo data)
        {
            if (data == null)
            {
                return default;
            }
            data.PostTime = Utils.GetDateTimeWithoutMillisecond();
            return Clients.All.ServerMessage(data);
        }
    }
    public class MessageInfo
    {
        public int IssuerId { get; set; }
        public string Name { get; set; }
        public DateTime? PostTime { get; set; }
        public string Content { get; set; }
    }
}
