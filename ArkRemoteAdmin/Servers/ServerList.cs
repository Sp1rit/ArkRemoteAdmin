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
    public partial class ServerList : Form
    {
        public ServerList()
        {
            InitializeComponent();
        }

        private void ServerList_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if ((new ServerAdd()).ShowDialog() == DialogResult.OK)
            {
                RefreshList();
            }
        }

        private void RefreshList()
        {
            listView.Items.Clear();

            foreach (var server in Data.Data.Servers)
            {
                var item = new ListViewItem(new string[] { server.Name, server.Host, server.Port.ToString() });
                item.Tag = server;

                if (server.Default)
                    item.Font = new Font(item.Font, FontStyle.Bold);

                listView.Items.Add(item);
            }
        }

        private void cmsServer_Opening(object sender, CancelEventArgs e)
        {
            if (listView.SelectedItems.Count != 1)
            {
                e.Cancel = true;
            }
        }

        private void editServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var server = listView.SelectedItems[0].Tag as Server;
            if (server != null)
            {
                if ((new ServerAdd(server)).ShowDialog() == DialogResult.OK)
                {
                    RefreshList();
                }
            }
        }

        private void deleteServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var server = listView.SelectedItems[0].Tag as Server;
            if (server != null)
            {
                if (MessageBox.Show("Do you really want to delete this server?", "Delete Server", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    Data.Data.Remove(server);
                    RefreshList();
                }
            }
        }

        private async void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var server = listView.SelectedItems[0].Tag as Server;
            if (server != null)
            {
                if (await Rcon.Connect(server))
                    this.Close();
            }
        }

        private void setAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedServer = listView.SelectedItems[0].Tag as Server;

            if (selectedServer != null)
            {
                foreach (var server in Data.Data.Servers)
                {
                    if (server == selectedServer)
                        server.Default = true;
                    else
                        server.Default = false;

                    Data.Data.Set(server);
                }
                RefreshList();
            }
        }
    }
}
