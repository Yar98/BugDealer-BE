using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.SignalR
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        
    }
}
