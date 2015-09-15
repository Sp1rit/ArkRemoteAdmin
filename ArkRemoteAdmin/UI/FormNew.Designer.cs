namespace ArkRemoteAdmin.UI
{
    partial class FormNew
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNew));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.aviraTabControl1 = new BssFramework.Windows.Forms.AviraTabControl();
            this.catUsers = new System.Windows.Forms.TabPage();
            this.tpOnline = new System.Windows.Forms.TabPage();
            this.tpOffline = new System.Windows.Forms.TabPage();
            this.tpBanned = new System.Windows.Forms.TabPage();
            this.catManagement = new System.Windows.Forms.TabPage();
            this.tpCommands = new System.Windows.Forms.TabPage();
            this.tpSchedules = new System.Windows.Forms.TabPage();
            this.catConfiguration = new System.Windows.Forms.TabPage();
            this.tpServers = new System.Windows.Forms.TabPage();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.onlineUsers2 = new ArkRemoteAdmin.UI.OnlineUsers();
            this.servers1 = new ArkRemoteAdmin.UI.Servers();
            this.aviraTabControl1.SuspendLayout();
            this.tpOnline.SuspendLayout();
            this.tpServers.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "user.png");
            this.imageList.Images.SetKeyName(1, "user_chief.png");
            this.imageList.Images.SetKeyName(2, "user_delete.png");
            this.imageList.Images.SetKeyName(3, "user_comment.png");
            this.imageList.Images.SetKeyName(4, "clock.png");
            this.imageList.Images.SetKeyName(5, "server.png");
            this.imageList.Images.SetKeyName(6, "cog.png");
            this.imageList.Images.SetKeyName(7, "server_add.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user.png");
            this.imageList1.Images.SetKeyName(1, "user_chief.png");
            this.imageList1.Images.SetKeyName(2, "user_delete.png");
            this.imageList1.Images.SetKeyName(3, "user_comment.png");
            this.imageList1.Images.SetKeyName(4, "clock.png");
            this.imageList1.Images.SetKeyName(5, "server.png");
            this.imageList1.Images.SetKeyName(6, "cog.png");
            // 
            // aviraTabControl1
            // 
            this.aviraTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.aviraTabControl1.Controls.Add(this.catUsers);
            this.aviraTabControl1.Controls.Add(this.tpOnline);
            this.aviraTabControl1.Controls.Add(this.tpOffline);
            this.aviraTabControl1.Controls.Add(this.tpBanned);
            this.aviraTabControl1.Controls.Add(this.catManagement);
            this.aviraTabControl1.Controls.Add(this.tpCommands);
            this.aviraTabControl1.Controls.Add(this.tpSchedules);
            this.aviraTabControl1.Controls.Add(this.catConfiguration);
            this.aviraTabControl1.Controls.Add(this.tpServers);
            this.aviraTabControl1.Controls.Add(this.tpSettings);
            this.aviraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aviraTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.aviraTabControl1.Font = new System.Drawing.Font("Verdana", 8F);
            this.aviraTabControl1.ImageListSelected = this.imageList1;
            this.aviraTabControl1.ImageListUnselected = this.imageList;
            this.aviraTabControl1.ItemSize = new System.Drawing.Size(21, 180);
            this.aviraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.aviraTabControl1.Multiline = true;
            this.aviraTabControl1.Name = "aviraTabControl1";
            this.aviraTabControl1.SelectedIndex = 0;
            this.aviraTabControl1.Size = new System.Drawing.Size(884, 539);
            this.aviraTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.aviraTabControl1.TabIndex = 0;
            // 
            // catUsers
            // 
            this.catUsers.BackColor = System.Drawing.Color.White;
            this.catUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catUsers.Location = new System.Drawing.Point(184, 4);
            this.catUsers.Name = "catUsers";
            this.catUsers.Padding = new System.Windows.Forms.Padding(3);
            this.catUsers.Size = new System.Drawing.Size(696, 531);
            this.catUsers.TabIndex = 0;
            this.catUsers.Tag = "catUsers";
            this.catUsers.Text = "Users";
            // 
            // tpOnline
            // 
            this.tpOnline.BackColor = System.Drawing.Color.White;
            this.tpOnline.Controls.Add(this.onlineUsers2);
            this.tpOnline.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOnline.Location = new System.Drawing.Point(184, 4);
            this.tpOnline.Name = "tpOnline";
            this.tpOnline.Padding = new System.Windows.Forms.Padding(3);
            this.tpOnline.Size = new System.Drawing.Size(696, 531);
            this.tpOnline.TabIndex = 1;
            this.tpOnline.Text = "Online Users (0)";
            // 
            // tpOffline
            // 
            this.tpOffline.BackColor = System.Drawing.Color.White;
            this.tpOffline.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOffline.Location = new System.Drawing.Point(184, 4);
            this.tpOffline.Name = "tpOffline";
            this.tpOffline.Size = new System.Drawing.Size(696, 531);
            this.tpOffline.TabIndex = 2;
            this.tpOffline.Text = "Offline Users";
            // 
            // tpBanned
            // 
            this.tpBanned.BackColor = System.Drawing.Color.White;
            this.tpBanned.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpBanned.Location = new System.Drawing.Point(184, 4);
            this.tpBanned.Name = "tpBanned";
            this.tpBanned.Size = new System.Drawing.Size(696, 531);
            this.tpBanned.TabIndex = 3;
            this.tpBanned.Text = "Banned Users";
            // 
            // catManagement
            // 
            this.catManagement.BackColor = System.Drawing.Color.White;
            this.catManagement.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catManagement.Location = new System.Drawing.Point(184, 4);
            this.catManagement.Name = "catManagement";
            this.catManagement.Size = new System.Drawing.Size(696, 531);
            this.catManagement.TabIndex = 4;
            this.catManagement.Tag = "catManagement";
            this.catManagement.Text = "Management";
            // 
            // tpCommands
            // 
            this.tpCommands.BackColor = System.Drawing.Color.White;
            this.tpCommands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpCommands.Location = new System.Drawing.Point(184, 4);
            this.tpCommands.Name = "tpCommands";
            this.tpCommands.Size = new System.Drawing.Size(696, 531);
            this.tpCommands.TabIndex = 5;
            this.tpCommands.Text = "Chat Commands";
            // 
            // tpSchedules
            // 
            this.tpSchedules.BackColor = System.Drawing.Color.White;
            this.tpSchedules.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpSchedules.Location = new System.Drawing.Point(184, 4);
            this.tpSchedules.Name = "tpSchedules";
            this.tpSchedules.Size = new System.Drawing.Size(696, 531);
            this.tpSchedules.TabIndex = 6;
            this.tpSchedules.Text = "Schedules";
            // 
            // catConfiguration
            // 
            this.catConfiguration.BackColor = System.Drawing.Color.White;
            this.catConfiguration.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catConfiguration.Location = new System.Drawing.Point(184, 4);
            this.catConfiguration.Name = "catConfiguration";
            this.catConfiguration.Size = new System.Drawing.Size(696, 531);
            this.catConfiguration.TabIndex = 7;
            this.catConfiguration.Tag = "catConfiguration";
            this.catConfiguration.Text = "Configuration";
            // 
            // tpServers
            // 
            this.tpServers.BackColor = System.Drawing.Color.White;
            this.tpServers.Controls.Add(this.servers1);
            this.tpServers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpServers.Location = new System.Drawing.Point(184, 4);
            this.tpServers.Name = "tpServers";
            this.tpServers.Size = new System.Drawing.Size(696, 531);
            this.tpServers.TabIndex = 8;
            this.tpServers.Text = "Servers";
            // 
            // tpSettings
            // 
            this.tpSettings.BackColor = System.Drawing.Color.White;
            this.tpSettings.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpSettings.Location = new System.Drawing.Point(184, 4);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Size = new System.Drawing.Size(696, 531);
            this.tpSettings.TabIndex = 9;
            this.tpSettings.Text = "Settings";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(767, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel2.Image")));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(102, 17);
            this.toolStripStatusLabel2.Text = "Not connected";
            // 
            // onlineUsers2
            // 
            this.onlineUsers2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.onlineUsers2.BackColor = System.Drawing.Color.White;
            this.onlineUsers2.Location = new System.Drawing.Point(3, 3);
            this.onlineUsers2.Name = "onlineUsers2";
            this.onlineUsers2.Size = new System.Drawing.Size(690, 525);
            this.onlineUsers2.TabIndex = 2;
            // 
            // servers1
            // 
            this.servers1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.servers1.BackColor = System.Drawing.Color.White;
            this.servers1.Location = new System.Drawing.Point(3, 3);
            this.servers1.Name = "servers1";
            this.servers1.Size = new System.Drawing.Size(690, 525);
            this.servers1.TabIndex = 0;
            // 
            // FormNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.aviraTabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNew";
            this.Text = "ARK Remote Admin";
            this.Load += new System.EventHandler(this.FormNew_Load);
            this.aviraTabControl1.ResumeLayout(false);
            this.tpOnline.ResumeLayout(false);
            this.tpServers.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BssFramework.Windows.Forms.AviraTabControl aviraTabControl1;
        private System.Windows.Forms.TabPage catUsers;
        private System.Windows.Forms.TabPage tpOnline;
        private System.Windows.Forms.TabPage tpOffline;
        private System.Windows.Forms.TabPage tpBanned;
        private System.Windows.Forms.TabPage catManagement;
        private System.Windows.Forms.TabPage tpCommands;
        private System.Windows.Forms.TabPage tpSchedules;
        private System.Windows.Forms.TabPage catConfiguration;
        private System.Windows.Forms.TabPage tpServers;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private OnlineUsers onlineUsers2;
        private Servers servers1;
    }
}