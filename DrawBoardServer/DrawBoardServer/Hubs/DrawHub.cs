using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DrawBoardServer.Hubs
{
    public class DrawHub : Hub
    {
        public async Task Draw(int x, int y, string color, int width, string globalCompositeOperation)
        {
            await Clients.Others.SendAsync("draw", x, y, color, width, globalCompositeOperation);
        }

        public async Task StopDrawing()
        {
            await Clients.Others.SendAsync("stopDrawing");
        }
        public async Task Clear()
        {
            await Clients.Others.SendAsync("clear");
        }

    }
}
