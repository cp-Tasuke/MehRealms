namespace wServer.networking.ClientMessages
{
    public class UseItemMessage : ClientMessage
    {
        public int time_ { get; set; }
        public ObjectSlot slotObject_ { get; set; }
        public Position itemUsePos_ { get; set; }
        public byte useType_ { get; set; }

        public override PacketID ID
        {
            get { return PacketID.UseItem; }
        }

        public override Packet CreateInstance()
        {
            return new UseItemMessage();
        }

        protected override void Read(NReader rdr)
        {
            time_ = rdr.ReadInt32();
            slotObject_ = ObjectSlot.Read(rdr);
            itemUsePos_ = Position.Read(rdr);
            useType_ = rdr.ReadByte();
        }

        protected override void Write(NWriter wtr)
        {
            wtr.Write(time_);
            slotObject_.Write(wtr);
            itemUsePos_.Write(wtr);
            wtr.Write(useType_);
        }
    }
}