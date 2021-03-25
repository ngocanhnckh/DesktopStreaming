using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace wbserver
{
    //default ports
    //text=4530
    //drawing=4531
    //audio=4532
    public delegate void msg2room(Message m);
    public delegate void sendroomunamelistdelage(string r);
    public delegate void msg2user(Message m);
    public delegate void setstreamimg(byte[] img);


    public partial class wb : Form
    {
        public ImageServer ds;
        public TextServer ts;
        public AudioServer ads;

        public sendroomunamelistdelage sendroomlistdelobj;
        public msg2room multicast_msg_2room;
        public setstreamimg setstreamimgob;


        DataClasses1DataContext db = new DataClasses1DataContext();

        public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }
        public void setimage(byte[] img)
        {
            
            pb_Client1.Image = byteArrayToImage(img);
        }
        public wb()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            // Start The Policy Server
            PolicySocketServer StartPolicyServer = new PolicySocketServer();
            Thread th = new Thread(new ThreadStart(StartPolicyServer.StartSocketServer));
            th.IsBackground = true;
            th.Start();

            //initialize other servers
            ts = new TextServer(this);
            ds = new ImageServer(4531,this);
            ads = new AudioServer(this);
            //attach methods to delegates
            multicast_msg_2room = new msg2room(msg_to_room);
            sendroomlistdelobj = new sendroomunamelistdelage(sendclientlist);
            setstreamimgob = new setstreamimg(setimage);


            Status_lb.Items.Add("Ready...");
            //initialize database
            try
            {
                var result = from p in db.ServerSettings select p;
                if (result.Count() == 0)
                {
                    Status_lb.Items.Add("Initializing Server Settings in Database.");
                    ServerSetting ss = new ServerSetting();
                    ss.TextServerIP = "0.0.0.0";
                    ss.TextServerPort = 4530;
                    ss.AudioServerIP = "0.0.0.0";
                    ss.ImageServerPort = 4531;
                    ss.ImageServerIP = "0.0.0.0";
                    ss.AudioServerPort = 4532;
                    ss.TextServerOnline = false;
                    ss.ImageServerOnline = false;
                    ss.AudioServerOnline = false;
                    ss.AutoConfig = true;
                    db.ServerSettings.InsertOnSubmit(ss);
                    db.SubmitChanges();
                }
            }
            catch (Exception)
            {
            }
        }

        public static string GetPublicIP()
        {
            try
            {
                string url = "http://checkip.dyndns.org";
                System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string response = sr.ReadToEnd().Trim();
                string[] a = response.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];
                return a4;
            }
            catch (Exception)
            {
                return "0.0.0.0";
            }
        }
        int port;
        bool checkifint(TextBox tx)
        {
            if (!int.TryParse(tx.Text, out port))
            {
                Status_lb.Items.Add(tx.Text + " is not valid port");
                return false;
            }
            else
                return true;
        }
        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (TextServerchk.Checked == true)
            {
                if (textBox2.Text == "")
                {
                    ts = new TextServer(this);
                    Status_lb.Items.Add(ts.Start());
                    TextServerchk.Enabled = false;
                    textBox1.Text = GetPublicIP();
                    Start_btn.Enabled = false;
                    Stop_btn.Enabled = true;
                    try
                    {
                        var result = (from p in db.ServerSettings select p).Single();
                        result.TextServerIP = textBox1.Text;
                        result.TextServerPort = 4530;
                        result.TextServerOnline = true;
                        db.SubmitChanges();
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                    if (checkifint(textBox2))
                    {
                        ts = new TextServer(this, port);
                        Status_lb.Items.Add(ts.Start());
                        TextServerchk.Enabled = false;
                        textBox1.Text = GetPublicIP();
                        try
                        {
                            var result = (from p in db.ServerSettings select p).Single();
                            result.TextServerIP = textBox1.Text;
                            result.TextServerPort = port;
                            result.TextServerOnline = true;
                            db.SubmitChanges();
                        }
                        catch (Exception)
                        {
                        }
                        Start_btn.Enabled = false;
                        Stop_btn.Enabled = true;
                    }
            }
            if (ImageServerchk.Checked == true)
            {
                if (textBox3.Text == "")
                {
                    ds = new ImageServer();
                    Status_lb.Items.Add(ds.Start());
                    ImageServerchk.Enabled = false;
                    textBox4.Text = GetPublicIP();
                    try
                    {
                        var result = (from p in db.ServerSettings select p).Single();
                        result.ImageServerIP = textBox4.Text;
                        result.ImageServerPort = 4531;
                        result.ImageServerOnline = true;
                        db.SubmitChanges();
                    }
                    catch (Exception)
                    {
                    }
                    Start_btn.Enabled = false;
                    Stop_btn.Enabled = true;
                }
                else
                    if (checkifint(textBox3))
                    {
                        ds = new ImageServer(port,this);
                        Status_lb.Items.Add(ds.Start());
                        ImageServerchk.Enabled = false;
                        textBox4.Text = GetPublicIP();
                        try
                        {
                            var result = (from p in db.ServerSettings select p).Single();
                            result.ImageServerIP = textBox4.Text;
                            result.ImageServerPort = port;
                            result.ImageServerOnline = true;
                            db.SubmitChanges();
                        }
                        catch (Exception)
                        {
                        }
                        Start_btn.Enabled = false;
                        Stop_btn.Enabled = true;
                    }
            }

            if (AudioServerchk.Checked == true)
            {
                if (textBox5.Text == "")
                {
                    ads = new AudioServer(this);
                    Status_lb.Items.Add(ads.Start());
                    AudioServerchk.Enabled = false;
                    textBox6.Text = GetPublicIP();
                    try
                    {
                        var result = (from p in db.ServerSettings select p).Single();
                        result.AudioServerIP = textBox6.Text;
                        result.AudioServerPort = 4532;
                        result.AudioServerOnline = true;
                        db.SubmitChanges();
                    }
                    catch (Exception)
                    {
                    }
                    Start_btn.Enabled = false;
                    Stop_btn.Enabled = true;
                }
                else
                    if (checkifint(textBox5))
                    {
                        ads = new AudioServer(this, port);
                        Status_lb.Items.Add(ads.Start());
                        AudioServerchk.Enabled = false;
                        textBox6.Text = GetPublicIP();
                        try
                        {
                            var result = (from p in db.ServerSettings select p).Single();
                            result.AudioServerIP = textBox6.Text;
                            result.AudioServerPort = port;
                            result.AudioServerOnline = true;
                            db.SubmitChanges();
                        }
                        catch (Exception)
                        {
                        }
                        Start_btn.Enabled = false;
                        Stop_btn.Enabled = true;
                    }
            }
            bool allchecked=false;
            if (TextServerchk.Checked == false & ImageServerchk.Checked == false & AudioServerchk.Checked == false)
            {
                Status_lb.Items.Add("Please, Select a server first.");
                allchecked = true;
            }
            Status_lb.SelectedIndex = Status_lb.Items.Count - 1;
            if (!allchecked)
            {
                TextServerchk.Enabled = false;
                ImageServerchk.Enabled = false;
                AudioServerchk.Enabled = false;
            }
        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            Start_btn.Enabled = true;
            Stop_btn.Enabled = false;
            if (TextServerchk.Checked == true)
            {
                Status_lb.Items.Add(ts.ShutDown());
                textBox1.Text = "";
                try
                {
                    var result = (from p in db.ServerSettings select p).Single();
                    result.TextServerIP = "0.0.0.0";
                    result.TextServerPort = 4530;
                    result.TextServerOnline = false;
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                }
                TextServerchk.Enabled = true;
                TextServerchk.Checked = false;
            }
            if (ImageServerchk.Checked == true)
            {
                Status_lb.Items.Add(ds.ShutDown());
                textBox4.Text = "";
                try
                {
                    var result = (from p in db.ServerSettings select p).Single();
                    result.ImageServerIP = "0.0.0.0";
                    result.ImageServerOnline = false;
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                }
                ImageServerchk.Enabled = true;
                ImageServerchk.Checked = false;
            }
            if (AudioServerchk.Checked == true)
            {
                Status_lb.Items.Add(ads.ShutDown());
                textBox6.Text = "";
                try
                {
                    var result = (from p in db.ServerSettings select p).Single();
                    result.AudioServerIP = "0.0.0.0";
                    result.AudioServerOnline = false;
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                }
                AudioServerchk.Enabled = true;
                AudioServerchk.Checked = false;
            }
            Status_lb.SelectedIndex = Status_lb.Items.Count - 1;
            TextServerchk.Enabled = true;
            ImageServerchk.Enabled = true;
            AudioServerchk.Enabled = true;
        }

        private void msg_to_room(Message d)
        {
            foreach (TextServer.TClient cl in TextServer.ClientsList)
            {
                if (d.strRoom == cl.room)
                {
                    try
                    {
                        cl.ReadSocket.NoDelay = true;
                        cl.ReadSocket.Send(d.ToByte());
                    }
                    catch
                    {
                        cl.ReadSocket.Close();
                        TextServer.ClientsList.Remove(cl);
                    }
                }
            }
        }
        
        private void sendclientlist(string r)
        {
            Message roomclientlist = new Message();
            roomclientlist.enumCommand = Command.List;
            roomclientlist.strRoom = r;
            foreach (TextServer.TClient cl in TextServer.ClientsList)
            {
                if (cl.room == r)
                    roomclientlist.strMessage += cl.uname + "*";
            }
            foreach (TextServer.TClient cl in TextServer.ClientsList)
            {
                if (r == cl.room)
                {
                    try
                    {
                        cl.ReadSocket.NoDelay = true;
                        cl.ReadSocket.Send(roomclientlist.ToByte());
                    }
                    catch
                    {
                        cl.ReadSocket.Close();
                        TextServer.ClientsList.Remove(cl);
                    }
                }
            }
        }
    }
}
