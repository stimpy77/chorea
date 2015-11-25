using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public interface IHasMessageQueue<TMessage>
    {
        ConcurrentQueue<TMessage> MessageQueue { get; }
        string QueueName { get; }
    }
}
