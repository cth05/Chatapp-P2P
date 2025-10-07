using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatapp_P2P.Modal
{
    internal class ChatMessage
    {
        public string Type { get; set; } = "chat";
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
