using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ClientMessages
{
    public class CreateMessage : ClientMessage
    {
        public int classType { get; set; }
        public int skinType { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Create; }
        }

        public override Packet CreateInstance()
        {
            return new CreateMessage();
        }

        protected override void Read(NReader rdr)
        {
            classType = rdr.ReadInt16();
            skinType = rdr.ReadInt16();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write((ushort)classType);
            wtr.Write((ushort)skinType);
        }
    }
}