using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ClientMessages
{
    public class InvDropMessage : ClientMessage
    {
        public ObjectSlot slotObject_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.InvDrop; }
        }

        public override Packet CreateInstance()
        {
            return new InvDropMessage();
        }

        protected override void Read(NReader rdr)
        {
            slotObject_ = ObjectSlot.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            slotObject_.Write(wtr);
        }
    }
}