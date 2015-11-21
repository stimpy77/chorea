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
        public override void Run()
        {
            while (!Stopped)
            {
                Publish(new QueueMessage("Some guid", Guid.NewGuid().ToString()));
                System.Threading.Thread.Sleep(100);
            }
        }

        public IPublishedMessages PublishedMessages { get; set; } = new LocalMessagePublishContainer();

        public void Publish<T>(T message)
        {
            ((LocalMessagePublishContainer)PublishedMessages).Publish(message);
        }
    }
}
