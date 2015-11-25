using System;
using Chorea;

namespace BrokenRecordConsole
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            var messageSystem = 
            //new BasicMessageQueueContainer<BrokenRecordRequest>("Marco");
            new LocalMessagePublishContainer<BrokenRecordRequest>("Marco");
            using (var requester = new BrokenRecordRequester(messageSystem))
            {
                using (var dispatcher = new ThreadedMessageEventDispatcher<BrokenRecordRequest>("Marco"))
                {
                    dispatcher.RegisterMessageSource(messageSystem);
                    var responder = new BrokenRecordResponder(dispatcher);
                    dispatcher.Start();

                    requester.Start();

                    Console.WriteLine("Press ENTER to exit ...");
                    Console.ReadLine();
                }

            }
        }
    }
}
