using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public class MessageEventArgs<TMessage> : EventArgs
    {
        public MessageEventArgs(string queueName, TMessage message)
        {
            Message = new KeyValuePair<string, TMessage>(queueName, message);
        }

        public MessageEventArgs(KeyValuePair<string, TMessage> message)
        {
            Message = message;
        }
        public KeyValuePair<string, TMessage> Message { get; set; }
    }
}
