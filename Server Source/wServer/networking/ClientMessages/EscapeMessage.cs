namespace wServer.networking.ClientMessages
{
    public class EscapeMessage : ClientMessage
    {
        public override PacketID ID
        {
            get { return PacketID.Escape; }
        }

        public override Packet CreateInstance()
        {
            return new EscapeMessage();
        }

        protected override void Read(NReader rdr)
        {
        }

        protected override void Write(NWriter wtr)
        {
        }
    }
}