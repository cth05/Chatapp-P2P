using Chatapp_P2P.Core;
using Chatapp_P2P.Modal;
using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Chatapp_P2P
{
    public partial class fChat : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private ChatSockets chatSockets;
        private string user;
        public fChat(ChatSockets socket, string user)
        {
            InitializeComponent();
            this.Resize += (s, e) => UpdateChatBubbleLayout();
            flowMain.SizeChanged += (s, e) => UpdateChatBubbleLayout();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            flowMain.HorizontalScroll.Enabled = false;
            flowMain.HorizontalScroll.Visible = false;
            flowMain.HorizontalScroll.Maximum = 0;

            flowMain.Padding = new Padding(0);
            flowMain.Margin = new Padding(0);
            this.chatSockets = socket;
            this.user = user;
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
                Name = "bubble",
                Tag = isSender, // dùng để biết căn trái/phải khi resize
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(12),
                Margin = new Padding(8),
                BackColor = isSender ? Color.LightSkyBlue : Color.LightGray,
                MaximumSize = new Size(Math.Max(120, flowMain.ClientSize.Width - 120), 0)
            };
            FlowLayoutPanel content = new FlowLayoutPanel
            {
                Name = "content",
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false,
                Dock = DockStyle.Fill
            };
            Label lblMessage = new Label
            {
                Name = "lblMessage",
                AutoSize = true,
                MaximumSize = new Size(bubble.MaximumSize.Width - bubble.Padding.Horizontal - 20, 0),
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                Text = text
            };
            Label lblTime = new Label
            {
                Name = "lblTime",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.DimGray,
                Text = DateTime.Now.ToString("HH:mm"),
                Margin = new Padding(0, 5, 0, 0),
                TextAlign = ContentAlignment.MiddleRight
            };
            content.Controls.Add(lblMessage);
            content.Controls.Add(lblTime);
            bubble.Controls.Add(content);

            // áp dụng bo góc
            bubble.Paint += (s, e) => FormHelper.MakeRounded(bubble, 12);

            row.Controls.Add(bubble);
            flowMain.Controls.Add(row);

            // cập nhật layout ngay khi thêm
            UpdateChatBubbleLayout();

            // scroll tới tin mới
            flowMain.ScrollControlIntoView(row);
        }
        private void UpdateChatBubbleLayout()
        {
            // đảm bảo chạy trên UI thread
            if (flowMain.InvokeRequired)
            {
                flowMain.Invoke(new Action(UpdateChatBubbleLayout));
                return;
            }

            flowMain.SuspendLayout();
            try
            {
                // khoảng cách 2 bên
                int sidePadding = 15;
                // giới hạn chiều ngang tối đa cho bubble
                int bubbleMaxWidth = Math.Max(120, flowMain.ClientSize.Width - 120);

                // duyệt tất cả các 'row' trong flowMain
                foreach (Control row in flowMain.Controls.OfType<Control>().ToArray())
                {
                    // đồng bộ chiều rộng row với flowMain
                    row.Width = flowMain.ClientSize.Width;

                    if (row.Controls.Count == 0) continue;
                    var bubble = row.Controls[0];

                    // cập nhật maximum size cho bubble
                    bubble.MaximumSize = new Size(bubbleMaxWidth, int.MaxValue);

                    // tìm content (FlowLayoutPanel) và label message để cập nhật MaximumSize
                    var content = bubble.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                    if (content != null)
                    {
                        var lblMessage = content.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblMessage");
                        if (lblMessage != null)
                        {
                            // trừ padding của bubble để label wrap chính xác
                            int usable = bubbleMaxWidth - bubble.Padding.Horizontal - 20;
                            lblMessage.MaximumSize = new Size(Math.Max(80, usable), 0);
                        }

                        // (tuỳ chọn) đảm bảo lblTime không quá rộng
                        var lblTime = content.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTime");
                        if (lblTime != null)
                        {
                            lblTime.MaximumSize = new Size(bubbleMaxWidth - bubble.Padding.Horizontal, 0);
                        }
                    }

                    // ép layout để kích thước thực tế của bubble được tính lại
                    bubble.PerformLayout();

                    // đặt vị trí trái hoặc phải cho bubble
                    bool isSender = bubble.Tag is bool b && b;
                    int x = isSender ? Math.Max(0, row.ClientSize.Width - bubble.Width - sidePadding) : sidePadding;
                    bubble.Location = new Point(x, bubble.Location.Y);
                }
            }
            finally
            {
                flowMain.ResumeLayout();
            }
        }
        private void fChat_Load(object sender, EventArgs e)
        {
            chatSockets.MessageReceived += OnMessageReceived;
            chatSockets.StatusChanged += OnStatusReceived;
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
            ChatMessage obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
            if (obj.Type == "chat")
            {
                AddMessage(obj.Message, false);
                this.Text = obj.From;
            }    
        }
        private void OnStatusReceived(string msg)
        {
            
        }
        private void fChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string text = txtMessage.Text;
            if (string.IsNullOrEmpty(text))
                return;
            ChatMessage msgObj = new ChatMessage
            {
                Type = "chat",
                From = user,
                To = "",
                Message = text,
            };
            txtMessage.Text = "";
            AddMessage(text, true);
            string rawMsgSend = JsonConvert.SerializeObject(msgObj, Formatting.None);
            chatSockets.SendMessage(rawMsgSend);
        }
    }
    public class FormHelper
    {
        public static void MakeRounded(Control control, int radius)
        {
            control.Region = new Region(GetRoundPath(control.ClientRectangle, radius));
        }

        private static System.Drawing.Drawing2D.GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
