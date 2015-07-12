using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin
{
    public partial class Chat : UserControl
    {
        public Chat()
        {
            InitializeComponent();
        }

        public void StartChat()
        {
            timerChat.Start();
        }

        public void StopChat()
        {
            timerChat.Stop();
            rtbChatLog.Clear();
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
                ChatLog.Append(chatLog);
                rtbChatLog.AppendText(chatLog + Environment.NewLine);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                bool done = await Rcon.SendChatMessage(textBox1.Text);

                if (done)
                    textBox1.Clear();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
