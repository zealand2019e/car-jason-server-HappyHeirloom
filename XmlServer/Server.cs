using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ModelLib;

namespace XmlServer
{
    class Server
    {
        static void Main(string[] args)
        {
            StartServer();
        }

        public static void StartServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 20001);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Server activated");

                Stream ns = client.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns) { AutoFlush = true };

                var message = sr.ReadToEnd();
                Console.WriteLine(message);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Car));

                using (StringReader reader = new StringReader(message))
                {
                    Car carCopy = (Car) xmlSerializer.Deserialize(reader);
                    Console.WriteLine(carCopy);
                }

            }

        }
    }
}
