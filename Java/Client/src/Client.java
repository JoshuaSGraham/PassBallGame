//package Player;

import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;

public class Client implements AutoCloseable {
    final int port = 8888;
    final Scanner reader;
    final PrintWriter writer;
    public int playerID;
    //creates client object and takes in its ID from the server
    public Client() throws Exception{
        Socket socket = new Socket("localhost", port);
        reader = new Scanner(socket.getInputStream());
        writer = new PrintWriter(socket.getOutputStream(), true);
        String line = reader.nextLine();
        if (line != null){
            playerID = Integer.parseInt(line);
        }
    }
    //sends the create message to the server to create a player
    public void createPlayer() throws Exception{
        writer.println("CREATE");
        String line = reader.nextLine();
        if (line.trim().compareToIgnoreCase("success")!= 0){
            throw new Exception(line);
        }
    }
    // sends the show message to the server
    //returns a int array of player ids
    public int[] getPlayersID(){
        writer.println("SHOW");
        String line = reader.nextLine();

        String[] inputString = line.split(",");

        int[] playerAccounts = new int[inputString.length];
        for (int i = 0; i < inputString.length; i++){
            playerAccounts[i] = Integer.parseInt(inputString[i]);
        }
        return playerAccounts;
    }
    //sends a find message to the server to find out which player has the ball
    public int whoHasBall(){
        writer.println("FIND");
        String line = reader.nextLine();
        return Integer.parseInt(line);
    }
    //sends a pass message with a player id
    public void passBall(int playerID) {
        writer.println("PASS "+playerID);
    }
    @Override
    public void close() throws Exception {
        reader.close();
        writer.close();
    }
}
