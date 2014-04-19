using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class TcpServers
    {
        private TcpListener tcpServer;
        private TcpClient tcpClient;
        private Thread th;
        ServerService serverService;
        //List<ServerService> Variable.SERVERSERVICES = new List<ServerService>();
        //private ArrayList threadArray = new ArrayList();
       // List<Thread> threadArra = new List<Thread>();
        ServerForm ParentForm;

       public TcpServers() { }

       public TcpServers(ServerForm ParentForm)
        {
            this.ParentForm = ParentForm;
        }

        #region Server Start/Stop

        /// <summary>
        /// This function spawns new thread for TCP communication
        /// </summary>
        public void StartServer()
        {
            th = new Thread(new ThreadStart(StartListen));
            th.Start();

        }

        /// <summary>
        /// Server listens on the given port and accepts the connection from Client.
        /// As soon as the connection id made a dialog box opens up for Chatting.
        /// </summary>
        public void StartListen()
        {

            IPAddress localAddr = IPAddress.Parse(Variable.IP);
            int port = Variable.PORT;
            tcpServer = new TcpListener(localAddr, port);
            tcpServer.Start();

            // Keep on accepting Client Connection
            while (true)
            {
                // New Client connected, call Event to handle it.
                Thread t = new Thread(new ParameterizedThreadStart(NewClient));
                tcpClient = tcpServer.AcceptTcpClient();
                t.Start(tcpClient);

            }

        }

        /// <summary>
        /// Function to stop the TCP communication. It kills the thread and closes client connection
        /// </summary>
        public void StopServer()
        {

            if (tcpServer != null)
            {

                // Abort All Running Threads
                foreach (Thread t in Variable.THREADS)
                {
                    t.Abort();
                }

                // Clear all ArrayList
                Variable.THREADS.Clear();
                Variable.SERVERSERVICES.Clear();

                // Abort Listening Thread and Stop listening
                th.Abort();
                tcpServer.Stop();
            }
        }

        #endregion

        #region Add/remove Clients
        /// <summary>
        /// 
        /// </summary>
        public void NewClient(Object obj)
        {
            ClientAdded(ParentForm, new MyEventArgs((TcpClient)obj));
        }

        /// <summary>
        /// Event Fired when a Client gets connected. Following actions are performed
        /// 1. Update Tree view
        /// 2. Open a chat box to chat with client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClientAdded(object sender, EventArgs e)
        {
            tcpClient = ((MyEventArgs)e).clientSock;
            String remoteIP = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            String remotePort = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port.ToString();

            serverService = new ServerService(ParentForm, tcpClient);
            Variable.SERVERSERVICES.Add(serverService);
            Variable.THREADS.Add(Thread.CurrentThread);
        }



        public void DisconnectClient(String remoteIP, String remotePort)
        {
            // Find Client Chat Dialog box corresponding to this Socket
            int counter = 0;
            foreach (var sv in Variable.SERVERSERVICES)
            {
                String remoteIP1 = ((IPEndPoint)sv.connectedClient.Client.RemoteEndPoint).Address.ToString();
                String remotePort1 = ((IPEndPoint)sv.connectedClient.Client.RemoteEndPoint).Port.ToString();

                if (remoteIP1.Equals(remoteIP) && remotePort1.Equals(remotePort))
                {
                    break;
                }
                counter++;

            }

            // Terminate Chat Dialog Box
            Variable.SERVERSERVICES.RemoveAt(counter);

            ((Thread)(Variable.THREADS[counter])).Abort();
            Variable.THREADS.RemoveAt(counter);

        }
        #endregion
    }
}
