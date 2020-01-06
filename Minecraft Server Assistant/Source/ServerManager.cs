using Newtonsoft.Json;
using System.IO;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Minecraft_Server_Assistant.GUI;

namespace Minecraft_Server_Assistant
{
    public class ServerManager
    {
        private string assistantDirectory;
        private string serverFile;
        private string bootupFile;
        private string jsonFile;
        public int ServerCount { get; set; }
        private EventWaitHandle wait;

        private JsonData jsonData;
        public string[] Servers { get; }

        public ServerManager()
        {
            assistantDirectory = Directory.GetCurrentDirectory();
            jsonFile = assistantDirectory + @"\Resources\Minecraft Server Data.JSON";
            serverFile = assistantDirectory + @"\Resources\server.jar";
            bootupFile = assistantDirectory + @"\Resources\Bootup.bat";
            jsonData = JsonConvert.DeserializeObject<JsonData>(ReadJsonFile());

            ServerCount = jsonData.MinecraftServers.Count;
            Servers = new string[256];

            if(!jsonData.ContainsServerFile)
            {
                wait = new EventWaitHandle(false, EventResetMode.AutoReset);
                DropServer drop = new DropServer(wait, this);
                drop.Show();
                wait.WaitOne();
                drop.Dispose();

                jsonData.ContainsServerFile = true;
            }

            if(jsonData.RootDirectory == null)
            {
                while (jsonData.RootDirectory == "" || jsonData.RootDirectory == null)
                {
                    FolderBrowserDialog setupServerFolder = new FolderBrowserDialog();
                    setupServerFolder.Description = "Select A Folder To Store Servers";

                    setupServerFolder.ShowDialog();
                    jsonData.RootDirectory = setupServerFolder.SelectedPath;
                    setupServerFolder.Dispose();
                }
            }

            for (int i = 0; i < jsonData.MinecraftServers.Count; i++)
            {
                Servers[i] = jsonData.MinecraftServers[i].Name;
            }

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

        public void AddServerFile(string server)
        {
            File.Copy(server, serverFile);
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
                string text = File.ReadAllText(newBootupFile).Insert(17, newServerDirectory);
                File.WriteAllText(newBootupFile, text);

                return "Success";
            }
        }

        public bool RunServer(string serverName, MemorySize min, MemorySize max, bool gui)
        {
            int index = FindServer(serverName);
            string directory = jsonData.MinecraftServers[index].Directory;
            string bootupFile = directory + @"\Bootup.bat";
            bool signed = false;
            try
            {
                using (Process runServer = new Process())
                {
                    runServer.StartInfo.FileName = bootupFile;
                    runServer.StartInfo.UseShellExecute = false;
                    runServer.StartInfo.CreateNoWindow = false;
                    runServer.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    runServer.StartInfo.RedirectStandardOutput = true;
                    runServer.StartInfo.RedirectStandardError = true;
                    runServer.ErrorDataReceived += RecieveBootupError;
                    runServer.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                    {
                        if (!runServer.HasExited)
                            runServer.Kill();
                    });

                    if (!jsonData.MinecraftServers[index].Signed)
                    {
                        runServer.Start();
                        runServer.BeginOutputReadLine();
                        runServer.WaitForExit();
                    }
                    else
                    {
                        runServer.StartInfo.RedirectStandardOutput = false;
                        runServer.StartInfo.RedirectStandardError = false;
                        runServer.Start();
                        signed = true;
                    }
                    runServer.Close();
           
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return signed;
        }

        public void SignEula(string serverName)
        {
            int index = FindServer(serverName);
            string directory = jsonData.MinecraftServers[index].Directory;
            string eula = directory + @"\eula.txt";
            string text = File.ReadAllText(eula);
            for(int i = 170; i < text.Length; i++)
            {
                if (text[i] == 'f')
                {
                    text = text.Remove(i).Insert(i, "true");
                    File.WriteAllText(eula, text);
                    jsonData.MinecraftServers[index].Signed = true;
                }
            }
        }

        private void RecieveBootupError(object sender, EventArgs e)
        {
            Console.WriteLine("Error Recieved");
        }

        private void RecieveBootupOutput(object sender, EventArgs e)
        {
            Console.WriteLine("Output Recieved");   
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
            string path = assistantDirectory + @"\Resources\Minecraft Server Data.JSON";
            File.WriteAllText(path, save);
        }
    }
}
