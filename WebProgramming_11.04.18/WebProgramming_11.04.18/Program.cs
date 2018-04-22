using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebProgramming_11._04._18
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Socket(socketType: SocketType.Stream, protocolType: ProtocolType.Tcp);
            server.Bind(new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 3080));
            server.Listen(10);
            var client = server.Accept();

            byte[] buffer = new byte[10];
            StringBuilder sb = new StringBuilder();
            while (client.Connected)
            {
                if (client.Poll(100, SelectMode.SelectRead))
                {
                    var readed = client.Receive(buffer);
                    for (int i = 0; i < readed; i++)
                    {
                        if (buffer[i] == 13)
                        {
                            client.Send(Encoding.UTF8.GetBytes("received the text: " + sb.ToString()));
                            sb.Clear();
                        }
                        else
                        {
                            if (buffer[i] == 10)
                                continue;
                            sb.Append(Convert.ToChar(buffer[i]));
                        }
                    }
                }
            }
        }
    }
}
