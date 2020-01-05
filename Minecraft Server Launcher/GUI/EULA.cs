using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Server_Launcher.GUI
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
