using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatapp_P2P.Core
{
    public class ChatSockets
    {
        public bool isServer { get;private set; }
        private TcpListener listener;
        private TcpClient client;
        private NetworkStream stream;
        private Thread listenThread;
        private Thread receiveThread;
        public event Action<string> MessageReceived;
        public event Action<string> StatusChanged;
        public int port { get; private set; }
        private Status code { get; set; }
        public ChatSockets(bool isServer)
        {
            this.isServer = isServer;
        }
        public Status GetStatus()
        {
            return code;
        }
        public void StartListening(IPAddress ip, int port)
        {
            code = Status.DISCONNECTED;
            listener = new TcpListener(ip, port);
            listener.Start();
            this.port = port;
            listenThread = new Thread(AcceptClient);
            listenThread.IsBackground = true;
            listenThread.Start();
            StatusChanged?.Invoke($"Đang lắng nghe trên port {port}");
        }

        private void AcceptClient()
        {
            try
            {
                client = listener.AcceptTcpClient();
                stream = client.GetStream();
                StatusChanged?.Invoke("Đã có máy kết nối tới");
                StartReceiving();
                code= Status.CONNECTED;
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke("Lỗi Accept: " + ex.Message);
            }
        }
        public void ConnectToPeer(IPAddress ipAddr, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ipAddr, port);
                stream = client.GetStream();
                StatusChanged?.Invoke("Kết nối thành công tới " + ipAddr + ":" + port);
                StartReceiving();
                code= Status.CONNECTED;
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke("Lỗi kết nối: " + ex.Message);
            }
        }
        private void StartReceiving()
        {
            receiveThread = new Thread(() =>
            {
                var reader = new StreamReader(stream, Encoding.UTF8);
                while (true)
                {
                    try
                    {
                        string msg = reader.ReadLine();
                        if (msg == null) break;
                        MessageReceived?.Invoke(msg);
                    }
                    catch
                    {
                        break;
                    }
                }
                StatusChanged?.Invoke("Ngắt kết nối");
            });
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public void SendMessage(string message)
        {
            if (stream == null) return;
            var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            writer.WriteLine(message);
        }

        public void Stop()
        {
            try
            {
                stream?.Close();
                client?.Close();
                listener?.Stop();
            }
            catch { }
        }
    }
    public enum Status
    {
        DISCONNECTED,
        CONNECTED
    }
    internal class NetHelper
    {
        public static bool IsPortAvailable(int port)
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();

            bool inUse = ipProps.GetActiveTcpListeners().Any(p => p.Port == port) ||
                         ipProps.GetActiveUdpListeners().Any(p => p.Port == port);

            return !inUse;
        }
        public static string GetLocalIPv4()
        {
            foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }
    }
}
