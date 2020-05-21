using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Minecraft_Server_Assistant.Source.GUI;

namespace Minecraft_Server_Assistant.Source
{
    public partial class MinecraftServerAssistant : Form
    {
        private string directory;
        private ServerManager manager;
        private List<Tuple<Label, Button, Button, Button, Button>> serverGUIs;

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
            serverGUIs = new List<Tuple<Label, Button, Button, Button, Button>>();
            ServerPanelStartUp();
        }

        private void ServerPanelStartUp()
        {
            int numOfServers = manager.ServerCount;
            if (numOfServers > 0)
            {
                List<string> servers = manager.ServerNames;
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
            serverGUIs.Add(Tuple.Create(new Label(), new Button(), new Button(), new Button(), new Button()));
            string file = directory + @"\Resources\Icons.png";
            Bitmap image = new Bitmap(file);

            Label newServerLabel = serverGUIs[index].Item1;
            newServerLabel.Location = new Point(LABEL_X_POSITION, yPos);
            newServerLabel.Size = new Size(250, COMPONENT_WIDTH);
            newServerLabel.Font = new Font("Microsoft Sans Serif", 25.0f);
            newServerLabel.TextAlign = ContentAlignment.MiddleCenter;
            newServerLabel.ForeColor = Color.FromName("White");
            newServerLabel.BackColor = Color.FromName("Transparent");
            newServerLabel.Text = name;
            ServerPanel.Controls.Add(newServerLabel);

            Button newRunButton = serverGUIs[index].Item2;
            newRunButton.Location = new Point(RUN_BUTTON_X_POSITION, yPos);
            newRunButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newRunButton.FlatStyle = FlatStyle.Flat;
            newRunButton.FlatAppearance.BorderSize = 0;
            newRunButton.BackColor = Color.FromName("Transparent");
            newRunButton.BackgroundImage = CropImage(image, new Rectangle(new Point(400, 0), new Size(50, 50)));
            ServerPanel.Controls.Add(newRunButton);
            newRunButton.Click += RunButton_Click;

            Button newDeleteButton = serverGUIs[index].Item3;
            newDeleteButton.Location = new Point(DELETE_BUTTON_X_POSITION, yPos);
            newDeleteButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newDeleteButton.FlatStyle = FlatStyle.Flat;
            newDeleteButton.FlatAppearance.BorderSize = 0;
            newDeleteButton.BackColor = Color.FromName("Transparent");
            newDeleteButton.BackgroundImage = CropImage(image, new Rectangle(new Point(450, 0), new Size(50, 50)));
            ServerPanel.Controls.Add(newDeleteButton);
            newDeleteButton.Click += DeleteButton_Click;

            Button newLinkButton = serverGUIs[index].Item4;
            newLinkButton.Location = new Point(LINK_BUTTON_X_POSITION, yPos);
            newLinkButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newLinkButton.Text = "Link";
            ServerPanel.Controls.Add(newLinkButton);
            newLinkButton.Click += LinkButton_Click;

            Button newServerPropertiesButton = serverGUIs[index].Item5;
            newServerPropertiesButton.Location = new Point(OPTIONS_BUTTON_X_POSITION, yPos);
            newServerPropertiesButton.Size = new Size(COMPONENT_WIDTH, COMPONENT_WIDTH);
            newServerPropertiesButton.Text = "Properties";
            ServerPanel.Controls.Add(newServerPropertiesButton);
            newServerPropertiesButton.Click += ServerPropertiesButton_Click;
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (Tuple<Label, Button, Button, Button, Button> serverGUI in serverGUIs)
            {
                if (serverGUI.Item2.Equals(b))
                {
                    MemorySize min = new MemorySize(500, "M");
                    MemorySize max = new MemorySize(2, "G");
                    ServerConsole output = new ServerConsole();

                    bool signed = manager.RunServer(serverGUI.Item1.Text, min, max, output, false);
                    if (signed)
                    {
                        string tabName = serverGUI.Item1.Text + " Output";
                        TabPage outputPage = CreateTabPage(tabName);
                        
                        SetupTabPage(outputPage, output);
                    }
                    else
                    {
                        string tabName = serverGUI.Item1.Text + " EULA";
                        TabPage eulaPage = CreateTabPage(tabName);
                        EULA eula = new EULA(serverGUI.Item1.Text, manager, this);
                        SetupTabPage(eulaPage, eula);
                    }
                }
            }
        }

        private TabPage CreateTabPage(string name)
        {
            TabPage newPage = new TabPage(name);
            foreach(TabPage page in Tabs.TabPages)
            {
                if (newPage.Text == page.Text)
                    return null;
            }

            Tabs.TabPages.Add(newPage);
            return newPage;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            for (int i = 0; i < serverGUIs.Count; i++)
            {
                Tuple<Label, Button, Button, Button, Button> serverGUI = serverGUIs[i];
                if (serverGUI.Item3.Equals(b))
                {
                    manager.DeleteServer(serverGUI.Item1.Text);
                    serverGUI.Item1.Dispose();
                    serverGUI.Item2.Dispose();
                    serverGUI.Item3.Dispose();
                    serverGUI.Item4.Dispose();
                    serverGUI.Item5.Dispose();

                    Sort(i);
                    return;
                }
            }
        }

        private void Sort(int skipIndex)
        {
            List<Tuple<Label, Button, Button, Button, Button>> newServerGUIs = new List<Tuple<Label, Button, Button, Button, Button>>();
            for (int i = 0; i < serverGUIs.Count; i++)
            {
                if (i != skipIndex)
                {
                    newServerGUIs.Add(serverGUIs[i]);
                    Tuple<Label, Button, Button, Button, Button> addedServer = newServerGUIs[newServerGUIs.Count - 1];
                    addedServer.Item1.Location = new Point(addedServer.Item1.Location.X, Y_INTERVAL * newServerGUIs.IndexOf(addedServer));
                    addedServer.Item2.Location = new Point(addedServer.Item2.Location.X, Y_INTERVAL * newServerGUIs.IndexOf(addedServer));
                    addedServer.Item3.Location = new Point(addedServer.Item3.Location.X, Y_INTERVAL * newServerGUIs.IndexOf(addedServer));
                    addedServer.Item4.Location = new Point(addedServer.Item4.Location.X, Y_INTERVAL * newServerGUIs.IndexOf(addedServer));
                    addedServer.Item5.Location = new Point(addedServer.Item5.Location.X, Y_INTERVAL * newServerGUIs.IndexOf(addedServer));
                }
            }
            serverGUIs = newServerGUIs;
        }

        private void LinkButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (Tuple<Label, Button, Button, Button, Button> serverGUI in serverGUIs)
            {
                if(serverGUI.Item4.Equals(sender))
                {
                    manager.LinkServer(serverGUI.Item1.Text);
                }
            }
        }

        private void ServerPropertiesButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (Tuple<Label, Button, Button, Button, Button> serverGUI in serverGUIs)
            {
                if (serverGUI.Item5.Equals(sender))
                {
                    string tabName = serverGUI.Item1.Text + " Server Properties";
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

        public void RemoveTabPage(UserControl control)
        {
            foreach(TabPage page in Tabs.TabPages)
            {
                if(page.Controls.Contains(control))
                {
                    page.Dispose();
                    Tabs.TabPages.Remove(page);
                    return;
                }
            }
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
                    int yPosition = Y_INTERVAL * serverGUIs.Count;
                    AddServerInPanel(serverGUIs.Count, NewServerName.Text, yPosition);
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
