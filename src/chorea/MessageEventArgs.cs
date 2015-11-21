using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(object message)
        {
            Message = message;
        }
        public object Message { get; set; }
    }
}
