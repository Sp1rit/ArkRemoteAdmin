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

namespace ArkRemoteAdmin
{
    public partial class Console : UserControl
    {
        private SynchronizationContext sc;

        public Console()
        {
            InitializeComponent();
            Rcon.CommandExecuted += Rcon_CommandExecuted;

            sc = SynchronizationContext.Current;
        }

        public void Clear()
        {
            sc.Post(state => rtbConsoleLog.Clear(), null);
        }

        void Rcon_CommandExecuted(object sender, CommandExecutedEventArgs e)
        {
            string log = string.Format("Command: {0}{1}Response: {2}", e.Command, Environment.NewLine, e.Response);
            ConsoleLog.Append(log);
            sc.Post((state) => rtbConsoleLog.AppendText(state.ToString() + Environment.NewLine), log);
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxCommand.Text))
            {
                await Rcon.ExecuteCommand(tbxCommand.Text);
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
    }
}
