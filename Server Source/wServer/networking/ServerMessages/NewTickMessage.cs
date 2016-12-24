using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ServerMessages
{
    public class NewTickMessage : ServerMessage
    {

        public int tickId_ { get; set; }
        public int tickTime_ { get; set; }
        public ObjectStats[] statuses_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.NewTick; }
        }

        public override Packet CreateInstance()
        {
            return new NewTickMessage();
        }

        protected override void Read(NReader rdr)
        {
            tickId_ = rdr.ReadInt32();
            tickTime_ = rdr.ReadInt32();

            statuses_ = new ObjectStats[rdr.ReadInt16()];
            for(int i =0; i < statuses_.Length; i++)
            {
                statuses_[i] = ObjectStats.Read(rdr);
            }
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(tickId_);
            wtr.Write(tickTime_);

            wtr.Write((ushort)statuses_.Length);
            foreach(ObjectStats i in statuses_)
            {
                i.Write(wtr);
            }
        }
    }
}