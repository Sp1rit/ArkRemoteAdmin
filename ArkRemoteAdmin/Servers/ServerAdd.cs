using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;

namespace ArkRemoteAdmin
{
    public partial class ServerAdd : Form
    {
        private Server server;

        public ServerAdd()
        {
            InitializeComponent();
            server = new Server();
        }

        public ServerAdd(Server server)
            : this()
        {
            this.server = server;
            tbxName.Text = server.Name;
            tbxHost.Text = server.Host;
            nudPort.Value = server.Port;
            tbxPassword.Text = Encryption.DecryptString(server.Password, "0?NRAnRBm;SWd41BUbKsT7)kN1y=RHLm=DR4ZZUBk&!JF3i\"Ra2Eg,8qwhA0^ydo");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            server.Name = tbxName.Text;
            server.Host = tbxHost.Text;
            server.Port = (int)nudPort.Value;
            server.Password = Encryption.EncryptString(tbxPassword.Text, "0?NRAnRBm;SWd41BUbKsT7)kN1y=RHLm=DR4ZZUBk&!JF3i\"Ra2Eg,8qwhA0^ydo");

            Data.Data.Set(server);
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            if (await TestConnection())
                btnAdd.Enabled = true;
        }

        private async Task<bool> TestConnection()
        {
            try
            {
                RconSharp.Net45.RconSocket socket = new RconSharp.Net45.RconSocket();
                RconSharp.RconMessenger messenger = new RconSharp.RconMessenger(socket);

                if (await messenger.ConnectAsync(tbxHost.Text, (int)nudPort.Value))
                {
                    if (await messenger.AuthenticateAsync(tbxPassword.Text))
                    {
                        messenger.CloseConnection();
                        return true;
                    }
                    else
                        MessageBox.Show("Could not authenticate to the server.");
                }
                else
                    MessageBox.Show("Could not connect to the server.");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not connecto to the server.");
            }

            return false;
        }
    }
}
