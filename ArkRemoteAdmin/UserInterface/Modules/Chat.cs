using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;
using ArkRemoteAdmin.SourceRcon.HighLevel.Commands;
using ArkRcon = ArkRemoteAdmin.SourceRcon.HighLevel.ArkRcon;

namespace ArkRemoteAdmin.UserInterface.Modules
{
    public partial class Chat : UserControl
    {
        public Action<string, StatusType> SetStatus;
        private readonly SynchronizationContext SyncContext;

        public Chat()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            SyncContext = SynchronizationContext.Current;

            ArkRcon.Client.Connected += Client_Connected;
            ArkRcon.Client.Disconnected += Client_Disconnected;
            ArkRcon.PlayersRefreshed += Rcon_PlayersRefreshed;
            ArkRcon.ChatMessages += Rcon_ChatMessages;
        }

        private void Rcon_ChatMessages(object sender, string e)
        {
            if (!string.IsNullOrEmpty(e))
            {
                Logging.LogChat(e);
                SyncContext.Send(state =>
                {
                    rtbLog.AppendText(e + Environment.NewLine);
                    rtbLog.SelectionStart = rtbLog.Text.Length;
                    rtbLog.ScrollToCaret();
                }, null);
            }
        }

        private void Rcon_PlayersRefreshed(object sender, System.Collections.Generic.List<Player> players)
        {
            SyncContext.Send(state =>
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add(new { Key = "Everyone", Value = (Player)null });
                comboBox1.Items.AddRange(players.Select(p => new { Key = p.Name, Value = p }).ToArray());
                comboBox1.SelectedIndex = 0;
            }, null);
        }

        private void Client_Disconnected(object sender, bool e)
        {
            SyncContext.Send(state =>
            {
                rtbLog.Clear();
                rtbMessage.Clear();
                ArkRcon.StopChatReceiver();
            }, null);
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            SyncContext.Send(state =>
            {
                ckbGetChat.Checked = ArkRcon.ConnectedServer.GetChat;
                if (ArkRcon.ConnectedServer.GetChat)
                    ArkRcon.StartChatReceiver();
            }, null);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void ckbGetChat_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbGetChat.Checked)
                ArkRcon.StartChatReceiver();
            else
                ArkRcon.StopChatReceiver();

            ArkRcon.ConnectedServer.GetChat = ckbGetChat.Checked;
            Data.Data.Set(ArkRcon.ConnectedServer);
        }

        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(rtbMessage.Text))
            {
                string message = rtbMessage.Text.ToRcon();
                if (!string.IsNullOrEmpty(ArkRcon.ConnectedServer.ChatName))
                    message = ArkRcon.ConnectedServer.ChatName + ": " + message;

                if (comboBox1.SelectedIndex > 0)
                    ArkRcon.Client.ExecuteCommandAsync(new ServerChatTo(((dynamic)comboBox1.SelectedItem).Value.SteamId, message), MessageSent);
                else
                    ArkRcon.Client.ExecuteCommandAsync(new ServerChat(message), MessageSent);
            }
        }

        private void MessageSent(object sender, SourceRcon.HighLevel.CommandExecutedEventArgs e)
        {
            if (e.Successful)
            {
                SyncContext.Send(state => rtbMessage.Clear(), null);
                SetStatus("Message sent", StatusType.Ok);
            }
            else
                SetStatus("Message could not be sent", StatusType.Error);
        }

        private void rtbMessage_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = !string.IsNullOrEmpty(rtbMessage.Text);
        }
    }
}
