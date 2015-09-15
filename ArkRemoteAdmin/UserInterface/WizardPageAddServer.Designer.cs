namespace ArkRemoteAdmin.UserInterface
{
    partial class WizardPageAddServer
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
            this.nudRconPort = new System.Windows.Forms.NumericUpDown();
            this.nudQueryPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxChatName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRconPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQueryPort)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.Controls.Add(this.tbxChatName, 2, 6);
            this.tableLayoutPanel.Controls.Add(this.label6, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.label2, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.label3, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.nudRconPort, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.nudQueryPort, 2, 4);
            this.tableLayoutPanel.Controls.Add(this.label4, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.label5, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.tbxPassword, 2, 5);
            this.tableLayoutPanel.Controls.Add(this.tbxName, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.tbxHost, 2, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 9;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(476, 229);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 20);
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
            this.label2.Location = new System.Drawing.Point(28, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Host/IP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "RCON Port";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudRconPort
            // 
            this.nudRconPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRconPort.Location = new System.Drawing.Point(128, 77);
            this.nudRconPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudRconPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRconPort.Name = "nudRconPort";
            this.nudRconPort.Size = new System.Drawing.Size(55, 21);
            this.nudRconPort.TabIndex = 3;
            this.nudRconPort.Value = new decimal(new int[] {
            27020,
            0,
            0,
            0});
            // 
            // nudQueryPort
            // 
            this.nudQueryPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQueryPort.Location = new System.Drawing.Point(128, 104);
            this.nudQueryPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudQueryPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQueryPort.Name = "nudQueryPort";
            this.nudQueryPort.Size = new System.Drawing.Size(55, 21);
            this.nudQueryPort.TabIndex = 4;
            this.nudQueryPort.Value = new decimal(new int[] {
            27015,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 27);
            this.label4.TabIndex = 7;
            this.label4.Text = "Query Port";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(28, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "Password";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxPassword
            // 
            this.tbxPassword.Location = new System.Drawing.Point(128, 131);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.Size = new System.Drawing.Size(150, 21);
            this.tbxPassword.TabIndex = 5;
            this.tbxPassword.UseSystemPasswordChar = true;
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(128, 23);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(200, 21);
            this.tbxName.TabIndex = 1;
            // 
            // tbxHost
            // 
            this.tbxHost.Location = new System.Drawing.Point(128, 50);
            this.tbxHost.Name = "tbxHost";
            this.tbxHost.Size = new System.Drawing.Size(200, 21);
            this.tbxHost.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(28, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 27);
            this.label6.TabIndex = 9;
            this.label6.Text = "Chat-Name";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxChatName
            // 
            this.tbxChatName.Location = new System.Drawing.Point(128, 158);
            this.tbxChatName.Name = "tbxChatName";
            this.tbxChatName.Size = new System.Drawing.Size(200, 21);
            this.tbxChatName.TabIndex = 10;
            // 
            // WizardPageAddServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "WizardPageAddServer";
            this.pageTitle = "Add a server";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRconPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQueryPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudRconPort;
        private System.Windows.Forms.NumericUpDown nudQueryPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxHost;
        private System.Windows.Forms.TextBox tbxChatName;
        private System.Windows.Forms.Label label6;
    }
}
