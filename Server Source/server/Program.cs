using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using db;
using log4net;
using log4net.Config;
using System.Globalization;

namespace server
{
    class Program
    {
        static HttpListener listener;
        static readonly Thread[] workers = new Thread[5];
        static readonly Queue<HttpListenerContext> contextQueue = new Queue<HttpListenerContext>();
        static readonly object queueLock = new object();
        static readonly ManualResetEvent queueReady = new ManualResetEvent(false);

        internal static SimpleSettings Settings;
        internal static XmlData GameData;

        static ILog log = LogManager.GetLogger("Server");

        static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.Name = "Entry";

            using (Settings = new SimpleSettings("server"))
            {
                GameData = new XmlData();
                int port = Settings.GetValue<int>("port", "80");

                listener = new HttpListener();
                listener.Prefixes.Add("http://*:" + port + "/");
                listener.Start();

                listener.BeginGetContext(ListenerCallback, null);
                for (var i = 0; i < workers.Length; i++)
                {
                    workers[i] = new Thread(Worker) { Name = "Worker " + i };
                    workers[i].Start();
                }
                Console.CancelKeyPress += (sender, e) => e.Cancel = true;
                log.Info("Listening at port " + port + "...");

                while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;

                log.Info("Terminating...");
                terminating = true;
                listener.Stop();
                queueReady.Set();
                GameData.Dispose();
                while (contextQueue.Count > 0)
                    Thread.Sleep(100);
            }
        }

        static void ListenerCallback(IAsyncResult ar)
        {
            if (!listener.IsListening) return;
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(ListenerCallback, null);
            lock (queueLock)
            {
                contextQueue.Enqueue(context);
                queueReady.Set();
            }
        }

        static bool terminating = false;
        static void Worker()
        {
            while (queueReady.WaitOne())
            {
                if (terminating) return;
                HttpListenerContext context;
                lock (queueLock)
                {
                    if (contextQueue.Count > 0)
                        context = contextQueue.Dequeue();
                    else
                    {
                        queueReady.Reset();
                        continue;
                    }
                }

                try
                {
                    ProcessRequest(context);
                }
                catch { }
            }
        }

        static void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                log.InfoFormat("Dispatching request '{0}'@{1}",
                    context.Request.Url.LocalPath, context.Request.RemoteEndPoint);
                IRequestHandler handler;

                if (!RequestHandlers.Handlers.TryGetValue(context.Request.Url.LocalPath, out handler))
                {
                    context.Response.StatusCode = 400;
                    context.Response.StatusDescription = "Bad request";
                    using (StreamWriter wtr = new StreamWriter(context.Response.OutputStream))
                        wtr.Write("<h1>Bad request</h1>");
                }
                else
                    handler.HandleRequest(context);
            }
            catch (Exception e)
            {
                using (StreamWriter wtr = new StreamWriter(context.Response.OutputStream))
                    wtr.Write("<Error>Internal Server Error</Error>");
                log.Error("Error when dispatching request", e);
            }

            context.Response.Close();
        }
    }
}
