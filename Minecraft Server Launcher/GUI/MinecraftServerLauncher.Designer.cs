namespace Minecraft_Server_Launcher.GUI
{
    partial class MinecraftServerLauncher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinecraftServerLauncher));
            this.CreateServerButton = new System.Windows.Forms.Button();
            this.NewServerName = new System.Windows.Forms.TextBox();
            this.ServerPanel = new System.Windows.Forms.Panel();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.HomePage = new System.Windows.Forms.TabPage();
            this.Message = new System.Windows.Forms.Label();
            this.Tabs.SuspendLayout();
            this.HomePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreateServerButton
            // 
            this.CreateServerButton.BackColor = System.Drawing.Color.Gray;
            this.CreateServerButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CreateServerButton.FlatAppearance.BorderSize = 3;
            this.CreateServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.CreateServerButton.ForeColor = System.Drawing.Color.White;
            this.CreateServerButton.Location = new System.Drawing.Point(609, 258);
            this.CreateServerButton.Name = "CreateServerButton";
            this.CreateServerButton.Size = new System.Drawing.Size(300, 100);
            this.CreateServerButton.TabIndex = 0;
            this.CreateServerButton.Text = "Create A Server";
            this.CreateServerButton.UseVisualStyleBackColor = false;
            this.CreateServerButton.Click += new System.EventHandler(this.CreateServerButton_Click);
            // 
            // NewServerName
            // 
            this.NewServerName.BackColor = System.Drawing.Color.White;
            this.NewServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.NewServerName.Location = new System.Drawing.Point(557, 380);
            this.NewServerName.Name = "NewServerName";
            this.NewServerName.Size = new System.Drawing.Size(400, 83);
            this.NewServerName.TabIndex = 1;
            this.NewServerName.Click += new System.EventHandler(this.NewServerName_Click);
            // 
            // ServerPanel
            // 
            this.ServerPanel.AutoScroll = true;
            this.ServerPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ServerPanel.Location = new System.Drawing.Point(3, 3);
            this.ServerPanel.Name = "ServerPanel";
            this.ServerPanel.Size = new System.Drawing.Size(526, 498);
            this.ServerPanel.TabIndex = 5;
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.HomePage);
            this.Tabs.Location = new System.Drawing.Point(-1, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(988, 530);
            this.Tabs.TabIndex = 6;
            // 
            // HomePage
            // 
            this.HomePage.BackgroundImage = global::Minecraft_Server_Launcher.Properties.Resources.backround;
            this.HomePage.Controls.Add(this.Message);
            this.HomePage.Controls.Add(this.CreateServerButton);
            this.HomePage.Controls.Add(this.ServerPanel);
            this.HomePage.Controls.Add(this.NewServerName);
            this.HomePage.Location = new System.Drawing.Point(4, 22);
            this.HomePage.Name = "HomePage";
            this.HomePage.Padding = new System.Windows.Forms.Padding(3);
            this.HomePage.Size = new System.Drawing.Size(980, 504);
            this.HomePage.TabIndex = 0;
            this.HomePage.Text = "Home";
            this.HomePage.UseVisualStyleBackColor = true;
            // 
            // Message
            // 
            this.Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.Message.ForeColor = System.Drawing.Color.White;
            this.Message.Location = new System.Drawing.Point(557, 57);
            this.Message.Margin = new System.Windows.Forms.Padding(0);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(400, 150);
            this.Message.TabIndex = 6;
            this.Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MinecraftServerLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 526);
            this.Controls.Add(this.Tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MinecraftServerLauncher";
            this.Text = "Minecraft Server Launcher";
            this.Tabs.ResumeLayout(false);
            this.HomePage.ResumeLayout(false);
            this.HomePage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateServerButton;
        private System.Windows.Forms.TextBox NewServerName;
        private System.Windows.Forms.Panel ServerPanel;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage HomePage;
        private System.Windows.Forms.Label Message;
    }
}

