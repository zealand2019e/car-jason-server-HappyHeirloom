using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ModelLib;
using Newtonsoft.Json;

namespace CarJsonServer
{
    public class Server
    {
        static int _clientNr = 0;

        public static void Start()
        {
            int port = 10001;
            TcpListener listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                _clientNr++;
                Console.WriteLine("User connected");
                Console.WriteLine($"Number of users online {_clientNr}");

                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                }
                );
            }

        }

        public static void DoClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            writer.WriteLine("Server started...");

            try
            {
                string clientLine = reader.ReadLine();

                while (clientLine != null && clientLine != " ")
                {
                    Car deserializedCar = JsonConvert.DeserializeObject<Car>(clientLine);

                    writer.WriteLine($"string: {deserializedCar}");
                    Console.WriteLine($"string: {deserializedCar}");

                    clientLine = reader.ReadLine();

                    AutoSale deserializedCarDealer = JsonConvert.DeserializeObject<AutoSale>(clientLine);

                    writer.WriteLine($"string: {deserializedCarDealer}");
                    Console.WriteLine($"string: {deserializedCarDealer}");

                    clientLine = reader.ReadLine();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            ns.Close();
            _clientNr--;
            Console.WriteLine($"User disconnected... current number of users: {_clientNr}");
        }
    }
}
