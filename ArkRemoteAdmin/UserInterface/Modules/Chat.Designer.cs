namespace ArkRemoteAdmin.UserInterface.Modules
{
    partial class Chat
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
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.ckbGetChat = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbxChatMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(3, 3);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(569, 335);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(3, 393);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ckbGetChat
            // 
            this.ckbGetChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbGetChat.AutoSize = true;
            this.ckbGetChat.Checked = true;
            this.ckbGetChat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbGetChat.Location = new System.Drawing.Point(3, 344);
            this.ckbGetChat.Name = "ckbGetChat";
            this.ckbGetChat.Size = new System.Drawing.Size(216, 17);
            this.ckbGetChat.TabIndex = 5;
            this.ckbGetChat.Text = "Get new chat messages from the server.";
            this.ckbGetChat.UseVisualStyleBackColor = true;
            this.ckbGetChat.CheckedChanged += new System.EventHandler(this.ckbGetChat_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Receiver:";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.DisplayMember = "Key";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Everyone"});
            this.comboBox1.Location = new System.Drawing.Point(143, 395);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.ValueMember = "Value";
            // 
            // tbxChatMessage
            // 
            this.tbxChatMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxChatMessage.Location = new System.Drawing.Point(3, 367);
            this.tbxChatMessage.Name = "tbxChatMessage";
            this.tbxChatMessage.Size = new System.Drawing.Size(569, 20);
            this.tbxChatMessage.TabIndex = 9;
            this.tbxChatMessage.TextChanged += new System.EventHandler(this.tbxChatMessage_TextChanged);
            this.tbxChatMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxChatMessage_KeyDown);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxChatMessage);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckbGetChat);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtbLog);
            this.Name = "Chat";
            this.Size = new System.Drawing.Size(575, 419);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox ckbGetChat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox tbxChatMessage;
    }
}
