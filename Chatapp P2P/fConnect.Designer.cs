namespace Chatapp_P2P
{
    partial class fConnect
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
            this.pageHeader1 = new AntdUI.PageHeader();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new AntdUI.Label();
            this.label1 = new AntdUI.Label();
            this.btnConnect = new AntdUI.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.button1 = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageHeader1.IconSvg = "";
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Size = new System.Drawing.Size(270, 58);
            this.pageHeader1.TabIndex = 12;
            this.pageHeader1.Text = "Input port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(97, 116);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(65, 29);
            this.txtPort.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(39, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "Port:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(54, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP:";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(95)))), ((int)(((byte)(193)))));
            this.btnConnect.Location = new System.Drawing.Point(138, 167);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(105, 36);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Submit";
            this.btnConnect.Type = AntdUI.TTypeMini.Primary;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(97, 75);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(144, 29);
            this.txtIP.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.button1.Location = new System.Drawing.Point(28, 167);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 36);
            this.button1.TabIndex = 15;
            this.button1.Text = "Cancel";
            this.button1.Type = AntdUI.TTypeMini.Info;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 217);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fConnect";
            this.Text = "fConnect";
            this.Load += new System.EventHandler(this.fConnect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.TextBox txtPort;
        private AntdUI.Label label3;
        private AntdUI.Label label1;
        private AntdUI.Button btnConnect;
        private System.Windows.Forms.TextBox txtIP;
        private AntdUI.Button button1;
    }
}