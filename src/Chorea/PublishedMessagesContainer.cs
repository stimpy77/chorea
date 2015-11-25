using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public class PublishedMessagesContainer<TMessage> : IHasPublishedMessages<TMessage>
    {
        public PublishedMessagesContainer(IPublishedMessages<TMessage> service)
        {
            PublishedMessages = service;
        }
        public IPublishedMessages<TMessage> PublishedMessages { get; }
    }
}
