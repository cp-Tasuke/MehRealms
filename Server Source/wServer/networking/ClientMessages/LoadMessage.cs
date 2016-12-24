using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ClientMessages
{
    public class LoadMessage : ClientMessage
    {

        public int charId_ { get; set; }
        public bool isFromArena_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Load; }
        }

        public override Packet CreateInstance()
        {
            return new LoadMessage();
        }

        protected override void Read(NReader rdr)
        {
            charId_ = rdr.ReadInt32();
            isFromArena_ = rdr.ReadBoolean();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(charId_);
            wtr.Write(isFromArena_);
        }
    }
}
