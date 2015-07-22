namespace ArkRemoteAdmin.UserInterface
{
    partial class WizardPageAddSchedule
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbBroadcast = new System.Windows.Forms.RadioButton();
            this.rdbChat = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.dtpDay = new System.Windows.Forms.DateTimePicker();
            this.rdbDay = new System.Windows.Forms.RadioButton();
            this.dtpHour = new System.Windows.Forms.DateTimePicker();
            this.rdbHour = new System.Windows.Forms.RadioButton();
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.rdbMinute = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.label2, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.label3, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.tbxName, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.panel1, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.richTextBox1, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.grpSchedule, 1, 4);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(476, 229);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 60);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(128, 3);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(200, 21);
            this.tbxName.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbChat);
            this.panel1.Controls.Add(this.rdbBroadcast);
            this.panel1.Location = new System.Drawing.Point(128, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 23);
            this.panel1.TabIndex = 8;
            // 
            // rdbBroadcast
            // 
            this.rdbBroadcast.AutoSize = true;
            this.rdbBroadcast.Checked = true;
            this.rdbBroadcast.Location = new System.Drawing.Point(3, 3);
            this.rdbBroadcast.Name = "rdbBroadcast";
            this.rdbBroadcast.Size = new System.Drawing.Size(73, 17);
            this.rdbBroadcast.TabIndex = 3;
            this.rdbBroadcast.TabStop = true;
            this.rdbBroadcast.Text = "Broadcast";
            this.rdbBroadcast.UseVisualStyleBackColor = true;
            // 
            // rdbChat
            // 
            this.rdbChat.AutoSize = true;
            this.rdbChat.Location = new System.Drawing.Point(82, 3);
            this.rdbChat.Name = "rdbChat";
            this.rdbChat.Size = new System.Drawing.Size(93, 17);
            this.rdbChat.TabIndex = 1;
            this.rdbChat.Text = "Chat Message";
            this.rdbChat.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(128, 30);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(200, 54);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // grpSchedule
            // 
            this.grpSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.grpSchedule, 2);
            this.grpSchedule.Controls.Add(this.dtpDay);
            this.grpSchedule.Controls.Add(this.rdbDay);
            this.grpSchedule.Controls.Add(this.dtpHour);
            this.grpSchedule.Controls.Add(this.rdbHour);
            this.grpSchedule.Controls.Add(this.nudMinutes);
            this.grpSchedule.Controls.Add(this.rdbMinute);
            this.grpSchedule.Location = new System.Drawing.Point(28, 119);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new System.Drawing.Size(300, 98);
            this.grpSchedule.TabIndex = 10;
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
            this.dtpDay.Size = new System.Drawing.Size(69, 21);
            this.dtpDay.TabIndex = 10;
            this.dtpDay.Value = new System.DateTime(2015, 7, 10, 12, 0, 0, 0);
            // 
            // rdbDay
            // 
            this.rdbDay.AutoSize = true;
            this.rdbDay.Location = new System.Drawing.Point(6, 71);
            this.rdbDay.Name = "rdbDay";
            this.rdbDay.Size = new System.Drawing.Size(88, 17);
            this.rdbDay.TabIndex = 9;
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
            this.dtpHour.Size = new System.Drawing.Size(50, 21);
            this.dtpHour.TabIndex = 8;
            this.dtpHour.Value = new System.DateTime(2015, 7, 10, 0, 30, 0, 0);
            // 
            // rdbHour
            // 
            this.rdbHour.AutoSize = true;
            this.rdbHour.Location = new System.Drawing.Point(6, 45);
            this.rdbHour.Name = "rdbHour";
            this.rdbHour.Size = new System.Drawing.Size(92, 17);
            this.rdbHour.TabIndex = 7;
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
            this.nudMinutes.Size = new System.Drawing.Size(41, 21);
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
            this.rdbMinute.Size = new System.Drawing.Size(147, 17);
            this.rdbMinute.TabIndex = 5;
            this.rdbMinute.TabStop = true;
            this.rdbMinute.Text = "Every                   Minutes";
            this.rdbMinute.UseVisualStyleBackColor = true;
            // 
            // WizardPageAddSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "WizardPageAddSchedule";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpSchedule.ResumeLayout(false);
            this.grpSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbChat;
        private System.Windows.Forms.RadioButton rdbBroadcast;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox grpSchedule;
        private System.Windows.Forms.DateTimePicker dtpDay;
        private System.Windows.Forms.RadioButton rdbDay;
        private System.Windows.Forms.DateTimePicker dtpHour;
        private System.Windows.Forms.RadioButton rdbHour;
        private System.Windows.Forms.NumericUpDown nudMinutes;
        private System.Windows.Forms.RadioButton rdbMinute;
    }
}
