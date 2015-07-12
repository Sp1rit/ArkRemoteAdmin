namespace ArkRemoteAdmin
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.lnkLblWebsite = new System.Windows.Forms.LinkLabel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lnkLblIcons = new System.Windows.Forms.LinkLabel();
            this.lblIcons = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lnkLblWebsite
            // 
            this.lnkLblWebsite.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
            this.lnkLblWebsite.AutoSize = true;
            this.lnkLblWebsite.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblWebsite.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.lnkLblWebsite.Location = new System.Drawing.Point(13, 105);
            this.lnkLblWebsite.Name = "lnkLblWebsite";
            this.lnkLblWebsite.Size = new System.Drawing.Size(91, 13);
            this.lnkLblWebsite.TabIndex = 16;
            this.lnkLblWebsite.TabStop = true;
            this.lnkLblWebsite.Tag = "http://xunion.net";
            this.lnkLblWebsite.Text = "http://xunion.net";
            this.lnkLblWebsite.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
            this.lnkLblWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Location = new System.Drawing.Point(13, 70);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(126, 26);
            this.lblCopyright.TabIndex = 15;
            this.lblCopyright.Text = "Copyright\r\nAlle Rechte vorbehalten.";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(13, 45);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(55, 13);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "Version: ";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(183, 24);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "ARK Remote Admin";
            // 
            // lnkLblIcons
            // 
            this.lnkLblIcons.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
            this.lnkLblIcons.AutoSize = true;
            this.lnkLblIcons.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblIcons.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.lnkLblIcons.Location = new System.Drawing.Point(113, 136);
            this.lnkLblIcons.Name = "lnkLblIcons";
            this.lnkLblIcons.Size = new System.Drawing.Size(65, 13);
            this.lnkLblIcons.TabIndex = 24;
            this.lnkLblIcons.TabStop = true;
            this.lnkLblIcons.Tag = "http://www.famfamfam.com/lab/icons/silk/";
            this.lnkLblIcons.Text = "Silk Icon Set";
            this.lnkLblIcons.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
            this.lnkLblIcons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // lblIcons
            // 
            this.lblIcons.AutoSize = true;
            this.lblIcons.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcons.Location = new System.Drawing.Point(14, 136);
            this.lblIcons.Name = "lblIcons";
            this.lblIcons.Size = new System.Drawing.Size(41, 13);
            this.lblIcons.TabIndex = 23;
            this.lblIcons.Text = "Icons:";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 179);
            this.Controls.Add(this.lnkLblIcons);
            this.Controls.Add(this.lblIcons);
            this.Controls.Add(this.lnkLblWebsite);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Info über";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.LinkLabel lnkLblWebsite;
        internal System.Windows.Forms.Label lblCopyright;
        internal System.Windows.Forms.Label lblVersion;
        internal System.Windows.Forms.Label lblName;
        internal System.Windows.Forms.LinkLabel lnkLblIcons;
        internal System.Windows.Forms.Label lblIcons;
    }
}