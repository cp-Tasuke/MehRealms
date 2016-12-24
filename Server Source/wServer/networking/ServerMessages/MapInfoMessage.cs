using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ServerMessages
{
    public class MapInfoMessage : ServerMessage
    {
        public int width_ { get; set; }
        public int height_ { get; set; }
        public string name_ { get; set; }
        public string displayName_ { get; set; }
        public int difficulty_ { get; set; }
        public uint fp_ { get; set; } //Seed
        public int background_ { get; set; }
        public bool allowPlayerTeleport_ { get; set; }
        public bool showDisplays_ { get; set; }
        public string[] clientXML_ { get; set; }
        public string[] extraXML_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.MapInfo; }
        }

        public override Packet CreateInstance()
        {
            return new MapInfoMessage();
        }

        protected override void Read(NReader rdr)
        {
            width_ = rdr.ReadInt32();
            height_ = rdr.ReadInt32();
            name_ = rdr.ReadUTF();
            displayName_ = rdr.ReadUTF();
            difficulty_ = rdr.ReadInt32();
            fp_ = rdr.ReadUInt32(); //Seed
            background_ = rdr.ReadInt32();
            allowPlayerTeleport_ = rdr.ReadBoolean();
            showDisplays_ = rdr.ReadBoolean();

            clientXML_ = new string[rdr.ReadInt16()];
            for (int i = 0; i < clientXML_.Length; i++)
                clientXML_[i] = rdr.ReadUTF();

            extraXML_ = new string[rdr.ReadInt16()];
            for (int i = 0; i < extraXML_.Length; i++)
                extraXML_[i] = rdr.ReadUTF();

        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(width_);
            wtr.Write(height_);
            wtr.WriteUTF(name_);
            wtr.WriteUTF(displayName_);
            wtr.Write(difficulty_);
            wtr.Write(fp_);
            wtr.Write(background_);
            wtr.Write(allowPlayerTeleport_);
            wtr.Write(showDisplays_);

            wtr.Write((ushort)clientXML_.Length);
            foreach (string i in clientXML_)
                wtr.Write32UTF(i);

            wtr.Write((ushort)extraXML_.Length);
            foreach (string i in extraXML_)
                wtr.Write32UTF(i);
        }
    }
}