import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

public class Players {
    public static final List<Player> playerMap = new ArrayList<>();
    //creates player object with id and boolean hasball
    public int createPlayer(int playerID){

        Player player = new Player(playerID,false);
        playerMap.add(player);
        return playerID;
    }
    //returns list of playerIDs
    public List<Integer> getListOfPlayers(){
        List<Integer> result = new ArrayList<>();
        for (Player player : playerMap){
            result.add(player.getPlayerID());
        }
        return result;
    }
    //seaches the player list to check if passed ID exists
    public boolean playerExist(int playerID){
        for (Player player : playerMap){
            if (player.getPlayerID() == playerID){
                return true;
            }
        }
        return false;
    }
    //searches for designated player and then sets their bool true
    public void giveBall(int playerID){
        for (Player player : playerMap){
            if (player.getPlayerID() == playerID){
                player.setWhoHasBall();
            }
        }
    }
    //searches the player list for a player whos hasball bool is true
    public int findBall(){

        for (Player player : playerMap){
            if (player.getBall() == true){
                return player.getPlayerID();
            }
        }
        return 0;
    }
    public List<Player> getPlayerMap(){
        return playerMap;
    }
    //seaches the player list for the player with the ball, takes it from them and then gives it to
    //the player specified
    public void passBall(int playerID1, int playerID2){
        for (Player player : playerMap){
            if (player.getPlayerID() == playerID1){
                player.removeWhoHasBall();
            }
            else if (player.getPlayerID() == playerID2){
                player.setWhoHasBall();
            }
        }
    }
}
