using System;
using System.Threading;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;

namespace ArkRemoteAdmin.UserInterface.Modules
{
    public partial class Chat : UserControl
    {
        public Action<string, StatusType> SetStatus;
        private readonly SynchronizationContext syncContext;

        public Chat()
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
                ckbGetChat.Checked = Rcon.Server.GetChat;
                StartChat();
            }, null);
        }

        private void Rcon_Disconnected(object sender, EventArgs e)
        {
            syncContext.Send(state => StopChat(), null);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void tbxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ckbGetChat_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbGetChat.Checked)
                StartChat();
            else
                timerChat.Stop();

            Rcon.Server.GetChat = ckbGetChat.Checked;
            Data.Data.Set(Rcon.Server);
        }

        private async void timerChat_Tick(object sender, EventArgs e)
        {
            if (!Rcon.IsConnected)
            {
                timerChat.Stop();
                return;
            }

            string chatLog = await Rcon.GetChat();
            if (!string.IsNullOrEmpty(chatLog))
            {
                Logging.LogChat(chatLog);
                rtbLog.AppendText(chatLog + Environment.NewLine);
            }
        }

        private async void SendMessage()
        {
            if (!string.IsNullOrEmpty(tbxMessage.Text))
            {
                bool done = await Rcon.SendChatMessage(tbxMessage.Text);

                if (done)
                {
                    tbxMessage.Clear();
                    SetStatus("Message sent", StatusType.Ok);
                }
                else
                    SetStatus("Message could not be sent", StatusType.Error);
            }
        }

        public void StartChat()
        {
            if (ckbGetChat.Checked)
                timerChat.Start();
        }

        public void StopChat()
        {
            timerChat.Stop();
            rtbLog.Clear();
            tbxMessage.Clear();
        }

        private void tbxMessage_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = !string.IsNullOrEmpty(tbxMessage.Text);
        }
    }


}
