using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CW_Web_12._04._18
{
    class ChatServerLogic
    {
        private static ManualResetEvent socketEvent = new ManualResetEvent(false);
        List<ClientContext> clientList = new List<ClientContext>();
        Dictionary<Guid, ClientContext> client2Id = new Dictionary<Guid, ClientContext>();
        private ClientContext RegisterClient(Socket socket)
        {
            lock (clientList)
            {
                var client = new ClientContext()
                {
                    Id = Guid.NewGuid(),
                    Socket = socket
                };
                clientList.Add(client);
                client2Id[client.Id] = client;
                return client;
            }
            
        }
        public void Start(string ip, int port)
        {
            Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            listener.Listen(100);
            while (true)//Главный поток
            {
                socketEvent.Reset();
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(BeginAcceptCallBack, listener);
                socketEvent.WaitOne();
            }
        }

        public void BeginAcceptCallBack(IAsyncResult ar)
        {
            Console.WriteLine("Start accept in " + Thread.CurrentThread.ManagedThreadId);
            var client = ((Socket)ar.AsyncState).EndAccept(ar);
            socketEvent.Set();
            var context = this.RegisterClient(client);
            client.BeginReceive(context.buffer, 0, ClientContext.BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), context);
        }

        public static void Read_Callback(IAsyncResult ar)
        {
            Console.WriteLine("Start read in " + Thread.CurrentThread.ManagedThreadId);
            ClientContext so = (ClientContext)ar.AsyncState;
            Socket s = so.Socket;
            int read = s.EndReceive(ar);
            if (read > 0)
            {
                so.SB.Append(Encoding.UTF8.GetString(so.buffer, 0, read));
                s.BeginReceive(so.buffer, 0, ClientContext.BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), so);
            }
            else
            {
                if (so.SB.Length > 1)
                {
                    string strContent;
                    strContent = so.SB.ToString();
                    Console.WriteLine(String.Format("Read {0} byte from socket data {1} ", strContent.Length, strContent));
                }
            }
        }

    }
    public class ClientContext
    {
        public static int BUFFER_SIZE = 1024;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Socket Socket { get; set; }
        public byte[] buffer = new byte[BUFFER_SIZE];
        public StringBuilder SB = new StringBuilder();
    }
}
