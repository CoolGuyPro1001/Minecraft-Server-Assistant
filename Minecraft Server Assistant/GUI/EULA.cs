using System;
using System.Windows.Forms;
using Minecraft_Server_Assistant.Source;

namespace Minecraft_Server_Assistant.GUI
{
    public partial class EULA : UserControl
    {
        private string server;
        private ServerManager manager;
        private MinecraftServerAssistant assistant;

        public EULA(string server, ServerManager manager, MinecraftServerAssistant assistant)
        {
            InitializeComponent();
            this.server = server;
            this.manager = manager;
            this.assistant = assistant;
        }

        private void AgreeButton_Click(object sender, EventArgs e)
        {
            manager.SignEula(server);
            assistant.RemoveTabPage(this);
        }
    }
}
