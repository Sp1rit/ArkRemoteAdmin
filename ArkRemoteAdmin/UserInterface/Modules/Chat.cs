using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;
using Rcon;
using Rcon.Commands;
using ArkRcon = ArkRemoteAdmin.Core.ArkRcon;

namespace ArkRemoteAdmin.UserInterface.Modules
{
    public partial class Chat : UserControl
    {
        private static readonly Regex Regex = new Regex(@"^\[[0-9]{2}:[0-9]{2}:[0-9]{2}\]", RegexOptions.Compiled | RegexOptions.Multiline);
        public Action<string, StatusType> SetStatus;
        private readonly SynchronizationContext SyncContext;

        public Chat()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
            comboBox1.SelectedIndex = 0;

            ArkRcon.Client.Connected += Client_Connected;
            ArkRcon.Client.Disconnected += Client_Disconnected;
            ArkRcon.PlayersRefreshed += Rcon_PlayersRefreshed;
            ArkRcon.ChatMessages += Rcon_ChatMessages;
        }

        private void Rcon_ChatMessages(object sender, string e)
        {
            if (!string.IsNullOrEmpty(e))
                SyncContext.Send(state => AddMessages(state.ToString()), e);
        }

        private void Rcon_PlayersRefreshed(object sender, System.Collections.Generic.List<Player> players)
        {
            SyncContext.Send(state =>
            {
                try
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add(new {Key = "Everyone", Value = (Player) null});
                    comboBox1.Items.AddRange(players.Select(p => new {Key = p.Name, Value = p}).ToArray());
                    comboBox1.SelectedIndex = 0;
                }
                catch { }
            }, null);
        }

        private void Client_Disconnected(object sender, bool e)
        {
            SyncContext.Send(state =>
            {
                try
                {
                    rtbLog.Clear();
                    tbxChatMessage.Clear();
                }
                catch { }
                ArkRcon.StopChatReceiver();
            }, null);
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            SyncContext.Send(state =>
            {
                LoadLog();
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

        private void tbxChatMessage_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = !string.IsNullOrEmpty(tbxChatMessage.Text);
        }

        private void tbxChatMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #region Public Methods

        public void FocusMessage()
        {
            tbxChatMessage.Focus();
        }

        #endregion // Public Methods

        #region Private Methods

        /// <summary>
        /// Send a chat message to the server.
        /// </summary>
        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(tbxChatMessage.Text))
            {
                string message = tbxChatMessage.Text.ToRcon();
                if (!string.IsNullOrEmpty(ArkRcon.ConnectedServer.ChatName))
                    message = ArkRcon.ConnectedServer.ChatName + ": " + message;

                if (comboBox1.SelectedIndex > 0)
                    ArkRcon.Client.ExecuteCommandAsync(new ServerChatTo(((dynamic)comboBox1.SelectedItem).Value.SteamId, message), MessageSent);
                else
                    ArkRcon.Client.ExecuteCommandAsync(new ServerChat(message), MessageSent);
            }
        }

        private void MessageSent(object sender, CommandExecutedEventArgs e)
        {
            if (e.Successful)
            {
                SyncContext.Send(state => tbxChatMessage.Clear(), null);
                SetStatus("Message sent", StatusType.Ok);
            }
            else
                SetStatus("Message could not be sent", StatusType.Error);
        }

        /// <summary>
        /// Add one message as received from the server.
        /// </summary>
        /// <param name="message"></param>
        private void AddMessage(string message)
        {
            try
            {
                if (ArkRcon.ConnectedServer != null && !string.IsNullOrEmpty(ArkRcon.ConnectedServer.ChatName) && message.StartsWith("SERVER: "))
                    message = message.Substring(8, message.Length - 8);

                Logging.LogChat(message);
                int length = rtbLog.TextLength;
                rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
                rtbLog.Select(length, 10);
                rtbLog.SelectionColor = Color.Green;
            }
            catch { }
        }

        /// <summary>
        /// Add a message stack as received from the server.
        /// Messages are seperated by \n.
        /// </summary>
        /// <param name="messages"></param>
        private void AddMessages(string messages)
        {
            string[] splits = messages.Split('\n');
            foreach (string split in splits)
                AddMessage(split);

            ScrollToEnd();
        }

        /// <summary>
        /// Load the log from file and format the output.
        /// </summary>
        private void LoadLog()
        {
            try
            {
                string log = Logging.GetChatLog();
                rtbLog.Text = log;
                MatchCollection matches = Regex.Matches(log.Replace(Environment.NewLine, "\n"));
                foreach(Match match in matches)
                {
                    rtbLog.Select(match.Index, match.Length);
                    rtbLog.SelectionColor = Color.Green;
                }
                ScrollToEnd();
            }
            catch { }
        }

        private void ScrollToEnd()
        {
            try
            {
                rtbLog.Select(rtbLog.Text.Length, 0);
                rtbLog.ScrollToCaret();
            }
            catch { }
        }

        #endregion // Private Methods
    }
}
