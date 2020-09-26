//package ;

import javax.swing.*;
import java.util.Scanner;

public class ClientProgram {
    public static ClientGUI clientGUI;
    public static int whoHasBall;
    public static int[] storedIDs;

    public static void main(String[] args) {
        try {
            //creates a client object with its GUI and reads from the server what its id is
            Scanner in = new Scanner(System.in);
            Client client = new Client();
            System.out.println("Logged in successfully");
            client.reader.nextLine();
            client.reader.nextLine();
            clientGUI = new ClientGUI(client);

            new Thread(() -> {
                while (true) {
                    updateGUI(client);
                }
            }).start();
        } catch (Exception e) {

            System.out.println(e.getMessage());
            e.printStackTrace();
        }
    }
    //updates GUI and enables the pass button if the client has the ball
    public static void updateGUI(Client client) {

        int currentwhoHasBall = client.whoHasBall();
        int[] ids = client.getPlayersID();
        boolean currentPlayerHasBall = client.playerID == currentwhoHasBall;
        if (whoHasBall != currentwhoHasBall || storedIDs.length != ids.length) {

            SwingUtilities.invokeLater(() -> {
                clientGUI.label.setText("Waiting for player: " + currentwhoHasBall + " to pass");
                clientGUI.button.setEnabled(false);
                clientGUI.dropDown.removeAllItems();
                for (int id : ids) {
                    String inputString = "Player: " + id;
                    clientGUI.dropDown.addItem(inputString);
                }
                if (currentPlayerHasBall) {
                    clientGUI.label.setText("You have the ball, select the player you wish to pass to");
                    clientGUI.button.setEnabled(true);
                }
            });
            whoHasBall = currentwhoHasBall;
            storedIDs = ids;
        }
    }
}
