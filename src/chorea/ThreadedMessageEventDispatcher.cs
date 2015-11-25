using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Chorea
{
    public class ThreadedMessageEventDispatcher<TMessage> : MicroServiceThreadedProcess, IMessageEventDispatcher<TMessage>, IDisposable
    {
        public ThreadedMessageEventDispatcher(string intendedRecipient = null)
        {
            Starting += OnStarting;
            Stopping += OnStopping;
            _intendedRecipient = intendedRecipient;
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
        private readonly string _intendedRecipient;

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
                    foreach (var message in service.PublishedMessages.GetAllPublishedMessagesSinceLast(_intendedRecipient))
                    {
                        MessageReceived?.Invoke(this, new MessageEventArgs<TMessage>(message));
                    }
                }
                Thread.Sleep(10);
            }
        }

        void IDisposable.Dispose()
        {
            if (!Stopped) Stop();
            foreach (var disposable in _microServices.Select(service => service as IDisposable))
            {
                disposable?.Dispose();
            }

        }
    }
}
