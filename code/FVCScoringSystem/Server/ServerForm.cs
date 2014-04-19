using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form
    {
        TcpServers tcpServers;
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;


        public ServerForm()
        {
            InitializeComponent();
            tcpServers = new TcpServers(this);
            Variable.SERVERSERVICES = new List<ServerService>();
            Variable.THREADS = new List<Thread>();

            // Add Event to handle when a client is connected
            Changed += new ChangedEventHandler(tcpServers.ClientAdded);

            tcpServers.StartServer();
            tmrServer.Enabled = true;
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpServers.StopServer();
        }

        private void tmrServer_Tick(object sender, EventArgs e)
        {
            foreach (var sv in Variable.SERVERSERVICES)
            {
                Variable.SENTTEXT = DateTime.Now.Second.ToString();
                sv.SentText(Variable.SENTTEXT);
            }
            lblClock.Text = Variable.RECEIVETEXT;
        }
      
    }
}
