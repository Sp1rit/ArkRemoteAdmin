using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ArkRemoteAdmin.UI
{
    public partial class OnlineUsers : UserControl
    {
        private SynchronizationContext SyncContext;
        private bool selectedVisible = true;

        public OnlineUsers()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
            selectedUser.SetPlayer(new Player() { Name = "[xU] .$pIrit", SteamId = "76561197992236997" });

            HideSelected();

            Rcon.Connected += Rcon_Connected;
            Rcon.Disconnected += Rcon_Disconnected;
            Rcon.PlayersRefreshed += Rcon_PlayersRefreshed;
        }

        private void Rcon_Connected(object sender, EventArgs e)
        {
            //SetPlayers();
        }

        private void Rcon_Disconnected(object sender, EventArgs e)
        {
            Clear();
        }

        private void Rcon_PlayersRefreshed(object sender, Player[] players)
        {
            SyncContext.Send(state =>
            {
                lvPlayers.Clear();
                lvPlayers.Items.AddRange(players.Select(p => new ListViewItem(new[] { p.Name, p.SteamId }) { Tag = p }).ToArray());
            }, null);
        }

        #region Events

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetPlayers();
        }

        private void lvPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPlayers.SelectedItems.Count == 0)
                HideSelected();
            else
                ShowSelected();
        }

        #endregion // Events

        #region Private Methods

        private void Clear()
        {
            SyncContext.Send(state => lvPlayers.Clear(), null);
        }

        private async void SetPlayers()
        {
            Player[] players = await Rcon.ListPlayers();

            SyncContext.Send(state =>
            {
                lvPlayers.Clear();
                lvPlayers.Items.AddRange(players.Select(p => new ListViewItem(new[] { p.Name, p.SteamId }) { Tag = p }).ToArray());
            }, null);
        }

        private void ShowSelected()
        {
            if (!selectedVisible)
            {
                SyncContext.Send(state => lvPlayers.Height -= 101, null);
                selectedVisible = true;
            }
        }

        private void HideSelected()
        {
            if (selectedVisible)
            {
                SyncContext.Send(state => lvPlayers.Height += 101, null);
                selectedVisible = false;
            }
        }

        #endregion // Private Methods
    }
}
