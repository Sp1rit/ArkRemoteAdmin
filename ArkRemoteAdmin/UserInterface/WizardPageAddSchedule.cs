using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;

namespace ArkRemoteAdmin.UserInterface
{
    public partial class WizardPageAddSchedule : dotNetBase.Windows.Forms.Aero.wizardPage
    {
        public string ScheduleName
        {
            get { return tbxName.Text; }
            set { tbxName.Text = value; }
        }
        public string Message
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }

        public MessageType MessageType
        {
            get { return rdbBroadcast.Checked ? MessageType.BroadCast :  MessageType.Chat; }
            set
            {
                if (value == MessageType.BroadCast) rdbBroadcast.Checked = true;
                else rdbChat.Checked = true;
            }
        }

        public ScheduleType Type
        {
            get { return rdbMinute.Checked ? ScheduleType.Minute : rdbHour.Checked ? ScheduleType.Hourly : ScheduleType.Daily; }
            set
            {
                if (value == ScheduleType.Minute) rdbMinute.Checked = true;
                else if (value == ScheduleType.Hourly) rdbHour.Checked = true;
                else rdbDay.Checked = true;
            }
        }

        public TimeSpan TimeSpan
        {
            get
            {
                if (rdbMinute.Checked) return new TimeSpan(0, (int) nudMinutes.Value, 0);
                else if (rdbHour.Checked) return new TimeSpan(0, dtpHour.Value.Minute, dtpHour.Value.Second);
                else return new TimeSpan(dtpDay.Value.Hour, dtpDay.Value.Minute, dtpDay.Value.Second);
            }
            set
            {
                if (rdbMinute.Checked) nudMinutes.Value = value.Minutes;
                else if (rdbHour.Checked) dtpHour.Value = new DateTime(1753, 1, 1, 0, value.Minutes, value.Seconds);
                else dtpHour.Value = new DateTime(1753, 1, 1, value.Hours, value.Minutes, value.Seconds);
            }
        }

        public WizardPageAddSchedule()
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

            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                Host.showBorderMessage(Properties.Resources.exclamation, "Please enter a message.");
                return false;
            }

            return true;
        }
    }
}
