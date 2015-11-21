using System;
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
            MessageQueue = new Queue<object>();
        }
        public void EnqueueMessage(object message)
        {
            MessageQueue.Enqueue(message);
        }
        public Queue<object> MessageQueue { get; set; }
    }
}
