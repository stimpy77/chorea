using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace WinFormsFirehose
{
    public class DataFirehose : MicroServiceThreadedProcess, IHasPublishedMessages, IPublishMessage
    {
        private readonly int _firehoseIndex = ++_firehoseCount;
        private static int _firehoseCount;

        public override void Run()
        {
            while (!Stopped)
            {
                Publish(new QueueMessage("Firehose", "Firehose " + _firehoseIndex + " message: " + Guid.NewGuid().ToString()));
                System.Threading.Thread.Sleep(10);
            }
        }

        public IPublishedMessages PublishedMessages { get; set; } = new LocalMessagePublishContainer();

        public void Publish(object message)
        {
            ((LocalMessagePublishContainer)PublishedMessages).Publish(message);
        }
    }
}
