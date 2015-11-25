using System;
using System.Collections.Generic;
using System.Linq;

namespace Chorea
{
    public class LocalMessagePublishContainer<TMessage> : IPublishedMessages<TMessage>, IPublishMessage<TMessage>
    {
        int _last;
        readonly string _intendedRecipient;

        public LocalMessagePublishContainer(string intendedRecipient = null)
        {
            _intendedRecipient = intendedRecipient ?? "*";
        }
        readonly List<KeyValuePair<string, TMessage>> _messages = new List<KeyValuePair<string, TMessage>>();
        public IEnumerable<KeyValuePair<string, TMessage>> GetAllPublishedMessages(string recipientKey = null)
        {
            return _messages.Where(m=>recipientKey == null || m.Key == recipientKey);
        }

        public IEnumerable<KeyValuePair<string, TMessage>> GetAllPublishedMessagesSince(object id, string recipientKey = null)
        {
            recipientKey = recipientKey ?? _intendedRecipient;
            if (!(id is int)) throw new ArgumentException("This container uses System.Int32 as the message ID.");
            var idx = (int) id;
            var count = _messages.Count;
            for (var i = idx + 1; i < count && i >= 0; i++)
            {
                if (recipientKey == null || _messages[i].Key == recipientKey)
                    yield return _messages[i];
            }
            _last = count - 1;
        }

        public IEnumerable<KeyValuePair<string, TMessage>> GetAllPublishedMessagesSinceLast(string recipientKey = null)
        {
            recipientKey = recipientKey ?? _intendedRecipient;
            return GetAllPublishedMessagesSince(_last).Where(
                m => recipientKey == null || recipientKey == "*" || m.Key == recipientKey);
        }

        public void Publish(TMessage message)
        {
            _messages.Add(new KeyValuePair<string, TMessage>("*", message));
            if (Threshold < _messages.Count)
            {
                lock (_messages)
                {
                    _messages.RemoveAt(0);
                    _last--;
                }

            }
        }

        public void Publish(string intendedRecipient, TMessage message)
        {
            _messages.Add(new KeyValuePair<string, TMessage>(intendedRecipient, message));
        }

        public int Threshold { get; set; } = 1048576;
    }
}