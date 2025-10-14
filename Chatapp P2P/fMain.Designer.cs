namespace Chatapp_P2P
{
    partial class fMain
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
            this.pnlUser = new AntdUI.Panel();
            this.btnConnect = new AntdUI.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.msgList = new AntdUI.Chat.MsgList();
            this.pnlToolChat = new AntdUI.Panel();
            this.btnSendImage = new AntdUI.Button();
            this.btnUpload = new AntdUI.Button();
            this.btnSend = new AntdUI.Button();
            this.pnlChatInfo = new AntdUI.Panel();
            this.lbTarget = new AntdUI.Label();
            this.pnlChat = new AntdUI.Panel();
            this.txtInput = new AntdUI.Input();
            this.pnlUser.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlToolChat.SuspendLayout();
            this.pnlChatInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(867, 35);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Chatapp P2P";
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.btnConnect);
            this.pnlUser.Controls.Add(this.statusStrip1);
            this.pnlUser.Controls.Add(this.msgList);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlUser.Location = new System.Drawing.Point(0, 35);
            this.pnlUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(328, 474);
            this.pnlUser.TabIndex = 1;
            this.pnlUser.Text = "panel1";
            // 
            // btnConnect
            // 
            this.btnConnect.BackExtend = "";
            this.btnConnect.ColorScheme = AntdUI.TAMode.Light;
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnConnect.IconRatio = 1.5F;
            this.btnConnect.IconSvg = "PlusOutlined";
            this.btnConnect.LoadingRespondClick = true;
            this.btnConnect.Location = new System.Drawing.Point(0, 406);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(328, 46);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect new user";
            this.btnConnect.Type = AntdUI.TTypeMini.Primary;
            this.btnConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbIP,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.lbPort});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(328, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(20, 17);
            this.toolStripStatusLabel1.Text = "IP:";
            // 
            // lbIP
            // 
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(52, 17);
            this.lbIP.Text = "127.0.0.1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(19, 17);
            this.toolStripStatusLabel3.Text = "    ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel4.Text = "Port:";
            // 
            // lbPort
            // 
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(13, 17);
            this.lbPort.Text = "0";
            // 
            // msgList
            // 
            this.msgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgList.Location = new System.Drawing.Point(0, 0);
            this.msgList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.msgList.Name = "msgList";
            this.msgList.Size = new System.Drawing.Size(328, 474);
            this.msgList.TabIndex = 0;
            this.msgList.Text = "msgList1";
            this.msgList.ItemClick += new AntdUI.ItemClickEventHandler(this.msgList_ItemClick);
            // 
            // pnlToolChat
            // 
            this.pnlToolChat.Controls.Add(this.txtInput);
            this.pnlToolChat.Controls.Add(this.btnSendImage);
            this.pnlToolChat.Controls.Add(this.btnUpload);
            this.pnlToolChat.Controls.Add(this.btnSend);
            this.pnlToolChat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolChat.Enabled = false;
            this.pnlToolChat.Location = new System.Drawing.Point(328, 441);
            this.pnlToolChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolChat.Name = "pnlToolChat";
            this.pnlToolChat.Size = new System.Drawing.Size(539, 68);
            this.pnlToolChat.TabIndex = 2;
            this.pnlToolChat.Text = "panel2";
            this.pnlToolChat.Visible = false;
            // 
            // btnSendImage
            // 
            this.btnSendImage.BackExtend = "";
            this.btnSendImage.ColorScheme = AntdUI.TAMode.Light;
            this.btnSendImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendImage.IconRatio = 1.5F;
            this.btnSendImage.IconSvg = "FileImageOutlined";
            this.btnSendImage.Location = new System.Drawing.Point(367, 0);
            this.btnSendImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(54, 68);
            this.btnSendImage.TabIndex = 5;
            this.btnSendImage.Type = AntdUI.TTypeMini.Primary;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.BackExtend = "";
            this.btnUpload.ColorScheme = AntdUI.TAMode.Light;
            this.btnUpload.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUpload.IconRatio = 1.5F;
            this.btnUpload.IconSvg = "UploadOutlined";
            this.btnUpload.Location = new System.Drawing.Point(421, 0);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(49, 68);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Type = AntdUI.TTypeMini.Primary;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackExtend = "";
            this.btnSend.ColorScheme = AntdUI.TAMode.Light;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.IconRatio = 1.5F;
            this.btnSend.IconSvg = "SendOutlined";
            this.btnSend.Location = new System.Drawing.Point(470, 0);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Shape = AntdUI.TShape.Round;
            this.btnSend.Size = new System.Drawing.Size(69, 68);
            this.btnSend.TabIndex = 3;
            this.btnSend.Type = AntdUI.TTypeMini.Primary;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pnlChatInfo
            // 
            this.pnlChatInfo.Controls.Add(this.lbTarget);
            this.pnlChatInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChatInfo.Location = new System.Drawing.Point(328, 35);
            this.pnlChatInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlChatInfo.Name = "pnlChatInfo";
            this.pnlChatInfo.Size = new System.Drawing.Size(539, 57);
            this.pnlChatInfo.TabIndex = 3;
            this.pnlChatInfo.Text = "panel2";
            // 
            // lbTarget
            // 
            this.lbTarget.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbTarget.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarget.IconRatio = 1.5F;
            this.lbTarget.Location = new System.Drawing.Point(0, 0);
            this.lbTarget.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Prefix = "User";
            this.lbTarget.PrefixColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(95)))), ((int)(((byte)(167)))));
            this.lbTarget.PrefixSvg = "User";
            this.lbTarget.Size = new System.Drawing.Size(539, 57);
            this.lbTarget.Suffix = "";
            this.lbTarget.SuffixSvg = "";
            this.lbTarget.TabIndex = 0;
            this.lbTarget.Text = "";
            // 
            // pnlChat
            // 
            this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChat.Location = new System.Drawing.Point(328, 92);
            this.pnlChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(539, 349);
            this.pnlChat.TabIndex = 4;
            this.pnlChat.Text = "panel2";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.PlaceholderText = "Nhập tin nhắn";
            this.txtInput.Size = new System.Drawing.Size(367, 68);
            this.txtInput.TabIndex = 6;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 509);
            this.Controls.Add(this.pnlChat);
            this.Controls.Add(this.pnlChatInfo);
            this.Controls.Add(this.pnlToolChat);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.pageHeader1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fMain";
            this.Text = "fMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlToolChat.ResumeLayout(false);
            this.pnlChatInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Panel pnlUser;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private AntdUI.Chat.MsgList msgList;
        private AntdUI.Panel pnlToolChat;
        private AntdUI.Button btnSend;
        private AntdUI.Button btnUpload;
        private AntdUI.Panel pnlChatInfo;
        private AntdUI.Panel pnlChat;
        private AntdUI.Label lbTarget;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbIP;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lbPort;
        private AntdUI.Button btnConnect;
        private AntdUI.Button btnSendImage;
        private AntdUI.Input txtInput;
    }
}