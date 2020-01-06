using System;
using System.Windows.Forms;
using Minecraft_Server_Assistant;

namespace Minecraft_Server_Assistant.GUI
{
    public partial class EULA : UserControl
    {
        private string server;
        private ServerManager manager;

        public EULA(string server, ServerManager manager)
        {
            InitializeComponent();
            this.server = server;
            this.manager = manager;
        }

        private void AgreeButton_Click(object sender, EventArgs e)
        {
            manager.SignEula(server);
        }
    }
}
