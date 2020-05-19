using Newtonsoft.Json;
using System.IO;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Minecraft_Server_Assistant.GUI;
using System.Text;

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

        private JsonData jsonData;
        public List<string> Servers { get; }
        private static List<Process> runningServers;

        private static ServerRunner serverRunner;

        public ServerManager()
        {
            assistantDirectory = Directory.GetCurrentDirectory();
            jsonFile = assistantDirectory + @"\Resources\Minecraft Server Data.JSON";
            serverFile = assistantDirectory + @"\Resources\server.jar";
            bootupFile = assistantDirectory + @"\Resources\Bootup.bat";
            jsonData = JsonConvert.DeserializeObject<JsonData>(ReadJsonFile());
            runningServers = new List<Process>();

            ServerCount = jsonData.MinecraftServers.Count;
            Servers = new List<string>();
            serverRunner = new ServerRunner();

            if (!jsonData.ContainsServerFile)
            {
                wait = new EventWaitHandle(false, EventResetMode.AutoReset);
                DropServer drop = new DropServer(wait, this);
                drop.Show();
                wait.WaitOne();
                drop.Dispose();

                jsonData.ContainsServerFile = true;
            }

            if (jsonData.RootDirectory == null)
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
            string newServerDirectory = jsonData.RootDirectory + @"\" + name;
            if (Directory.Exists(newServerDirectory))
            {
                return "Server Already Exists";
            }
            else
            {
                Servers.Add(name);
                jsonData.MinecraftServers.Add(new MinecraftServerJson(name, newServerDirectory));
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

        Action<Process> ManageProcess = (Process server) =>
        {
            //Handle Output
            server.StandardOutput.ReadToEnd();
        };

        public bool RunServer(string serverName, MemorySize min, MemorySize max, bool gui)
        {
            int index = Servers.IndexOf(serverName);
            MinecraftServerJson runningServer = jsonData.MinecraftServers[index];
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

                runServer.Start();
                serverRunner.AddProcess(runServer);

                if (!runningServer.Signed)
                {

                    
                    runServer.WaitForExit();
                    runServer.Close();
                }
                else
                {
                    runningServers.Add(serverName, runServer);
                    Task serverTask = new Task(() => ManageProcess(runServer));
                    serverTask.Start();
                    runningServer.Signed = true;
                    signed = true;
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
            if(!string.IsNullOrEmpty(outline.Data))
            {
                Console.WriteLine(outline.Data);
            }
        }

        public void SignEula(string serverName)
        {
            int index = Servers.IndexOf(serverName);
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

        public void ChangeProperties()
        {


        }

        public void StopServer(string minecraftServer)
        {

        }

        public void DeleteServer(string name)
        {
            int index = Servers.IndexOf(name);
            Directory.Delete(jsonData.MinecraftServers[index].Directory, true);
            jsonData.MinecraftServers.RemoveAt(index);
        }

        public void LinkServer(string minecraftServer)
        {
            FolderBrowserDialog setupLink = new FolderBrowserDialog();
            setupLink.Description = "Select A Minecraft Save";
            setupLink.ShowDialog();
            jsonData.MinecraftServers[Servers.IndexOf(minecraftServer)].LinkedWorldDirectory = setupLink.SelectedPath;
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