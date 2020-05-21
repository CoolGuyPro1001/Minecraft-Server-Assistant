using Newtonsoft.Json;
using System.IO;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Minecraft_Server_Assistant.Source.GUI;

namespace Minecraft_Server_Assistant.Source
{
    public class ServerManager
    {
        private string assistantDirectory;
        private string serverFile;
        private string bootupFile;
        private string jsonFile;
        public int ServerCount { get; set; }
        private EventWaitHandle wait;

        private JsonData ServerData;
        public List<string> ServerNames { get; }
        private static List<Process> runningServers;
        private List<ServerConsole> serverOutputs;

        private static ServerRunner serverRunner;

        public ServerManager()
        {
            assistantDirectory = Directory.GetCurrentDirectory();
            jsonFile = assistantDirectory + @"\Resources\Minecraft Server Data.JSON";
            serverFile = assistantDirectory + @"\Resources\server.jar";
            bootupFile = assistantDirectory + @"\Resources\Bootup.bat";
            ServerData = JsonConvert.DeserializeObject<JsonData>(ReadJsonFile());
            runningServers = new List<Process>();
            serverOutputs = new List<ServerConsole>();

            ServerCount = ServerData.MinecraftServers.Count;
            ServerNames = new List<string>();
            serverRunner = new ServerRunner();

            if (!ServerData.ContainsServerFile)
            {
                wait = new EventWaitHandle(false, EventResetMode.AutoReset);
                DropServer drop = new DropServer(wait, this);
                drop.Show();
                wait.WaitOne();
                drop.Dispose();

                ServerData.ContainsServerFile = true;
            }

            if (ServerData.RootDirectory == null)
            {
                while (ServerData.RootDirectory == "" || ServerData.RootDirectory == null)
                {
                    FolderBrowserDialog setupServerFolder = new FolderBrowserDialog();
                    setupServerFolder.Description = "Select A Folder To Store Servers";

                    setupServerFolder.ShowDialog();
                    ServerData.RootDirectory = setupServerFolder.SelectedPath;
                    setupServerFolder.Dispose();
                }
            }

            for (int i = 0; i < ServerData.MinecraftServers.Count; i++)
            {
                ServerNames.Add(ServerData.MinecraftServers[i].Name);
            }

        }

        private string ReadJsonFile()
        {
            if (!File.Exists(jsonFile))
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

        public string CreateServer(string name)
        {
            string newServerDirectory = ServerData.RootDirectory + @"\" + name;
            if (Directory.Exists(newServerDirectory))
            {
                return "Server Already Exists";
            }
            else
            {
                ServerNames.Add(name);
                ServerData.MinecraftServers.Add(new MinecraftServerJson(name, newServerDirectory));
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

        public bool RunServer(string serverName, MemorySize min, MemorySize max, ServerConsole outputControl, bool gui)
        {
            int index = ServerNames.IndexOf(serverName);
            MinecraftServerJson runningServer = ServerData.MinecraftServers[index];
            string directory = runningServer.Directory;
            string serverjar = directory + @"\server.jar";
            bool signed = false;
            try
            {
                Process runServer = new Process();


                runServer.StartInfo.FileName = @"java.exe";
                runServer.StartInfo.Arguments = @"-jar " + serverjar + " nogui";
                runServer.StartInfo.UseShellExecute = false;
                runServer.StartInfo.CreateNoWindow = true;
                runServer.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                runServer.StartInfo.RedirectStandardOutput = true;
                runServer.StartInfo.RedirectStandardError = true;
                runServer.StartInfo.RedirectStandardInput = true;

                runServer.StartInfo.WorkingDirectory = directory;


                runServer.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!runningServer.Signed)
                    {
                        if (!runServer.HasExited)
                            runServer.Kill();
                    }
                    else
                    {
                        string reader = runServer.StandardError.ReadToEnd();
                        Console.WriteLine(reader);
                    }
                });

                runServer.OutputDataReceived += SortOutputHandler;

                runServer.Start();
                serverRunner.AddProcess(runServer);

                if (!runningServer.Signed)
                {
                    runServer.WaitForExit();
                    runServer.Close();
                }
                else
                {
                    runningServers.Add(runServer);
                    runningServer.Signed = true;
                    outputControl.SetID(runningServers.IndexOf(runServer));
                    serverOutputs.Add(outputControl);
                    signed = true;
                    runServer.BeginOutputReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return signed;
        }

        private void SortOutputHandler(object sendingProcess, DataReceivedEventArgs outline)
        {
            Process server = (Process)sendingProcess;
            int id = runningServers.IndexOf(server);

            if(!string.IsNullOrEmpty(outline.Data))
            {
                serverOutputs[id].Invoke(new Action(() => serverOutputs[id].WriteLine(outline.Data)));
            }
        }

        public static void Input(int serverID, string x)
        {
            Process server = runningServers[serverID];
            server.StandardInput.WriteLine(x);
        }


        public void SignEula(string serverName)
        {
            int id = ServerNames.IndexOf(serverName);
            string directory = ServerData.MinecraftServers[id].Directory;
            string eula = directory + @"\eula.txt";
            string text = File.ReadAllText(eula);
            for(int i = 170; i < text.Length; i++)
            {
                if (text[i] == 'f')
                {
                    text = text.Remove(i).Insert(i, "true");
                    File.WriteAllText(eula, text);
                    ServerData.MinecraftServers[id].Signed = true;
                }
            }
        }

        public void ChangeProperties()
        {


        }

        public void StopServer(string minecraftServer)
        {

        }

        public void DeleteServer(string name)
        {
            int id = ServerNames.IndexOf(name);
            Directory.Delete(ServerData.MinecraftServers[id].Directory, true);
            ServerData.MinecraftServers.RemoveAt(id);
        }

        public void LinkServer(string minecraftServer)
        {
            FolderBrowserDialog setupLink = new FolderBrowserDialog();
            setupLink.Description = "Select A Minecraft Save";
            setupLink.ShowDialog();
            ServerData.MinecraftServers[ServerNames.IndexOf(minecraftServer)].LinkedWorldDirectory = setupLink.SelectedPath;
            setupLink.Dispose();
        }

        public void UnlinkServerFromSinglePlayerWorld(string minecraftServer)
        {
            //Unlink Server
            
        }

        public void Close()
        {
            string save = JsonConvert.SerializeObject(ServerData);
            string path = assistantDirectory + @"\Resources\Minecraft Server Data.JSON";
            File.WriteAllText(path, save);
        }
    }
}