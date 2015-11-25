using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public interface IPublishMessage<TMessage>
    {
        void Publish(string route, TMessage message);
    }
}
