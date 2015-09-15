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

namespace ArkRemoteAdmin.UserInterface
{
    partial class WizardAddServer : dotNetBase.Windows.Forms.Aero.Wizard
    {
        private readonly WizardPageAddServer wizardPage;
        private readonly Server server;

        public WizardAddServer()
        {
            InitializeComponent();
            server = new Server();
            wizardPage = new WizardPageAddServer();
            Pages.Add(wizardPage, "Main", null, null);

            loadPage("Main");
        }

        public WizardAddServer(Server server)
            : this()
        {
            this.server = server;
            wizardPage.ServerName = server.Name;
            wizardPage.ServerHost = server.Host;
            wizardPage.RconPort = server.Port;
            wizardPage.QueryPort = server.QueryPort;
            wizardPage.Password = Encryption.DecryptString(server.Password, "0?NRAnRBm;SWd41BUbKsT7)kN1y=RHLm=DR4ZZUBk&!JF3i\"Ra2Eg,8qwhA0^ydo");
            wizardPage.ChatName = server.ChatName;

            btnAdd.Text = "Edit";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (currentPage.validatePage())
            {
                server.Name = wizardPage.ServerName;
                server.Host = wizardPage.ServerHost;
                server.Port = wizardPage.RconPort;
                server.QueryPort = wizardPage.QueryPort;
                server.Password = Encryption.EncryptString(wizardPage.Password, "0?NRAnRBm;SWd41BUbKsT7)kN1y=RHLm=DR4ZZUBk&!JF3i\"Ra2Eg,8qwhA0^ydo");
                server.ChatName = wizardPage.ChatName;
                Data.Data.Set(server);

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
