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
using ArkRcon = ArkRemoteAdmin.SourceRcon.HighLevel.ArkRcon;

namespace ArkRemoteAdmin.UserInterface
{
    partial class WizardAddSchedule : dotNetBase.Windows.Forms.Aero.Wizard
    {
        public Schedule Schedule { get; private set; }

        private readonly WizardPageAddSchedule wizardPage;

        public WizardAddSchedule()
        {
            InitializeComponent();
            Schedule = new Schedule();
            wizardPage = new WizardPageAddSchedule();
            Pages.Add(wizardPage, "Main", null, null);

            loadPage("Main");
        }

        public WizardAddSchedule(Schedule schedule)
            :this()
        {
            this.Schedule = schedule;
            wizardPage.ScheduleName = schedule.Name;
            wizardPage.Message = schedule.Message;
            wizardPage.MessageType = schedule.MessageType;
            wizardPage.Type = schedule.Type;
            wizardPage.TimeSpan = schedule.TimeSpan;

            btnAdd.Text = "Edit";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (currentPage.validatePage())
            {
                Schedule.Name = wizardPage.ScheduleName;
                Schedule.Message = wizardPage.Message;
                Schedule.MessageType = wizardPage.MessageType;
                Schedule.Type = wizardPage.Type;
                Schedule.TimeSpan = wizardPage.TimeSpan;
                Schedule.Active = false;
                Schedule.ServerId = ArkRcon.ConnectedServer.Id;
                Data.Data.Set(Schedule);

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
