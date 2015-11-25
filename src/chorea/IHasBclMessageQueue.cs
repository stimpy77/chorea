using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    /// <summary>
    /// When implemented, exposes a <see cref="MessageQueue"/> property which is a <see cref="ConcurrentQueue&lt;TMessage&gt;"/>.
    /// A <see cref="QueueName"/> is also declared.
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IHasBclMessageQueue<TMessage>
    {
        ConcurrentQueue<TMessage> MessageQueue { get; }
        string QueueName { get; }
    }
}
