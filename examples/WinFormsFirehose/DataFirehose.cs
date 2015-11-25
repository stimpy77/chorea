using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chorea;

namespace WinFormsFirehose
{
    public class DataFirehose : MicroServiceThreadedProcess, IHasPublishedMessages<QueueMessage>, IPublishMessage<QueueMessage>, IPausable
    {
        readonly int _firehoseIndex = ++_firehoseCount;
        static int _firehoseCount;
        private bool _paused;
        private long _ctr;

        public override void Run()
        {
            ((LocalMessagePublishContainer<QueueMessage>) PublishedMessages).Threshold = 10;
            while (!Stopped)
            {
                if (!_paused) Publish(new QueueMessage("Firehose", _firehoseIndex + " message: " + _ctr++ + "-" + Guid.NewGuid().ToString()));
                System.Threading.Thread.Sleep(10);
            }
        }

        public IPublishedMessages<QueueMessage> PublishedMessages { get; set; } = new LocalMessagePublishContainer<QueueMessage>();

        public void Publish(QueueMessage message)
            => ((LocalMessagePublishContainer<QueueMessage>)PublishedMessages).Publish(message);

        public void Publish(string route, QueueMessage message)
            => ((LocalMessagePublishContainer<QueueMessage>)PublishedMessages).Publish(message);

        public void Pause()
        {
            _paused = true;
        }

        public void Continue()
        {
            _paused = false;
        }
    }
}
