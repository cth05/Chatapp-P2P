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
    public partial class fConnect : AntdUI.Window
    {
        public fConnect()
        {
            InitializeComponent();
        }
        public int port { get; private set; }
        public string ip { get; private set; }
        private void fConnect_Load(object sender, EventArgs e)
        {
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
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
            this.port = port;
            this.ip = txtIP.Text;
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
