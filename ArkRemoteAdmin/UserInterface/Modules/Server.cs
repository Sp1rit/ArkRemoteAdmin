using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.UserInterface.Modules
{
    public partial class Server : UserControl
    {
        public Action<string, StatusType> SetStatus;
        private SynchronizationContext syncContext;

        public Server()
        {
            InitializeComponent();
            syncContext = SynchronizationContext.Current;

            Rcon.Connected += Rcon_Connected;
            Rcon.Disconnected += Rcon_Disconnected;
        }

        private void Rcon_Connected(object sender, EventArgs e)
        {
            syncContext.Send(state =>
            {
                rtbMotd.Text = Rcon.Server.MessageOfTheDay;
                btnMotd.Enabled = !string.IsNullOrEmpty(rtbMotd.Text);
            }, null);
        }

        private void Rcon_Disconnected(object sender, EventArgs e)
        {
            syncContext.Send(state =>
            {
                rtbMotd.Clear();
                rtbBroadcast.Clear();
                dtbTimeOfDay.Value = new DateTime(1753, 1, 1, 6, 0, 0);
            }, null);
        }

        private async void btnMotd_Click(object sender, EventArgs e)
        {
            string motd = rtbMotd.Text.Replace(Environment.NewLine, "\n").Trim();
            string response = await Rcon.ExecuteCommand(string.Format("setmessageoftheday {0}", motd));

            if (response.Trim() == string.Format("Message of set to {0}", motd))
            {
                Rcon.Server.MessageOfTheDay = rtbMotd.Text;
                Data.Data.Set(Rcon.Server);
                SetStatus("MOTD set", StatusType.Ok);
            }
            else
                SetStatus("MOTD could not be set", StatusType.Error);
        }

        private async void btnShowMOTD_Click(object sender, EventArgs e)
        {
            if (await Rcon.ShowMessageOfTheDay())
                SetStatus("Message of the day shown", StatusType.Ok);
            else
                SetStatus("Could not show message of the day", StatusType.Error);
        }

        private async void btnBroadcast_Click(object sender, EventArgs e)
        {
            if (await Rcon.Broadcast(rtbBroadcast.Text))
            {
                rtbBroadcast.Clear();
                SetStatus("Broadcast sent", StatusType.Ok);
            }
            else
                SetStatus("Broadcast could not be sent", StatusType.Error);
        }

        private async void btnSetTime_Click(object sender, EventArgs e)
        {
            if (await Rcon.SetTimeOfDay(dtbTimeOfDay.Value))
                SetStatus("TimeOfDay set", StatusType.Ok);
            else
                SetStatus("TimeOfDay could not be set", StatusType.Error);
        }

        private void rtbMotd_TextChanged(object sender, EventArgs e)
        {
            btnMotd.Enabled = !string.IsNullOrEmpty(rtbMotd.Text);
        }

        private void rtbBroadcast_TextChanged(object sender, EventArgs e)
        {
            btnBroadcast.Enabled = !string.IsNullOrEmpty(rtbBroadcast.Text);
        }
    }
}
