using ModelLib;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace XmlClient
{
    class Client
    {
        static void Main(string[] args)
        {
            StartClient();
        }

        public static void StartClient()
        {
            int port = 20001;

            TcpClient client = new TcpClient("localhost", port);

            Car car = new Car{Model = "Ford", Color = "Yellow", RegistrationNumber = "DKDKDK"};

            using (client)
            {
                NetworkStream ns = client.GetStream();
                StreamReader reader = new StreamReader(ns);
                StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

                XmlSerializer xmlSerializer = new XmlSerializer(car.GetType());
                string message = "empty";

                using (StringWriter texWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(texWriter, car);
                    message = texWriter.ToString();
                    Console.WriteLine(message);
                }
                writer.WriteLine(message);
            }
        }
    }
}
