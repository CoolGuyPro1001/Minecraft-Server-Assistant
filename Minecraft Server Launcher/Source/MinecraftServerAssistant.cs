using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Minecraft_Server_Assistant.GUI;

namespace Minecraft_Server_Assistant
{
    public partial class MinecraftServerAssistant : Form
    {
        private string directory;
        private ServerManager manager;
        private List<Tuple<Label, Button, Button, Button, Button>> servers;

        private const int LABEL_X_POSITION = 7;
        private const int DELETE_BUTTON_X_POSITION = 329;
        private const int RUN_BUTTON_X_POSITION = 272;
        private const int LINK_BUTTON_X_POSITION = 386;
        private const int OPTIONS_BUTTON_X_POSITION = 443;
        private const int COMPONENT_WIDTH = 50;
        private const int Y_INTERVAL = 70;

        public MinecraftServerAssistant()
        {
            manager = new ServerManager();
            InitializeComponent();
            directory = Directory.GetCurrentDirectory();
            servers = new List<Tuple<Label, Button, Button, Button, Button>>();
            ServerPanelStartUp();
        }

        private void ServerPanelStartUp()
        {
            int numOfServers = manager.ServerCount;
            if (numOfServers > 0)
            {
                string[] servers = manager.Servers;
                int i = 0;
                while (i < numOfServers)
                {
                    int yPosition = Y_INTERVAL * i;
                    AddServerInPanel(i, servers[i], yPosition);
                    i++;
                }
            }
        }

        private Bitmap CropImage(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }

