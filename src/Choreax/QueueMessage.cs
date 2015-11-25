using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    [Serializable]
    public class QueueMessage
    {
        public QueueMessage(string messageType = null, string message = null)
        {
            MessageType = messageType ?? "";
            Message = message ?? "";
        }
        public QueueMessage() { }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return MessageType + ":" + Message;
        }
    }
}
