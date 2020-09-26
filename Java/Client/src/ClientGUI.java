import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class ClientGUI extends JFrame implements ActionListener {

    public JLabel label;
    public JComboBox dropDown;
    public JButton button;

    private Client client;

    //crates the GUI when the client is made
    public ClientGUI(Client client){
        this.client = client;
        setLayout(new BorderLayout());
        setTitle("Player: "+ client.playerID);
        setSize(500,180);
        label = new JLabel();
        dropDown = new JComboBox();
        button = new JButton("PASS");
        JPanel panel = new JPanel();
        button.addActionListener(this);
        panel.add(label, BorderLayout.CENTER);
        panel.add(dropDown, BorderLayout.SOUTH);
        panel.add(button, BorderLayout.SOUTH);
        this.add(panel);
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        setVisible(true);
    }
    //when the button is pressed, the current combo box selection is saved
    //and then sent to the server
    @Override
    public void actionPerformed(ActionEvent e) {
        String input = String.valueOf(dropDown.getSelectedItem());
        String[] line = input.split(" ");
        int passToID = Integer.parseInt(line[1]);
        client.passBall(passToID);

    }
}
