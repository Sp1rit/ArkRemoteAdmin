using System;
using System.Threading;
using System.Windows.Forms;

namespace ArkRemoteAdmin
{
    public partial class Console : UserControl
    {
        private readonly SynchronizationContext syncContext;
        public Action<string, StatusType> SetStatus;

        public Console()
        {
            InitializeComponent();

            Rcon.Disconnected += Rcon_Disconnected;
            Rcon.CommandExecuted += Rcon_CommandExecuted;

            syncContext = SynchronizationContext.Current;
        }

        private void Rcon_Disconnected(object sender, EventArgs e)
        {
            Clear();
        }

        private void Rcon_CommandExecuted(object sender, CommandExecutedEventArgs e)
        {
            string log = string.Format("Command: {0}{1}Response: {2}", e.Command, Environment.NewLine, e.Response);
            Logging.LogConsole(log);
            syncContext.Post(state => rtbConsoleLog.AppendText(state.ToString() + Environment.NewLine), log);
        }

        private void Clear()
        {
            syncContext.Post(state =>
            {
                rtbConsoleLog.Clear();
                tbxCommand.Clear();
            }, null);
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxCommand.Text))
            {
                await Rcon.ExecuteCommand(tbxCommand.Text);
                SetStatus("Command executed", StatusType.Ok);
                tbxCommand.Clear();
            }
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
