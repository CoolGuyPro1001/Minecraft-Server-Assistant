using System.Collections.Generic;
using Newtonsoft.Json;

namespace Minecraft_Server_Assistant
{
    public class JsonData
    {
        public bool ContainsServerFile { get; set; }
        public string RootDirectory { get; set; }
        public List<MinecraftServerJson> MinecraftServers { get; set; }
    }

    public class MinecraftServerJson
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public bool Active { get; set; }
        public string LinkedWorldDirectory { get; set; }
        public bool Signed { get; set; }
        
        [JsonConstructor]
        public MinecraftServerJson(string name, string directory, string linkedDirectory, bool signed)
        {
            Name = name;
            Directory = directory;
            LinkedWorldDirectory = linkedDirectory;
            Signed = signed;
            Active = false;
        }

        public MinecraftServerJson(string name, string directory)
        {
            Name = name;
            Directory = directory;
            LinkedWorldDirectory = "";
            Signed = false;
            Active = false;
        }
    }
}
