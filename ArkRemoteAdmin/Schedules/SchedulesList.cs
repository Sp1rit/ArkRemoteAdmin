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

namespace ArkRemoteAdmin
{
    public partial class SchedulesList : UserControl
    {
        public SchedulesList()
        {
            InitializeComponent();
        }

        private void SchedulesList_Load(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            (new ScheduleAdd()).ShowDialog();

            LoadSchedules();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (lvSchedules.SelectedItems.Count == 1)
            {
                var schedule = lvSchedules.SelectedItems[0].Tag as Schedule;

                if (schedule != null)
                {
                    (new ScheduleAdd(schedule)).ShowDialog();

                    LoadSchedules();
                }
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (lvSchedules.SelectedItems.Count == 1)
            {
                var schedule = lvSchedules.SelectedItems[0].Tag as Schedule;

                if (schedule != null)
                {
                    if (MessageBox.Show("Do you really want to delete this scheduled broadcast?", "Delete Broadcast", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Data.Data.Remove(schedule);
                        LoadSchedules();
                    }
                }
            }
        }

        private void tsbActivate_Click(object sender, EventArgs e)
        {
            if (lvSchedules.SelectedItems.Count == 1)
            {
                var schedule = lvSchedules.SelectedItems[0].Tag as Schedule;

                if (schedule != null)
                {
                    Scheduler.Instance.Activate(schedule);
                    SetMenus();
                }
            }
        }

        private void tsbDeactivate_Click(object sender, EventArgs e)
        {
            if (lvSchedules.SelectedItems.Count == 1)
            {
                var schedule = lvSchedules.SelectedItems[0].Tag as Schedule;

                if (schedule != null)
                {
                    Scheduler.Instance.Deactivate(schedule);
                    SetMenus();
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (lvSchedules.SelectedItems.Count == 1)
            {
                var schedule = lvSchedules.SelectedItems[0].Tag as Schedule;

                if (schedule != null)
                {
                    editBroadcastToolStripMenuItem.Visible = !schedule.Active;
                    deleteBroadcastToolStripMenuItem.Visible = !schedule.Active;
                    toolStripSeparator1.Visible = !schedule.Active;
                    activateToolStripMenuItem.Visible = !schedule.Active;
                    deactivateToolStripMenuItem.Visible = schedule.Active;

                    return;
                }
            }

            e.Cancel = true;
        }

        private void lvSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMenus();
        }

        private void SetMenus()
        {
            Schedule schedule = null;

            if (lvSchedules.SelectedItems.Count == 1)
                schedule = lvSchedules.SelectedItems[0].Tag as Schedule;

            tsSeparator1.Visible = schedule != null;
            tsbEdit.Visible = schedule != null && !schedule.Active;
            tsbDelete.Visible = schedule != null && !schedule.Active;
            tsSeparator2.Visible = schedule != null && !schedule.Active;
            tsbActivate.Visible = schedule != null && !schedule.Active;
            tsbDeactivate.Visible = schedule != null && schedule.Active;
        }

        public void LoadSchedules()
        {
            lvSchedules.Items.Clear();

            foreach (var schedule in Data.Data.Schedules.Where(s => s.ServerId == Rcon.Server.Id))
                lvSchedules.Items.Add(new ListViewItem(new string[] { schedule.Server.Name, schedule.Name, schedule.Message, schedule.Type.ToString() }) { Tag = schedule });

            SetMenus();
        }

        public void ActivateSchedules()
        {
            foreach (var schedule in Data.Data.Schedules.Where(s => s.ServerId == Rcon.Server.Id))
            {
                if (schedule.Active)
                    Scheduler.Instance.Activate(schedule);
            }
        }
    }
}
