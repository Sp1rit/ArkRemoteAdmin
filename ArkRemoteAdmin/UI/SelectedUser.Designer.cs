namespace ArkRemoteAdmin.UI
{
    partial class SelectedUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedUser));
            this.lblSelected = new BssFramework.Windows.Forms.SeperatorLabel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblVacValue = new System.Windows.Forms.Label();
            this.lblVac = new System.Windows.Forms.Label();
            this.lnkProfile = new System.Windows.Forms.LinkLabel();
            this.lblSteamId = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnMessage = new BssFramework.Windows.Forms.AeroButton();
            this.btnAddWhitelist = new BssFramework.Windows.Forms.AeroButton();
            this.btnBan = new BssFramework.Windows.Forms.AeroButton();
            this.btnKick = new BssFramework.Windows.Forms.AeroButton();
            this.pbxAvatar = new System.Windows.Forms.PictureBox();
            this.lblSteamIdValue = new System.Windows.Forms.Label();
            this.btnRemoveWhitelist = new BssFramework.Windows.Forms.AeroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.lblSelected.Location = new System.Drawing.Point(3, 0);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(657, 23);
            this.lblSelected.TabIndex = 6;
            this.lblSelected.Text = "Selected User";
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Goldenrod;
            this.label15.Location = new System.Drawing.Point(360, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "370";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Green;
            this.label14.Location = new System.Drawing.Point(360, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(240, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Number of GameBans:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(240, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Days since last Ban:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(360, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(240, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Number of VACBans:";
            // 
            // lblVacValue
            // 
            this.lblVacValue.AutoSize = true;
            this.lblVacValue.ForeColor = System.Drawing.Color.Red;
            this.lblVacValue.Location = new System.Drawing.Point(360, 26);
            this.lblVacValue.Name = "lblVacValue";
            this.lblVacValue.Size = new System.Drawing.Size(25, 13);
            this.lblVacValue.TabIndex = 32;
            this.lblVacValue.Text = "Yes";
            // 
            // lblVac
            // 
            this.lblVac.AutoSize = true;
            this.lblVac.Location = new System.Drawing.Point(240, 26);
            this.lblVac.Name = "lblVac";
            this.lblVac.Size = new System.Drawing.Size(71, 13);
            this.lblVac.TabIndex = 31;
            this.lblVac.Text = "VAC-Banned:";
            // 
            // lnkProfile
            // 
            this.lnkProfile.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkProfile.AutoSize = true;
            this.lnkProfile.Location = new System.Drawing.Point(76, 74);
            this.lnkProfile.Name = "lnkProfile";
            this.lnkProfile.Size = new System.Drawing.Size(98, 13);
            this.lnkProfile.TabIndex = 30;
            this.lnkProfile.TabStop = true;
            this.lnkProfile.Text = "Open Steam Profile";
            this.lnkProfile.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkProfile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProfile_LinkClicked);
            // 
            // lblSteamId
            // 
            this.lblSteamId.AutoSize = true;
            this.lblSteamId.Location = new System.Drawing.Point(77, 53);
            this.lblSteamId.Name = "lblSteamId";
            this.lblSteamId.Size = new System.Drawing.Size(49, 13);
            this.lblSteamId.TabIndex = 29;
            this.lblSteamId.Text = "SteamId:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(76, 26);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(89, 19);
            this.lblName.TabIndex = 28;
            this.lblName.Text = "[xU] .$pIrit";
            // 
            // btnMessage
            // 
            this.btnMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMessage.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnMessage.Image = ((System.Drawing.Image)(resources.GetObject("btnMessage.Image")));
            this.btnMessage.Location = new System.Drawing.Point(485, 60);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(130, 28);
            this.btnMessage.TabIndex = 42;
            this.btnMessage.Text = "Send Message";
            this.btnMessage.UseVisualStyleBackColor = true;
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            // 
            // btnAddWhitelist
            // 
            this.btnAddWhitelist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddWhitelist.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnAddWhitelist.Image = ((System.Drawing.Image)(resources.GetObject("btnAddWhitelist.Image")));
            this.btnAddWhitelist.Location = new System.Drawing.Point(485, 26);
            this.btnAddWhitelist.Name = "btnAddWhitelist";
            this.btnAddWhitelist.Size = new System.Drawing.Size(175, 28);
            this.btnAddWhitelist.TabIndex = 41;
            this.btnAddWhitelist.Text = "Add to Whitelist";
            this.btnAddWhitelist.UseVisualStyleBackColor = true;
            this.btnAddWhitelist.Click += new System.EventHandler(this.btnAddWhitelist_Click);
            // 
            // btnBan
            // 
            this.btnBan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBan.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnBan.Image = ((System.Drawing.Image)(resources.GetObject("btnBan.Image")));
            this.btnBan.Location = new System.Drawing.Point(409, 60);
            this.btnBan.Name = "btnBan";
            this.btnBan.Size = new System.Drawing.Size(70, 28);
            this.btnBan.TabIndex = 40;
            this.btnBan.Text = "Ban";
            this.btnBan.UseVisualStyleBackColor = true;
            this.btnBan.Click += new System.EventHandler(this.btnBan_Click);
            // 
            // btnKick
            // 
            this.btnKick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKick.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnKick.Image = ((System.Drawing.Image)(resources.GetObject("btnKick.Image")));
            this.btnKick.Location = new System.Drawing.Point(409, 26);
            this.btnKick.Name = "btnKick";
            this.btnKick.Size = new System.Drawing.Size(70, 28);
            this.btnKick.TabIndex = 39;
            this.btnKick.Text = "Kick";
            this.btnKick.UseVisualStyleBackColor = true;
            this.btnKick.Click += new System.EventHandler(this.btnKick_Click);
            // 
            // pbxAvatar
            // 
            this.pbxAvatar.ImageLocation = "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/19/190638a35" +
    "8151afd2fb88d49750cdea849d121e7_medium.jpg";
            this.pbxAvatar.Location = new System.Drawing.Point(6, 26);
            this.pbxAvatar.Name = "pbxAvatar";
            this.pbxAvatar.Size = new System.Drawing.Size(64, 64);
            this.pbxAvatar.TabIndex = 7;
            this.pbxAvatar.TabStop = false;
            // 
            // lblSteamIdValue
            // 
            this.lblSteamIdValue.AutoSize = true;
            this.lblSteamIdValue.Location = new System.Drawing.Point(123, 53);
            this.lblSteamIdValue.Name = "lblSteamIdValue";
            this.lblSteamIdValue.Size = new System.Drawing.Size(109, 13);
            this.lblSteamIdValue.TabIndex = 43;
            this.lblSteamIdValue.Text = "76561197992236997";
            // 
            // btnRemoveWhitelist
            // 
            this.btnRemoveWhitelist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveWhitelist.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnRemoveWhitelist.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveWhitelist.Image")));
            this.btnRemoveWhitelist.Location = new System.Drawing.Point(485, 26);
            this.btnRemoveWhitelist.Name = "btnRemoveWhitelist";
            this.btnRemoveWhitelist.Size = new System.Drawing.Size(175, 28);
            this.btnRemoveWhitelist.TabIndex = 44;
            this.btnRemoveWhitelist.Text = "Remove from Whitelist";
            this.btnRemoveWhitelist.UseVisualStyleBackColor = true;
            this.btnRemoveWhitelist.Visible = false;
            this.btnRemoveWhitelist.Click += new System.EventHandler(this.btnRemoveWhitelist_Click);
            // 
            // SelectedUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnRemoveWhitelist);
            this.Controls.Add(this.lblSteamIdValue);
            this.Controls.Add(this.btnMessage);
            this.Controls.Add(this.btnAddWhitelist);
            this.Controls.Add(this.btnBan);
            this.Controls.Add(this.btnKick);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblVacValue);
            this.Controls.Add(this.lblVac);
            this.Controls.Add(this.lnkProfile);
            this.Controls.Add(this.lblSteamId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbxAvatar);
            this.Controls.Add(this.lblSelected);
            this.Name = "SelectedUser";
            this.Size = new System.Drawing.Size(663, 95);
            ((System.ComponentModel.ISupportInitialize)(this.pbxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BssFramework.Windows.Forms.SeperatorLabel lblSelected;
        private System.Windows.Forms.PictureBox pbxAvatar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblVacValue;
        private System.Windows.Forms.Label lblVac;
        private System.Windows.Forms.LinkLabel lnkProfile;
        private System.Windows.Forms.Label lblSteamId;
        private System.Windows.Forms.Label lblName;
        private BssFramework.Windows.Forms.AeroButton btnKick;
        private BssFramework.Windows.Forms.AeroButton btnBan;
        private BssFramework.Windows.Forms.AeroButton btnAddWhitelist;
        private BssFramework.Windows.Forms.AeroButton btnMessage;
        private System.Windows.Forms.Label lblSteamIdValue;
        private BssFramework.Windows.Forms.AeroButton btnRemoveWhitelist;
    }
}
