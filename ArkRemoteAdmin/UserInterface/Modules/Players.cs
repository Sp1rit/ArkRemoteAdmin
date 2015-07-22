using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BssFramework.Windows.Forms;

namespace ArkRemoteAdmin.UserInterface
{
    public partial class Players : UserControl
    {
        public Action<string, StatusType> SetStatus;
        private SynchronizationContext syncContext;

        public Players()
        {
            InitializeComponent();
            toolStrip.Renderer = new SevenToolStripRenderer();
            cmsPlayer.Renderer = new SevenToolStripRenderer();
            syncContext = SynchronizationContext.Current;

            Rcon.Connected += Rcon_Connected;
            Rcon.Disconnected += Rcon_Disconnected;
        }

        private async void Rcon_Connected(object sender, EventArgs e)
        {
            await RefreshPlayers();
        }

        private void Rcon_Disconnected(object sender, EventArgs e)
        {
            ClearPlayers();
        }

        private void cmsPlayer_Opening(object sender, CancelEventArgs e)
        {
            if (GetSelectedPlayers().Length == 0)
                e.Cancel = true;
        }

        private void lvPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            bool bannedPlayers = players.Any(p => p.Banned);
            bool unbannedPlayers = players.Any(p => !p.Banned);

            tsbKick.Visible = unbannedPlayers && !bannedPlayers;
            tsbBan.Visible = unbannedPlayers && !bannedPlayers;
            tsbUnban.Visible = !unbannedPlayers && bannedPlayers;
            tsbAddWhitelist.Visible = unbannedPlayers && !bannedPlayers;
            tsbRemoveWhitelist.Visible = unbannedPlayers && !bannedPlayers;
            toolStripSeparator3.Visible = bannedPlayers || unbannedPlayers;
            toolStripSeparator1.Visible = unbannedPlayers;

            kickToolStripMenuItem.Visible = unbannedPlayers && !bannedPlayers;
            banToolStripMenuItem.Visible = unbannedPlayers && !bannedPlayers;
            unbanToolStripMenuItem.Visible = !unbannedPlayers && bannedPlayers;
            addToWhitelistToolStripMenuItem.Visible = unbannedPlayers && !bannedPlayers;
            removeFromWhitelistToolStripMenuItem.Visible = unbannedPlayers && !bannedPlayers;
            toolStripSeparator5.Visible = bannedPlayers || unbannedPlayers;
            toolStripSeparator2.Visible = unbannedPlayers;
        }

        #region Menu Commands

        private async void tsbRefresh_Click(object sender, EventArgs e)
        {
            if (await RefreshPlayers() > 0)
                SetStatus("Players refreshed", StatusType.Ok);
            else
                SetStatus("No players connected", StatusType.Warning);
        }

        private void openSteamProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();

            foreach (var player in players)
                Process.Start(string.Format("http://steamcommunity.com/profiles/{0}", player.SteamId));
        }

        private void copyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            Clipboard.SetText(string.Join(Environment.NewLine, players.Select(p => p.Name)));
        }

        private void copySteamIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            Clipboard.SetText(string.Join(Environment.NewLine, players.Select(p => p.SteamId)));
        }

        private async void kickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            List<Player> donePlayers = new List<Player>(), failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.Kick())
                    donePlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }

            if (donePlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} kicked.", string.Join(", ", donePlayers.Select(p => p.Name))), StatusType.Ok);
            if (failedPlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} could not be kicked!", string.Join(", ", failedPlayers.Select(p => p.Name))), StatusType.Error);

            await RefreshPlayers();
        }

        private async void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            List<Player> donePlayers = new List<Player>(), failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.Ban())
                    donePlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }
            Data.Data.Set(Rcon.Server);

            if (donePlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} banned.", string.Join(", ", donePlayers.Select(p => p.Name))), StatusType.Ok);
            if (failedPlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} could not be banned!", string.Join(", ", failedPlayers.Select(p => p.Name))), StatusType.Error);

            await RefreshPlayers();
        }

        private async void unbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            List<Player> donePlayers = new List<Player>(), failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.Unban())
                    donePlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }
            Data.Data.Set(Rcon.Server);

            if (donePlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} unbanned.", string.Join(", ", donePlayers.Select(p => p.Name))), StatusType.Ok);
            if (failedPlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} could not be unbanned!", string.Join(", ", failedPlayers.Select(p => p.Name))), StatusType.Error);

            await RefreshPlayers();
        }

        private async void addToWhitelistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            List<Player> donePlayers = new List<Player>(), failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.AddToWhitelist())
                    donePlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }

            if (donePlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} added to whitelist.", string.Join(", ", donePlayers.Select(p => p.Name))), StatusType.Ok);
            if (failedPlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} could not be added to whitelist!", string.Join(", ", failedPlayers.Select(p => p.Name))), StatusType.Error);

            await RefreshPlayers();
        }

        private async void removeFromWhitelistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player[] players = GetSelectedPlayers();
            List<Player> donePlayers = new List<Player>(), failedPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (await player.RemoveFromWhitelist())
                    donePlayers.Add(player);
                else
                    failedPlayers.Add(player);
            }

            if (donePlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} removed from whitelist.", string.Join(", ", donePlayers.Select(p => p.Name))), StatusType.Ok);
            if (failedPlayers.Count > 0)
                SetStatus(string.Format("Player(s) {0} could not be removed from whitelist!", string.Join(", ", failedPlayers.Select(p => p.Name))), StatusType.Error);

            await RefreshPlayers();
        }

        #endregion // Menu Commands

        #region Internal Methods

        private Player[] GetSelectedPlayers()
        {
            Player[] players = (from ListViewItem item in lvPlayers.SelectedItems where item.Tag is Player select item.Tag as Player).ToArray();

            return players;
        }

        private Player GetSelectedPlayer()
        {
            if (lvPlayers.SelectedItems.Count > 0)
                return lvPlayers.SelectedItems[0].Tag as Player;

            return null;
        }

        private async Task<int> RefreshPlayers()
        {
            var players = await Rcon.ListPlayers();

            syncContext.Send(state =>
            {
                lvPlayers.Items.Clear();
                if (players.Length > 0)
                    lvPlayers.Items.AddRange(players.Select(p => new ListViewItem(new[] { p.Name, p.SteamId }) { Tag = p, Group = lvPlayers.Groups["lvgOnline"] }).ToArray());
                lvPlayers.Items.AddRange(Rcon.Server.BannedPlayers.Select(p => new ListViewItem(new[] {p.Name, p.SteamId}) {Tag = p, Group = lvPlayers.Groups["lvgBanned"]}).ToArray());
                lvPlayers_SelectedIndexChanged(this, new EventArgs());
            }, null);

            return players.Length;
        }

        private void ClearPlayers()
        {
            syncContext.Send(state => lvPlayers.Items.Clear(), null);
        }

        #endregion // Internal Methods
    }
}
