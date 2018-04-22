using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Http
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://ru.wikipedia.org");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            Console.WriteLine(resp.Headers.ToString());
            Console.WriteLine(sr.ReadToEnd());
            HttpClient client = new HttpClient();
        }
    }
}
