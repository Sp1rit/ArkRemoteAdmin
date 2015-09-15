namespace ArkRemoteAdmin.UI
{
    partial class Servers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Servers));
            this.btnAdd = new BssFramework.Windows.Forms.AeroButton();
            this.lblOnline = new BssFramework.Windows.Forms.SeperatorLabel();
            this.serverList1 = new ArkRemoteAdmin.UI.Controls.ServerList();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(125, 45);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add Server";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // lblOnline
            // 
            this.lblOnline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOnline.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.lblOnline.Location = new System.Drawing.Point(3, 51);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(713, 23);
            this.lblOnline.TabIndex = 9;
            this.lblOnline.Text = "Servers";
            this.lblOnline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // serverList1
            // 
            this.serverList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverList1.AutoScroll = true;
            this.serverList1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.serverList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverList1.Location = new System.Drawing.Point(3, 77);
            this.serverList1.Name = "serverList1";
            this.serverList1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.serverList1.Size = new System.Drawing.Size(713, 441);
            this.serverList1.TabIndex = 0;
            this.serverList1.WrapContents = false;
            // 
            // Servers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblOnline);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.serverList1);
            this.Name = "Servers";
            this.Size = new System.Drawing.Size(719, 521);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ServerList serverList1;
        private BssFramework.Windows.Forms.AeroButton btnAdd;
        private BssFramework.Windows.Forms.SeperatorLabel lblOnline;
    }
}
