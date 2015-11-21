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

namespace WinFormsFirehose
{
    public partial class Form1 : Form
    {
        private readonly MessageEventPumpDispatcher _microserviceMessageDispatcher;

        public Form1()
        {
            InitializeComponent();
            _microserviceMessageDispatcher = new MessageEventPumpDispatcher();
            _microserviceMessageDispatcher.MessageReceived += MicroserviceMessageDispatcherOnMessageReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _microserviceMessageDispatcher.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _microserviceMessageDispatcher.Stop();
        }

        private void MicroserviceMessageDispatcherOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            if (lstMessages.Items.Count > 100) lstMessages.Items.RemoveAt(0);
            lstMessages.Items.Add(messageEventArgs.Message);
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
        }

        private void AddNewFirehose()
        {
            var firehose = new DataFirehose();
            firehose.Start();
            _microserviceMessageDispatcher.RegisterMessageSource(firehose);
            UpdateFirehoseCountLabel();
        }

        private void RemoveFirehose()
        {
            if (_microserviceMessageDispatcher.MessageSources.Count == 0) return;
            var firehose = _microserviceMessageDispatcher.MessageSources[0] as DataFirehose;
            firehose?.Stop();
            _microserviceMessageDispatcher.MessageSources.RemoveAt(0);
            UpdateFirehoseCountLabel();
        }

        private void cmdAddFirehose_Click(object sender, EventArgs e)
        {
            AddNewFirehose();
        }

        private void UpdateFirehoseCountLabel()
        {
            lblFirehoseCount.Text = "Firehose count: " + _microserviceMessageDispatcher.MessageSources.Count;
        }

        private void cmdRemoveFirehose_Click(object sender, EventArgs e)
        {
            RemoveFirehose();
        }
    }
}
