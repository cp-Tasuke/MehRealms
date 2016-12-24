using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ClientMessages
{
    public class InvSwapMessage : ClientMessage
    {
        public int time_ { get; set; }
        public Position position_ { get; set; }
        public ObjectSlot slotObject1_ { get; set; }
        public ObjectSlot slotObject2_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.InvSwap; }
        }

        public override Packet CreateInstance()
        {
            return new InvSwapMessage();
        }

        protected override void Read(NReader rdr)
        {
            time_ = rdr.ReadInt32();
            position_ = Position.Read(rdr);
            slotObject1_ = ObjectSlot.Read(rdr);
            slotObject2_ = ObjectSlot.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(time_);
            position_.Write(wtr);
            slotObject1_.Write(wtr);
            slotObject2_.Write(wtr);
        }
    }
}