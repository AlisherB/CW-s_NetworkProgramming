using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client(new byte[] { 127, 0, 0, 1 }, 3080);
            client.Call((socket) => 
                            {
                                client.Write("Hello \r\n");
                                var buffer = new byte[1000];
                                var received = 0;
                                StringBuilder sb = new StringBuilder();
                                while ((received = socket.Receive(buffer)) > 0)
                                {
                                    for (int i = 0; i < received; i++)
                                    {
                                        sb.Append(Convert.ToChar(buffer[i]));
                                    }
                                }
                            }
                        );
        }
    }
    public class Client
    {
        private Socket socket;
        public Client()
        {

        }

        public void Call(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
