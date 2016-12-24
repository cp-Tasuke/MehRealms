using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.networking.ClientMessages;
using wServer.realm.terrain;

namespace wServer.networking.ServerMessages
{
    public class UpdateMessage : ClientMessage
    {
        public struct TileData
        {
            public short X;
            public short Y;
            public Tile Tile;
        }

        public TileData[] tiles_ { get; set; }
        public ObjectDef[] newObjs_ { get; set; }
        public int[] drops_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Update; }
        }

        public override Packet CreateInstance()
        {
            return new UpdateMessage();
        }

        protected override void Read(NReader rdr)
        {
            tiles_ = new TileData[rdr.ReadInt16()];

            for (int i = 0; i < tiles_.Length; i++)
            {
                tiles_[i] = new TileData
                {
                    X = rdr.ReadInt16(),
                    Y = rdr.ReadInt16(),
                    Tile = (Tile)rdr.ReadByte()
                };
            }
            newObjs_ = new ObjectDef[rdr.ReadInt16()];

            for (int i = 0; i < newObjs_.Length; i++)
                newObjs_[i] = ObjectDef.Read(rdr);

            drops_ = new int[rdr.ReadInt16()];
            for (int i = 0; i < drops_.Length; i++)
                drops_[i] = rdr.ReadInt32();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write((short)tiles_.Length);

            foreach(TileData i in tiles_)
            {
                wtr.Write(i.X);
                wtr.Write(i.Y);
                wtr.Write((short)i.Tile);
            }
            wtr.Write((short)newObjs_.Length);

            foreach (ObjectDef i in newObjs_)
                i.Write(wtr);

            wtr.Write((short)drops_.Length);
            foreach (int i in drops_)
                wtr.Write(i);
        }
    }
}
