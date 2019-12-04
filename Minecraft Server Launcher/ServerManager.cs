using Newtonsoft.Json;
using System.IO;
using System;
using System.Windows.Forms;
using Minecraft_Server_Launcher.GUI;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minecraft_Server_Launcher
{
    public class ServerManager
    {
        private string launcherDirectory;
        private string serverFile;
        private string bootupFile;
        private string jsonFile;
        public int ServerCount { get; set; }

        public MinecraftServerLauncher GUI;

        private JsonData jsonData;
        public string[] Servers { get; }

        public bool AllowNether { get; set; }

        public ServerManager(MinecraftServerLauncher gui)
        {
            launcherDirectory = Directory.GetCurrentDirectory();
            jsonFile = launcherDirectory + @"\Resources\Minecraft Server Data.JSON";
            serverFile = launcherDirectory + @"\Resources\server.jar";
            bootupFile = launcherDirectory + @"\Resources\Bootup.bat";
            jsonData = JsonConvert.DeserializeObject<JsonData>(ReadJsonFile());

            ServerCount = jsonData.MinecraftServers.Count;
            Servers = new string[256];

            if(jsonData.RootDirectory == null)
            {
                FolderBrowserDialog setupServerFolder = new FolderBrowserDialog();
                setupServerFolder.Description = "Select A Folder To Store Servers";
                
                setupServerFolder.ShowDialog();
                if (setupServerFolder.SelectedPath != "")
                {
                    jsonData.RootDirectory = setupServerFolder.SelectedPath;
                }

                setupServerFolder.Dispose();
            }

            for (int i = 0; i < jsonData.MinecraftServers.Count; i++)
            {
                Servers[i] = jsonData.MinecraftServers[i].Name;
            }

            GUI = gui;
        }

        private string ReadJsonFile()
        {
            if(!File.Exists(jsonFile))
            {
                Console.WriteLine("Can't Find JSON File");
                return "";
            }
            else
            {
                string text = File.ReadAllText(jsonFile);
                return text;
            }
        }

        public int FindServer(string name)
        {
            List<MinecraftServer> servers = jsonData.MinecraftServers;
            for (int i = 0; i < servers.Count; i++)
            {
                if (servers[i].Name == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public string CreateServer(string name)
        {
            string newServerDirectory = jsonData.RootDirectory + @"\" + name;
            if (Directory.Exists(newServerDirectory))
            {
                return "Server Already Exists";
            }
            else
            {
                jsonData.MinecraftServers.Add(new MinecraftServer(name, newServerDirectory));
                Directory.CreateDirectory(newServerDirectory);

                string newServerFile = newServerDirectory + @"\server.jar";
                File.Copy(serverFile, newServerFile);

                string newBootupFile = newServerDirectory + @"\Bootup.bat";
                File.Copy(bootupFile, newBootupFile);
                return "Success";
            }
        }

        public void RunServer(string serverName)
        {
            int index = FindServer(serverName);
            string directory = jsonData.MinecraftServers[index].Directory;
            string bootupFile = directory + @"\Bootup.bat";

            string text = File.ReadAllText(bootupFile);
            text = text.Insert(17, directory);
            File.WriteAllText(bootupFile, text);
            try
            {
                using (Process runServer = new Process())
                {
                    runServer.StartInfo.FileName = bootupFile;
                    runServer.StartInfo.UseShellExecute = false;
                    runServer.StartInfo.CreateNoWindow = false;
                    runServer.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    runServer.OutputDataReceived += RecieveData;
                    runServer.Start();

                    if (!jsonData.MinecraftServers[index].Signed)
                    {
                        
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RecieveData(object sender, EventArgs e)
        {
            
        }

        public void StopServer(string minecraftServer)
        {

        }

        public void DeleteServer(string name)
        {
            int index = FindServer(name);
            Directory.Delete(jsonData.MinecraftServers[index].Directory, true);
            jsonData.MinecraftServers.RemoveAt(index);
        }

        public void LinkServer(string minecraftServer)
        {
            FolderBrowserDialog setupLink = new FolderBrowserDialog();
            setupLink.Description = "Select A Minecraft Save";
            setupLink.ShowDialog();
            jsonData.MinecraftServers[FindServer(minecraftServer)].LinkedWorldDirectory = setupLink.SelectedPath;
            setupLink.Dispose();
        }

        public void UnlinkServerFromSinglePlayerWorld(string minecraftServer)
        {
            //Unlink Server
            
        }

        public void Close()
        {
            string save = JsonConvert.SerializeObject(jsonData);
            string path = launcherDirectory + @"\Resources\Minecraft Server Data.JSON";
            File.WriteAllText(path, save);
        }
    }
}
