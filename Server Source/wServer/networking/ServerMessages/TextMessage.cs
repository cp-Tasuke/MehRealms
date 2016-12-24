using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ServerMessages
{
    class TextMessage : ServerMessage
    {
        public string name_ { get; set; }
        public int objectId_ { get; set; }
        public int numStars_ { get; set; }
        public byte bubbleTime_ { get; set; }
        public string recipient_ { get; set; }
        public string text_ { get; set; }
        public string cleanText_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Text;  }
        }

        public override Packet CreateInstance()
        {
            return new TextMessage();
        }

        protected override void Read(NReader rdr)
        {
            name_ = rdr.ReadUTF();
            objectId_ = rdr.ReadInt32();
            numStars_ = rdr.ReadInt32();
            bubbleTime_ = rdr.ReadByte();
            recipient_ = rdr.ReadUTF();
            text_ = rdr.ReadUTF();
            cleanText_ = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.WriteUTF(name_);
            wtr.Write(objectId_);
            wtr.Write(numStars_);
            wtr.Write(bubbleTime_);
            wtr.WriteUTF(recipient_);
            wtr.WriteUTF(text_);
            wtr.WriteUTF(cleanText_);
        }
    }
}
