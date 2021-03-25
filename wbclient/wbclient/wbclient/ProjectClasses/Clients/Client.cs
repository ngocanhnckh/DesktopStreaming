using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
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
    public class Client
    {
        private MainPage parent;
        private Socket socket;
        private byte[] transferBuffer;
        private TextMessage message;
        private SocketAsyncEventArgs SocketEventArgs;
        private static int BUFFER_SIZE = 1024;
        private static int TPORT = 4530;
        private static DnsEndPoint TIP_END_POINT = new DnsEndPoint("39.47.95.62", TPORT);
        public Client(MainPage p)
        {
            parent = p;
        }
        public void Connect()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs connectEventArgs = new SocketAsyncEventArgs();
            connectEventArgs.RemoteEndPoint = TIP_END_POINT;
            connectEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(connectEventArgs_Completed);
            socket.ConnectAsync(connectEventArgs);
        }
        // connection complete
        void connectEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            SocketEventArgs = new SocketAsyncEventArgs();
            SocketEventArgs.RemoteEndPoint = TIP_END_POINT;
            e.Completed -= new EventHandler<SocketAsyncEventArgs>(connectEventArgs_Completed);
            e.Completed += new EventHandler<SocketAsyncEventArgs>(receiveEventArgs_Completed);
            transferBuffer = new byte[BUFFER_SIZE];
            e.SetBuffer(transferBuffer, 0, transferBuffer.Length);
            socket.ReceiveAsync(e);
            //send connection parameters
            parent.Dispatcher.BeginInvoke(parent.condelobj);
        }
        // incoming data
        void receiveEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            message = new TextMessage(transferBuffer);
            parent.Set(message);
            try
            {
                socket.ReceiveAsync(e);
            }
            catch
            {
            }
        }
        public void Send(TextMessage msg)
        {
            List<ArraySegment<byte>> m = new List<ArraySegment<byte>>();
            m.Add(new ArraySegment<byte>(msg.ToByte()));
            SocketEventArgs.BufferList = m;
            socket.SendAsync(SocketEventArgs);
        }
    }
}
