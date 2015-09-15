using System;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;

namespace ArkRemoteAdmin.UserInterface
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Settings.General.AllowMultipleInstances;
            nudChatInterval.Value = Settings.Server.GetChatInterval.Clamp<int>((int)nudChatInterval.Minimum, (int)nudChatInterval.Maximum);
            nudPlayerInterval.Value = Settings.Server.PlayerRefreshInterval.Clamp<int>((int)nudPlayerInterval.Minimum, (int)nudPlayerInterval.Maximum);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.General.AllowMultipleInstances = checkBox1.Checked;
            Settings.Server.GetChatInterval = (int)nudChatInterval.Value;
            Settings.Server.PlayerRefreshInterval = (int)nudPlayerInterval.Value;
            Settings.Save();

            Close();
        }

        private void tvMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
            {
                switch (e.Node.Name)
                {
                    case "nodeGeneral":
                        tbCtrlPages.SelectedTab = tbPgGeneral;
                        break;
                    case "nodeServer":
                        tbCtrlPages.SelectedTab = tbPgServer;
                        break;
                }
            }
        }

    }
}
