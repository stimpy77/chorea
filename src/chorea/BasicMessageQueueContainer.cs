using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public class BasicMessageQueueContainer : IEnqueueMessage
    {
        public BasicMessageQueueContainer()
        {
            MessageQueue = new ConcurrentQueue<object>();
        }
        public void EnqueueMessage(object message)
        {
            MessageQueue.Enqueue(message);
        }
        public ConcurrentQueue<object> MessageQueue { get; set; }
    }
}
