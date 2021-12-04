using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DrawBoardServer.Hubs
{
    public class DrawHub : Hub
    {
        public async Task Draw(int x, int y)
        {
            await Clients.Others.SendAsync("draw", x, y);
        }
    }
}
