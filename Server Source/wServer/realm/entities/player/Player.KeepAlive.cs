using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.networking.ServerMessages;
using wServer.networking.ClientMessages;

namespace wServer.realm.entities
{
    public partial class Player
    {
        long lastPong = -1;
        int? lastTime = null;
        long tickMapping = 0;
        Queue<long> ts = new Queue<long>();

        bool sentPing = false;
        bool KeepAlive(RealmTime time)
        {
            if (lastPong == -1) lastPong = time.tickTimes - 1500;
            if (time.tickTimes - lastPong > 1500 && !sentPing)
            {
                sentPing = true;
                ts.Enqueue(time.tickTimes);
                client.SendPacket(new PingPacket());
            }
            else if (time.tickTimes - lastPong > 3000)
            {
                //client.Disconnect();
                return false;
            }
            return true;
        }
        internal void Pong(int time)
        {
            if (lastTime != null && (time - lastTime.Value > 3000 || time - lastTime.Value < 0))
                ;//client.Disconnect();
            else
                lastTime = time;
            tickMapping = ts.Dequeue() - time;
            lastPong = time + tickMapping;
            sentPing = false;
        }
    }
}
