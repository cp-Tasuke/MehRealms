using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ServerMessages
{
    public class ShowEffectMessage : ServerMessage
    {
        public EffectType effectType_ { get; set; }
        public int targetObjectId_ { get; set; }
        public Position pos1_ { get; set; }
        public Position pos2_ { get; set; }
        public ARGB color_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.ShowEffect; }
        }

        public override Packet CreateInstance()
        {
            return new ShowEffectMessage();
        }

        protected override void Read(NReader rdr)
        {
            effectType_ = (EffectType)rdr.ReadByte();
            targetObjectId_ = rdr.ReadInt32();
            pos1_ = Position.Read(rdr);
            pos2_ = Position.Read(rdr);
            color_ = ARGB.Read(rdr);
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write((byte)effectType_);
            wtr.Write(targetObjectId_);
            pos1_.Write(wtr);
            pos2_.Write(wtr);
            color_.Write(wtr);
        }
    }
}
