namespace ArkRemoteAdmin.UI
{
    partial class OnlineUsers
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "[xU] .$pIrit",
            "76561197992236997"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineUsers));
            this.lvPlayers = new BssFramework.Windows.Forms.ListViewEx();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSteamId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsPlayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openSteamProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.copyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySteamIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unbanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addToWhitelistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromWhitelistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblOnline = new BssFramework.Windows.Forms.SeperatorLabel();
            this.btnRefresh = new BssFramework.Windows.Forms.AeroButton();
            this.selectedUser = new ArkRemoteAdmin.UI.SelectedUser();
            this.cmsPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPlayers
            // 
            this.lvPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chSteamId});
            this.lvPlayers.ContextMenuStrip = this.cmsPlayer;
            this.lvPlayers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPlayers.FullRowSelect = true;
            this.lvPlayers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPlayers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvPlayers.Location = new System.Drawing.Point(3, 77);
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(807, 383);
            this.lvPlayers.TabIndex = 9;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            this.lvPlayers.SelectedIndexChanged += new System.EventHandler(this.lvPlayers_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 153;
            // 
            // chSteamId
            // 
            this.chSteamId.Text = "SteamId";
            this.chSteamId.Width = 396;
            // 
            // cmsPlayer
            // 
            this.cmsPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSteamProfileToolStripMenuItem,
            this.toolStripSeparator4,
            this.copyNameToolStripMenuItem,
            this.copySteamIdToolStripMenuItem,
            this.toolStripSeparator5,
            this.kickToolStripMenuItem,
            this.banToolStripMenuItem,
            this.unbanToolStripMenuItem,
            this.toolStripSeparator2,
            this.addToWhitelistToolStripMenuItem,
            this.removeFromWhitelistToolStripMenuItem});
            this.cmsPlayer.Name = "cmsPlayer";
            this.cmsPlayer.Size = new System.Drawing.Size(194, 198);
            // 
            // openSteamProfileToolStripMenuItem
            // 
            this.openSteamProfileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openSteamProfileToolStripMenuItem.Image")));
            this.openSteamProfileToolStripMenuItem.Name = "openSteamProfileToolStripMenuItem";
            this.openSteamProfileToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.openSteamProfileToolStripMenuItem.Text = "Open Steam Profile(s)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(190, 6);
            // 
            // copyNameToolStripMenuItem
            // 
            this.copyNameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyNameToolStripMenuItem.Image")));
            this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
            this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.copyNameToolStripMenuItem.Text = "Copy Name(s)";
            // 
            // copySteamIdToolStripMenuItem
            // 
            this.copySteamIdToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copySteamIdToolStripMenuItem.Image")));
            this.copySteamIdToolStripMenuItem.Name = "copySteamIdToolStripMenuItem";
            this.copySteamIdToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.copySteamIdToolStripMenuItem.Text = "Copy SteamId(s)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(190, 6);
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("kickToolStripMenuItem.Image")));
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.kickToolStripMenuItem.Text = "Kick";
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("banToolStripMenuItem.Image")));
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.banToolStripMenuItem.Text = "Ban";
            // 
            // unbanToolStripMenuItem
            // 
            this.unbanToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("unbanToolStripMenuItem.Image")));
            this.unbanToolStripMenuItem.Name = "unbanToolStripMenuItem";
            this.unbanToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.unbanToolStripMenuItem.Text = "Unban";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // addToWhitelistToolStripMenuItem
            // 
            this.addToWhitelistToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addToWhitelistToolStripMenuItem.Image")));
            this.addToWhitelistToolStripMenuItem.Name = "addToWhitelistToolStripMenuItem";
            this.addToWhitelistToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.addToWhitelistToolStripMenuItem.Text = "Add to whitelist";
            // 
            // removeFromWhitelistToolStripMenuItem
            // 
            this.removeFromWhitelistToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeFromWhitelistToolStripMenuItem.Image")));
            this.removeFromWhitelistToolStripMenuItem.Name = "removeFromWhitelistToolStripMenuItem";
            this.removeFromWhitelistToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.removeFromWhitelistToolStripMenuItem.Text = "Remove from whitelist";
            // 
            // lblOnline
            // 
            this.lblOnline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOnline.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.lblOnline.Location = new System.Drawing.Point(3, 51);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(807, 23);
            this.lblOnline.TabIndex = 8;
            this.lblOnline.Text = "Online Users";
            this.lblOnline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnRefresh.Image = global::ArkRemoteAdmin.Properties.Resources.arrow_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(125, 45);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // selectedUser
            // 
            this.selectedUser.BackColor = System.Drawing.Color.White;
            this.selectedUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.selectedUser.Location = new System.Drawing.Point(0, 466);
            this.selectedUser.Name = "selectedUser";
            this.selectedUser.Size = new System.Drawing.Size(813, 95);
            this.selectedUser.TabIndex = 10;
            // 
            // OnlineUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.selectedUser);
            this.Controls.Add(this.lblOnline);
            this.Controls.Add(this.btnRefresh);
            this.Name = "OnlineUsers";
            this.Size = new System.Drawing.Size(813, 561);
            this.cmsPlayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SelectedUser selectedUser;
        private BssFramework.Windows.Forms.ListViewEx lvPlayers;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chSteamId;
        private BssFramework.Windows.Forms.SeperatorLabel lblOnline;
        private BssFramework.Windows.Forms.AeroButton btnRefresh;
        private System.Windows.Forms.ContextMenuStrip cmsPlayer;
        private System.Windows.Forms.ToolStripMenuItem openSteamProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem copyNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySteamIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unbanToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addToWhitelistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromWhitelistToolStripMenuItem;
    }
}
