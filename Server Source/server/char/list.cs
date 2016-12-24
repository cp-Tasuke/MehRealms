using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using db;
using System.Xml.Serialization;
using System.IO;
using MySql.Data.MySqlClient;
using System.Web;
using System.Collections.Specialized;

namespace server.@char
{
    class list : IRequestHandler
    {
        Lazy<List<ServerItem>> svrList;
        public list()
        {
            svrList = new Lazy<List<ServerItem>>(GetServerList, true);
        }
        List<ServerItem> GetServerList()
        {
            var ret = new List<ServerItem>();
            int num = Program.Settings.GetValue<int>("svrNum");
            for (int i = 0; i < num; i++)
                ret.Add(new ServerItem()
                {
                    Name = Program.Settings.GetValue("svr" + i + "Name"),
                    Lat = Program.Settings.GetValue<int>("svr" + i + "Lat", "0"),
                    Long = Program.Settings.GetValue<int>("svr" + i + "Long", "0"),
                    DNS = Program.Settings.GetValue("svr" + i + "Adr", "127.0.0.1"),
                    Usage = 0.2,
                    AdminOnly = Program.Settings.GetValue<bool>("svr" + i + "Admin", "false")
                });
            return ret;
        }

        public void HandleRequest(HttpListenerContext context)
        {
            NameValueCollection query;
            using (StreamReader rdr = new StreamReader(context.Request.InputStream))
                query = HttpUtility.ParseQueryString(rdr.ReadToEnd());

            using (var db = new Database(Program.Settings.GetValue("conn")))
            {

                Chars chrs = new Chars()
                {
                    Characters = new List<Char>() { },
                    NextCharId = 2,
                    MaxNumChars = 1,
                    Account = db.Verify(query["guid"], query["password"]),
                    Servers = GetServerList()
                };
                if (chrs.Account != null)
                {
                    db.GetCharData(chrs.Account, chrs);
                    db.LoadCharacters(chrs.Account, chrs);
                    chrs.News = db.GetNews(Program.GameData, chrs.Account);
                }
                else
                {
                    chrs.Account = Database.CreateGuestAccount(query["guid"]);
                    chrs.News = db.GetNews(Program.GameData, null);
                }

                MemoryStream ms = new MemoryStream();
                XmlSerializer serializer = new XmlSerializer(chrs.GetType(), new XmlRootAttribute(chrs.GetType().Name) { Namespace = "" });

                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;
                xws.Encoding = Encoding.UTF8;
                XmlWriter xtw = XmlWriter.Create(context.Response.OutputStream, xws);
                serializer.Serialize(xtw, chrs, chrs.Namespaces);
            }
        }
    }
}
