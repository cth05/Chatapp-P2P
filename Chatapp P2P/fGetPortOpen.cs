using Chatapp_P2P.Core;
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
    public partial class fGetPortOpen : AntdUI.Window
    {
        public fGetPortOpen()
        {
            InitializeComponent();
        }
        public int port { get; private set; }
        public string ip { get;private set; }
        public string name { get; private set; }
        private void button1_Click(object sender, EventArgs e)
        {
            int defaultPort = 9000;
            int freePort = NetHelper.IsPortAvailable(defaultPort) ? defaultPort : NetHelper.GetFreePort();
            if (freePort > 0)
                txtPort.Text = freePort.ToString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPort.Text, out int port))
            {
                MessageBox.Show("Port không đúng định dạng"); return;
            }
            if (port < 0 || port > 65535)
            {
                MessageBox.Show("Port không khả dụng"); return;
            }
            if (!NetHelper.IsPortAvailable(port))
            {
                MessageBox.Show("Port đã được sử dụng"); return;
            }
            if (!IPAddress.TryParse(lbIP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("IP không đúng định dạng"); return;
            }
            this.port=port;
            this.ip = lbIP.Text;
            this.name = txtName.Text;
            this.Close();
        }

        private void fGetPortOpen_Load(object sender, EventArgs e)
        {
            lbIP.Text = NetHelper.GetLocalIPv4();
            txtName.Text = Environment.MachineName;
        }
    }
}
