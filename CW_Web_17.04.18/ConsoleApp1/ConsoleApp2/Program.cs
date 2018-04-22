using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip_server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3081);
            var clientUdp = new UdpClient();
            clientUdp.Connect(ip_server);
            var buff = clientUdp.Receive(ref ip_server);
            //WriteLine("Sending datagrams to the server...");
            //string message = "Helooo-o-o-o";
            //clientUdp.Send(Encoding.UTF8.GetBytes(message), message.Length, ip_server);
            WriteLine(Encoding.ASCII.GetString(buff));
            ReadLine();
        }
    }
}
