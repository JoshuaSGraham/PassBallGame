using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgram
{
    class Player
    {
        //getters and setters for the player to provide easy access to details about specififc players
        private readonly int playerID;
        private bool hasBall;

        public Player(int playerID, bool hasBall)
        {
            this.playerID = playerID;
            this.hasBall = hasBall;
        }
        public int getPlayerID()
        {
            return playerID;
        }
        public void setWhoHasBall()
        {
            hasBall = true;
        }
        public void removeWhoHasBall()
        {
            hasBall = false;
        }
        public bool getBall()
        {
            return hasBall;
        }
    }
}
