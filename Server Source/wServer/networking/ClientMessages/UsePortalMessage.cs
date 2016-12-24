using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ClientMessages
{
    public class UsePortalMessage : ClientMessage
    {
        public int objectId_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.UsePortal; }
        }

        public override Packet CreateInstance()
        {
            return new UsePortalMessage();
        }

        protected override void Read(NReader rdr)
        {
            objectId_ = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(objectId_);
        }
    }
}
