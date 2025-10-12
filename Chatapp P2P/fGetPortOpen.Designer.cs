namespace Chatapp_P2P
{
    partial class fGetPortOpen
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
            this.label1 = new AntdUI.Label();
            this.lbIP = new AntdUI.Label();
            this.label3 = new AntdUI.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.button1 = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.btnSubmit = new AntdUI.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new AntdUI.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(50, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // lbIP
            // 
            this.lbIP.Location = new System.Drawing.Point(105, 62);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(226, 23);
            this.lbIP.TabIndex = 1;
            this.lbIP.Text = "";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(36, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 37);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(105, 107);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(75, 29);
            this.txtPort.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Chọn port trống";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageHeader1.IconSvg = "";
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Size = new System.Drawing.Size(355, 36);
            this.pageHeader1.TabIndex = 5;
            this.pageHeader1.Text = "Input port";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(95)))), ((int)(((byte)(193)))));
            this.btnSubmit.Location = new System.Drawing.Point(129, 196);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(98, 39);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Type = AntdUI.TTypeMini.Primary;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(105, 154);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(226, 29);
            this.txtName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(25, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 37);
            this.label2.TabIndex = 7;
            this.label2.Text = "Name:";
            // 
            // fGetPortOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 247);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fGetPortOpen";
            this.Text = "fGetPortOpen";
            this.Load += new System.EventHandler(this.fGetPortOpen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AntdUI.Label label1;
        private AntdUI.Label lbIP;
        private AntdUI.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private AntdUI.Button button1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Button btnSubmit;
        private System.Windows.Forms.TextBox txtName;
        private AntdUI.Label label2;
    }
}