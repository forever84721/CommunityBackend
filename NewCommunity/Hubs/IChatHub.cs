using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCommunity.Hubs
{
    public interface IChatHub
    {
        Task ServerMessage(string message);
    }
}
