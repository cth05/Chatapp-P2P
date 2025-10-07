# Ứng dụng Chat P2P bằng C#

## 📖 Giới thiệu

Ứng dụng chat P2P (peer-to-peer) xây dựng bằng C# WinForms, sử dụng Socket TCP để kết nối giữa client và server. Hệ thống hỗ trợ mã hóa đầu cuối (end-to-end) bằng RSA + AES để đảm bảo bảo mật khi truyền tin.

## ⚙️ Tính năng chính

- Kết nối client ↔ server qua mạng LAN bằng TCP socket
- Gửi nhận tin nhắn hai chiều theo giao thức length-prefix
- Mã hóa RSA 2048 + AES 256 cho tin nhắn
- Hiển thị chat bubble (căn trái/phải) và system message (ở giữa)
- Giao diện thân thiện, dễ sử dụng

## 🧩 Thành phần chính

| File | Mô tả |
|------|-------|
| `ChatSockets.cs` | Xử lý kết nối, gửi/nhận dữ liệu socket |
| `CryptoHelper.cs` | Mã hóa & giải mã tin nhắn RSA/AES |
| `ChatMessage.cs` | Định nghĩa cấu trúc tin nhắn |
| `fServer.cs` | Form phía Server |
| `fClient.cs` | Form phía Client |
| `fChat.cs` | Giao diện chat chính |

## 💬 Quy trình hoạt động

1. Server mở ứng dụng → Listen
2. Client nhập IP và Port → Connect tới server
3. Hai bên trao đổi khóa RSA/AES
4. Người dùng chat → tin nhắn được mã hóa → gửi/nhận và giải mã

## 💻 Công nghệ sử dụng

- **Ngôn ngữ**: C# (.NET WinForms)
- **Giao tiếp**: TCP Socket
- **Mã hóa**: RSA 2048-bit + AES 256-bit
- **IDE**: Visual Studio

## 🚀 Hướng dẫn sử dụng

### Yêu cầu hệ thống

- .NET Framework 4.7.2 trở lên
- Visual Studio 2019 trở lên
- Windows 7/8/10/11

### Cài đặt

1. Clone repository này về máy:
```bash
git clone [<https://github.com/cth05/Chatapp-P2P>]
```

2. Mở solution trong Visual Studio

3. Cấu hình build:
   - Mở file `Program.cs`
   - **Để build Client**: Giữ nguyên `Application.Run(new fClient());`
   - **Để build Server**: Đổi thành `Application.Run(new fServer());`

```csharp
// Program.cs
namespace Chatapp_P2P
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Đổi fClient() thành fServer() để build Server
            Application.Run(new fClient());
        }
    }
}
```

4. Build solution (Ctrl + Shift + B)

### Chạy ứng dụng

**Phía Server:**
1. Chạy ứng dụng Server
2. Nhấn nút "Listen" để bắt đầu lắng nghe kết nối

**Phía Client:**
1. Chạy ứng dụng Client
2. Nhập địa chỉ IP và Port của Server
3. Nhấn "Connect" để kết nối
4. Bắt đầu chat sau khi kết nối thành công
