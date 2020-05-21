using System;
using System.Windows.Forms;
using Minecraft_Server_Assistant.Source;

namespace Minecraft_Server_Assistant.Source.GUI
{
    public partial class ServerConsole : UserControl
    {
        private int serverID;

        public ServerConsole()
        {
            InitializeComponent();
        }

        public void SetID(int id)
        {
            serverID = id;
        }

        public void WriteLine(string x)
        {
            Console.Text += x;
            Console.Text += "\r\n";
        }

        private void Input_TextChanged(object sender, EventArgs e)
        {
            System.Console.WriteLine(sender.ToString());
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {   
                ServerManager.Input(serverID, Input.Text);
                WriteLine(Input.Text);
                Input.Text = "";
                
            }
        }
    }
}
