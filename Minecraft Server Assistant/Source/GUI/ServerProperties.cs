using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Minecraft_Server_Assistant.Source.GUI
{
    public partial class ServerProperties : System.Windows.Forms.UserControl
    {
        string directory;
        string file;
        Bitmap source;
        public ServerProperties()
        {
            InitializeComponent();
            directory = Directory.GetCurrentDirectory();
            file = directory + @"\Resources\Icons.png";
            source = new Bitmap(file);

            AllowNetherButton.BackgroundImage = CropImage(source, new Rectangle(new Point(100, 0), new Size(100, 100)));
            PVPButton.BackgroundImage = CropImage(source, new Rectangle(new Point(200, 0), new Size(100, 100)));
        }

        private Bitmap CropImage (Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }

        private void ToolTipMessage(string message, Button button)
        {
            ToolTip tip = new ToolTip();
            tip.UseAnimation = true;
            tip.UseFading = true;

            tip.Show(message, button, 2);
        }

        private void AllowNetherButton_MouseHover(object sender, System.EventArgs e)
        {
            ToolTipMessage("Allow Travel To The Nether", AllowNetherButton);
        }

        private void PVPButton_MouseHover(object sender, System.EventArgs e)
        {
            ToolTipMessage("Enable PVP", PVPButton);
        }
    }
}