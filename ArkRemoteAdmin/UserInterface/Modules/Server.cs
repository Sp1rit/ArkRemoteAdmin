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
using Rcon;
using Rcon.Commands;
using ArkRcon = ArkRemoteAdmin.Core.ArkRcon;

namespace ArkRemoteAdmin.UserInterface.Modules
{
    public partial class Server : UserControl
    {
        public Action<string, StatusType> SetStatus;
        private readonly SynchronizationContext syncContext;

        public Server()
        {
            InitializeComponent();
            syncContext = SynchronizationContext.Current;

            ArkRcon.Client.Connected += Client_Connected;
            ArkRcon.Client.Disconnected += Client_Disconnected;
        }

        private void Client_Disconnected(object sender, bool e)
        {
            syncContext.Send(state =>
            {
                rtbMotd.Clear();
                rtbBroadcast.Clear();
                dtbTimeOfDay.Value = new DateTime(1753, 1, 1, 6, 0, 0);
            }, null);
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            syncContext.Send(state =>
            {
                rtbMotd.Text = ArkRcon.ConnectedServer.MessageOfTheDay;
                btnMotd.Enabled = !string.IsNullOrEmpty(rtbMotd.Text);
            }, null);
        }

        private void btnMotd_Click(object sender, EventArgs e)
        {
            string motd = rtbMotd.Text.ToRcon();
            ArkRcon.Client.ExecuteCommandAsync(new SetMessageOfTheDay(motd), MessageOfTheDaySet);
        }

        private void MessageOfTheDaySet(object sender, CommandExecutedEventArgs e)
        {
            if (e.Successful)
            {
                syncContext.Send(state => ArkRcon.ConnectedServer.MessageOfTheDay = rtbMotd.Text, null);
                Data.Data.Set(ArkRcon.ConnectedServer);
                SetStatus("MOTD set", StatusType.Ok);
            }
            else
                SetStatus("MOTD could not be set", StatusType.Error);
        }

        private void btnShowMOTD_Click(object sender, EventArgs e)
        {
            ArkRcon.Client.ExecuteCommandAsync(new ShowMessageOfTheDay(), MessageOfTheDayShown);
        }

        private void MessageOfTheDayShown(object sender, CommandExecutedEventArgs e)
        {
            if (e.Successful)
                SetStatus("Message of the day shown", StatusType.Ok);
            else
                SetStatus("Could not show message of the day", StatusType.Error);
        }

        private void btnBroadcast_Click(object sender, EventArgs e)
        {
            ArkRcon.Client.ExecuteCommandAsync(new Broadcast(rtbBroadcast.Text.ToRcon()), Broadcasted);
        }

        private void Broadcasted(object sender, CommandExecutedEventArgs e)
        {
            if (e.Successful)
            {
                syncContext.Send(state => rtbBroadcast.Clear(), null);
                SetStatus("Broadcast sent", StatusType.Ok);
            }
            else
                SetStatus("Broadcast could not be sent", StatusType.Error);
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            ArkRcon.Client.ExecuteCommandAsync(new SetTimeOfDay(dtbTimeOfDay.Value.TimeOfDay), TimeSet);
        }

        private void TimeSet(object sender, CommandExecutedEventArgs e)
        {
            if (e.Successful)
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
