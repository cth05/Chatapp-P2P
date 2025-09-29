using Chatapp_P2P.Core;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatapp_P2P
{
    public partial class fChat : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private ChatSockets chatSockets;
        public fChat(ChatSockets socket)
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            flowMain.HorizontalScroll.Enabled = false;
            flowMain.HorizontalScroll.Visible = false;
            flowMain.HorizontalScroll.Maximum = 0;

            flowMain.Padding = new Padding(0);
            flowMain.Margin = new Padding(0);
            this.chatSockets = socket;
        }
        private void AddMessage(string text, bool isSender)
        {
            Panel row = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                Width = flowMain.ClientSize.Width,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            Panel bubble = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(10),
                Margin = new Padding(5),
                BackColor = isSender ? Color.Orange : Color.YellowGreen,
                MaximumSize = new Size(flowMain.ClientSize.Width - 40, 0) // giới hạn ngang
            };

            Label lbl = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(flowMain.ClientSize.Width - 60, 0),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Black,
                Text = text
            };
            bubble.Controls.Add(lbl);

            if (isSender)
            {
                bubble.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                bubble.Location = new Point(row.Width - bubble.Width - 5, 0);
            }
            else
            {
                bubble.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                bubble.Location = new Point(5, 0);
            }

            row.Controls.Add(bubble);
            flowMain.Controls.Add(row);
            flowMain.ScrollControlIntoView(row);
        }

        private void fChat_Load(object sender, EventArgs e)
        {
            chatSockets.MessageReceived += OnMessageReceived;
            string info = chatSockets.isServer ? "Server" : "Client";
            this.Text = $"{info} - Port: {chatSockets.port}";
        }
        private void OnMessageReceived(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnMessageReceived(msg)));
                return;
            }
            AddMessage(msg, false);
        }

        private void fChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string msg = txtMessage.Text;
            if (string.IsNullOrEmpty(msg))
                return;
            txtMessage.Text = "";
            AddMessage(msg, true);
            chatSockets.SendMessage(msg);
        }
    }
}
