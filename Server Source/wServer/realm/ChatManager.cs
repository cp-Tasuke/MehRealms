using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using wServer.realm.entities;
using wServer.networking.ServerMessages;

namespace wServer.realm
{
    public class ChatManager
    {
        static ILog log = LogManager.GetLogger(typeof(ChatManager));

        RealmManager manager;
        public ChatManager(RealmManager manager)
        {
            this.manager = manager;
        }

        public void Say(Player src, string text)
        {
            src.Owner.BroadcastPacket(new TextMessage()
            {
                Name = (src.Client.Account.Admin ? "@" : "") + src.Name,
                ObjectId = src.Id,
                Stars = src.Stars,
                bubbleTime_ = 5,
                Recipient = "",
                Text = text,
                CleanText = text
            }, null);
            log.InfoFormat("[{0}({1})] <{2}> {3}", src.Owner.Name, src.Owner.Id, src.Name, text);
        }

        public void Announce(string text)
        {
            foreach (var i in manager.Clients.Values)
                i.SendPacket(new TextMessage()
                {
                    bubbleTime_ = 0,
                    Stars = -1,
                    Name = "@Announcement",
                    Text = text
                });
            log.InfoFormat("<Announcement> {0}", text);
        }

        public void Oryx(World world, string text)
        {
            world.BroadcastPacket(new TextMessage()
            {
                bubbleTime_ = 0,
                Stars = -1,
                Name = "#Oryx the Mad God",
                Text = text
            }, null);
            log.InfoFormat("[{0}({1})] <Oryx the Mad God> {2}", world.Name, world.Id, text);
        }
    }
}
