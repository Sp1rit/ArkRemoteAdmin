using System;
using System.Threading;
using System.Windows.Forms;
using ArkRemoteAdmin.SourceRcon.HighLevel.Commands;
using ArkRcon = ArkRemoteAdmin.SourceRcon.HighLevel.ArkRcon;

namespace ArkRemoteAdmin
{
    public partial class Console : UserControl
    {
        private readonly SynchronizationContext syncContext;
        public Action<string, StatusType> SetStatus;

        public Console()
        {
            InitializeComponent();

            ArkRcon.Client.Disconnected += Client_Disconnected;
            ArkRcon.Client.CommandExecuted += Client_CommandExecuted;

            syncContext = SynchronizationContext.Current;
        }

        private void Client_CommandExecuted(object sender, SourceRcon.HighLevel.CommandExecutedEventArgs e)
        {
            if (e.Command.Type != SourceRcon.HighLevel.CommandType.GetChat && e.Command.Type != SourceRcon.HighLevel.CommandType.ListPlayers)
            {
                string log = $"Command: {e.Command}{Environment.NewLine}Response: {e.Response}{Environment.NewLine}";
                Logging.LogConsole(log);
                syncContext.Post(state =>
                {
                    try
                    {
                        rtbConsoleLog.AppendText(state.ToString() + Environment.NewLine);
                    }
                    catch { }
                }, log);
            }
        }

        private void Client_Disconnected(object sender, bool e)
        {
            Clear();
        }

        private void Clear()
        {
            syncContext.Post(state =>
            {
                rtbConsoleLog.Clear();
                tbxCommand.Clear();
            }, null);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxCommand.Text))
                ArkRcon.Client.ExecuteCommandAsync(new Raw(tbxCommand.Text), CommandExecuted);
        }

        private void CommandExecuted(object sender, SourceRcon.HighLevel.CommandExecutedEventArgs e)
        {
            SetStatus("Command executed", StatusType.Ok);
            syncContext.Send(state => tbxCommand.Clear(), null);
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
    }
}