        private void AddServerInPanel(int index, string name, int yPos)
        {
            servers.Add(Tuple.Create(new Label(), new Button(), new Button(), new Button(), new Button()));
            string file = directory + @"\Resources\Icons.png";
            Bitmap image = new Bitmap(file);

            Label newServerLabel = servers[index].Item1;
            newServerLabel.Location = new Point(LABEL_X_POSITION, yPos);
            newServerLabel.Size = new Size(250, COMPONENT_WIDTH);
            newServerLabel.Font = new Font("Microsoft Sans Serif", 25.0f);
            newServerLabel.TextAlign = ContentAlignment.MiddleCenter;
            newServerLabel.ForeColor = Color.FromName("White");
            newServerLabel.BackColor = Color.FromName("Transparent");
            newServerLabel.Text = name;
            ServerPanel.Controls.Add(newServerLabel);

            Button newRunButton = servers[index].Item2;
            newRunButton.Location = new Point(RUN_BUTTON_X_POSITION, yPos);
            newRunButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newRunButton.FlatStyle = FlatStyle.Flat;
            newRunButton.FlatAppearance.BorderSize = 0;
            newRunButton.BackColor = Color.FromName("Transparent");
            newRunButton.BackgroundImage = CropImage(image, new Rectangle(new Point(400, 0), new Size(50, 50)));
            ServerPanel.Controls.Add(newRunButton);
            newRunButton.Click += RunButton_Click;

            Button newDeleteButton = servers[index].Item3;
            newDeleteButton.Location = new Point(DELETE_BUTTON_X_POSITION, yPos);
            newDeleteButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newDeleteButton.FlatStyle = FlatStyle.Flat;
            newDeleteButton.FlatAppearance.BorderSize = 0;
            newDeleteButton.BackColor = Color.FromName("Transparent");
            newDeleteButton.BackgroundImage = CropImage(image, new Rectangle(new Point(450, 0), new Size(50, 50)));
            ServerPanel.Controls.Add(newDeleteButton);
            newDeleteButton.Click += DeleteButton_Click;

            Button newLinkButton = servers[index].Item4;
            newLinkButton.Location = new Point(LINK_BUTTON_X_POSITION, yPos);
            newLinkButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newLinkButton.Text = "Link";
            ServerPanel.Controls.Add(newLinkButton);
            newLinkButton.Click += LinkButton_Click;

            Button newServerPropertiesButton = servers[index].Item5;
            newServerPropertiesButton.Location = new Point(OPTIONS_BUTTON_X_POSITION, yPos);
            newServerPropertiesButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newServerPropertiesButton.Text = "Properties";
            ServerPanel.Controls.Add(newServerPropertiesButton);
            newServerPropertiesButton.Click += ServerPropertiesButton_Click;
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (Tuple<Label, Button, Button, Button, Button> server in servers)
            {
                if (server.Item2.Equals(b))
                {
                    MemorySize min = new MemorySize(500, "M");
                    MemorySize max = new MemorySize(2, "G");

                    bool signed = manager.RunServer(server.Item1.Text, min, max, false);
                    if (signed)
                    {
                        Console.WriteLine("Vrrroooom");
                    }
                    else
                    {
                        string tabName = server.Item1.Text + " EULA";
                        TabPage eulaPage = new TabPage(tabName);
                        foreach (TabPage page in Tabs.TabPages)
                        {
                            if (eulaPage.Text == page.Text)
                            {
                                return;
                            }
                        }

                        Tabs.TabPages.Add(eulaPage);
                        EULA eula = new EULA(server.Item1.Text, manager);
                        SetupTabPage(eulaPage, eula);
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            for (int i = 0; i < servers.Count; i++)
            {
                Tuple<Label, Button, Button, Button, Button> server = servers[i];
                if (server.Item3.Equals(b))
                {
                    manager.DeleteServer(server.Item1.Text);
                    server.Item1.Dispose();
                    server.Item2.Dispose();
                    server.Item3.Dispose();
                    server.Item4.Dispose();
                    server.Item5.Dispose();

                    Sort(i);
                    return;
                }
            }
        }

        private void Sort(int skipIndex)
        {
            List<Tuple<Label, Button, Button, Button, Button>> newServers = new List<Tuple<Label, Button, Button, Button, Button>>();
            for (int i = 0; i < servers.Count; i++)
            {
                if (i != skipIndex)
                {
                    newServers.Add(servers[i]);
                    Tuple<Label, Button, Button, Button, Button> addedServer = newServers[newServers.Count - 1];
                    addedServer.Item1.Location = new Point(addedServer.Item1.Location.X, Y_INTERVAL * newServers.IndexOf(addedServer));
                    addedServer.Item2.Location = new Point(addedServer.Item2.Location.X, Y_INTERVAL * newServers.IndexOf(addedServer));
                    addedServer.Item3.Location = new Point(addedServer.Item3.Location.X, Y_INTERVAL * newServers.IndexOf(addedServer));
                    addedServer.Item4.Location = new Point(addedServer.Item4.Location.X, Y_INTERVAL * newServers.IndexOf(addedServer));
                    addedServer.Item5.Location = new Point(addedServer.Item5.Location.X, Y_INTERVAL * newServers.IndexOf(addedServer));
                }
            }
            servers = newServers;
        }

        private void LinkButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (Tuple<Label, Button, Button, Button, Button> server in servers)
            {
                if (server.Item4.Equals(sender))
                {
                    manager.LinkServer(server.Item1.Text);
                }
            }
        }

        private void ServerPropertiesButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (Tuple<Label, Button, Button, Button, Button> server in servers)
            {
                if (server.Item5.Equals(sender))
                {
                    string tabName = server.Item1.Text + " Server Properties";
                    TabPage newPage = new TabPage(tabName);
                    foreach (TabPage page in Tabs.TabPages)
                    {
                        if (newPage.Text == page.Text)
                        {
                            return;
                        }
                    }

                    Tabs.TabPages.Add(newPage);
                    ServerProperties properties = new ServerProperties();
                    SetupTabPage(newPage, properties);
                }
            }
        }

        private void SetupTabPage(TabPage page, UserControl control)
        {
            page.Controls.Add(control);
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
        }

        private void CreateServerButton_Click(object sender, EventArgs e)
        {
            if (NewServerName.Text != "")
            {
                if (manager.CreateServer(NewServerName.Text) == "Success")
                {
                    int yPosition = Y_INTERVAL * servers.Count;
                    AddServerInPanel(servers.Count, NewServerName.Text, yPosition);
                }
                else if (manager.CreateServer(NewServerName.Text) == "Server Already Exists")
                {
                    WriteMessage("Server Already Exists In Server Folder");
                }

                NewServerName.Text = "";
            }
        }

        private void NewServerName_Click(object sender, EventArgs e)
        {
            NewServerName.Text = "";
            NewServerName.BackColor = Color.White;
        }

        private void Button_MouseHover(object sender, EventArgs e)
        {

        }

        public void ShutDownSequence()
        {
            manager.Close();
        }

        public void WriteMessage(string message)
        {
            Message.Text = message;
        }
    }
}
