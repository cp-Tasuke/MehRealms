using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ServerMessages
{
    public class CreateSuccess : ServerMessage
    {
        public int objectId_ { get; set; }
        public int charId_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.CreateSuccess; }
        }

        public override Packet CreateInstance()
        {
            return new CreateSuccess();
        }

        protected override void Read(NReader rdr)
        {
            objectId_ = rdr.ReadInt32();
            charId_ = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(objectId_);
            wtr.Write(charId_);
        }
    }
}