using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using ModelLib;
using Newtonsoft.Json;

namespace CarJsonClient
{
    class Client
    {
        public static void Start()
        {
            int port = 10001;

            TcpClient client = new TcpClient("localhost", port);

            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            Console.WriteLine(reader.ReadLine());
            Car car = new Car("Tesla", "Green", "SK98129");


            while (true)
            {
                Console.ReadLine();
                var serializedLine = JsonConvert.SerializeObject(car);
                writer.WriteLine(serializedLine);
                Console.WriteLine($"Sending {serializedLine} to server");

                string lineReceived = reader.ReadLine();
                Console.WriteLine($"Received '{lineReceived}' from server");
                Console.WriteLine();
            }

        }
    }
}
