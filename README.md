# Chat Peer-to-Peer (P2P) TCP/IP

Ứng dụng chat ngang hàng (P2P) được xây dựng bằng C# WinForms, cho phép hai hoặc nhiều người dùng giao tiếp trực tiếp qua mạng TCP/IP mà không cần máy chủ trung tâm.

## Tính năng chính

- **Chat tin nhắn văn bản**: Gửi và nhận tin nhắn thời gian thực
- **Chia sẻ hình ảnh**: Gửi hình ảnh được nén tự động (độ rộng tối đa 400px)
- **Chia sẻ file**: Gửi các tệp tin với xác nhận từ người nhận
- **Quản lý nhiều kết nối**: Hỗ trợ chat với nhiều người dùng đồng thời
- **Tự động cấp port**: Tìm port khả dụng nếu port mặc định (9000) bị chiếm dụng
- **Thông báo hệ thống**: Hiển thị thông báo khi có tin nhắn mới

## Yêu cầu hệ thống

- .NET Framework 4.8 trở lên
- Visual Studio 2019 hoặc cao hơn
- NuGet packages: `Newtonsoft.Json`, `AntdUI`

## Cách sử dụng

### 1. Khởi động ứng dụng

- Chạy chương trình, cửa sổ `fGetPortOpen` sẽ hiển thị
- Nhập tên máy (mặc định là tên máy tính)
- Chọn port (tự động hoặc nhập thủ công)
- IP địa chỉ được điền tự động từ IPv4 cục bộ

### 2. Kết nối với người dùng khác

- Nhấn nút **Connect** để mở cửa sổ kết nối
- Nhập IP và port của người dùng muốn kết nối
- Cả hai bên sẽ hiển thị nhau trong danh sách liên hệ

### 3. Gửi tin nhắn

- Chọn người dùng từ danh sách bên trái
- Nhập tin nhắn và nhấn **Send** hoặc Enter
- Hỗ trợ gửi ảnh (nút hình ảnh) hoặc file (nút upload)

## Kiến trúc chính

### Core - ChatSockets.cs
Quản lý kết nối TCP/IP:
- `Start()`: Bắt đầu lắng nghe trên port
- `ConnectToPeer()`: Kết nối đến peer khác
- `SendMessage()`: Gửi tin nhắn qua socket
- `MergeIPPort()`: Ánh xạ endpoint thực để gửi tin nhắn

### UI Forms

**fMain.cs**: Giao diện chính
- Hiển thị danh sách liên hệ và chat
- Xử lý nhận/gửi tin nhắn, hình ảnh, file
- Quản lý người dùng được kết nối

**fGetPortOpen.cs**: Cấu hình ban đầu
- Nhập tên người dùng
- Chọn port nghe

**fConnect.cs**: Kết nối đến peer
- Nhập IP và port của peer muốn kết nối

### Model

**ChatMessage.cs**: Định dạng tin nhắn
- `Type`: Loại tin nhắn (chat, image, file, info)
- `From/To`: Người gửi và nhận
- `Message`: Nội dung (văn bản hoặc Base64)
- `Note`: Tên file/hình ảnh
- `Timestamp`: Thời gian gửi

**Helper/ImageHelper.cs**: Nén hình ảnh
- Giảm kích thước ảnh trước khi gửi

## Ghi chú kỹ thuật

- **Serialization**: Sử dụng JSON để tuần tự hóa tin nhắn
- **Base64 Encoding**: Hình ảnh và file được mã hóa Base64 trước khi gửi
- **Thread-safe**: Sử dụng `Dictionary` với `lock` để tránh race condition
- **Protocol**: Gửi độ dài tin nhắn (4 byte) rồi dữ liệu để đảm bảo nhận đủ dữ liệu

## Các lỗi có thể gặp

| Lỗi | Nguyên nhân | Giải pháp |
|-----|-----------|----------|
| Port không khả dụng | Port đã được sử dụng | Chọn port khác hoặc sử dụng tự động |
| IP không đúng định dạng | Nhập IP không hợp lệ | Kiểm tra lại IP |
| Không gửi được tin nhắn | Mất kết nối với peer | Kiểm tra kết nối mạng, kết nối lại |

## Hướng phát triển

- Thêm mã hóa cho tin nhắn
- Hỗ trợ video call
- Lưu lịch sử chat
- Giao diện đẹp hơn
- Hỗ trợ group chat

## Tác giả

Bài tập học tập - Chat P2P TCP/IP
