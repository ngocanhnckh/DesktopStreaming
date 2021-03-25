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
    public class AudioClient
    {
        private Socket _client_socket;
        public int BufferSize = 4000;
        MainPage parent;

        public AudioClient(MainPage p)
        {
            parent = p;
        }

        public void Conncet(string IP_Address)
        {
            _client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs()
            {
                RemoteEndPoint = new IPEndPoint(IPAddress.Parse("39.47.95.62"), 4532)
            };
            socketEventArg.Completed += parent.OnConncetCompleted;
            _client_socket.ConnectAsync(socketEventArg);
        }

        public void Send_Bytes(byte[] buffer)
        {
            _client_socket.NoDelay = true;

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.Completed += parent.OnSendCompleted;
            socketEventArg.SetBuffer(buffer, 0, buffer.Length);
            _client_socket.SendAsync(socketEventArg);
        }

        public void StartReceiving()
        {
            byte[] response = new byte[BufferSize];
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.Completed += parent.OnReceiveCompleted;
            socketEventArg.SetBuffer(response, 0, response.Length);
            _client_socket.ReceiveAsync(socketEventArg);
        }

    }
}
