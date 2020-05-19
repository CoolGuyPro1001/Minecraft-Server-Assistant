using System;
using System.Windows.Forms;
using System.Threading;
using Minecraft_Server_Assistant.Source;

namespace Minecraft_Server_Assistant.GUI
{
    public partial class DropServer : Form
    {
        private EventWaitHandle wait;
        private ServerManager manager;

        public DropServer(EventWaitHandle wait, ServerManager manager)
        {
            this.wait = wait;
            this.manager = manager;
            InitializeComponent();
        }

        private void DropServer_DragDrop(object sender, DragEventArgs e)
        {
            string format = DataFormats.FileDrop;
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            if (files.Length == 1)
                manager.AddServerFile(files[0]);
            else
                throw new Exception("Just the server.jar file. No adding pictures");
            wait.Set();
        }


        private void DropServer_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
