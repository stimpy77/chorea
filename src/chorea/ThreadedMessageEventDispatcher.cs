using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Chorea
{
    public class ThreadedMessageEventDispatcher<TMessage> : MicroServiceThreadedProcess, IMessageEventDispatcher<TMessage>, IDisposable
    {
        public ThreadedMessageEventDispatcher(string route = null)
        {
            Starting += OnStarting;
            Stopping += OnStopping;
            _route = route;
        }

        private void OnStarting(object sender, EventArgs eventArgs)
        {
            foreach (var startable in _microServices.Select(service => service as IStartable))
                startable?.Start();
        }

        private void OnStopping(object sender, EventArgs eventArgs)
        {
            foreach (var stoppable in _microServices.Select(service => service as IStoppable))
            {
                stoppable?.Stop();
                Thread.Yield();
            }
        }

        public event EventHandler<MessageEventArgs<TMessage>> MessageReceived;
        readonly List<object> _microServices = new List<object>();
        private readonly string _route;

        public virtual void RegisterMessageSource(IPublishedMessages<TMessage> service)
        {
            _microServices.Add(new PublishedMessagesContainer<TMessage>(service));
        }
        public virtual void RegisterMessageSource(IHasPublishedMessages<TMessage> service)
        {
            _microServices.Add(service);
        }
        public virtual void RegisterMessageSource<T>(IHasMessageQueue<T> service)
        {
            _microServices.Add(service);
        }

        public override void Run()
        {
            // repeat indefinitely
            while (!Stopped)
            {
                // iterate over known IHasMessageQueue services
                foreach (IHasMessageQueue<TMessage> service in _microServices.Where(s => s is IHasMessageQueue<TMessage>))
                {
                    // flush this service's queue
                    while (!service.MessageQueue.IsEmpty)
                    {
                        TMessage message;
                        if (service.MessageQueue.TryDequeue(out message))
                            MessageReceived?.Invoke(this, new MessageEventArgs<TMessage>(new KeyValuePair<string, TMessage>(service.QueueName, message)));
                    }
                }
                // iterate over known IHasPublishedMessages services
                foreach (IHasPublishedMessages<TMessage> service in _microServices.Where(s => s is IHasPublishedMessages<TMessage>))
                {
                    // flush this service's queue
                    //var message = service.MessageQueue.Dequeue();
                    //MessageReceived?.Invoke(this, new MessageEventArgs(message));
                    foreach (var message in service.PublishedMessages.GetAllPublishedMessagesSinceLast(_route))
                    {
                        MessageReceived?.Invoke(this, new MessageEventArgs<TMessage>(message));
                    }
                }
                Thread.Sleep(10);
            }
        }

        public override void Stop()
        {
            foreach (IStoppable stoppable in _microServices.Where(service => service is IStoppable))
                stoppable.Stop();
            base.Stop();
        }

        void IDisposable.Dispose()
        {
            if (!Stopped) Stop();
            foreach (IDisposable disposable in _microServices.Where(service => service is IDisposable))
                disposable.Dispose();

        }
    }
}
