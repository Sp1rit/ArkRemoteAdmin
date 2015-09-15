using ArkRemoteAdmin.UserInterface.Modules;

namespace ArkRemoteAdmin
{
    partial class FormMain
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formStateSaver = new BssFramework.Windows.Forms.FormStateSaver();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpPlayers = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbConnect = new System.Windows.Forms.ToolStripButton();
            this.tsbDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSaveWorld = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsbWebsite = new System.Windows.Forms.ToolStripButton();
            this.tsbDonate = new System.Windows.Forms.ToolStripButton();
            this.tsExitServer = new System.Windows.Forms.ToolStripButton();
            this.progressOverlay = new dotNetBase.Windows.Forms.progressOverlay();
            this.tslLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.players = new ArkRemoteAdmin.UserInterface.Players();
            this.server = new ArkRemoteAdmin.UserInterface.Modules.Server();
            this.schedulesList = new ArkRemoteAdmin.UserInterface.Modules.SchedulesList();
            this.chat = new ArkRemoteAdmin.UserInterface.Modules.Chat();
            this.console = new ArkRemoteAdmin.Console();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmsTray.SuspendLayout();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpPlayers.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.cmsTray;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "ARK Remote Admin";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.cmsTray.Name = "contextMenuStrip1";
            this.cmsTray.Size = new System.Drawing.Size(104, 76);
            this.cmsTray.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTray_Opening);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showToolStripMenuItem.Image")));
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hideToolStripMenuItem.Image")));
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // formStateSaver
            // 
            this.formStateSaver.FormToSave = this;
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer.ContentPanel.Controls.Add(this.tableLayoutPanel1);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(645, 350);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(645, 397);
            this.toolStripContainer.TabIndex = 0;
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel1,
            this.tslLog,
            this.tslConnection});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(645, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(467, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Status";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslConnection
            // 
            this.tslConnection.Image = global::ArkRemoteAdmin.Properties.Resources.disconnect;
            this.tslConnection.Name = "tslConnection";
            this.tslConnection.Size = new System.Drawing.Size(104, 17);
            this.tslConnection.Text = "Not Connected";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpPlayers);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 350);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Visible = false;
            // 
            // tpPlayers
            // 
            this.tpPlayers.Controls.Add(this.players);
            this.tpPlayers.Location = new System.Drawing.Point(4, 22);
            this.tpPlayers.Name = "tpPlayers";
            this.tpPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayers.Size = new System.Drawing.Size(637, 324);
            this.tpPlayers.TabIndex = 0;
            this.tpPlayers.Text = "Players";
            this.tpPlayers.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.server);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(637, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Server";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.schedulesList);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(637, 324);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Schedules";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.chat);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(637, 324);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Chat";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.console);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(637, 324);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Console";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(645, 350);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please connect to a server.";
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnect,
            this.tsbDisconnect,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.tsbSaveWorld,
            this.tsbSettings,
            this.tsbUpdate,
            this.tsbWebsite,
            this.tsbDonate,
            this.tsExitServer});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(645, 25);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 0;
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(72, 22);
            this.tsbConnect.Text = "Connect";
            this.tsbConnect.Click += new System.EventHandler(this.tsbConnect_Click);
            // 
            // tsbDisconnect
            // 
            this.tsbDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbDisconnect.Image")));
            this.tsbDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDisconnect.Name = "tsbDisconnect";
            this.tsbDisconnect.Size = new System.Drawing.Size(86, 22);
            this.tsbDisconnect.Text = "Disconnect";
            this.tsbDisconnect.Visible = false;
            this.tsbDisconnect.Click += new System.EventHandler(this.tsbDisconnect_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "About";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // tsbSaveWorld
            // 
            this.tsbSaveWorld.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveWorld.Image")));
            this.tsbSaveWorld.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveWorld.Name = "tsbSaveWorld";
            this.tsbSaveWorld.Size = new System.Drawing.Size(86, 22);
            this.tsbSaveWorld.Text = "Save World";
            this.tsbSaveWorld.Visible = false;
            this.tsbSaveWorld.Click += new System.EventHandler(this.tsbSaveWorld_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(23, 22);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsbUpdate
            // 
            this.tsbUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdate.Image")));
            this.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdate.Name = "tsbUpdate";
            this.tsbUpdate.Size = new System.Drawing.Size(23, 22);
            this.tsbUpdate.Text = "Check for updates";
            this.tsbUpdate.Click += new System.EventHandler(this.tsbUpdate_Click);
            // 
            // tsbWebsite
            // 
            this.tsbWebsite.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbWebsite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWebsite.Image = ((System.Drawing.Image)(resources.GetObject("tsbWebsite.Image")));
            this.tsbWebsite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWebsite.Name = "tsbWebsite";
            this.tsbWebsite.Size = new System.Drawing.Size(23, 22);
            this.tsbWebsite.Tag = "http://www.xunion.net";
            this.tsbWebsite.Text = "xUnion.net";
            this.tsbWebsite.Click += new System.EventHandler(this.tsbWebsite_Click);
            // 
            // tsbDonate
            // 
            this.tsbDonate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbDonate.Image = ((System.Drawing.Image)(resources.GetObject("tsbDonate.Image")));
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(65, 22);
            this.tsbDonate.Tag = resources.GetString("tsbDonate.Tag");
            this.tsbDonate.Text = "Donate";
            this.tsbDonate.ToolTipText = "Donate";
            this.tsbDonate.Click += new System.EventHandler(this.tsbDonate_Click);
            // 
            // tsExitServer
            // 
            this.tsExitServer.Image = ((System.Drawing.Image)(resources.GetObject("tsExitServer.Image")));
            this.tsExitServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExitServer.Name = "tsExitServer";
            this.tsExitServer.Size = new System.Drawing.Size(86, 22);
            this.tsExitServer.Text = "Stop Server";
            this.tsExitServer.Visible = false;
            this.tsExitServer.Click += new System.EventHandler(this.tsExitServer_Click);
            // 
            // progressOverlay
            // 
            this.progressOverlay.allowClose = true;
            this.progressOverlay.backColor = System.Drawing.Color.White;
            this.progressOverlay.cancelText = "Cancel";
            this.progressOverlay.Description = "Establishing a connection to the server.\r\nPlease wait...";
            this.progressOverlay.descriptionColor = System.Drawing.Color.Black;
            this.progressOverlay.descriptionFont = new System.Drawing.Font("Tahoma", 8F);
            this.progressOverlay.frameColor = System.Drawing.Color.DarkGray;
            this.progressOverlay.Owner = this;
            this.progressOverlay.Title = "Connecting";
            this.progressOverlay.titleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(151)))));
            this.progressOverlay.titleFont = new System.Drawing.Font("Tahoma", 10F);
            // 
            // tslLog
            // 
            this.tslLog.IsLink = true;
            this.tslLog.Name = "tslLog";
            this.tslLog.Size = new System.Drawing.Size(59, 17);
            this.tslLog.Text = "Show Log";
            this.tslLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tslLog.Click += new System.EventHandler(this.tslLog_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 397);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status Log";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(558, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 50);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(621, 335);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // players
            // 
            this.players.Dock = System.Windows.Forms.DockStyle.Fill;
            this.players.Location = new System.Drawing.Point(3, 3);
            this.players.Name = "players";
            this.players.Size = new System.Drawing.Size(631, 318);
            this.players.TabIndex = 3;
            // 
            // server
            // 
            this.server.Dock = System.Windows.Forms.DockStyle.Fill;
            this.server.Location = new System.Drawing.Point(3, 3);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(631, 318);
            this.server.TabIndex = 0;
            // 
            // schedulesList
            // 
            this.schedulesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schedulesList.Location = new System.Drawing.Point(3, 3);
            this.schedulesList.Name = "schedulesList";
            this.schedulesList.Size = new System.Drawing.Size(631, 318);
            this.schedulesList.TabIndex = 0;
            // 
            // chat
            // 
            this.chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chat.Location = new System.Drawing.Point(3, 3);
            this.chat.Name = "chat";
            this.chat.Size = new System.Drawing.Size(631, 318);
            this.chat.TabIndex = 0;
            // 
            // console
            // 
            this.console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console.Location = new System.Drawing.Point(3, 3);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(631, 318);
            this.console.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "accept.png");
            this.imageList1.Images.SetKeyName(1, "error.png");
            this.imageList1.Images.SetKeyName(2, "exclamation.png");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 397);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = " ARK Remote Admin";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.cmsTray.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpPlayers.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripButton tsbDisconnect;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSaveWorld;
        private System.Windows.Forms.ToolStripButton tsbDonate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPlayers;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripButton tsExitServer;
        private SchedulesList schedulesList;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private Chat chat;
        private Console console;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripStatusLabel tslConnection;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripButton tsbUpdate;
        private System.Windows.Forms.ToolStripButton tsbWebsite;
        private UserInterface.Players players;
        private UserInterface.Modules.Server server;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private BssFramework.Windows.Forms.FormStateSaver formStateSaver;
        private dotNetBase.Windows.Forms.progressOverlay progressOverlay;
        private System.Windows.Forms.ToolStripStatusLabel tslLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}

