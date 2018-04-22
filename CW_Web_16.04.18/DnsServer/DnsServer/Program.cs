using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static System.Console;

namespace DnsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var udp_listener = new Socket(SocketType.Dgram, ProtocolType.Udp);
            var ip_server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3081);
            udp_listener.Bind(ip_server);
            Thread th = new Thread(()=>{
                byte[] buff = new byte[1024];
                EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                var ms = new MemoryStream();
                WriteLine("Waiting to receive datagrams from client...");
                var received = udp_listener.ReceiveFrom(buff, ref ep);
                ms.Write(buff, 0, received);
                if(ms.Length > 0)
                {
                    WriteLine(Encoding.UTF8.GetString(ms.GetBuffer()));
                }
            });
            th.Start();
            ReadLine();
            var clientUdp = new Socket(SocketType.Dgram, ProtocolType.Udp);
            WriteLine("Sending datagrams to the server...");
            string message = "Helooo-o-o-o";
            clientUdp.SendTo(Encoding.UTF8.GetBytes(message), message.Length, SocketFlags.None, ip_server);
            ReadLine();
        }
    }
}
