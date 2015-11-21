using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public interface IHasPublishedMessages
    {
        IPublishedMessages PublishedMessages { get; }
    }

    public interface IPublishedMessages
    {
        IEnumerable<object> GetPublishedMessages();
        IEnumerable<object> GetPublishedMessagesSince(object id);
        IEnumerable<object> GetPublishedMessagesSinceLast();
    }
}
