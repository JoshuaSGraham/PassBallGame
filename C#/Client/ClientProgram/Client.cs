using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientProgram
{
    class Client : IDisposable
    {
        private int port = 8888;
        public StreamWriter writer;
        public StreamReader reader;
        public int playerID;

        public Client()
        {
            try
            {
                //creates connection to the server
                TcpClient tcpClient = new TcpClient("localhost", port);
                NetworkStream stream = tcpClient.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                String line = reader.ReadLine();
                if (line != null)
                {
                    playerID = int.Parse(line);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error in making the connection");
            }
        }
        //sends message to server to create a new player
        public void createPlayer()
        {
            try { 
            writer.WriteLine("CREATE");
            writer.Flush();
            String line = reader.ReadLine();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error in creating player");
            }

        }
        //sends message to server to get all player ids in the game
        public int[] getPlayersID()
        {
            try
            {
                writer.WriteLine("SHOW");
                writer.Flush();
                String line = reader.ReadLine();

                String[] inputString = line.Split(',');

                int[] playerAccounts = new int[inputString.Length];
                for (int i = 0; i < inputString.Length; i++)
                {
                    playerAccounts[i] = int.Parse(inputString[i]);
                }
                return playerAccounts;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error in returning player iD");
                return null;
            }
            
        }
        //sends message to the server which finds the player who has the ball
        public int whoHasBall()
        {
            try
            {
                writer.WriteLine("FIND");
                writer.Flush();
                string line = reader.ReadLine();
                return int.Parse(line.Trim());
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error in finding who has the ball");
            }
            return 1;
        }
        //sends message to the server to pass the ball and the id with which to give the ball to
        public void passBall(int playerID)
        {
            writer.WriteLine("PASS " + playerID);
            writer.Flush();
        }
        public void Dispose()
        {
            reader.Close();
            writer.Close();
        }

    }
}
   
