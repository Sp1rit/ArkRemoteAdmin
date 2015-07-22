using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;
using BssFramework.Windows.Forms;

namespace ArkRemoteAdmin.UserInterface.Modules
{
    public partial class SchedulesList : UserControl
    {
        private SynchronizationContext syncContext;

        public SchedulesList()
        {
            InitializeComponent();
            syncContext = SynchronizationContext.Current;
            toolStrip.Renderer = new SevenToolStripRenderer();
            cmsSchedule.Renderer = new SevenToolStripRenderer();

            Rcon.Connected += Rcon_Connected;
            Rcon.Disconnected += Rcon_Disconnected;
        }

        private void Rcon_Connected(object sender, EventArgs e)
        {
            LoadSchedules();
            ActivateSchedules();
        }

        private void Rcon_Disconnected(object sender, EventArgs e)
        {
            Scheduler.Instance.DeactivateAll();
            syncContext.Send(state => lvSchedules.Items.Clear(), null);
        }

        private void lvSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            Schedule schedule = GetSelectedSchedule();

            tsbRun.Visible = schedule != null;
            tsbEdit.Visible = schedule != null;
            tsbDelete.Visible = schedule != null;
            tsbActivate.Visible = schedule != null && !schedule.Active;
            tsbDeactivate.Visible = schedule != null && schedule.Active;
            tsSeparator1.Visible = schedule != null;
            tsSeparator2.Visible = schedule != null;
            toolStripSeparator3.Visible = schedule != null;

            runNowToolStripMenuItem.Visible = schedule != null;
            editBroadcastToolStripMenuItem.Visible = schedule != null;
            deleteBroadcastToolStripMenuItem.Visible = schedule != null;
            activateToolStripMenuItem.Visible = schedule != null && !schedule.Active;
            deactivateToolStripMenuItem.Visible = schedule != null && schedule.Active;
            toolStripSeparator1.Visible = schedule != null;
            toolStripSeparator2.Visible = schedule != null;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Schedule schedule = GetSelectedSchedule();

            if (schedule == null)
                e.Cancel = true;
        }

        #region Menu Commands

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            (new WizardAddSchedule()).ShowDialog();

            LoadSchedules();
        }

        private async void tsbRun_Click(object sender, EventArgs e)
        {
            Schedule schedule = GetSelectedSchedule();

            if (schedule != null)
            {
                if (schedule.MessageType == MessageType.Chat)
                    await Rcon.SendChatMessage(schedule.Message);
                else
                    await Rcon.Broadcast(schedule.Message);
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            Schedule schedule = GetSelectedSchedule();

            if (schedule != null)
            {
                (new WizardAddSchedule(schedule)).ShowDialog();

                LoadSchedules();
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            Schedule schedule = GetSelectedSchedule();

            if (schedule != null)
            {
                if (MessageBox.Show("Do you really want to delete this scheduled broadcast?", "Delete Broadcast", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Data.Data.Remove(schedule);
                    LoadSchedules();
                }
            }
        }

        private void tsbActivate_Click(object sender, EventArgs e)
        {
            Schedule schedule = GetSelectedSchedule();

            if (schedule != null)
            {
                Scheduler.Instance.Activate(schedule);
                LoadSchedules();
            }
        }

        private void tsbDeactivate_Click(object sender, EventArgs e)
        {
            Schedule schedule = GetSelectedSchedule();

            if (schedule != null)
            {
                Scheduler.Instance.Deactivate(schedule);
                LoadSchedules();
            }
        }

        #endregion // Menu Commands

        #region Internal Methods

        private Schedule GetSelectedSchedule()
        {
            if (lvSchedules.SelectedItems.Count > 0)
                return lvSchedules.SelectedItems[0].Tag as Schedule;

            return null;
        }

        private void LoadSchedules()
        {
            lvSchedules.Items.Clear();

            foreach (Schedule schedule in Data.Data.Schedules.Where(s => s.ServerId == Rcon.Server.Id))
                lvSchedules.Items.Add(new ListViewItem(new[] { schedule.Server.Name, schedule.Name, schedule.Message, schedule.Type.ToString() }) { Tag = schedule, ImageIndex = schedule.Active ? 0 : -1 });

            lvSchedules_SelectedIndexChanged(this, new EventArgs());
        }

        private void ActivateSchedules()
        {
            foreach (Schedule schedule in Data.Data.Schedules.Where(s => s.ServerId == Rcon.Server.Id))
            {
                if (schedule.Active)
                    Scheduler.Instance.Activate(schedule);
            }
        }

        #endregion // Internal Methods
    }
}
