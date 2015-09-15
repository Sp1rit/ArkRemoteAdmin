using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using BssFramework;
using BssFramework.Windows.Forms;

namespace ArkRemoteAdmin.UI
{
    partial class SelectedUser : UserControl
    {
        #region Events

        public event EventHandler Kick;
        public event EventHandler Ban;
        public event EventHandler AddToWhitelist;
        public event EventHandler RemoveFromWhitelist;
        public event EventHandler SendMessage;

        #endregion // Events

        public Player Player { get; private set; } = new Player();

        public SelectedUser()
        {
            InitializeComponent();
        }

        #region Events

        private void btnKick_Click(object sender, EventArgs e) => Kick?.Invoke(this, EventArgs.Empty);

        private void btnBan_Click(object sender, EventArgs e) => Ban?.Invoke(this, EventArgs.Empty);

        private void btnAddWhitelist_Click(object sender, EventArgs e) => AddToWhitelist?.Invoke(this, EventArgs.Empty);

        private void btnRemoveWhitelist_Click(object sender, EventArgs e) => RemoveFromWhitelist?.Invoke(this, EventArgs.Empty);

        private void btnMessage_Click(object sender, EventArgs e) => SendMessage?.Invoke(this, EventArgs.Empty);

        private void lnkProfile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.OpenWebsite(lnkProfile.Tag?.ToString());
        }

        #endregion // Events

        #region Public Methods

        public void SetPlayer(Player player)
        {
            Player = player;
            lblName.Text = player.Name;
            lblSteamIdValue.Text = player.SteamId;
            lnkProfile.Tag = $"http://steamcommunity.com/profiles/{player.SteamId}";
        }

        #endregion // Public Methods
    }
}
