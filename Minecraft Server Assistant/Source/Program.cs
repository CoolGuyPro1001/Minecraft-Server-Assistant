using System;
using System.Windows.Forms;

namespace Minecraft_Server_Assistant.Source
{
    public static class Program
    {
        static MinecraftServerAssistant assistant;
        
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
            assistant = new MinecraftServerAssistant();
            Application.Run(assistant);
        }

        static private void OnApplicationExit(object sender, EventArgs e)
        {
            assistant.ShutDownSequence();
        }
    }
}
