using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public interface IHasPublishedMessages<TMessage>
    {
        IPublishedMessages<TMessage> PublishedMessages { get; }
    }

    public interface IPublishedMessages<TMessage>
    {
        IEnumerable<KeyValuePair<string, TMessage>> GetAllPublishedMessages(string recipientKey = null);
        IEnumerable<KeyValuePair<string, TMessage>> GetAllPublishedMessagesSince(object id, string recipientKey = null);
        IEnumerable<KeyValuePair<string, TMessage>> GetAllPublishedMessagesSinceLast(string recipientKey = null);
    }
}
