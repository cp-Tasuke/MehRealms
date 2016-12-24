using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.networking.ServerMessages;
using wServer.realm.setpieces;
using wServer.realm.commands;

namespace wServer.realm.entities
{
    partial class Player
    {
        public void SendInfo(string text)
        {
            client.SendPacket(new TextMessage()
            {
                bubbleTime_ = 0,
                numStars_ = -1,
                name_ = "",
                text_ = text
            });
        }
        public void SendError(string text)
        {
            client.SendPacket(new TextMessage()
            {
                bubbleTime_ = 0,
                numStars_ = -1,
                name_ = "*Error*",
                text_ = text
            });
        }
        public void SendClientText(string text)
        {
            client.SendPacket(new TextMessage()
            {
                bubbleTime_ = 0,
                numStars_ = -1,
                name_ = "*Client*",
                text_ = text
            });
        }
        public void SendHelp(string text)
        {
            client.SendPacket(new TextMessage()
            {
                bubbleTime_ = 0,
                numStars_ = -1,
                name_ = "*Help*",
                text_ = text
            });
        }
        public void SendEnemy(string name, string text)
        {
            client.SendPacket(new TextMessage()
            {
                bubbleTime_ = 0,
                numStars_ = -1,
                name_ = "#" + name,
                text_ = text
            });
        }
        public void SendText(string sender, string text)
        {
            client.SendPacket(new TextMessage()
            {
                bubbleTime_ = 0,
                numStars_ = -1,
                name_ = sender,
                text_ = text
            });
        }
    }
}
