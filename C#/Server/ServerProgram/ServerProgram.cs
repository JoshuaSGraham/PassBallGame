using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ServerProgram
{
    class ServerProgram
    {
        private const int port = 8888;
        private static readonly Players players = new Players();
        private static int ID = 0;

        static void Main(string[] args)
        {
            RunServer();
        }
        private static void RunServer()
        {
            //creates a server and assigns a new connection a id
            TcpListener listener = new TcpListener(IPAddress.Loopback, port);
             listener.Start();
             Console.WriteLine("Waiting for incoming connections...");
             while (true)
             {
                TcpClient tcpClient = listener.AcceptTcpClient();
                NetworkStream stream = tcpClient.GetStream();
                new ClientHandler(stream, players, ++ID).Start();
             }
        }
    }
}
