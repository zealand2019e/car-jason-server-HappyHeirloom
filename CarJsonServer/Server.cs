using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLib;

namespace CarJsonServer
{
    public class Server
    {
        static int _clientNr = 0;
        private static string _model;
        private static string _color;
        private static string _registrationNumber;

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

            string[] wordsArray;
            string inputLine = "";
            try
            {
                inputLine = reader.ReadLine();
                while (inputLine != null && inputLine != " ")
                {
                    writer.WriteLine($"string: {inputLine}");
                    Console.WriteLine($"string: {inputLine}");


                    wordsArray = inputLine.Split(" ");

                    if (wordsArray.Length == 3)
                    {
                        _model = wordsArray[0];
                        _color = wordsArray[1];
                        _registrationNumber = wordsArray[2];

                        Car car = new Car(_model, _color, _registrationNumber);
                        writer.WriteLine($"Your car is a {_color} {_model} with reg.nr {_registrationNumber}");

                    }
                    else
                    {
                        writer.WriteLine("Enter a valid car object. ('Model color regNr' Ford yellow SK18922 ) ");
                    }

                    inputLine = reader.ReadLine();
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
