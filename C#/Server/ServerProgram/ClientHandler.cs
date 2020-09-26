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
    class ClientHandler
    {
        private Thread thread;
        private Players players;
        private int playerID;
        private NetworkStream stream;

        public ClientHandler(NetworkStream stream, Players players, int ID)
        {
            // ClientHandler constructor like in java
            this.stream = stream;
            this.players = players;
            this.playerID = ID;
            thread = new Thread(Run);
        }

        public void Run()
        {
            // Implement runnable run() here
            //gives the ball to the player if they are the only player in the game
            players.createPlayer(playerID);
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);
            if (players.getPlayerMap().Count() == 1)
            {
                players.giveBall(playerID);
            }
            try
            {
                //reads in input from users and calls the corresponding function
                Console.WriteLine("New connection; Player ID " + playerID);
                writer.WriteLine(playerID);
                writer.Flush();
                sendAllPlayersInGame(writer);
                writer.WriteLine("you are player: " + playerID);
                writer.Flush();
                while (true)
                {
                    String line = reader.ReadLine();
                    String[] substrings = line.Split(' ');
                    switch (substrings[0].ToLower())
                    {
                        case "show":
                            sendAllPlayersInGame(writer);
                            break;

                        case "pass":
                            Console.WriteLine("TYRING TO PASS");
                            int passToID = int.Parse(substrings[1]);
                            Console.WriteLine(passToID);
                            if (players.playerExist(passToID))
                            {
                                players.passBall(playerID, passToID);
                            }
                            break;
                        case "create":
                            players.createPlayer(playerID);
                            break;
                        case "find":
                            writer.WriteLine(players.findBall());
                            writer.Flush();
                            break;
                        default:
                            throw new Exception("Unkown command: " + substrings[0]);
                    }
                }
            }
            catch (Exception e)
            {
                writer.WriteLine("ERROR " + e.Message);
                stream.Close();
            }
            finally
            {
                //deals with the leaving of users, removes them from the player list and if the player with the ball
                // leaves a new player is given the ball
                Console.WriteLine("Player " + playerID + " has disconnected.");
                Console.WriteLine(players.getListOfPlayers());
                if (playerID == players.findBall() && players.playerMap.Count() > 0)
                {
                    if (players.playerMap.Count() > 0)
                    {
                        Player newBallPlayer = players.playerMap[0];
                        newBallPlayer.setWhoHasBall();
                        Console.WriteLine("Player " + newBallPlayer.getPlayerID() + " now has ball");
                    }
                    else
                    {
                        players.playerMap.RemoveAll(p => p.getPlayerID() == playerID);
                    }
                }
            }
        }
        // iterates through list of players, creating a string of their IDs and sends it to the client
        void sendAllPlayersInGame(StreamWriter writer)
            {
                String IDS;
                List<int> listOfPlayers = players.getListOfPlayers();
                IDS = String.Join(",", listOfPlayers);
                writer.WriteLine(IDS);
            writer.Flush();
            }
        public void Start()
        {
            thread.Start();
        }
    }
}