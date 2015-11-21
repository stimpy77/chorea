using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chorea
{
    public abstract class MicroServiceEventPumpTimerProcess : IStartable, IStoppable, IDisposable
    {
        public event EventHandler Starting;
        public event EventHandler Stopping;

        readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        protected MicroServiceEventPumpTimerProcess()
        {
            _timer.Interval = 10;
            _timer.Tick += (sender, args) => Tick();
        }

        public int Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }
        
        protected bool Stopped { get; set; }
        public virtual void Start()
        {
            this.Stopped = false;
            this.Starting?.Invoke(this, new EventArgs());
            _timer.Enabled = true;
            _timer.Start();
        }

        public virtual void Stop()
        {
            Stopped = true;
            Stopping?.Invoke(this, new EventArgs());
            _timer.Stop();
        }

        public virtual bool IsRunning => _timer.Enabled;

        public abstract void Tick();

        public void Dispose()
        {
            if (IsRunning) _timer.Stop();
        }
    }
}
