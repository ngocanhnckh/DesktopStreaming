using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net.Sockets;

namespace wbclient
{
    public class DrawingImageClient
    {
        private Socket Drawsocket;
        private static int DPORT = 4531;
        private static DnsEndPoint DIP_END_POINT = new DnsEndPoint("39.47.95.62", DPORT);
        private SocketAsyncEventArgs DrawsocketEventArg;
        private static int BUFFER_SIZE = 1280000;
        private MainPage parent;
        public DrawingImageClient(MainPage p)
        {
            parent = p;
        }
        public void Connect()
        {
            DrawsocketEventArg = new SocketAsyncEventArgs();
            Drawsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            DrawsocketEventArg.RemoteEndPoint = DIP_END_POINT;
            DrawsocketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(DrawOnConncetCompleted);
            Drawsocket.ConnectAsync(DrawsocketEventArg);
        }
        void DrawOnConncetCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                DrawStartReceiving();
                parent.Dispatcher.BeginInvoke(parent.showmessage, "Connected to Drawing Image Server");
            }
            else
                parent.Dispatcher.BeginInvoke(parent.showmessage,e.SocketError.ToString());
        }
        public void DrawStartReceiving()
        {
            byte[] response = new byte[BUFFER_SIZE];
            SocketAsyncEventArgs DrawsocketEventArg = new SocketAsyncEventArgs();
            DrawsocketEventArg.Completed += DrawOnReceiveCompleted;
            DrawsocketEventArg.SetBuffer(response, 0, response.Length);
            Drawsocket.ReceiveAsync(DrawsocketEventArg);
        }
        void DrawOnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            parent.Dispatcher.BeginInvoke(parent.drimgobj, e.Buffer);
        }
        public void Send_Bytes(byte[] buffer)
        {
            SocketAsyncEventArgs DrawsocketEventArg = new SocketAsyncEventArgs();
            DrawsocketEventArg.Completed += DrawOnSendCompleted;
            DrawsocketEventArg.SetBuffer(buffer, 0, buffer.Length);
            Drawsocket.SendAsync(DrawsocketEventArg);
        }
        void DrawOnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            //Show Message or something ...
            parent.Dispatcher.BeginInvoke(parent.showmessage, "Sent Successfully!");
        }

    }
}
