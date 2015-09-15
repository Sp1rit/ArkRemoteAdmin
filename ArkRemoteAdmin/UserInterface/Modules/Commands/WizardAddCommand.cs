using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.UserInterface
{
    public partial class WizardAddCommand : dotNetBase.Windows.Forms.Aero.Wizard
    {
        private WizardPageAddTrigger pageTriggers;

        public WizardAddCommand()
        {
            InitializeComponent();
            pageTriggers = new WizardPageAddTrigger();
            Pages.Add(pageTriggers, "Triggers", null, null);

            loadPage("Triggers");
        }
    }
}
