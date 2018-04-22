using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static System.Console;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var udp_listener = new UdpClient("127.255.255.255", 3081);
            udp_listener.ExclusiveAddressUse = true;
            udp_listener.EnableBroadcast = true;
            
            
            Thread th = new Thread(() => {
                var ipEp = new IPEndPoint(IPAddress.Any, 0);
                var ip_Br = new IPEndPoint(IPAddress.Parse("127.255.255.255"), 0);
                var ms = new MemoryStream();
                WriteLine("Waiting to receive datagrams from client...");
                var msg = Encoding.ASCII.GetBytes("Hello");
                udp_listener.Send(msg, msg.Length, ip_Br);
                //buff = udp_listener.Receive(ref ipEp);
                //ms.Write(buff, 0, buff.Length);
                //if (ms.Length > 0)
                //{
                //    WriteLine(Encoding.UTF8.GetString(ms.GetBuffer()));
                //}
            });
            th.Start();
            ReadLine();
        }
    }
}
