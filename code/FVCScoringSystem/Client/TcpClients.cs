using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public class TcpClients
    {
        byte[] data = new byte[1024];
        TcpClient server;
        NetworkStream ns;

        public TcpClients()
        {
        }

        //Tạo connect tới server đã được mở sẵn
        public Boolean Connection()
        {
            try
            {
                server = new TcpClient(Variable.IP, Variable.PORT);
            }
            catch (SocketException)
            {
                MessageBox.Show("Không thể kết nối");
                return false;
            }
            //MessageBox.Show("Kết nối thành công");
            ns = server.GetStream();
            StateObject state = new StateObject();
            state.workSocket = server.Client;
            server.Client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback((new TcpClients()).OnReceive), state);
            return true;
        }

        //Gửi qua server một đoạn text
        public void Sent(string str)
        {
            byte[] bt;
            bt = Encoding.UTF8.GetBytes(str);
            ns.Write(bt, 0, bt.Length);
            ns.Flush();
        }

        public void Disconnect()
        {
            MessageBox.Show("Disconnecting from server...");
            ns.Close();
            server.Close();
        }

        //Sự kiện đợi nhận dữ liệu từ server
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

                        //Text
                        content = state.sb.ToString();
                        Variable.RECEIVETEXT = content;

                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(OnReceive), state);

                    }
                }

                catch (SocketException socketException)
                {
                    //WSAECONNRESET, the other side closed impolitely
                    if (socketException.ErrorCode == 10054 || ((socketException.ErrorCode != 10004) && (socketException.ErrorCode != 10053)))
                    {
                        handler.Close();
                    }
                }

            // Eat up exception....Hmmmm I'm loving eat!!!
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\n" + exception.StackTrace);

                }
            }
        }
    }
}
