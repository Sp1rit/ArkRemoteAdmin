namespace ArkRemoteAdmin
{
    partial class ScheduleAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleAdd));
            this.lblNachricht = new System.Windows.Forms.Label();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.dtpDay = new System.Windows.Forms.DateTimePicker();
            this.rdbDay = new System.Windows.Forms.RadioButton();
            this.dtpHour = new System.Windows.Forms.DateTimePicker();
            this.rdbHour = new System.Windows.Forms.RadioButton();
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.rdbMinute = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNachricht
            // 
            this.lblNachricht.AutoSize = true;
            this.lblNachricht.Location = new System.Drawing.Point(12, 41);
            this.lblNachricht.Name = "lblNachricht";
            this.lblNachricht.Size = new System.Drawing.Size(56, 13);
            this.lblNachricht.TabIndex = 0;
            this.lblNachricht.Text = "Nachricht:";
            // 
            // rtbMessage
            // 
            this.rtbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbMessage.Location = new System.Drawing.Point(74, 38);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(266, 100);
            this.rtbMessage.TabIndex = 2;
            this.rtbMessage.Text = "";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // tbxName
            // 
            this.tbxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxName.Location = new System.Drawing.Point(74, 12);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(266, 20);
            this.tbxName.TabIndex = 1;
            // 
            // grpSchedule
            // 
            this.grpSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSchedule.Controls.Add(this.dtpDay);
            this.grpSchedule.Controls.Add(this.rdbDay);
            this.grpSchedule.Controls.Add(this.dtpHour);
            this.grpSchedule.Controls.Add(this.rdbHour);
            this.grpSchedule.Controls.Add(this.nudMinutes);
            this.grpSchedule.Controls.Add(this.rdbMinute);
            this.grpSchedule.Location = new System.Drawing.Point(12, 144);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new System.Drawing.Size(328, 98);
            this.grpSchedule.TabIndex = 4;
            this.grpSchedule.TabStop = false;
            this.grpSchedule.Text = "Schedule";
            // 
            // dtpDay
            // 
            this.dtpDay.CustomFormat = "HH:mm:ss";
            this.dtpDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDay.Location = new System.Drawing.Point(102, 68);
            this.dtpDay.Name = "dtpDay";
            this.dtpDay.ShowUpDown = true;
            this.dtpDay.Size = new System.Drawing.Size(69, 20);
            this.dtpDay.TabIndex = 8;
            this.dtpDay.Value = new System.DateTime(2015, 7, 10, 12, 0, 0, 0);
            // 
            // rdbDay
            // 
            this.rdbDay.AutoSize = true;
            this.rdbDay.Location = new System.Drawing.Point(6, 71);
            this.rdbDay.Name = "rdbDay";
            this.rdbDay.Size = new System.Drawing.Size(86, 17);
            this.rdbDay.TabIndex = 5;
            this.rdbDay.TabStop = true;
            this.rdbDay.Text = "Every Day at";
            this.rdbDay.UseVisualStyleBackColor = true;
            // 
            // dtpHour
            // 
            this.dtpHour.CustomFormat = "mm:ss";
            this.dtpHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHour.Location = new System.Drawing.Point(102, 45);
            this.dtpHour.Name = "dtpHour";
            this.dtpHour.ShowUpDown = true;
            this.dtpHour.Size = new System.Drawing.Size(50, 20);
            this.dtpHour.TabIndex = 7;
            this.dtpHour.Value = new System.DateTime(2015, 7, 10, 0, 30, 0, 0);
            // 
            // rdbHour
            // 
            this.rdbHour.AutoSize = true;
            this.rdbHour.Location = new System.Drawing.Point(6, 45);
            this.rdbHour.Name = "rdbHour";
            this.rdbHour.Size = new System.Drawing.Size(90, 17);
            this.rdbHour.TabIndex = 4;
            this.rdbHour.TabStop = true;
            this.rdbHour.Text = "Every Hour at";
            this.rdbHour.UseVisualStyleBackColor = true;
            // 
            // nudMinutes
            // 
            this.nudMinutes.Location = new System.Drawing.Point(61, 19);
            this.nudMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinutes.Name = "nudMinutes";
            this.nudMinutes.Size = new System.Drawing.Size(41, 20);
            this.nudMinutes.TabIndex = 6;
            this.nudMinutes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // rdbMinute
            // 
            this.rdbMinute.AutoSize = true;
            this.rdbMinute.Checked = true;
            this.rdbMinute.Location = new System.Drawing.Point(6, 19);
            this.rdbMinute.Name = "rdbMinute";
            this.rdbMinute.Size = new System.Drawing.Size(146, 17);
            this.rdbMinute.TabIndex = 3;
            this.rdbMinute.TabStop = true;
            this.rdbMinute.Text = "Every                   Minutes";
            this.rdbMinute.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 251);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ScheduleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 286);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpSchedule);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.rtbMessage);
            this.Controls.Add(this.lblNachricht);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScheduleAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Schedule";
            this.grpSchedule.ResumeLayout(false);
            this.grpSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNachricht;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.GroupBox grpSchedule;
        private System.Windows.Forms.DateTimePicker dtpHour;
        private System.Windows.Forms.RadioButton rdbHour;
        private System.Windows.Forms.NumericUpDown nudMinutes;
        private System.Windows.Forms.RadioButton rdbMinute;
        private System.Windows.Forms.DateTimePicker dtpDay;
        private System.Windows.Forms.RadioButton rdbDay;
        private System.Windows.Forms.Button btnSave;
    }
}