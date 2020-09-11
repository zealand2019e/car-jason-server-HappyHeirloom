using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using ModelLib;

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

            while (true)
            {
                Console.Write("Enter your car model 'ford': ");
                var model = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Enter your car color 'yellow': ");
                var color = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Enter your car regNr 'SK99821': ");
                var regNr = Console.ReadLine();
                Console.WriteLine();

                Car line = new Car(model, color, regNr);
                writer.WriteLine(line);
                Console.WriteLine($"Sending {line} to server");
                string lineReceived = reader.ReadLine();
                Console.WriteLine($"Received {lineReceived} from server");
                Console.WriteLine();
            }

        }
    }
}
