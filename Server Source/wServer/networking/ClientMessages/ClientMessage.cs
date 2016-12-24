namespace wServer.networking.ClientMessages
{
    public abstract class ClientMessage : Packet
    {
        public override void Crypt(Client client, byte[] dat, int offset, int len)
        {
            client.ReceiveKey.Crypt(dat, offset, len);
        }
    }
}