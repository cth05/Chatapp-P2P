using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatapp_P2P.Model
{
    internal class ChatMessage
    {
        public string Type { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public string Message { get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
    internal class User
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Endpoint { get; set; }
    }
}
