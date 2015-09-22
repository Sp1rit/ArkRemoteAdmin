using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Rcon;
using Rcon.Commands;
using ArkRcon = ArkRemoteAdmin.Core.ArkRcon;

namespace ArkRemoteAdmin
{
    public partial class Console : UserControl
    {
        private static readonly Regex Regex = new Regex(@"^\[[0-9]{2}:[0-9]{2}:[0-9]{2}\]", RegexOptions.Compiled | RegexOptions.Multiline);
        private readonly SynchronizationContext syncContext;
        public Action<string, StatusType> SetStatus;

        public Console()
        {
            InitializeComponent();
            syncContext = SynchronizationContext.Current;

            ArkRcon.Client.Connected += Client_Connected;
            ArkRcon.Client.Disconnected += Client_Disconnected;
            ArkRcon.Client.CommandExecuted += Client_CommandExecuted;
        }

        private void Client_CommandExecuted(object sender, CommandExecutedEventArgs e)
        {
            if (e.Command.Type != CommandType.GetChat && e.Command.Type != CommandType.ListPlayers)
            {
                syncContext.Post(state =>
                {
                    AddMessage($"Command: {e.Command}");
                    AddMessage($"Response: {e.Response}");
                    ScrollToEnd();
                }, e);
            }
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            syncContext.Send(state => LoadLog(), null);
        }

        private void Client_Disconnected(object sender, bool e)
        {
            Clear();
        }

        private void Clear()
        {
            syncContext.Post(state =>
            {
                try
                {
                    rtbConsoleLog.Clear();
                    tbxCommand.Clear();
                }
                catch { }
            }, null);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxCommand.Text))
                ArkRcon.Client.ExecuteCommandAsync(new Raw(tbxCommand.Text), CommandExecuted);
        }

        private void CommandExecuted(object sender, CommandExecutedEventArgs e)
        {
            SetStatus("Command executed", StatusType.Ok);
            syncContext.Send(state =>
            {
                try
                {
                    tbxCommand.Clear();
                }
                catch { }
            }, null);
        }

        private void tbxCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnExecute_Click(sender, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void tbxCommand_TextChanged(object sender, EventArgs e)
        {
            btnExecute.Enabled = !string.IsNullOrEmpty(tbxCommand.Text);
        }

        #region Public Methods

        public void FocusMessage()
        {
            tbxCommand.Focus();
        }

        #endregion // Public Methods

        #region Private Methods

        /// <summary>
        /// Add one message as received from the server.
        /// </summary>
        /// <param name="message"></param>
        private void AddMessage(string message)
        {
            try
            {
                Logging.LogConsole(message);
                int length = rtbConsoleLog.TextLength;
                rtbConsoleLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
                rtbConsoleLog.Select(length, 10);
                rtbConsoleLog.SelectionColor = Color.Green;
            }
            catch { }
        }

        /// <summary>
        /// Load the log from file and format the output.
        /// </summary>
        private void LoadLog()
        {
            try
            {
                string log = Logging.GetConsoleLog();
                rtbConsoleLog.Text = log;
                MatchCollection matches = Regex.Matches(log.Replace(Environment.NewLine, "\n"));

                foreach(Match match in matches)
                {
                    rtbConsoleLog.Select(match.Index, match.Length);
                    rtbConsoleLog.SelectionColor = Color.Green;
                }
                ScrollToEnd();
            }
            catch { }
        }

        private void ScrollToEnd()
        {
            try
            {
                rtbConsoleLog.Select(rtbConsoleLog.Text.Length, 0);
                rtbConsoleLog.ScrollToCaret();
            }
            catch { }
        }

        #endregion // Private Methods
    }
}
