public class Player {
    private final int playerID;
    private  boolean hasBall;

    public Player(int playerID, boolean hasBall){
        this.playerID = playerID;
        this.hasBall = hasBall;
    }

    public int getPlayerID(){return playerID;}

    public void setWhoHasBall(){hasBall = true;}
    public void removeWhoHasBall(){hasBall = false;}
    public boolean getBall(){return hasBall;}
}
