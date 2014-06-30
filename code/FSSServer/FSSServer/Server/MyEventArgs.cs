using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class MyEventArgs : EventArgs
    {
        private TcpClient sock;
        public TcpClient clientSock
        {
            get { return sock; }
            set { sock = value; }
        }

        public MyEventArgs(TcpClient tcpClient)
        {
            sock = tcpClient;
        }


    }
}
