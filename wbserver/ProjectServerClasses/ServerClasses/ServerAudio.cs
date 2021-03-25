using System;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wbserver
{
    public class AudioServer
    {
        public static ArrayList AudioClientsList = new ArrayList();
        static AClient AudioNewclient;
        Socket alistner;
        int port;
        IPAddress[] AddressAr = null;
        static wb parent;
        public AudioServer(wb p)
        {
            port = 4532;
            parent = p;
        }

        public AudioServer(wb p, int tport)
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
            alistner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            alistner.Bind(new IPEndPoint(IPAddress.Any, port));
            alistner.Listen(-1);//  -1 is for no back logs
            alistner.BeginAccept(new AsyncCallback(EndAccept), alistner);
            return ("Audio Listening On " + AddressAr[0].ToString() + ":" + port + "... OK");
        }

        public string ShutDown()
        {
            try
            {
                alistner.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return ("Audio Shutted down ...");
            }
            catch (Exception ex) { return (ex.Message); }
        }

        // (2) Accepting Client Connections
        private void EndAccept(IAsyncResult ar)
        {
            try
            {
                alistner = (Socket)ar.AsyncState;
                Add_Client(alistner.EndAccept(ar));
                alistner.BeginAccept(new AsyncCallback(EndAccept), alistner);
            }
            catch
            {
            }
        }

        // (3) Maintaining Socket For Each Client
        private static void Add_Client(Socket sockClient)
        {
            AudioNewclient = new AClient(sockClient);

            AudioClientsList.Add(AudioNewclient);

            AudioNewclient.SetupRecieveCallback();
        }

        // (4) Send The Recieved Data to All Clients in The Room
        private static void OnRecievedData(IAsyncResult ar)
        {
            AClient client = (AClient)ar.AsyncState;
            byte[] aryRet = client.GetRecievedData(ar);

            if (aryRet.Length < 1)
            {
                client.ReadSocket.Close();
                AudioClientsList.Remove(client);
                return;
            }
            else
            {
                Message dm = new Message(aryRet);
                if (dm.enumCommand == Command.Audio)
                {
                    foreach (AClient cl in AudioClientsList)
                    {
                        try
                        {
                            if (cl.room != null)
                                if (dm.strRoom == cl.room)
                                {
                                    cl.ReadSocket.NoDelay = true;
                                    cl.ReadSocket.Send(aryRet);
                                }
                        }
                        catch
                        {
                            cl.ReadSocket.Close();
                            AudioClientsList.Remove(cl);
                        }
                    }
                }
                else if (dm.enumCommand == Command.Join)
                    client.room = dm.strRoom;
                client.SetupRecieveCallback();
            }
        }
        internal class AClient
        {
            // To create a new socket for each client 
            private Socket New_Socket;
            public string room;
            public string uname;
            private byte[] buffer = null;

            public AClient(Socket Sock)
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
                    buffer = new byte[1280000];
                    AsyncCallback recieveData = new AsyncCallback(AudioServer.OnRecievedData);
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
