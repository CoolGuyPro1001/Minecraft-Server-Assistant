namespace Minecraft_Server_Assistant.GUI
{
    partial class EULA
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
            this.AgreeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AgreeButton
            // 
            this.AgreeButton.BackColor = System.Drawing.Color.Gray;
            this.AgreeButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.AgreeButton.FlatAppearance.BorderSize = 3;
            this.AgreeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AgreeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AgreeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.AgreeButton.ForeColor = System.Drawing.Color.White;
            this.AgreeButton.Location = new System.Drawing.Point(531, 400);
            this.AgreeButton.Name = "AgreeButton";
            this.AgreeButton.Size = new System.Drawing.Size(230, 70);
            this.AgreeButton.TabIndex = 0;
            this.AgreeButton.Text = "Agree";
            this.AgreeButton.UseVisualStyleBackColor = false;
            this.AgreeButton.Click += new System.EventHandler(this.AgreeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(277, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "End User License Agreement";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(118, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(797, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "https://account.mojang.com/documents/minecraft_eula";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.Gray;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CancelButton.FlatAppearance.BorderSize = 3;
            this.CancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.CancelButton.ForeColor = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(177, 400);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(230, 70);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // EULA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::Minecraft_Server_Assistant.Properties.Resources.backround;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AgreeButton);
            this.Name = "EULA";
            this.Size = new System.Drawing.Size(980, 504);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AgreeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CancelButton;
    }
}
