using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ClientProgram
{
    class ClientProgram
    {
        public static int whoHasBall = -1;
        public static int[] storedIDs = { };

        static void Main(String[] args)
        {
            try
            {
                Thread thread = new Thread(() =>
                {
                    using (Client client = new Client())
                    {
                        Console.WriteLine("Logged in successfully");
                        String line = client.reader.ReadLine();
                        line = client.reader.ReadLine();

                        storedIDs = new int[] { };

                        while (true)
                        {
                            lock (storedIDs)
                            {
                                UpdateGUI(client);
                            }
                        }
                    }
                });
                thread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        //checks to see if user has ball, if they do they get the option to enter who they want to pass to
        static void UpdateGUI(Client client)
        {
            int currentwhoHasBall = client.whoHasBall();
            int[] ids = client.getPlayersID();
            bool currentPlayerHasBall = client.playerID == currentwhoHasBall;

            if (whoHasBall != currentwhoHasBall || storedIDs.Length != ids.Length)
            {
                Console.WriteLine("Waiting for player: " + currentwhoHasBall + " to pass");
                if (currentPlayerHasBall)
                {
                    Console.WriteLine("You have the ball, select the player you wish to pass to");
                }
            }
            whoHasBall = currentwhoHasBall;
            storedIDs = ids;
        }
    }
}
