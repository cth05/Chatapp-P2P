namespace Chatapp_P2P
{
    partial class fServer
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
            this.btnListen = new MaterialSkin.Controls.MaterialButton();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.lbUser = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.btnSelectPortAvailable = new System.Windows.Forms.LinkLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatusChanged = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnListen.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnListen.Depth = 0;
            this.btnListen.HighEmphasis = true;
            this.btnListen.Icon = null;
            this.btnListen.Location = new System.Drawing.Point(357, 116);
            this.btnListen.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnListen.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnListen.Name = "btnListen";
            this.btnListen.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnListen.Size = new System.Drawing.Size(70, 36);
            this.btnListen.TabIndex = 9;
            this.btnListen.Text = "Listen";
            this.btnListen.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnListen.UseAccentColor = false;
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(122, 161);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(78, 25);
            this.txtPort.TabIndex = 8;
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(122, 122);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(168, 25);
            this.txtIP.TabIndex = 7;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(70, 161);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(34, 19);
            this.materialLabel2.TabIndex = 6;
            this.materialLabel2.Text = "Port:";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(85, 122);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(19, 19);
            this.materialLabel1.TabIndex = 5;
            this.materialLabel1.Text = "IP:";
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Depth = 0;
            this.lbUser.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lbUser.Location = new System.Drawing.Point(119, 84);
            this.lbUser.MouseState = MaterialSkin.MouseState.HOVER;
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(1, 0);
            this.lbUser.TabIndex = 11;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(68, 84);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(36, 19);
            this.materialLabel3.TabIndex = 10;
            this.materialLabel3.Text = "User:";
            // 
            // btnSelectPortAvailable
            // 
            this.btnSelectPortAvailable.AutoSize = true;
            this.btnSelectPortAvailable.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPortAvailable.Location = new System.Drawing.Point(206, 163);
            this.btnSelectPortAvailable.Name = "btnSelectPortAvailable";
            this.btnSelectPortAvailable.Size = new System.Drawing.Size(108, 17);
            this.btnSelectPortAvailable.TabIndex = 12;
            this.btnSelectPortAvailable.TabStop = true;
            this.btnSelectPortAvailable.Text = "Chọn port trống";
            this.btnSelectPortAvailable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSelectPortAvailable_LinkClicked);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatusChanged});
            this.statusStrip1.Location = new System.Drawing.Point(3, 205);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(483, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatusChanged
            // 
            this.lbStatusChanged.Name = "lbStatusChanged";
            this.lbStatusChanged.Size = new System.Drawing.Size(0, 17);
            // 
            // fServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 230);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSelectPortAvailable);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Name = "fServer";
            this.Text = "TCP/IP Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fServer_FormClosing);
            this.Load += new System.EventHandler(this.fServer_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton btnListen;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel lbUser;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private System.Windows.Forms.LinkLabel btnSelectPortAvailable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatusChanged;
    }
}