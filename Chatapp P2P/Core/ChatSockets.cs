using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Chatapp_P2P.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Chatapp_P2P.Core
{
    public class ChatSockets
    {
        public bool isServer { get; private set; }
        private Thread listenThread;
        private Thread receiveThread;
        public event Action<string> MessageReceived;
        public event Action<string> StatusChanged;
        private Socket listenerSocket;
        private Socket clientSocket;
        public int port { get; private set; }
        private Status code { get; set; }
        public ChatSockets(bool isServer)
        {
            this.isServer = isServer;
        }
        public void StartListening(IPAddress ip, int port)
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(ip, port));
            listenerSocket.Listen(10);

            listenThread = new Thread(AcceptClient);
            listenThread.IsBackground = true;
            listenThread.Start();

            StatusChanged?.Invoke($"Đang lắng nghe trên port {port}");
        }
        private void AcceptClient()
        {
            try
            {
                clientSocket = listenerSocket.Accept();
                code = Status.CONNECTED;
                StatusChanged?.Invoke("Đã có máy kết nối tới");
                StartReceiving();
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
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(ipAddr, port));

                StatusChanged?.Invoke("Kết nối thành công tới " + ipAddr + ":" + port);
                StartReceiving();
                code = Status.CONNECTED;
                this.port = port;
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke("Lỗi kết nối: " + ex.Message);
                throw;
            }
        }
        private void StartReceiving()
        {
            receiveThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        // đọc 4 byte độ dài
                        byte[] lengthBytes = new byte[4];
                        int read = clientSocket.Receive(lengthBytes, 0, 4, SocketFlags.None);
                        if (read == 0) break; // mất kết nối

                        int length = BitConverter.ToInt32(lengthBytes, 0);

                        // đọc dữ liệu
                        byte[] buffer = new byte[length];
                        int totalRead = 0;
                        while (totalRead < length)
                        {
                            int r = clientSocket.Receive(buffer, totalRead, length - totalRead, SocketFlags.None);
                            if (r == 0) throw new Exception("Ngắt kết nối");
                            totalRead += r;
                        }

                        string msg = Encoding.UTF8.GetString(buffer);
                        MessageReceived?.Invoke(msg);
                    }
                }
                catch
                {
                    StatusChanged?.Invoke("Ngắt kết nối");
                }
            });
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        public void SendMessage(string message)
        {
            if (clientSocket == null) return;

            byte[] data = Encoding.UTF8.GetBytes(message);
            byte[] lengthPrefix = BitConverter.GetBytes(data.Length);

            clientSocket.Send(lengthPrefix);
            clientSocket.Send(data);
        }
        public Status GetStatus()
        {
            return code;
        }

        public void Stop()
        {
            try
            {
                clientSocket?.Shutdown(SocketShutdown.Both);
                clientSocket?.Close();
                listenerSocket?.Close();
            }
            catch { }
        }
    }
    public class ChatSocketLibTcp
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
        public ChatSocketLibTcp(bool isServer)
        {
            this.isServer = isServer;
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
                code = Status.CONNECTED;
                StatusChanged?.Invoke("Đã có máy kết nối tới");
                StartReceiving();
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
                this.port= port;
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke("Lỗi kết nối: " + ex.Message);
                throw ex;
            }
        }
        private void StartReceiving()
        {
            receiveThread = new Thread(() =>
            {
                while (true)
                {
                    byte[] lengthBytes = new byte[4];
                    int read = stream.Read(lengthBytes, 0, 4);
                    if (read == 0) break; // mất kết nối

                    int length = BitConverter.ToInt32(lengthBytes, 0);

                    // đọc đúng số byte đã báo
                    byte[] buffer = new byte[length];
                    int totalRead = 0;
                    while (totalRead < length)
                    {
                        int r = stream.Read(buffer, totalRead, length - totalRead);
                        if (r == 0) throw new Exception("Ngắt kết nối");
                        totalRead += r;
                    }

                    string msg = Encoding.UTF8.GetString(buffer);
                    MessageReceived?.Invoke(msg);
                }
            });
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        public void SendMessage(string message)
        {
            if (stream == null) return;
            byte[] data = Encoding.UTF8.GetBytes(message);
            // gửi 4 byte đầu là độ dài message
            byte[] lengthPrefix = BitConverter.GetBytes(data.Length);
            stream.Write(lengthPrefix, 0, lengthPrefix.Length);
            stream.Write(data, 0, data.Length);
        }
        public Status GetStatus()
        {
            return code;
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
