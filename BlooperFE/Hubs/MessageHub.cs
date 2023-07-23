using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlooperFE.Hubs
{
    public class MessageHub : Hub
    {
        public const string HubUrl = "/messagehub";

        public async Task SendMessage(string to_id, string from_id, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", to_id, from_id, message);
        }
    }
}
