using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace BasicNamedMicroservice
{
    public class BrokenRecordResponder
    {
        IMessageEventDispatcher<BrokenRecordRequest> _messagesSource;

        public BrokenRecordResponder(IMessageEventDispatcher<BrokenRecordRequest> messagesSource)
        {
            _messagesSource = messagesSource;
            _messagesSource.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(object sender, MessageEventArgs<BrokenRecordRequest> messageEventArgs)
        {
            var msg = messageEventArgs.Message.Value as BrokenRecordRequest;
            Console.WriteLine(msg?.Message);
            if (messageEventArgs.Message.Key == "Marco")
            {
                Console.WriteLine("Polo!");
            }
        }
    }
}
