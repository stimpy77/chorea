using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorea
{
    public interface IMessageEventDispatcher<TMessage>
    {
        event EventHandler<MessageEventArgs<TMessage>> MessageReceived;
    }
}
