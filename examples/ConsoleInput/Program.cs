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
            using (var messageEventDispatcher = new MessageEventDispatcher())
            {
                messageEventDispatcher.RegisterMessageSource(new ConsoleCommandInputService());
                var commandProcessor = new CommandProcessor(messageEventDispatcher);
                messageEventDispatcher.Start();
                while (messageEventDispatcher.IsRunning) Thread.Sleep(100);
            }
        }
    }
}
