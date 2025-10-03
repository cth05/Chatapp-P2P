using Chatapp_P2P.Core;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatapp_P2P
{
    public partial class fServer : MaterialForm
    {
        private bool isRunning = false;
        private ChatSockets server = new ChatSockets(true);
        private string user = $"{Environment.UserName}@{Environment.MachineName}";
        public fServer()
        {
            InitializeComponent();
            server.StatusChanged += OnStatusReceived;
        }
        private void OnStatusReceived(string msg)
        {
            if (lbStatusChanged.GetCurrentParent().InvokeRequired)
            {
                lbStatusChanged.GetCurrentParent().Invoke(new Action(() =>
                {
                    lbStatusChanged.Text = msg;
                }));
            }
            else
            {
                lbStatusChanged.Text = msg;
            }
        }
        private async void btnListen_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                this.Text = $"TCP/IP Server";
                btnListen.Text = "Listen";
                isRunning= false;
                return;
            }
            if (!int.TryParse(txtPort.Text, out int port))
            {
                MessageBox.Show("Port không đúng định dạng");return;
            }
            if(port<0 || port > 65535)
            {
                MessageBox.Show("Port không khả dụng");return;
            }
            if(!NetHelper.IsPortAvailable(port))
            {
                MessageBox.Show("Port đã được sử dụng");return;
            }
            if(!IPAddress.TryParse(txtIP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("IP không đúng định dạng");return;
            }
            server.StartListening(ipAddress,port);
            btnListen.Text = "Listening...";
            isRunning = true;
            OnStatusReceived($"TCP/IP Server: Mở port {port}");
            while (server.GetStatus() == Status.DISCONNECTED && isRunning)
                await Task.Delay(1000);
            if (server.GetStatus() == Status.DISCONNECTED || !isRunning)
                return;
            fChat f = new fChat(server, user);
            f.Show();
            this.Hide();
        }

        private void fServer_Load(object sender, EventArgs e)
        {
            string ipLocal = NetHelper.GetLocalIPv4();
            if (ipLocal == null)
                return;
            txtIP.Text = ipLocal;
            lbUser.Text = user;
        }

        private void fServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSelectPortAvailable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int defaultPort = 9000;
            int freePort = NetHelper.IsPortAvailable(defaultPort) ? defaultPort : NetHelper.GetFreePort();
            if (freePort>0)
                txtPort.Text = freePort.ToString();
        }
    }
}
