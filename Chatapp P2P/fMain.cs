using AntdUI;
using AntdUI.Chat;
using Chatapp_P2P.Core;
using Chatapp_P2P.Model;
using Chatapp_P2P.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Chatapp_P2P
{
    public partial class fMain : AntdUI.Window
    {
        public fMain()
        {
            InitializeComponent();
        }
        Chatapp_P2P.Core.ChatSockets sockets;
        User target, myInfo;
        Dictionary<User, ChatList> userChatList = new Dictionary<User, ChatList>();
        private async void fMain_Load(object sender, EventArgs e)
        {
            await Task.Delay(500);
            fGetPortOpen f = new fGetPortOpen();
            f.ShowDialog(this);
            sockets = new ChatSockets();
            lbIP.Text = f.ip;
            this.pageHeader1.SubText = f.name;
            lbPort.Text = f.port.ToString();
            sockets.Start(f.port);
            myInfo = new User
            {
                Id = f.ip.Replace(".", "") + f.port + f.name.Replace(" ", "").ToLower(),
                Name = f.name,
                Endpoint = f.ip + ":" + f.port
            };
            sockets.MessageReceived += OnMessageReceived;
            sockets.StatusChanged += OnStatusReceived;
            sockets.PeerChanged += OnPeerReceived;
        }
        private void OnPeerReceived(string type, string endpoint)
        {
            if (type == "add")
            {
                ChatMessage msgObj = new ChatMessage
                {
                    Type = "info",
                    From = myInfo,
                    To = new User { Endpoint = endpoint },
                    Message = "",
                };
                string rawMsgSend = JsonConvert.SerializeObject(msgObj, Formatting.None);
                sockets.SendMessage(endpoint, rawMsgSend);
            }
            else if (type == "delete")
            {
                User userDelete = null;
                foreach (var item in userChatList)
                {
                    if (item.Key.Endpoint == endpoint)
                    {
                        userDelete = item.Key;
                        var chatlist = userChatList[item.Key];
                        chatlist.Visible = false;
                        userChatList.Remove(item.Key);
                        break;
                    }
                }
                foreach (var item in msgList.Items)
                {
                    if (item.ID == userDelete.Id)
                    {
                        item.Text = "offline";
                        item.Visible = false;
                        msgList.Items.Remove(item);
                    }
                }
                if (userDelete == target)
                {
                    lbTarget.Text = "";
                    pnlChatInfo.Visible = false;
                    pnlToolChat.Visible = false;
                    pnlToolChat.Enabled = false;
                }
            }
        }
        private void OnMessageReceived(string endpoint, string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnMessageReceived(endpoint, msg)));
                return;
            }
            try
            {
                var obj = JsonConvert.DeserializeObject<ChatMessage>(msg);
                if (obj == null)
                {
                    return;
                }
                sockets.MergeIPPort(endpoint, obj.From.Endpoint);
                if (obj.Type == "chat")
                {
                    User from = obj.From;
                    var existingUser = userChatList.Keys.FirstOrDefault(u => u.Id == obj.From.Id);
                    ChatList chatlist = userChatList[existingUser];
                    chatlist.AddToBottom(new TextChatItem(obj.Message, Resources._464760996_1254146839119862_3605321457742435801_n, from.Name));
                    NotifyMessage(from.Name, $"{from.Name}: {obj.Message}");
                    var item = msgList.Items.FirstOrDefault(i => i.ID == from.Id);
                    if (item != null)
                    {
                        item.Text = obj.Message;
                        item.Time = DateTime.Now.ToString("HH:mm");
                        if (target == null || target.Id != from.Id)
                            item.Count += 1;
                    }
                }
                else if (obj.Type == "image")
                {
                    string fileName = obj.Note;
                    string saveDir = Path.Combine(System.Windows.Forms.Application.StartupPath, "Downloads");
                    Directory.CreateDirectory(saveDir);
                    string savePath = Path.Combine(saveDir, fileName);
                    byte[] bytes = Convert.FromBase64String(obj.Message);
                    File.WriteAllBytes(savePath, bytes);
                    User from = obj.From;
                    var existingUser = userChatList.Keys.FirstOrDefault(u => u.Id == obj.From.Id);
                    ChatList chatlist = userChatList[existingUser];
                    var item = msgList.Items.FirstOrDefault(i => i.ID == from.Id);
                    if (item != null)
                    {
                        item.Text = obj.Note;
                        item.Time = DateTime.Now.ToString("HH:mm");
                        if (target == null || target.Id != from.Id)
                            item.Count += 1;
                    }
                    NotifyMessage(from.Name, $"Đã gửi hình ảnh");
                    chatlist.AddToBottom(new TextChatItem("data:image/png;base64," + obj.Message, Resources._464760996_1254146839119862_3605321457742435801_n, from.Name) { Me = false });
                }
                else if (obj.Type == "file")
                {
                    string fileName = obj.Note;
                    User from = obj.From;
                    var existingUser = userChatList.Keys.FirstOrDefault(u => u.Id == obj.From.Id);
                    ChatList chatlist = userChatList[existingUser];
                    var item = msgList.Items.FirstOrDefault(i => i.ID == from.Id);
                    if (item != null)
                    {
                        item.Text = obj.Note;
                        item.Time = DateTime.Now.ToString("HH:mm");
                        if (target == null || target.Id != from.Id)
                            item.Count += 1;
                    }
                    chatlist.AddToBottom(new TextChatItem($"📎 {Path.GetFileName(fileName)} (đã gửi)", null, myInfo.Name) { Me = false });
                    NotifyMessage(from.Name, $"Đã gửi tệp tin: {obj.Note}");
                    AntdUI.Modal.open(new Modal.Config(this, "Xác nhận", $"{from.Name} muốn gửi cho bạn tệp tin: {obj.Note}", AntdUI.TType.Info)
                    {
                        CancelText = "No",
                        OkText = "Yes",
                        OnOk = config =>
                        {
                            string saveDir = Path.Combine(System.Windows.Forms.Application.StartupPath, "Downloads");
                            Directory.CreateDirectory(saveDir);
                            byte[] bytes = Convert.FromBase64String(obj.Message);
                            string savePath = Path.Combine(saveDir, fileName);
                            File.WriteAllBytes(savePath, bytes);
                            AntdUI.Message.info(this, $"Đã lưu: {savePath}", Font);
                            return true;
                        },
                    });
                }
                else if (obj.Type == "info")
                {
                    User from = obj.From;
                    if (ContainUser(from))
                        return;
                    ChatList chatlist = new ChatList();
                    chatlist.Dock = DockStyle.Fill;
                    chatlist.Enabled = false;
                    chatlist.Visible = false;
                    userChatList.Add(from, chatlist);
                    pnlChat.Controls.Add(chatlist);
                    this.msgList.Items.Add(new MsgItem { ID = from.Id, Name = from.Name, Text = "Connected", Tag = from.Endpoint, Count = 0, Icon = Resources._464760996_1254146839119862_3605321457742435801_n });
                    ChatMessage msgObj = new ChatMessage
                    {
                        Type = "info",
                        From = myInfo,
                        To = new User { Endpoint = endpoint },
                        Message = "",
                    };
                    string rawMsgSend = JsonConvert.SerializeObject(msgObj, Formatting.None);
                    sockets.SendMessage(endpoint, rawMsgSend);
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.info(this, $"[Error] {ex.Message}", Font);
            }
        }
        private void NotifyMessage(string from, string message)
        {
            AntdUI.Notification.info(this, $"Bạn có tin nhắn từ: {from}", message, TAlignFrom.BR, Font);
        }
        private bool ContainUser(User user)
        {
            foreach (var item in userChatList)
            {
                if (item.Key.Endpoint == user.Endpoint)
                    return true;
            }
            return false;
        }
        private void OnStatusReceived(string msg)
        {
            AntdUI.Message.info(this, msg, Font);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sockets != null)
                sockets.Stop();
            Environment.Exit(0);
        }
        private void msgList_ItemClick(object sender, AntdUI.MsgItemClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                e.Item.Select = true;
                target = FindUserFromID(e.Item.ID);
                lbTarget.Text = $"Tên:{target.Name}-Endpoint Listener:{target.Endpoint}";
                ChatList chatlist;
                if (!userChatList.TryGetValue(target, out chatlist))
                {
                    AntdUI.Message.error(this, $"Không tìm thấy user {e.Item.ID}", Font);
                }
                foreach (var item in userChatList)
                {
                    if (item.Key.Id != target.Id)
                    {
                        item.Value.Visible = false;
                        item.Value.Enabled = false;
                        continue;
                    }
                    item.Value.Visible = item.Value.Enabled = true;
                }
                e.Item.Count = 0;
                pnlToolChat.Enabled = pnlToolChat.Visible = true;
            }
        }
        private User FindUserFromID(string id)
        {
            foreach (var item in userChatList)
            {
                if (item.Key.Id == id)
                    return item.Key;
            }
            return null;
        }
        private void SendMessage()
        {
            string text = txtInput.Text;
            if (string.IsNullOrEmpty(text))
                return;
            ChatMessage msgObj = new ChatMessage
            {
                Type = "chat",
                From = myInfo,
                To = target,
                Message = text,
                Note = ""
            };
            string rawMsgSend = JsonConvert.SerializeObject(msgObj, Formatting.None);
            try
            {
                sockets.SendMessage(target.Endpoint, rawMsgSend);
                ChatList chatlist;
                if (userChatList.TryGetValue(target, out chatlist) && target != null)
                {
                    chatlist.AddToBottom(new TextChatItem(text, Resources._464760996_1254146839119862_3605321457742435801_n, myInfo.Name) { Me = true });
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, $"❌ Không gửi được tin nhắn! Lỗi: {ex.Message}", Font);
            }
            finally
            {
                txtInput.Clear();
            }
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ảnh|*.png;*.jpg;*.jpeg;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SendImage(target, ofd.FileName);
                }
            }
        }
        private void SendImage(User target, string filePath)
        {
            try
            {
                byte[] bytes = Helper.ImageHelper.CompressImage(filePath);
                string base64 = Convert.ToBase64String(bytes);

                ChatMessage msgObj = new ChatMessage
                {
                    Type = "image",
                    From = myInfo,
                    To = target,
                    Note = Path.GetFileName(filePath),
                    Message = base64
                };

                string raw = JsonConvert.SerializeObject(msgObj, Formatting.None);
                sockets.SendMessage(target.Endpoint, raw);
                if (userChatList.TryGetValue(target, out ChatList chatlist))
                {
                    chatlist.AddToBottom(new TextChatItem("data:image/png;base64," + base64, Resources._464760996_1254146839119862_3605321457742435801_n, myInfo.Name) { Me = true });
                    AntdUI.Message.error(this, $"Gửi hình ảnh thành công", Font);

                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, $"Không gửi được file: {ex.Message}", Font);
            }
        }
        private void SendFile(User target, string filePath)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                string base64 = Convert.ToBase64String(bytes);

                ChatMessage msgObj = new ChatMessage
                {
                    Type = "file",
                    From = myInfo,
                    To = target,
                    Note = Path.GetFileName(filePath),
                    Message = base64
                };

                string raw = JsonConvert.SerializeObject(msgObj, Formatting.None);
                sockets.SendMessage(target.Endpoint, raw);
                if (userChatList.TryGetValue(target, out ChatList chatlist))
                {
                    chatlist.AddToBottom(new TextChatItem($"📎 {Path.GetFileName(filePath)} (đã gửi)", null, myInfo.Name) { Me = true });
                    AntdUI.Message.error(this, $"Gửi tệp tin thành công", Font);
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, $"Không gửi được file: {ex.Message}", Font);
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    AntdUI.Modal.open(new Modal.Config(this, $"Xác nhận gửi tệp tin [{ofd.FileName}]", "", AntdUI.TType.Info)
                    {
                        CancelText = "No",
                        OkText = "Yes",
                        OnOk = config =>
                        {
                            SendFile(target, ofd.FileName);
                            return true;
                        },
                    });
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            fConnect f = new fConnect();
            f.ShowDialog(this);
            if (string.IsNullOrEmpty(f.ip) || string.IsNullOrEmpty(f.port.ToString()))
                return;
            string ip = f.ip;
            int port = f.port;
            sockets.ConnectToPeer(ip, port);
        }
    }
}
