using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Ftp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var server = new BroadcastServer();
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://ru.wikipedia.org");
            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //StreamReader sr = new StreamReader(resp.GetResponseStream());
            //Console.WriteLine(resp.Headers.ToString());
            //Console.WriteLine(sr.ReadToEnd());

            var ftp = new FtpClient();
            string path = @"Z://ubuntu-archive-keyring.gpg";
            try
            {
                ftp.Url = "ftp://ftp.archive.ubuntu.com/ubuntu/project/ubuntu-archive-keyring.gpg";
                ftp.DownloadFile(path);
                Console.WriteLine("Файл скачан и находится по пути:\n{0}", path);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            //Console.WriteLine(ftp.GetDir());
            //foreach (String s in ftp.GetDirectoryList())
            //{
            //    Console.WriteLine(s);
            //}
            Console.ReadLine();
        }
    }

    //internal class BroadcastServer
    //{
    //    public BroadcastServer()
    //    {
    //    }
    //}

    class FtpClient
    {
        public String Url { get; set; }

        public FtpWebRequest InitRequest(String Method)
        {
            var rslt = (FtpWebRequest)WebRequest.Create(this.Url);
            rslt.Method = Method;
            rslt.KeepAlive = true;
            rslt.UsePassive = true;
            rslt.UseBinary = true;
            return rslt;
        }

        public IEnumerable<String> GetDirectoryList()
        {
            var req = InitRequest(WebRequestMethods.Ftp.ListDirectoryDetails);
            var resp = (FtpWebResponse)req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream());
            while (sr.Peek() >= 0)
            {
                yield return sr.ReadLine();
            }
            sr.Close();
            resp.Close();
        }

        public String GetDir()
        {
            var req = InitRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);
            var resp = (FtpWebResponse)req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd();
        }
        public void DownloadFile(String path)
        {
            var req = InitRequest(WebRequestMethods.Ftp.DownloadFile);
            var resp = (FtpWebResponse)req.GetResponse();
            var stream = resp.GetResponseStream();
            var ms = new FileStream(path, FileMode.CreateNew);
            stream.CopyTo(ms);
            ms.Flush();
            ms.Close();
        }
    }
}
