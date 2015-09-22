using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.UserInterface
{
    public partial class WizardPageAddServer : dotNetBase.Windows.Forms.Aero.wizardPage
    {
        public string ServerName
        {
            get { return tbxName.Text; }
            set { tbxName.Text = value; }
        }
        public string ServerHost
        {
            get { return tbxHost.Text; }
            set { tbxHost.Text = value; }
        }
        public int RconPort
        {
            get { return (int)nudRconPort.Value; }
            set { nudRconPort.Value = value >= nudRconPort.Minimum && value <= nudRconPort.Maximum ? value : 27020; }
        }
        public int QueryPort
        {
            get { return (int)nudQueryPort.Value; }
            set { nudQueryPort.Value = value >= nudQueryPort.Minimum && value <= nudQueryPort.Maximum ? value : 27015; }
        }
        public string Password
        {
            get { return tbxPassword.Text; }
            set { tbxPassword.Text = value; }
        }
        public string ChatName
        {
            get { return tbxChatName.Text; }
            set { tbxChatName.Text = value; }
        }

        public WizardPageAddServer()
        {
            InitializeComponent();
        }

        public override bool validatePage()
        {
            if (string.IsNullOrEmpty(tbxName.Text))
            {
                Host.showBorderMessage(Properties.Resources.exclamation, "Please enter a name.");
                return false;
            }

            if (string.IsNullOrEmpty(tbxHost.Text))
            {
                Host.showBorderMessage(Properties.Resources.exclamation, "Please enter a host.");
                return false;
            }

            if (string.IsNullOrEmpty(tbxPassword.Text))
            {
                Host.showBorderMessage(Properties.Resources.exclamation, "Please enter a password.");
                return false;
            }

            Host.showBorderMessage(Properties.Resources.hourglass, "Testing connection. Please wait ...");
            Host.setTaskBarProgressState(dotNetBase.Windows.Forms.thumbnailProgressState.Indeterminate);

            Rcon.RconBase client = new Rcon.RconBase();
            try
            {
                if (!client.Connect(tbxHost.Text, (int)nudRconPort.Value))
                    throw new Exception("Connection to server failed. Check your host and port.");

                if (!client.Authenticate(tbxPassword.Text))
                    throw new Exception("Authentication with server failed. The password is incorrect.");

                if (!client.Connected)
                    return false;
            }
            catch (Exception ex)
            {
                Host.showBorderMessage(Properties.Resources.exclamation, ex.Message);
                Host.setTaskBarProgressState(dotNetBase.Windows.Forms.thumbnailProgressState.NoProgress);
                return false;
            }
            finally
            {
                if (client.Connected)
                    client.Disconnect();
            }

            Host.hideBorderMessage();
            Host.setTaskBarProgressState(dotNetBase.Windows.Forms.thumbnailProgressState.NoProgress);
            return true;
        }
    }
}
