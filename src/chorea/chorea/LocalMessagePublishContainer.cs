using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public class LocalMessagePublishContainer : IPublishedMessages, IPublishMessage
    {
        private readonly ArrayList _messages = new ArrayList();
        public int last;

        public IEnumerable<object> GetPublishedMessages()
        {
            return _messages.Cast<object>();
        }

        public IEnumerable<object> GetPublishedMessagesSince(object id)
        {
            if (!(id is int)) throw new ArgumentException("This container uses System.Int32 as the message ID.");
            var idx = (int) id;
            var count = _messages.Count;
            for (var i = idx + 1; i < count && i >= 0; i++)
            {
                yield return _messages[i];
            }
            last = count - 1;
        }

        public IEnumerable<object> GetPublishedMessagesSinceLast()
        {
            return GetPublishedMessagesSince(last);
        }

        public void Publish<T>(T message)
        {
            _messages.Add(message);
            if (Threshold < _messages.Count)
            {
                lock (_messages)
                {
                    _messages.RemoveAt(0);
                    last--;
                }

            }
        }

        public int Threshold { get; set; } = 1048576;
    }
}