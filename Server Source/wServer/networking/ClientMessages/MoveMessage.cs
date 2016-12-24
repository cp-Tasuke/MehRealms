using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ClientMessages
{
    public class MoveMessage : ClientMessage
    {
        public int tickId_ { get; set; }
        public int time_ { get; set; }
        public Position newPosition_ { get; set; }
        public TimedPosition[] records_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Move; }
        }

        public override Packet CreateInstance()
        {
            return new MoveMessage();
        }

        protected override void Read(NReader rdr)
        {
            tickId_ = rdr.ReadInt32();
            time_ = rdr.ReadInt32();
            newPosition_ = Position.Read(rdr);
            records_ = new TimedPosition[rdr.ReadInt16()];
            for (int i = 0; i < records_.Length; i++)
                records_[i] = TimedPosition.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(tickId_);
            wtr.Write(time_);
            newPosition_.Write(wtr);
            if (records_ == null)
            {
                wtr.Write((ushort)0);
                return;
            }
            wtr.Write((ushort)records_.Length);
            foreach (TimedPosition i in records_)
                i.Write(wtr);
        }
    }
}
