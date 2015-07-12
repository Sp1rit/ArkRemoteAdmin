using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;

namespace ArkRemoteAdmin
{
    public partial class FormMain : Form
    {
        private SynchronizationContext sc;

        public FormMain()
        {
            InitializeComponent();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            // Set synchronization context
            sc = SynchronizationContext.Current;

            // Check for updates
            var updater = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "updater.exe");

            if (File.Exists(updater))
                Process.Start(updater, "/silent /checknow");

            // Register Events
            Rcon.Connected += Rcon_Connected;
            Rcon.Disconnected += Rcon_Disconnected;

            // Connect to default server
            var defaultServer = Data.Data.Servers.FirstOrDefault(s => s.Default);

            if (defaultServer != null)
                await Rcon.Connect(defaultServer);
        }

        private async void Rcon_Connected(object sender, EventArgs e)
        {
            sc.Send(state =>
            {
                tsbConnect.Visible = false;
                tsbDisconnect.Visible = true;
                tsbRefresh.Visible = true;
                rtbMotd.Enabled = true;
                btnMotd.Enabled = true;
                toolStripSeparator1.Visible = true;
                toolStripLabel1.Visible = true;
                tsbSaveWorld.Visible = true;
                tsExitServer.Visible = true;

                chat1.StartChat();
                schedulesList1.LoadSchedules();
                schedulesList1.ActivateSchedules();

                tabControl1.Visible = true;
            }, null);

            if (Rcon.IsConnected)
                await RefreshPlayers();

            SetStatus("Connected");
        }

        private void Rcon_Disconnected(object sender, EventArgs e)
        {
            sc.Send(state =>
            {
                chat1.StopChat();
                console1.Clear();
                listView1.Items.Clear();
                Scheduler.Instance.DeactivateAll();

                tsbConnect.Visible = true;
                tsbDisconnect.Visible = false;
                tsbRefresh.Visible = false;
                rtbMotd.Enabled = false;
                btnMotd.Enabled = false;
                toolStripSeparator1.Visible = false;
                toolStripLabel1.Visible = false;
                tsbSaveWorld.Visible = false;
                tsExitServer.Visible = false;

                tabControl1.Visible = false;
            }, null);

            SetStatus("Disconnected");
        }

        private void tsbConnect_Click(object sender, EventArgs e)
        {
            (new ServerList()).ShowDialog();
        }

        private void cmsPlayer_Opening(object sender, CancelEventArgs e)
        {
            List<Player> players = (from ListViewItem item in listView1.Items select item.Tag as Player).ToList();

            if (players.Count == 0)
                e.Cancel = true;
        }

        private async void kickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Player> players = (from ListViewItem item in listView1.Items select item.Tag as Player).ToList();
            List<Player> kickedPlayers = new List<Player>();
            List<Player> failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.Kick())
                    kickedPlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }

            if (kickedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} kicked.", string.Join(", ", kickedPlayers.Select(p => p.Name))));
            if (failedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} could not be kicked!", string.Join(", ", failedPlayers.Select(p => p.Name))));

            await RefreshPlayers();
        }

        private async void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Player> players = (from ListViewItem item in listView1.Items select item.Tag as Player).ToList();
            List<Player> kickedPlayers = new List<Player>();
            List<Player> failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.Ban())
                    kickedPlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }

            if (kickedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} banned.", string.Join(", ", kickedPlayers.Select(p => p.Name))));
            if (failedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} could not be banned!", string.Join(", ", failedPlayers.Select(p => p.Name))));

            await RefreshPlayers();
        }

        private async void addToWhitelistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Player> players = (from ListViewItem item in listView1.Items select item.Tag as Player).ToList();
            List<Player> kickedPlayers = new List<Player>();
            List<Player> failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.AddToWhitelist())
                    kickedPlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }

            if (kickedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} added to whitelist.", string.Join(", ", kickedPlayers.Select(p => p.Name))));
            if (failedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} could not be added to whitelist!", string.Join(", ", failedPlayers.Select(p => p.Name))));
        }

        private async void removeFromWhitelistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Player> players = (from ListViewItem item in listView1.Items select item.Tag as Player).ToList();
            List<Player> kickedPlayers = new List<Player>();
            List<Player> failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.RemoveFromWhitelist())
                    kickedPlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }

            if (kickedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} removed from whitelist.", string.Join(", ", kickedPlayers.Select(p => p.Name))));
            if (failedPlayers.Count > 0)
                MessageBox.Show(string.Format("Player(s) {0} could not be removed from whitelist!", string.Join(", ", failedPlayers.Select(p => p.Name))));
        }

        private async void tsbRefresh_Click(object sender, EventArgs e)
        {
            await RefreshPlayers();
        }

        private async void tsbSaveWorld_Click(object sender, EventArgs e)
        {
            string response = await Rcon.ExecuteCommand("saveworld");

            if (response.Trim() == "World Saved")
                SetStatus("World saved");
            else
            {
                SetStatus("World save failed");
                MessageBox.Show("World save failed");
            }
        }

        private async Task RefreshPlayers()
        {

            var players = await Rcon.ListPlayers();

            sc.Send(state =>
            {
                listView1.Items.Clear();
                if (players.Length > 0)
                    listView1.Items.AddRange(players.Select(p => new ListViewItem(new[] { p.Name, p.SteamId }) { Tag = p }).ToArray());
                else
                    SetStatus("No players connected");
            }, null);
        }

        private async void btnMotd_Click(object sender, EventArgs e)
        {
            string motd = rtbMotd.Text.Replace(Environment.NewLine, "\n").Trim();
            string response = await Rcon.ExecuteCommand(string.Format("setmessageoftheday {0}", motd));

            if (response.Trim() == string.Format("Message of set to {0}", motd))
                MessageBox.Show("MOTD set.");
            else
                MessageBox.Show("MOTD could not be set.");
        }

        private void tsbDisconnect_Click(object sender, EventArgs e)
        {
            Rcon.Disconnect();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            (new FormAbout()).ShowDialog();
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            Process.Start(tsbDonate.Tag.ToString());
        }

        private void SetStatus(string status)
        {
            sc.Post(state => toolStripStatusLabel1.Text = state.ToString(), status);
        }

        private async void btnShowMOTD_Click(object sender, EventArgs e)
        {
            if (await Rcon.ShowMessageOfTheDay())
                SetStatus("Message of the day shown");
            else
                SetStatus("Could not show message of the day");
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (await Rcon.Broadcast(rtbBroadcast.Text))
            {
                rtbBroadcast.Clear();
                SetStatus("Broadcast sent");
            }
            else
                SetStatus("Broadcast could not be sent");
        }

        private async void btnSetTime_Click(object sender, EventArgs e)
        {
            if (await Rcon.SetTimeOfDay(dateTimePicker1.Value))
                SetStatus("TimeOfDay set");
            else
                SetStatus("TimeOfDay could not be set");
        }

        private async void tsExitServer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to shut down the server?", "Exit Server", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (await Rcon.ExitServer())
                    SetStatus("Server shutting down");
                else
                    SetStatus("Server could not be shut down");
            }
        }
    }
}
