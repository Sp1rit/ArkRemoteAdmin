﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;
using ArkRemoteAdmin.Properties;
using ArkRemoteAdmin.UserInterface;
using BssFramework.Windows.Forms;

namespace ArkRemoteAdmin
{
    public partial class FormMain : Form
    {
        private SynchronizationContext syncContext;
        private FormWindowState savedState;

        public FormMain()
        {
            InitializeComponent();

            toolStrip.Renderer = new SevenToolStripRenderer();
            cmsTray.Renderer = new SevenToolStripRenderer();

            players.SetStatus = SetStatus;
            server.SetStatus = SetStatus;
            chat.SetStatus = SetStatus;
            console.SetStatus = SetStatus;

            if (!Updater.Exists())
                tsbUpdate.Visible = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Set synchronization context
            syncContext = SynchronizationContext.Current;

            if (Updater.Exists())
                Updater.CheckSilent();

            // Register Events
            SourceRcon.HighLevel.ArkRcon.Client.Connecting += Client_Connecting;
            SourceRcon.HighLevel.ArkRcon.Client.Connected += Client_Connected;
            SourceRcon.HighLevel.ArkRcon.Client.ConnectionFailed += Client_ConnectionFailed;
            SourceRcon.HighLevel.ArkRcon.Client.Disconnected += Client_Disconnected;

            // Connect to default server
            Server defaultServer = Data.Data.Servers.FirstOrDefault(s => s.Default);

            if (defaultServer != null)
            {
                try
                {
                    SourceRcon.HighLevel.ArkRcon.Connect(defaultServer);
                    //Rcon.Connect(defaultServer);
                }
                catch (Exception ex)
                {

                    new TaskDialog()
                    {
                        WindowTitle = "Connection failed",
                        MainInstruction = ex.Message,
                        Content = "Make sure that the server is online and configured correctly.",
                        CommonButtons = TaskDialogCommonButtons.Ok,
                        MainIcon = TaskDialogIcon.Warning,
                        PositionRelativeToWindow = true
                    }.Show(this);
                }
            }
        }

        private void Client_Connecting(object sender, EventArgs e)
        {
            progressOverlay.showOverlay();
        }

        private void Client_ConnectionFailed(object sender, string e)
        {
            progressOverlay.hideOverlay();
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            progressOverlay.hideOverlay();

            syncContext.Send(state =>
            {
                tsbConnect.Visible = false;
                tsbDisconnect.Visible = true;
                toolStripSeparator1.Visible = true;
                tsbSaveWorld.Visible = true;
                tsExitServer.Visible = true;

                tabControl1.Visible = true;

                tslConnection.Image = Resources.connect;
                tslConnection.Text = "Connected to " + SourceRcon.HighLevel.ArkRcon.ConnectedServer.Name;
            }, null);

            SetStatus("Connected");
        }

        private void Client_Disconnected(object sender, bool e)
        {
            syncContext.Send(state =>
            {
                tsbConnect.Visible = true;
                tsbDisconnect.Visible = false;
                toolStripSeparator1.Visible = false;
                tsbSaveWorld.Visible = false;
                tsExitServer.Visible = false;

                tabControl1.Visible = false;

                tslConnection.Image = Resources.disconnect;
                tslConnection.Text = "Not Connected";
            }, null);

            SetStatus("Disconnected");
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                ShowInTaskbar = false;
            else
            {
                savedState = WindowState;
                ShowInTaskbar = true;
            }
        }

        public void SetVisible()
        {
            WindowState = savedState;
        }

        private void tsbConnect_Click(object sender, EventArgs e)
        {
            (new ServerList()).ShowDialog();
        }

        private void tsbSaveWorld_Click(object sender, EventArgs e)
        {
            SourceRcon.HighLevel.ArkRcon.Client.ExecuteCommandAsync(new SourceRcon.HighLevel.Commands.SaveWorld(), WorldSaved);
        }

        private void WorldSaved(object sender, SourceRcon.HighLevel.CommandExecutedEventArgs e)
        {
            if (e.Successful)
                SetStatus("World saved");
            else
                SetStatus("World save failed", StatusType.Error);

        }

        private void tsbDisconnect_Click(object sender, EventArgs e)
        {
            SourceRcon.HighLevel.ArkRcon.Client.Disconnect();
            //Rcon.Disconnect();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            (new FormAbout()).ShowDialog();
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            Process.Start(tsbDonate.Tag.ToString());
        }

        private void tsExitServer_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this, "Exit Server", "Are you sure you want to shut down the server?", "Be sure to save your world before you should the server down.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SourceRcon.HighLevel.ArkRcon.Client.ExecuteCommandAsync(new SourceRcon.HighLevel.Commands.DoExit(), ServerExited);
            }
        }

        private void ServerExited(object sender, SourceRcon.HighLevel.CommandExecutedEventArgs e)
        {
            if (e.Successful)
                SetStatus("Server shutting down");
            else
                SetStatus("Server could not be shut down", StatusType.Error);
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            (new FormSettings()).ShowDialog();
        }

        public void SetStatus(string status, StatusType type = StatusType.Ok)
        {
            syncContext.Post(state =>
            {
                var statusType = (StatusType)state;
                toolStripStatusLabel1.Text = status.ToString();
                switch (statusType)
                {
                    case StatusType.Ok:
                        toolStripStatusLabel3.Image = Resources.ok;
                        break;
                    case StatusType.Warning:
                        toolStripStatusLabel3.Image = Resources.warning;
                        break;
                    case StatusType.Error:
                        toolStripStatusLabel3.Image = Resources.error;
                        break;
                }
                listView1.Items.Add(new ListViewItem(status.ToString())
                {
                    ForeColor = statusType == StatusType.Ok ? System.Drawing.Color.Green : statusType == StatusType.Warning ? System.Drawing.Color.Goldenrod : System.Drawing.Color.Red,
                    ImageIndex = statusType == StatusType.Ok ? 0 : statusType == StatusType.Warning ? 1 : 2
                });
            }, type);
        }

        private void tsbWebsite_Click(object sender, EventArgs e)
        {
            Process.Start(tsbWebsite.Tag.ToString());
        }

        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            Updater.CheckNow();
        }

        private void tslLog_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
            panel1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel1.SendToBack();
        }

        #region Tray Icon

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = savedState;
        }

        private void cmsTray_Opening(object sender, CancelEventArgs e)
        {
            showToolStripMenuItem.Visible = WindowState == FormWindowState.Minimized;
            hideToolStripMenuItem.Visible = WindowState != FormWindowState.Minimized;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = savedState;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = savedState;
            Close();
        }

        #endregion // Tray Icon
    }

    public enum StatusType
    {
        Ok,
        Warning,
        Error
    }
}
