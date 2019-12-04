using System.Drawing;
using System.IO;

namespace Minecraft_Server_Launcher
{
    partial class Options
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PVPButton = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.AllowNetherButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PVPButton
            // 
            this.PVPButton.BackColor = System.Drawing.Color.Gray;
            this.PVPButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PVPButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.PVPButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PVPButton.Location = new System.Drawing.Point(158, 37);
            this.PVPButton.Name = "PVPButton";
            this.PVPButton.Size = new System.Drawing.Size(100, 100);
            this.PVPButton.TabIndex = 21;
            this.PVPButton.UseVisualStyleBackColor = false;
            this.PVPButton.MouseHover += new System.EventHandler(this.PVPButton_MouseHover);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(264, 37);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(100, 100);
            this.button10.TabIndex = 20;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(158, 355);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(100, 100);
            this.button9.TabIndex = 19;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(52, 143);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(100, 100);
            this.button8.TabIndex = 18;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(158, 143);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 100);
            this.button7.TabIndex = 17;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(52, 249);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 100);
            this.button6.TabIndex = 16;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(264, 249);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 100);
            this.button5.TabIndex = 15;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(158, 249);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 100);
            this.button4.TabIndex = 14;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(264, 143);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 100);
            this.button3.TabIndex = 13;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(52, 355);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 100);
            this.button2.TabIndex = 12;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // AllowNetherButton
            // 
            this.AllowNetherButton.BackColor = System.Drawing.Color.Gray;
            this.AllowNetherButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AllowNetherButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.AllowNetherButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AllowNetherButton.Location = new System.Drawing.Point(52, 37);
            this.AllowNetherButton.Name = "AllowNetherButton";
            this.AllowNetherButton.Size = new System.Drawing.Size(100, 100);
            this.AllowNetherButton.TabIndex = 11;
            this.AllowNetherButton.UseVisualStyleBackColor = false;
            this.AllowNetherButton.MouseHover += new System.EventHandler(this.AllowNetherButton_MouseHover);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Minecraft_Server_Launcher.Properties.Resources.backround;
            this.Controls.Add(this.PVPButton);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.AllowNetherButton);
            this.Name = "Options";
            this.Size = new System.Drawing.Size(980, 504);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PVPButton;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button AllowNetherButton;
    }
}
