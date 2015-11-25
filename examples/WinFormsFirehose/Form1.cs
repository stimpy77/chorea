using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chorea;
using Chorea.Windows;

namespace WinFormsFirehose
{
    public partial class Form1 : Form
    {
        private readonly EventPumpMessageEventDispatcher<QueueMessage> _messageDispatcher;
        private bool _paused;

        public Form1()
        {
            InitializeComponent();
            _messageDispatcher = new EventPumpMessageEventDispatcher<QueueMessage>();
            _messageDispatcher.MessageReceived += OnFirehoseMessageReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _messageDispatcher.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _messageDispatcher.Stop();
        }

        private void OnFirehoseMessageReceived(object sender, MessageEventArgs<QueueMessage> messageEventArgs)
        {
            if (lstMessages.Items.Count > 40) lstMessages.Items.RemoveAt(0);
            var message = messageEventArgs.Message.Value;
            lstMessages.Items.Add(message);
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
        }

        private void AddNewFirehose()
        {
            var firehose = new DataFirehose();
            _messageDispatcher.RegisterMessageSource(firehose);
            firehose.Start();
            if (_paused) firehose.Pause();
            UpdateFirehoseCountLabel();
        }

        private void RemoveFirehose()
        {
            if (_messageDispatcher.MessageSources.Count == 0) return;
            var firehose = _messageDispatcher.MessageSources[0] as DataFirehose;
            firehose?.Stop();
            _messageDispatcher.MessageSources.RemoveAt(0);
            UpdateFirehoseCountLabel();
        }

        private void cmdAddFirehose_Click(object sender, EventArgs e)
        {
            AddNewFirehose();
        }

        private void UpdateFirehoseCountLabel()
        {
            lblFirehoseCount.Text = "Firehose count: " + _messageDispatcher.MessageSources.Count;
        }

        void cmdRemoveFirehose_Click(object sender, EventArgs e)
        {
            RemoveFirehose();
        }

        void cmdPauseContinue_Click(object sender, EventArgs e)
        {
            if (_paused)
            {
                Continue();
                cmdPauseContinue.Text = "Pause";
            }
            else
            {
                Pause();
                cmdPauseContinue.Text = "Continue";
            }
        }

        private void Pause()
        {
            _paused = true;
            _messageDispatcher.Pause();
        }

        private void Continue()
        {
            _paused = false;
            _messageDispatcher.Continue();
        }
    }
}
