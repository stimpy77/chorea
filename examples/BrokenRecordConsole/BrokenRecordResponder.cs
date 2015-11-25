using System;
using Chorea;

namespace BrokenRecordConsole
{
    public class BrokenRecordResponder
    {
        public BrokenRecordResponder(IMessageEventDispatcher<BrokenRecordRequest> messagesSource)
        {
            messagesSource.MessageReceived += OnMessageReceived;
        }

        void OnMessageReceived(object sender, MessageEventArgs<BrokenRecordRequest> messageEventArgs)
        {
            var msg = messageEventArgs.Message.Value;
            Console.WriteLine(msg?.Message);
            if (messageEventArgs.Message.Key == "Marco")
            {
                Console.WriteLine("Polo!");
            }
        }
    }
}
