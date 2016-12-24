using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking
{
    public enum PacketID : byte
    {
        Hello = 86,
        Update = 44,
        NewTick = 31,
        MapInfo = 28,
        Load = 63,
        Create = 48,
        CreateSuccess = 58,
        Move = 24,
        InvSwap = 64,
        InvDrop = 97,
        Escape = 16,
        UseItem = 3,
        UsePortal = 23,
        Text = 34,
        ShowEffect = 78
    }
}
