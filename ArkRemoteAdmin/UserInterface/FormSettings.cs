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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.General.AllowMultipleInstances = checkBox1.Checked;
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
