using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public class BasicMessageQueueContainer<T> : IHasBclMessageQueue<T>, IEnqueueMessage<T>
    {
        public BasicMessageQueueContainer(string queueName = null)
        {
            MessageQueue = new ConcurrentQueue<T>();
            QueueName = queueName ?? "*";
        }
        public void EnqueueMessage(string route, T message)
        {
            MessageQueue.Enqueue(message);
        }
        public ConcurrentQueue<T> MessageQueue { get; set; }

        public string QueueName { get; set; }
    }
}
