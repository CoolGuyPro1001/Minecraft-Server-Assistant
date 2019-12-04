using System;
using System.Windows.Forms;
using Minecraft_Server_Launcher.GUI;

namespace Minecraft_Server_Launcher
{
    static class Program
    {
        static MinecraftServerLauncher launcher;
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyApplicationContext();
        }

        static private void MyApplicationContext()
        {

            // Handle the ApplicationExit event to know when the application is exiting.
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            launcher = new MinecraftServerLauncher();
            Application.Run(launcher);
        }

        static private void OnApplicationExit(object sender, EventArgs e)
        {
            launcher.ShutDownSequence();
        }
    }
}
