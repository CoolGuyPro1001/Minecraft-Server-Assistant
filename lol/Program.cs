using System;using System.Windows.Forms;using Minecraft_Server_Launcher.GUI;using Newtonsoft.Json;using System.IO;usingSystem.Collections.Generic;
using System.Drawing;namespace Minecraft_Server_Launcher{static class Program{
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MinecraftServerLauncher());
        }
    }
    public class ServerManager
    {
        private string launcherDirectory; private
string serverFile; private string jsonFile; public MinecraftServerLauncher GUI; JsonData jsonData; public
ServerManager(MinecraftServerLauncher gui)
        {
            launcherDirectory = Directory.GetCurrentDirectory();
            jsonFile = launcherDirectory + @"\Resources\Minecraft Server Data.JSON";
            serverFile = launcherDirectory + @"\Resources\server.jar";
            jsonData = JsonConvert.DeserializeObject<JsonData>(ReadJsonFile()); if (jsonData.RootDirectory == null)
            {
                FolderBrowserDialog
setupServerFolder = new FolderBrowserDialog(); setupServerFolder.Description = "Select A Folder To Store Servers";
                setupServerFolder.ShowDialog();
                jsonData.RootDirectory = setupServerFolder.SelectedPath; setupServerFolder.Dispose();
            }
            GUI = gui;
        }
        private string
ReadJsonFile()
        {
            if (!File.Exists(jsonFile)) { Console.WriteLine("Can't Find JSON File"); return ""; }
            else
            {
                string
text = File.ReadAllText(jsonFile); return text;
            }
        }
        public int FindServer(string name)
        {
            List<MinecraftServer>
servers = jsonData.MinecraftServers; for (int i = 0; i < servers.Count; i++) { if (servers[i].Name == name) { return i; } }
            return -1;
        }
        public void CreateServer(string name)
        {
            string newServerDirectory = jsonData.RootDirectory + @"\" + name;
            jsonData.MinecraftServers.Add(new MinecraftServer(name, newServerDirectory));
            Directory.CreateDirectory(newServerDirectory); string newServerFile = newServerDirectory +
            @"\server.jar"; File.Copy(serverFile, newServerFile);
        }
        public void RunServer() { }
        public void StopServer(string
minecraftServer)
        { }
        public void DeleteServer(string name)
        {
            int index = FindServer(name);
            Directory.Delete(jsonData.MinecraftServers[index].Directory, true); jsonData.MinecraftServers.RemoveAt(index);
        }
        public
void LinkServer(string minecraftServer)
        {
            FolderBrowserDialog setupLink = new FolderBrowserDialog();
            setupLink.Description = "Select A Minecraft Save"; setupLink.ShowDialog();
            jsonData.MinecraftServers[FindServer(minecraftServer)].LinkedWorldDirectory = setupLink.SelectedPath;
            setupLink.Dispose();
        }
        public void UnlinkServerFromSinglePlayerWorld(string minecraftServer)
        {//Unlink Server }} public
class JsonData { public string RootDirectory { get; set; } public List<MinecraftServer> MinecraftServers { get; set; } }
        public
class MinecraftServer
        {
            public string Name { get; set; }
            public string Directory { get; set; }
            public bool Active { get; set; }
            public
string LinkedWorldDirectory
            { get; set; }
            [JsonConstructor]
            public MinecraftServer(string name, string
directory)
            { Name = name; Directory = directory; }
            public MinecraftServer(string name, string directory, string
linkedWorldDirectory)
            { Name = name; Directory = directory; LinkedWorldDirectory = linkedWorldDirectory; }
            public void
RunServer(string minMemory, string maxMemory)
            {//Code }}} namespace Minecraft_Server_Launcher.GUI{public partial class
                MinecraftServerLauncher: Form{private ServerManager manager; private
                Dictionary<string, Tuple<Label, Button, Button, Button>> servers; private const int labelXPos = 7; private const int
                       deleteButtonXPos = 329; private const int runButtonXPos = 272; private const int linkButtonXPos = 386; private const int
                               componentWidth = 50; public MinecraftServerLauncher()
            {
                InitializeComponent(); manager = new ServerManager(this); servers = new
Dictionary<string, Tuple<Label, Button, Button, Button>>();
            }
            private void AddServerInPanel(string
name)
            {
                servers.Add(name, Tuple.Create(new Label(), new Button(), new Button(), new Button())); Label
              newServerLabel = servers[name].Item1; newServerLabel.Location = new Point(labelXPos, 20); newServerLabel.Size = new
                    Size(250, componentWidth); newServerLabel.Font = new Font("Microsoft Sans Serif",
                      25.0f); newServerLabel.TextAlign = ContentAlignment.MiddleCenter; newServerLabel.Text = name;
                ServerPanel.Controls.Add(newServerLabel); Button newRunButton = servers[name].Item2; newRunButton.Location = new
                   Point(runButtonXPos, 20); newRunButton.Size = new Size(componentWidth, componentWidth); newRunButton.Text = "Run";
                ServerPanel.Controls.Add(newRunButton); Button newDeleteButton = servers[name].Item3; newDeleteButton.Location = new
                   Point(deleteButtonXPos, 20); newDeleteButton.Size = new Size(componentWidth, componentWidth); newDeleteButton.Text = "Delete";
                ServerPanel.Controls.Add(newDeleteButton); newDeleteButton.Click += DeleteButton_Click; Button
                 newLinkButton = servers[name].Item4; newLinkButton.Location = new Point(linkButtonXPos, 20); newLinkButton.Size = new
                       Size(componentWidth, componentWidth); newLinkButton.Text = "Link";
                ServerPanel.Controls.Add(newLinkButton); newLinkButton.Click += LinkButton_Click;
            }
            private void RunButton_Click(object
sender, EventArgs e)
            { Console.WriteLine("Running"); }
            private void DeleteButton_Click(object sender, EventArgs e)
            {
                Button
b = (Button)sender; foreach (KeyValuePair<string, Tuple<Label, Button, Button, Button>> server in servers)
                {
                    if (server.Value.Item3.Equals(sender)) { manager.DeleteServer(server.Key); }
                }
            }
            private void LinkButton_Click(object
sender, EventArgs e)
            {
                Button b = (Button)sender; foreach (KeyValuePair<string, Tuple<Label, Button, Button, Button>> server in
servers) { if (server.Value.Item4.Equals(sender)) { manager.LinkServer(server.Key); } }
            }
            private void
CreateServerButton_Click(object sender, EventArgs e)
            {
                if (NewServerName.Text != "")
                {
                    manager.CreateServer(NewServerName.Text);
                }
                AddServerInPanel(NewServerName.Text);
            }
            private void
NewServerName_Click(object sender, EventArgs e)
            { NewServerName.Text = ""; NewServerName.BackColor = Color.White; }
        }
        partial
class MinecraftServerLauncher
        {
            private System.ComponentModel.IContainer components = null; protected override void
Dispose(bool disposing)
            { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }
            private void
InitializeComponent()
            {
                resources = new
System.ComponentModel.ComponentResourceManager(typeof(MinecraftServerLauncher)); this.CreateServerButton = new
System.Windows.Forms.Button(); this.NewServerName = new System.Windows.Forms.TextBox(); this.ServerPanel = new
System.Windows.Forms.Panel(); this.SuspendLayout(); System.Drawing.Font("Microsoft Sans Serif", 20F);
                this.CreateServerButton.Location = new System.Drawing.Point(570, 236);
                this.CreateServerButton.Name = "CreateServerButton"; this.CreateServerButton.Size = new System.Drawing.Size(337, 134);
                this.CreateServerButton.TabIndex = 0; this.CreateServerButton.Text = "Create A Server";
                this.CreateServerButton.UseVisualStyleBackColor = true; this.CreateServerButton.Click += new
                   System.EventHandler(this.CreateServerButton_Click); System.Drawing.Color.White; this.NewServerName.Font = new
                    System.Drawing.Font("Microsoft Sans Serif", 50F); this.NewServerName.Location = new
                     System.Drawing.Point(538, 392); this.NewServerName.Name = "NewServerName"; this.NewServerName.Size = new
                         System.Drawing.Size(400, 83); this.NewServerName.TabIndex = 1; this.NewServerName.Click += new
                              System.EventHandler(this.NewServerName_Click); System.Windows.Forms.DockStyle.Left; this.ServerPanel.Location = new
                               System.Drawing.Point(0, 0); this.ServerPanel.Name = "ServerPanel"; this.ServerPanel.Size = new
                                  System.Drawing.Size(500, 526); this.ServerPanel.TabIndex = 5; MinecraftServerLauncher
System.Windows.Forms.AutoScaleMode.Font; this.ClientSize = new System.Drawing.Size(984, 526);
                this.Controls.Add(this.CreateServerButton); this.Controls.Add(this.ServerPanel); this.Controls.Add(this.NewServerName);
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon"))); this.Name = "MinecraftServerLauncher"; this.Text =
                      "Minecraft Server Launcher"; this.ResumeLayout(false); this.PerformLayout();
            }
            private System.Windows.Forms.Button
CreateServerButton; private System.Windows.Forms.TextBox NewServerName; private System.Windows.Forms.Panel ServerPanel;
        }
    }