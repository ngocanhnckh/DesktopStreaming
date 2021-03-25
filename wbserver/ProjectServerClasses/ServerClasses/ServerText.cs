using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace wbserver
{

    public class TextServer
    {
        static TClient Newclient;
        const int textBuffSize = 50000;
        public static ArrayList ClientsList = new ArrayList();
        Socket tlistner;
        int port;
        IPAddress[] AddressAr = null;
        static wb parent;
        public TextServer(wb p)
        {
            port = 4530;
            parent = p;
        }

        public TextServer(wb p, int tport)
        {
            port = tport;
            parent = p;
        }
        // (1) Starting the Text Server
        public string Start()
        {
            try
            {
                IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
                AddressAr = ipEntry.AddressList;
            }
            catch (Exception) { }

            if (AddressAr == null || AddressAr.Length < 1)
            {
                return "Error: Unable to get local address";
            }
            tlistner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tlistner.Bind(new IPEndPoint(IPAddress.Any, port));
            tlistner.Listen(-1);//  -1 is for no back logs
            tlistner.BeginAccept(new AsyncCallback(EndAccept), tlistner);
            return ("Text Listening On " + AddressAr[0].ToString() + ":" + port + "... OK");
        }

        public string ShutDown()
        {
            try
            {
                tlistner.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return ("Text Shutted down ...");
            }
            catch (Exception ex) { return (ex.Message); }
        }

        // (2) Accepting Client Connections
        private void EndAccept(IAsyncResult ar)
        {
            try
            {
                tlistner = (Socket)ar.AsyncState;
                Add_Client(tlistner.EndAccept(ar));
                tlistner.BeginAccept(new AsyncCallback(EndAccept), tlistner);
            }
            catch
            {
            }
        }

        // (3) Maintaining Socket For Each Client
        private static void Add_Client(Socket sockClient)
        {
            Newclient = new TClient(sockClient);
            ClientsList.Add(Newclient);
            Newclient.SetupRecieveCallback();
        }

        // (4) Send The Recieved Data to All Clients in The Room
        public static void OnRecievedData(IAsyncResult ar)
        {
            TClient client = (TClient)ar.AsyncState;
            byte[] aryRet = client.GetRecievedData(ar);

            if (aryRet.Length < 1)
            {
                client.ReadSocket.Close();
                ClientsList.Remove(client);
                return;
            }
            else
            {
                Message msg = new Message(aryRet);

                if (msg.enumCommand == Command.Join)
                {
                    if (msg.strRoom != null)
                    {
                        client.room = msg.strRoom;
                        client.uname = msg.strName;
                        Message userjoined = new Message();
                        userjoined.strName = msg.strName;
                        userjoined.enumCommand = Command.Join;
                        userjoined.strRoom = msg.strRoom;
                        userjoined.strMessage = msg.strName + " has joined " + msg.strRoom;
                        parent.Invoke(parent.multicast_msg_2room, userjoined);//notify all room users for new joining
                        parent.Invoke(parent.sendroomlistdelobj, msg.strRoom);//send list of users to the newly joined usr
                        
                        //parent.Invoke(parent.usersynobj, client);
                    }
                    else
                    {
                        client.uname = msg.strName;
                        //Message selroom = new Message();
                        //selroom.enumCommand = Command.Msg;
                        //selroom.strMessage = "Connected.Please Select Room.";
                        //selroom.strName = "Server";
                        //client.ReadSocket.Send(selroom.ToByte());
                    }
                }
                else if (msg.enumCommand == Command.Msg)
                {
                    parent.Invoke(parent.multicast_msg_2room, msg);
                }
                else if (msg.enumCommand == Command.Draw)
                {
                    parent.Invoke(parent.multicast_msg_2room, msg);
                }
                else if (msg.enumCommand == Command.Left)
                {
                    if (client.room != null)
                    {
                        Message userleft = new Message();
                        userleft.enumCommand = Command.Left;
                        userleft.strMessage = client.uname + " has left " + client.room;
                        userleft.strRoom = client.room;
                        parent.Invoke(parent.multicast_msg_2room, userleft);
                    }
                    string temproom = client.room;
                    client.room = msg.strRoom;
                    if (temproom != null)
                        parent.Invoke(parent.sendroomlistdelobj, temproom);
                    parent.Invoke(parent.sendroomlistdelobj, client.room);
                    Message userjoin = new Message();
                    userjoin.enumCommand = Command.Join;
                    userjoin.strMessage = client.uname + " has joined " + client.room;
                    userjoin.strRoom = msg.strRoom;
                    parent.Invoke(parent.multicast_msg_2room, userjoin);
                    Message clearCmd = new Message();
                    clearCmd.enumCommand = Command.Draw;
                    clearCmd.strMessage = Command.Clear.ToString() + "?";
                    clearCmd.strName = client.uname;
                    client.ReadSocket.Send(clearCmd.ToByte());
                }
                client.SetupRecieveCallback();
            }
        }
        public class TClient
        {
            // To create a new socket for each client 
            private Socket New_Socket;
            public string room;
            public string uname;
            private byte[] buffer = null;

            public TClient(Socket Sock)
            {
                New_Socket = Sock;
                New_Socket.NoDelay = true;
                room = null;
                uname = null;
            }

            public Socket ReadSocket
            {
                get { return New_Socket; }
            }

            public void SetupRecieveCallback()
            {
                try
                {
                    buffer = new byte[textBuffSize];
                    AsyncCallback recieveData = new AsyncCallback(TextServer.OnRecievedData);
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
