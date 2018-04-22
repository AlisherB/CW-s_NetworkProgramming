using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CW_Web_12._04._18
{
    //public delegate void OnAcceptSocketDelegate(Socket socket);
    //class SyncServer
    //{
    //    private Socket listener;
    //    public OnAcceptSocketDelegate OnAccept { get; set; }
    //    public SyncServer()
    //    {
    //        this.listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
    //    }
    //    public void Bind(string ip, int port)
    //    {
    //        listener.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
    //    }
    //    public void Start()
    //    {
    //        this.listener.Listen(10);
    //        this.listener.Accept();
    //    }
        
    //}
    

    class Program
    {
        static void Main(string[] args)
        {
            ChatServerLogic csl = new ChatServerLogic();
            csl.Start("127.0.0.1", 3080);
        }
    }
}
