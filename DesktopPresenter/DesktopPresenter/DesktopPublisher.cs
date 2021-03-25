using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DesktopPublisher
{
    public partial class DesktopPublisher : Form
    {

        const int wToCompare = 100;
        const int hToCompare = 100;

        const int wToSend = 640;
        const int hToSend = 480;

        Socket SenderSocket;
        ScreenCapture scr = new ScreenCapture();
        bool stop = true;
        Thread th;
        IPEndPoint ipend;

        public DesktopPublisher()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            SenderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void hideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectBTN_Click(object sender, EventArgs e)
        {
            if (!SenderSocket.Connected)
                try
                {
                    SenderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ipend = new IPEndPoint(IPAddress.Parse(ServerIP.Text), 4531);
                    SenderSocket.Connect(ipend);

                    sendingSt.ForeColor = Color.Blue;
                    sendingSt.Text = "Connected";

                    StartBTN.Enabled = SenderSocket.Connected;
                    ConnectBTN.Text = "Disconnect";
                    ServerIP.Enabled = false;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            else
            {
                if (th != null)
                {
                    stop = true;
                    th.Abort();
                }

                SenderSocket.Close();

               
                ConnectBTN.Text = "Connect";
                sendingSt.ForeColor = Color.Red;
                sendingSt.Text = "Disconnected";
                differencelab.Text = "0";
                ImageToSend.Image = null;
                StartBTN.Enabled = false;
                StopBtn.Enabled = false;
                ServerIP.Enabled = true;
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            stop = true;
            th.Abort();
            StartBTN.Enabled = true;
            StopBtn.Enabled = false;
            sendingSt.ForeColor = Color.Red;
            sendingSt.Text = "Paused";
        }

        private void StartBTN_Click(object sender, EventArgs e)
        {
            stop = false;

            th = new Thread(new ThreadStart(StartSending));
            th.IsBackground = true;
            th.Start();


            StartBTN.Enabled = false;
            StopBtn.Enabled = true;
        }

        void StartSending()
        {
            while (!stop)
            
                try
                {
                    Image oldimage = scr.Get_Resized_Image(wToCompare, hToCompare, scr.GetDesktopBitmapBytes());
                    Thread.Sleep(10);
                    Image newimage = scr.Get_Resized_Image(wToCompare, hToCompare, scr.GetDesktopBitmapBytes());
                    
                    byte[] buffer = scr.GetDesktop_ResizedBytes(wToSend, hToSend);
                    
                    float difference = scr.difference(newimage, oldimage);
                    differencelab.Text = difference.ToString() + "%";

                    if (difference >= 1)
                    {
                        Message m = new Message();
                        m.enumCommand = Command.Screen;
                        m.strRoom = "Room";
                        m.strName = "Client";
                        m.databytes = buffer;

                        sendingSt.ForeColor = Color.Green;
                        sendingSt.Text = "Sending ...";
                        ImageToSend.Image = newimage;
                        SenderSocket.Send(m.ToByte());
                    }
                    else { sendingSt.ForeColor = Color.Blue; sendingSt.Text = "Comparing..."; }

                }
                catch (Exception) { }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
    }
}
