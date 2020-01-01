using System;
using System.Windows.Forms;
using System.Threading;

namespace Minecraft_Server_Launcher
{
    public partial class DropServer : Form
    {
        private EventWaitHandle wait;

        public DropServer(EventWaitHandle wait)
        {
            this.wait = wait;
            InitializeComponent();
        }

        private void DropServer_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) Console.WriteLine(file);
            wait.Set();
        }

        private void DropServer_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}
