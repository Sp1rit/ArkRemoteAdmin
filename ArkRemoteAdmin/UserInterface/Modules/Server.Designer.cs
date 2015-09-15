namespace ArkRemoteAdmin.UserInterface.Modules
{
    partial class Server
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpTimeOfDay = new System.Windows.Forms.GroupBox();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.dtbTimeOfDay = new System.Windows.Forms.DateTimePicker();
            this.grpBroadcast = new System.Windows.Forms.GroupBox();
            this.btnBroadcast = new System.Windows.Forms.Button();
            this.rtbBroadcast = new System.Windows.Forms.RichTextBox();
            this.grpMotd = new System.Windows.Forms.GroupBox();
            this.btnShowMotd = new System.Windows.Forms.Button();
            this.btnMotd = new System.Windows.Forms.Button();
            this.rtbMotd = new System.Windows.Forms.RichTextBox();
            this.grpTimeOfDay.SuspendLayout();
            this.grpBroadcast.SuspendLayout();
            this.grpMotd.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTimeOfDay
            // 
            this.grpTimeOfDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTimeOfDay.Controls.Add(this.btnSetTime);
            this.grpTimeOfDay.Controls.Add(this.dtbTimeOfDay);
            this.grpTimeOfDay.Location = new System.Drawing.Point(3, 267);
            this.grpTimeOfDay.Name = "grpTimeOfDay";
            this.grpTimeOfDay.Size = new System.Drawing.Size(569, 48);
            this.grpTimeOfDay.TabIndex = 7;
            this.grpTimeOfDay.TabStop = false;
            this.grpTimeOfDay.Text = "Set TimeOfDay";
            // 
            // btnSetTime
            // 
            this.btnSetTime.Location = new System.Drawing.Point(87, 19);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(75, 23);
            this.btnSetTime.TabIndex = 1;
            this.btnSetTime.Text = "Set";
            this.btnSetTime.UseVisualStyleBackColor = true;
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // dtbTimeOfDay
            // 
            this.dtbTimeOfDay.CustomFormat = "HH:mm:ss";
            this.dtbTimeOfDay.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtbTimeOfDay.Location = new System.Drawing.Point(6, 20);
            this.dtbTimeOfDay.Name = "dtbTimeOfDay";
            this.dtbTimeOfDay.ShowUpDown = true;
            this.dtbTimeOfDay.Size = new System.Drawing.Size(75, 20);
            this.dtbTimeOfDay.TabIndex = 0;
            this.dtbTimeOfDay.Value = new System.DateTime(1753, 1, 1, 6, 0, 0, 0);
            // 
            // grpBroadcast
            // 
            this.grpBroadcast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBroadcast.Controls.Add(this.btnBroadcast);
            this.grpBroadcast.Controls.Add(this.rtbBroadcast);
            this.grpBroadcast.Location = new System.Drawing.Point(3, 135);
            this.grpBroadcast.Name = "grpBroadcast";
            this.grpBroadcast.Size = new System.Drawing.Size(569, 126);
            this.grpBroadcast.TabIndex = 6;
            this.grpBroadcast.TabStop = false;
            this.grpBroadcast.Text = "Broadcast Message";
            // 
            // btnBroadcast
            // 
            this.btnBroadcast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBroadcast.Enabled = false;
            this.btnBroadcast.Location = new System.Drawing.Point(6, 97);
            this.btnBroadcast.Name = "btnBroadcast";
            this.btnBroadcast.Size = new System.Drawing.Size(75, 23);
            this.btnBroadcast.TabIndex = 1;
            this.btnBroadcast.Text = "Broadcast";
            this.btnBroadcast.UseVisualStyleBackColor = true;
            this.btnBroadcast.Click += new System.EventHandler(this.btnBroadcast_Click);
            // 
            // rtbBroadcast
            // 
            this.rtbBroadcast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbBroadcast.Location = new System.Drawing.Point(6, 19);
            this.rtbBroadcast.Name = "rtbBroadcast";
            this.rtbBroadcast.Size = new System.Drawing.Size(557, 72);
            this.rtbBroadcast.TabIndex = 0;
            this.rtbBroadcast.Text = "";
            this.rtbBroadcast.TextChanged += new System.EventHandler(this.rtbBroadcast_TextChanged);
            // 
            // grpMotd
            // 
            this.grpMotd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMotd.Controls.Add(this.btnShowMotd);
            this.grpMotd.Controls.Add(this.btnMotd);
            this.grpMotd.Controls.Add(this.rtbMotd);
            this.grpMotd.Location = new System.Drawing.Point(3, 3);
            this.grpMotd.Name = "grpMotd";
            this.grpMotd.Size = new System.Drawing.Size(569, 126);
            this.grpMotd.TabIndex = 5;
            this.grpMotd.TabStop = false;
            this.grpMotd.Text = "Message of the day";
            // 
            // btnShowMotd
            // 
            this.btnShowMotd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowMotd.Location = new System.Drawing.Point(87, 97);
            this.btnShowMotd.Name = "btnShowMotd";
            this.btnShowMotd.Size = new System.Drawing.Size(127, 23);
            this.btnShowMotd.TabIndex = 2;
            this.btnShowMotd.Text = "Show MOTD on Server";
            this.btnShowMotd.UseVisualStyleBackColor = true;
            this.btnShowMotd.Click += new System.EventHandler(this.btnShowMOTD_Click);
            // 
            // btnMotd
            // 
            this.btnMotd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMotd.Enabled = false;
            this.btnMotd.Location = new System.Drawing.Point(6, 97);
            this.btnMotd.Name = "btnMotd";
            this.btnMotd.Size = new System.Drawing.Size(75, 23);
            this.btnMotd.TabIndex = 1;
            this.btnMotd.Text = "Set MOTD";
            this.btnMotd.UseVisualStyleBackColor = true;
            this.btnMotd.Click += new System.EventHandler(this.btnMotd_Click);
            // 
            // rtbMotd
            // 
            this.rtbMotd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbMotd.Location = new System.Drawing.Point(6, 19);
            this.rtbMotd.Name = "rtbMotd";
            this.rtbMotd.Size = new System.Drawing.Size(557, 72);
            this.rtbMotd.TabIndex = 0;
            this.rtbMotd.Text = "";
            this.rtbMotd.TextChanged += new System.EventHandler(this.rtbMotd_TextChanged);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpTimeOfDay);
            this.Controls.Add(this.grpBroadcast);
            this.Controls.Add(this.grpMotd);
            this.Name = "Server";
            this.Size = new System.Drawing.Size(575, 321);
            this.grpTimeOfDay.ResumeLayout(false);
            this.grpBroadcast.ResumeLayout(false);
            this.grpMotd.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTimeOfDay;
        private System.Windows.Forms.Button btnSetTime;
        private System.Windows.Forms.DateTimePicker dtbTimeOfDay;
        private System.Windows.Forms.GroupBox grpBroadcast;
        private System.Windows.Forms.Button btnBroadcast;
        private System.Windows.Forms.RichTextBox rtbBroadcast;
        private System.Windows.Forms.GroupBox grpMotd;
        private System.Windows.Forms.Button btnShowMotd;
        private System.Windows.Forms.Button btnMotd;
        private System.Windows.Forms.RichTextBox rtbMotd;
    }
}
