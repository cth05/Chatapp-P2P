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
    public partial class fClient : MaterialForm
    {
        private ChatSockets server = new ChatSockets(false);
        public fClient()
        {
            InitializeComponent();
        }

        private void fClient_Load(object sender, EventArgs e)
        {
            string ipLocal = NetHelper.GetLocalIPv4();
            if (ipLocal == null)
                return;
            txtIP.Text = ipLocal;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPort.Text, out int port))
            {
                MessageBox.Show("Port không đúng định dạng"); return;
            }
            if (port < 0 || port > 65535)
            {
                MessageBox.Show("Port không khả dụng"); return;
            }
            if (!IPAddress.TryParse(txtIP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("IP không đúng định dạng"); return;
            }
            server.ConnectToPeer(ipAddress, port);
            btnConnect.Enabled = false;
            btnConnect.Text = "Connecting...";
            while (server.GetStatus() == Status.DISCONNECTED)
                await Task.Delay(1000);
            fChat f = new fChat(server);
            f.Show();
            this.Hide();
        }

        private void fClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
