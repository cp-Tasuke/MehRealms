using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.ClientMessages
{

    public class HelloMessage : ClientMessage
    {
        public string buildVersion_ { get; set; }
        public int gameId_ { get; set; }
        public string guid_ { get; set; }
        public string password_ { get; set; }
        public string secret_ { get; set; }
        public string keyTime_ { get; set; }
        public byte[] key_ { get; set; }
        public string mapJSON_ { get; set; }
        public string entrytag_ { get; set; }
        public string gameNet { get; set; }
        public string gameNetUserId { get; set; }
        public string playPlatform { get; set; }
        public string platformToken { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Hello; }
        }

        public override Packet CreateInstance()
        {
            return new HelloMessage();
        }

        protected override void Read(NReader rdr)
        {
            buildVersion_ = rdr.ReadUTF();
            gameId_ = rdr.ReadInt32();
            guid_ = rdr.ReadUTF();
            rdr.ReadInt32();
            password_ = rdr.ReadUTF();
            rdr.ReadInt32();
            secret_ = rdr.ReadUTF();
            keyTime_ = rdr.ReadUTF();
            key_ = rdr.ReadBytes(rdr.ReadInt32());
            mapJSON_ = rdr.ReadUTF();
            entrytag_ = rdr.ReadUTF();
            gameNet = rdr.ReadUTF();
            gameNetUserId = rdr.ReadUTF();
            playPlatform = rdr.ReadUTF();
            platformToken = rdr.ReadUTF();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(buildVersion_);
            wtr.Write(gameId_);
            wtr.Write(guid_);
            wtr.Write(new Random().Next(0, 50000));
            wtr.Write(password_);
            wtr.Write(new Random().Next(0, 50000));
            wtr.Write(secret_);
            wtr.Write(keyTime_);
            wtr.Write(key_);
            wtr.Write(mapJSON_);
            wtr.Write(entrytag_);
            wtr.Write(gameNet);
            wtr.Write(gameNetUserId);
            wtr.Write(playPlatform);
            wtr.Write(platformToken);
        }
    }
}