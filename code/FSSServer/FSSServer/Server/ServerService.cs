using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using FSSServer;

namespace Server
{
    public class ServerService
    {
        private TcpClient client;
        private NetworkStream clientStream;
        public delegate void SetTextCallback(string s);
        private ServerForm owner;
        TcpServers tcpServers;

        public TcpClient connectedClient
        {
            get { return client; }
            set { client = value; }

        }

        #region Constructors
        ServerService() { }

        /// <summary>
        /// Constructor which accepts Client TCP object
        /// </summary>
        /// <param name="tcpClient"></param>
        public ServerService(ServerForm parent, TcpClient tcpClient)
        {
            this.owner = parent;
            tcpServers = new TcpServers(this.owner);
            // Get Stream Object
            connectedClient = tcpClient;
            clientStream = tcpClient.GetStream();

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = connectedClient.Client;

            //Call Asynchronous Receive Function
            connectedClient.Client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(OnReceive), state);

            //connectedClient.Client.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), state);
        }

        #endregion
        
        /// <summary>
        /// This function is used to display data in Rich Text Box
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            //Variable.RECEIVETEXT = text;
            //Console.WriteLine("---" + text);

            if (text.IndexOf("ClientInfo") > 0)
            {
                try
                {
                    ClientInfo clientInfo = new ClientInfo(text);
                    if (clientInfo.Computer == 1)
                    {
                        Variable.RECEIVETEXT1 = text;
                    }
                    if (clientInfo.Computer == 2)
                    {
                        Variable.RECEIVETEXT2 = text;
                    }
                    if (clientInfo.Computer == 3)
                    {
                        Variable.RECEIVETEXT3 = text;
                    }
                }
                catch (Exception){}
               
            }
            else if (text.IndexOf("ClockInfo") > 0)
            {
               Variable.RECEIVETEXTCLOCK = text;
            }
        }

        #region Send/Receive Data From Scokets
        public void SentText(string str)
        {
            byte[] bt;
            bt = Encoding.UTF8.GetBytes(str);
            connectedClient.Client.Send(bt);
        }


        /// <summary>
        /// Asynchronous Callback function which receives data from Server
        /// </summary>
        /// <param name="ar"></param>
        public void OnReceive(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead;

            if (handler.Connected)
            {

                // Read data from the client socket. 
                try
                {
                    bytesRead = handler.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        // There  might be more data, so store the data received so far.
                        state.sb.Remove(0, state.sb.Length);
                        state.sb.Append(Encoding.UTF8.GetString(
                                         state.buffer, 0, bytesRead));

                        // Display Text in Rich Text Box
                        content = state.sb.ToString();
                        SetText(content);
                        
                        

                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(OnReceive), state);

                    }
                }

                catch (SocketException socketException)
                {
                    //WSAECONNRESET, the other side closed impolitely
                    if (socketException.ErrorCode == 10054 || ((socketException.ErrorCode != 10004) && (socketException.ErrorCode != 10053)))
                    {
                        // Complete the disconnect request.
                        String remoteIP = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
                        String remotePort = ((IPEndPoint)handler.RemoteEndPoint).Port.ToString();
                        tcpServers.DisconnectClient(remoteIP, remotePort);

                        handler.Close();
                        handler = null;

                    }
                }

            // Eat up exception....Hmmmm I'm love FVC!!!
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\n" + exception.StackTrace);
                    
                }
            }
        }

        #endregion


        #region StateObject Class Definition
        /// <summary>
        /// StateObject Class to read data from Client
        /// </summary>
        public class StateObject
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            // Received data string.
            public StringBuilder sb = new StringBuilder();
        }
        #endregion

    }
}
