namespace DesktopPublisher
{
    partial class DesktopPublisher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesktopPublisher));
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.sendingSt = new System.Windows.Forms.Label();
            this.hideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.differencelab = new System.Windows.Forms.Label();
            this.StopBtn = new System.Windows.Forms.Button();
            this.StartBTN = new System.Windows.Forms.Button();
            this.ServerIP = new System.Windows.Forms.TextBox();
            this.ConnectBTN = new System.Windows.Forms.Button();
            this.lblServer = new System.Windows.Forms.Label();
            this.ImageToSend = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToSend)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(100, 6);
            // 
            // sendingSt
            // 
            this.sendingSt.AutoSize = true;
            this.sendingSt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendingSt.ForeColor = System.Drawing.Color.Red;
            this.sendingSt.Location = new System.Drawing.Point(338, 17);
            this.sendingSt.Name = "sendingSt";
            this.sendingSt.Size = new System.Drawing.Size(38, 16);
            this.sendingSt.TabIndex = 35;
            this.sendingSt.Text = "Stop";
            // 
            // hideToolStripMenuItem1
            // 
            this.hideToolStripMenuItem1.Name = "hideToolStripMenuItem1";
            this.hideToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem1.Text = "Hide";
            this.hideToolStripMenuItem1.Click += new System.EventHandler(this.hideToolStripMenuItem1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "DesktopPresenter";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 76);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // differencelab
            // 
            this.differencelab.AutoSize = true;
            this.differencelab.Location = new System.Drawing.Point(424, 4);
            this.differencelab.Name = "differencelab";
            this.differencelab.Size = new System.Drawing.Size(13, 13);
            this.differencelab.TabIndex = 34;
            this.differencelab.Text = "0";
            // 
            // StopBtn
            // 
            this.StopBtn.Enabled = false;
            this.StopBtn.Location = new System.Drawing.Point(152, 36);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(156, 26);
            this.StopBtn.TabIndex = 30;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // StartBTN
            // 
            this.StartBTN.Enabled = false;
            this.StartBTN.Location = new System.Drawing.Point(12, 36);
            this.StartBTN.Name = "StartBTN";
            this.StartBTN.Size = new System.Drawing.Size(134, 26);
            this.StartBTN.TabIndex = 29;
            this.StartBTN.Text = "Start";
            this.StartBTN.UseVisualStyleBackColor = true;
            this.StartBTN.Click += new System.EventHandler(this.StartBTN_Click);
            // 
            // ServerIP
            // 
            this.ServerIP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerIP.Location = new System.Drawing.Point(63, 4);
            this.ServerIP.MaxLength = 32;
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(129, 26);
            this.ServerIP.TabIndex = 26;
            // 
            // ConnectBTN
            // 
            this.ConnectBTN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectBTN.Location = new System.Drawing.Point(198, 3);
            this.ConnectBTN.Name = "ConnectBTN";
            this.ConnectBTN.Size = new System.Drawing.Size(110, 27);
            this.ConnectBTN.TabIndex = 24;
            this.ConnectBTN.Text = "Connect";
            this.ConnectBTN.Click += new System.EventHandler(this.ConnectBTN_Click);
            // 
            // lblServer
            // 
            this.lblServer.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(1, 9);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(56, 16);
            this.lblServer.TabIndex = 25;
            this.lblServer.Text = "Server";
            // 
            // ImageToSend
            // 
            this.ImageToSend.Location = new System.Drawing.Point(317, 36);
            this.ImageToSend.Name = "ImageToSend";
            this.ImageToSend.Size = new System.Drawing.Size(124, 26);
            this.ImageToSend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageToSend.TabIndex = 36;
            this.ImageToSend.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Motion Detection";
            // 
            // PresenterTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 65);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendingSt);
            this.Controls.Add(this.differencelab);
            this.Controls.Add(this.ServerIP);
            this.Controls.Add(this.ImageToSend);
            this.Controls.Add(this.ConnectBTN);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.StartBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "DesktopPresenter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DesktopPresenter";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageToSend)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Label sendingSt;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.Label differencelab;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button StartBTN;
        private System.Windows.Forms.TextBox ServerIP;
        private System.Windows.Forms.Button ConnectBTN;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.PictureBox ImageToSend;
        private System.Windows.Forms.Label label1;
    }
}

