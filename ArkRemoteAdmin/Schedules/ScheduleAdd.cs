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
    public partial class ScheduleAdd : Form
    {
        private Schedule schedule;

        public ScheduleAdd()
        {
            InitializeComponent();
            schedule = new Schedule();
        }

        public ScheduleAdd(Schedule schedule)
            : this()
        {
            this.schedule = schedule;
            tbxName.Text = schedule.Name;
            rtbMessage.Text = schedule.Message;
            if (schedule.Type == ScheduleType.Minute)
            {
                rdbMinute.Checked = true;
                nudMinutes.Value = schedule.TimeSpan.Minutes;
            }
            else if (schedule.Type == ScheduleType.Hourly)
            {
                rdbHour.Checked = true;
                dtpHour.Value = new DateTime(1753, 1, 1, 0, schedule.TimeSpan.Minutes, schedule.TimeSpan.Seconds);
            }
            else if (schedule.Type == ScheduleType.Daily)
            {
                rdbDay.Checked = true;
                dtpHour.Value = new DateTime(1753, 1, 1, schedule.TimeSpan.Hours, schedule.TimeSpan.Minutes, schedule.TimeSpan.Seconds);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            schedule.ServerId = Rcon.Server.Id;
            schedule.Name = tbxName.Text;
            schedule.Message = rtbMessage.Text;
            schedule.Active = false;
            if (rdbMinute.Checked)
            {
                schedule.Type = ScheduleType.Minute;
                schedule.TimeSpan = new TimeSpan(0, (int) nudMinutes.Value, 0);
            }
            else if (rdbHour.Checked)
            {
                schedule.Type = ScheduleType.Hourly;
                schedule.TimeSpan = new TimeSpan(0, dtpHour.Value.Minute, dtpHour.Value.Second);
            }
            else if (rdbDay.Checked)
            {
                schedule.Type = ScheduleType.Daily;
                schedule.TimeSpan = new TimeSpan(dtpDay.Value.Hour, dtpDay.Value.Minute, dtpDay.Value.Second);
            }

            Data.Data.Set(schedule);
            DialogResult = DialogResult.OK;

            Close();
        }
    }
}
