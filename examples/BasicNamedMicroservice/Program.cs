using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chorea;

namespace BasicNamedMicroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dispatcher = new ThreadedMessageEventDispatcher<BrokenRecordRequest>("Marco"))
            {
                var messageSystem = new BasicMessageQueueContainer<BrokenRecordRequest>("Marco");
                    //new LocalMessagePublishContainer<BrokenRecordRequest>();
                var requester = new BrokenRecordRequester(messageSystem);
                dispatcher.RegisterMessageSource(messageSystem);
                var responder = new BrokenRecordResponder(dispatcher);
                dispatcher.Start();
                requester.Start();
                while (true)
                {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
