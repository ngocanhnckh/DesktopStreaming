using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace wbserver
{
    public class ImageServer
    {
        static ArrayList ClientsList = new ArrayList();
        static Socket Listener_Socket;
        static DrawImageClient Newclient;
        private static int BUFFER_SIZE = 1580000;
        int DIPort;
        static wb xpoint;
        //yeah its clear about the server side, but what in the

        public ImageServer()
        {
            DIPort = 4531;
        }
        public ImageServer(int port, wb _point)
        {
            DIPort = port;
            xpoint = _point;
        }
        // (1) Establish The Server
        public string Start()
        {
            try
            {
                IPAddress[] AddressAr = null;
                String ServerHostName = "";

                try
                {
                    ServerHostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostByName(ServerHostName);
                    AddressAr = ipEntry.AddressList;
                }
                catch (Exception) { }

                if (AddressAr == null || AddressAr.Length < 1)
                {
                    return "Error: Unable to get local address";
                }

                Listener_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Listener_Socket.Bind(new IPEndPoint(IPAddress.Any, DIPort));
                Listener_Socket.Listen(-1);

                Listener_Socket.BeginAccept(new AsyncCallback(EndAccept), Listener_Socket);

                return ("Image Listening On " + AddressAr[0].ToString() + ":" + DIPort + "... OK");

            }
            catch (Exception ex) { return ex.Message; }
        }

        public string ShutDown()
        {
            try
            {
                Listener_Socket.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return ("Image Shuted down ...");
            }
            catch (Exception ex) { return (ex.Message); }

        }
        // (2) Accept Clients Conncetion
        private static void EndAccept(IAsyncResult ar)
        {
            try
            {
                Listener_Socket = (Socket)ar.AsyncState;
                AddClient(Listener_Socket.EndAccept(ar));
                Listener_Socket.BeginAccept(new AsyncCallback(EndAccept), Listener_Socket);
            }
            catch (Exception) { }
        }

        // (3) Create a Socket for Each Client and add his Socket to The ArrayList 
        private static void AddClient(Socket sockClient)
        {
            Newclient = new DrawImageClient(sockClient);

            ClientsList.Add(Newclient);

            // client.Sock.RemoteEndPoint - " has joined the room"
            Newclient.SetupRecieveCallback();
        }

        // (4) Send The Recieved Data to All Clients in The Room
        private static void OnRecievedData(IAsyncResult ar)
        {
            DrawImageClient client = (DrawImageClient)ar.AsyncState;



            byte[] aryRet = client.GetRecievedData(ar);


            if (aryRet.Length < 1)
            {
                // client.Sock.RemoteEndPoint - "has left the room"
                client.ReadOnlySocket.Close();
                ClientsList.Remove(client);
                return;
            }
            Message dm = new Message(aryRet);
            if (dm.enumCommand == Command.Image)
            {
                foreach (DrawImageClient clientSend in ClientsList)
                {
                    if (clientSend.room != null)
                        if (dm.strRoom == clientSend.room)
                            try
                            {
                                clientSend.ReadOnlySocket.NoDelay = true;
                                clientSend.ReadOnlySocket.Send(aryRet);
                            }
                            catch
                            {
                                clientSend.ReadOnlySocket.Close();
                                ClientsList.Remove(client);
                                return;
                            }
                }
            }
            else if (dm.enumCommand == Command.Screen)
            {
                xpoint.setimage(dm.databytes);

                foreach (DrawImageClient clientSend in ClientsList)
                {
                    if (clientSend.room != null)
                        if (dm.strRoom == clientSend.room)
                            try
                            {
                                clientSend.ReadOnlySocket.NoDelay = true;
                                clientSend.ReadOnlySocket.Send(aryRet);
                            }
                            catch
                            {
                                clientSend.ReadOnlySocket.Close();
                                ClientsList.Remove(client);
                                return;
                            }
                }
            }
            else if (dm.enumCommand == Command.Join)
                client.room = dm.strRoom;

            client.SetupRecieveCallback();
        }

        internal class DrawImageClient
        {
            // To create a new socket for each client 

            private Socket New_Socket;
            public string room;
            public string uname;
            private byte[] buffer = null;

            public DrawImageClient(Socket PassedSock)
            {
                New_Socket = PassedSock;
                New_Socket.NoDelay = true;
                room = null;
                uname = null;
            }

            public Socket ReadOnlySocket
            {
                get { return New_Socket; }
            }

            public void SetupRecieveCallback()
            {
                try
                {
                    buffer = new byte[BUFFER_SIZE];
                    AsyncCallback recieveData = new AsyncCallback(ImageServer.OnRecievedData);
                    New_Socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, recieveData, this);
                }
                catch (Exception)
                {
                }
            }
            public byte[] GetRecievedData(IAsyncResult ar)
            {
                int nBytesRec = 0;
                try
                {
                    nBytesRec = New_Socket.EndReceive(ar);
                }
                catch { }
                byte[] byReturn = new byte[nBytesRec];
                Array.Copy(buffer, byReturn, nBytesRec);
                return byReturn;
            }
        }

    }
}
