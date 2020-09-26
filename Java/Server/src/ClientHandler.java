//package Server;


import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class ClientHandler implements Runnable {
    private final Socket socket;
    private Players players;
    private int playerID;
    private PrintWriter writer;

    public ClientHandler(Socket socket, Players players, int ID) {
        this.socket = socket;
        this.players = players;
        this.playerID = ID;
        try {
            writer = new PrintWriter(socket.getOutputStream(), true);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        //creates a new player when they join
        players.createPlayer(playerID);
        //checks playerMap for if there is only one player left and gives them the ball
        if (players.getPlayerMap().size() == 1) {
            players.giveBall(playerID);
        }

        try {
            Scanner scanner = new Scanner(socket.getInputStream());

            try {
                //prints message when new player joins, and sends them information of the current state of the game
                System.out.println("New connection; Player ID " + playerID);
                writer.println(playerID);
                sendAllPlayersInGame(writer);
                writer.println("you are player: " + playerID);

                while (true) {
                    //reads in input from users and calls the corresponding function
                    String line = scanner.nextLine();
                    String[] substrings = line.split(" ");
                    switch (substrings[0].toLowerCase()) {
                        case "show":
                            sendAllPlayersInGame(writer);

                            break;
                        case "pass":
                            int passToID = Integer.parseInt(substrings[1]);
                            if (players.playerExist(passToID)) {
                                players.passBall(playerID, passToID);

                            }
                            break;
                        case "create":
                            players.createPlayer(playerID);

                            break;
                        case "find":
                            writer.println(players.findBall());

                            break;
                        default:
                            throw new Exception("Unkown command: " + substrings[0]);
                    }
                }

            } catch (Exception e) {
                writer.println("ERROR " + e.getMessage());
                socket.close();
            }
        } catch (Exception e) {
        } finally {
            //deals with the leaving of users, removes them from the player list and if the player with the ball
            // leaves a new player is given the ball
            System.out.println("Player " + playerID + " has disconnected.");
            if (playerID == players.findBall() && Players.playerMap.size() > 0) {
                Players.playerMap.removeIf((Player p) -> p.getPlayerID() == this.playerID);
                if (Players.playerMap.size() > 0) {
                    Player newBallPlayer = Players.playerMap.get(0);
                    newBallPlayer.setWhoHasBall();
                    System.out.println("Player " + newBallPlayer.getPlayerID() + " now has ball");

                }
            } else {
                Players.playerMap.removeIf((Player p) -> p.getPlayerID() == this.playerID);
            }
        }

    }
    // iterates through list of players, creating a string of their IDs and sends it to the client
    public void sendAllPlayersInGame(PrintWriter writer) {
        String IDS;
        List<Integer> listOfPlayers = players.getListOfPlayers();
        if (listOfPlayers.size() > 0) {
            IDS = listOfPlayers.get(0).toString();
            for (int i = 1; i < listOfPlayers.size(); i++)
                IDS += "," + listOfPlayers.get(i).toString();
            writer.println(IDS);
        }
    }
}

