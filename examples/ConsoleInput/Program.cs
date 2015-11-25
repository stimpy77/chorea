using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chorea;

namespace ConsoleInput
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var messageEventDispatcher = new ThreadedMessageEventDispatcher<QueueMessage>())
            {
                var commandInputLoop = new ConsoleCommandInputService();
                messageEventDispatcher.RegisterMessageSource(commandInputLoop);
                var commandProcessor = new CommandProcessor(messageEventDispatcher);
                messageEventDispatcher.Start();
                while (messageEventDispatcher.IsRunning) Thread.Sleep(100);
            }
        }
    }
}
