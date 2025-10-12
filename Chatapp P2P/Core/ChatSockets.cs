using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Chatapp_P2P.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Chatapp_P2P.Core
{
    public class ChatSockets
    {
        private Socket listener;
        private Thread listenThread;
        private bool stopRequested = false;

        // Danh sách tất cả peer đã kết nối
        private readonly Dictionary<string, Socket> peers = new Dictionary<string, Socket>();
        private readonly Dictionary<Socket, string> socketsInfo = new Dictionary<Socket, string>();
        private readonly object lockObj = new object();

        public event Action<string, string> MessageReceived; // (peer, msg)
        public event Action<string> StatusChanged;
        public event Action<string, string> PeerChanged;
        public void Start(int port)
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, port));
            listener.Listen(10);

            listenThread = new Thread(ListenLoop) { IsBackground = true };
            listenThread.Start();
            StatusChanged?.Invoke($"Đang lắng nghe trên port {port}");
        }
        private void ListenLoop()
        {
            while (!stopRequested)
            {
                try
                {
                    Socket socket = listener.Accept();
                    string endpoint = socket.RemoteEndPoint.ToString();
                    lock (lockObj)
                    {
                        peers[endpoint] = socket;
                        socketsInfo[socket] = null;
                    }
                    StatusChanged?.Invoke($"🔗 Peer mới: {endpoint}");

                    // Thread nhận dữ liệu từ peer đó
                    new Thread(() => ReceiveLoop(endpoint, socket)) { IsBackground = true }.Start();
                    PeerChanged?.Invoke("add", endpoint);
                }
                catch (Exception ex) { StatusChanged?.Invoke($"Lỗi: {ex.Message}"); }
            }
        }
        public void MergeIPPort(string endpoint, string ipListener)
        {
            peers.TryGetValue(endpoint, out Socket socket);
            if (socket != null)
            {
                socketsInfo[socket] = ipListener;
            }
        }
        private void ReceiveLoop(string endpoint, Socket socket)
        {
            try
            {
                while (!stopRequested)
                {
                    byte[] lenBuf = new byte[4];
                    int read = socket.Receive(lenBuf, 0, 4, SocketFlags.None);
                    if (read == 0) break;

                    int length = BitConverter.ToInt32(lenBuf, 0);
                    byte[] data = new byte[length];
                    int total = 0;
                    while (total < length)
                    {
                        int r = socket.Receive(data, total, length - total, SocketFlags.None);
                        if (r == 0) throw new Exception("Mất kết nối");
                        total += r;
                    }
                    string msg = Encoding.UTF8.GetString(data);
                    MessageReceived?.Invoke(endpoint, msg);
                }
            }
            catch
            {
                StatusChanged?.Invoke($"❌ Mất kết nối với {endpoint}");
                PeerChanged?.Invoke("delete", socketsInfo[socket]);
                lock (lockObj)
                {
                    peers.Remove(endpoint); socketsInfo.Remove(socket);
                }
            }
        }
        public void ConnectToPeer(string ip, int port)
        {
            new Thread(() =>
            {
                try
                {
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    s.Connect(ip, port);

                    string endpoint = s.RemoteEndPoint.ToString();
                    lock (lockObj) peers[endpoint] = s;
                    StatusChanged?.Invoke($"✅ Đã kết nối tới {endpoint}");
                    new Thread(() => ReceiveLoop(endpoint, s)) { IsBackground = true }.Start();
                }
                catch (Exception ex)
                {
                    StatusChanged?.Invoke($"⚠️ Kết nối thất bại: {ex.Message}");
                }
            })
            { IsBackground = true }.Start();
        }
        public void SendMessage(string endpoint, string message)
        {
            lock (lockObj)
            {
                Socket s=null;
                if (!peers.ContainsKey(endpoint))
                {
                    if (!socketsInfo.ContainsValue(endpoint))
                    {
                        throw new Exception($"Gửi tin nhắn tới {endpoint} thất bại");
                    }
                    s = socketsInfo.FirstOrDefault(x => x.Value == endpoint).Key;
                }
                if(s == null)
                    s = peers[endpoint];
                if(s == null)
                {
                    throw new Exception($"Gửi tin nhắn tới {endpoint} thất bại");
                }
                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] len = BitConverter.GetBytes(data.Length);
                s.Send(len);
                s.Send(data);
            }
        }
        public void Stop()
        {
            stopRequested = true;
            lock (lockObj)
            {
                foreach (var kv in peers)
                    kv.Value.Close();
                peers.Clear();
            }
            listener?.Close();
        }
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
        public static int GetFreePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
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
