using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgram
{
    class Players
    {
        public  List<Player> playerMap = new List<Player>();
        //adds a new player object to the player list
        public int createPlayer(int playerID)
        {
            Player player = new Player(playerID, false);
            playerMap.Add(player);
            return playerID;
        }
        //returns a list of ints to player ids
        public List<int> getListOfPlayers()
        {
            List<int> result = new List<int>();
            foreach(Player player in playerMap)
            {
                result.Add(player.getPlayerID());
            }
            return result;
        }
        //searches the player list for a player id to check if they exist
        public bool playerExist(int playerID)
        {
            foreach(Player player in playerMap)
            {
                if(player.getPlayerID() == playerID)
                {
                    return true;
                }
            }
            return false;
        }
        //searhces for a player with a player id and gives them the ball
        public void giveBall(int playerID)
        {
            foreach(Player player in playerMap)
            {
                if(player.getPlayerID() == playerID)
                {
                    player.setWhoHasBall();
                }
            }
        }
        //searches the player list for the ball
        public int findBall()
        {
            foreach(Player player in playerMap)
            {
                if(player.getBall() == true)
                {
                    return player.getPlayerID();
                }
            }
            return 0;
        }
        public List<Player> getPlayerMap()
        {
            return playerMap;
        }
        //passes the ball from one player to another
        public void passBall(int playerID1, int playerID2)
        {
            foreach(Player player in playerMap)
            {
                if(player.getPlayerID() == playerID1)
                {
                    player.removeWhoHasBall();
                }
                else if (player.getPlayerID() == playerID2)
                {
                    player.setWhoHasBall();
                }
            }
        }








    }
}
