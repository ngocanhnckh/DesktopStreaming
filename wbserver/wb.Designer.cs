namespace wbserver
{
	partial class wb
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
            this.Start_btn = new System.Windows.Forms.Button();
            this.Stop_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Status_lb = new System.Windows.Forms.ListBox();
            this.TextServerchk = new System.Windows.Forms.CheckBox();
            this.ImageServerchk = new System.Windows.Forms.CheckBox();
            this.AudioServerchk = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pb_Client1 = new System.Windows.Forms.PictureBox();
            this.pb_Client2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Client1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Client2)).BeginInit();
            this.SuspendLayout();
            // 
            // Start_btn
            // 
            this.Start_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start_btn.Location = new System.Drawing.Point(184, 237);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(75, 23);
            this.Start_btn.TabIndex = 0;
            this.Start_btn.Text = "Start";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Stop_btn
            // 
            this.Stop_btn.Enabled = false;
            this.Stop_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop_btn.Location = new System.Drawing.Point(265, 237);
            this.Stop_btn.Name = "Stop_btn";
            this.Stop_btn.Size = new System.Drawing.Size(75, 23);
            this.Stop_btn.TabIndex = 1;
            this.Stop_btn.Text = "Stop";
            this.Stop_btn.UseVisualStyleBackColor = true;
            this.Stop_btn.Click += new System.EventHandler(this.Stop_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Status:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(82, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(133, 20);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Internet Protocol";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Port";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(233, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(95, 20);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "4530";
            // 
            // Status_lb
            // 
            this.Status_lb.FormattingEnabled = true;
            this.Status_lb.Location = new System.Drawing.Point(12, 136);
            this.Status_lb.Name = "Status_lb";
            this.Status_lb.Size = new System.Drawing.Size(328, 95);
            this.Status_lb.TabIndex = 8;
            // 
            // TextServerchk
            // 
            this.TextServerchk.AutoSize = true;
            this.TextServerchk.Location = new System.Drawing.Point(7, 29);
            this.TextServerchk.Name = "TextServerchk";
            this.TextServerchk.Size = new System.Drawing.Size(47, 17);
            this.TextServerchk.TabIndex = 9;
            this.TextServerchk.Text = "Text";
            this.TextServerchk.UseVisualStyleBackColor = true;
            // 
            // ImageServerchk
            // 
            this.ImageServerchk.AutoSize = true;
            this.ImageServerchk.Location = new System.Drawing.Point(6, 56);
            this.ImageServerchk.Name = "ImageServerchk";
            this.ImageServerchk.Size = new System.Drawing.Size(55, 17);
            this.ImageServerchk.TabIndex = 11;
            this.ImageServerchk.Text = "Image";
            this.ImageServerchk.UseVisualStyleBackColor = true;
            // 
            // AudioServerchk
            // 
            this.AudioServerchk.AutoSize = true;
            this.AudioServerchk.Location = new System.Drawing.Point(6, 81);
            this.AudioServerchk.Name = "AudioServerchk";
            this.AudioServerchk.Size = new System.Drawing.Size(53, 17);
            this.AudioServerchk.TabIndex = 12;
            this.AudioServerchk.Text = "Audio";
            this.AudioServerchk.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(233, 61);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(95, 20);
            this.textBox3.TabIndex = 17;
            this.textBox3.Text = "4531";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(82, 62);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(133, 20);
            this.textBox4.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(233, 87);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(95, 20);
            this.textBox5.TabIndex = 19;
            this.textBox5.Text = "4532";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(82, 88);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(133, 20);
            this.textBox6.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextServerchk);
            this.groupBox1.Controls.Add(this.ImageServerchk);
            this.groupBox1.Controls.Add(this.AudioServerchk);
            this.groupBox1.Location = new System.Drawing.Point(13, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 110);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servers";
            // 
            // pb_Client1
            // 
            this.pb_Client1.ErrorImage = null;
            this.pb_Client1.Location = new System.Drawing.Point(396, 36);
            this.pb_Client1.Name = "pb_Client1";
            this.pb_Client1.Size = new System.Drawing.Size(100, 97);
            this.pb_Client1.TabIndex = 21;
            this.pb_Client1.TabStop = false;
            // 
            // pb_Client2
            // 
            this.pb_Client2.Location = new System.Drawing.Point(575, 36);
            this.pb_Client2.Name = "pb_Client2";
            this.pb_Client2.Size = new System.Drawing.Size(100, 97);
            this.pb_Client2.TabIndex = 22;
            this.pb_Client2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(422, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Stream1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(572, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Stream2";
            // 
            // wb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 318);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pb_Client2);
            this.Controls.Add(this.pb_Client1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.Status_lb);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stop_btn);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(361, 304);
            this.Name = "wb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "wbServer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Client1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Client2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Start_btn;
		private System.Windows.Forms.Button Stop_btn;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox Status_lb;
        private System.Windows.Forms.CheckBox TextServerchk;
        private System.Windows.Forms.CheckBox ImageServerchk;
        private System.Windows.Forms.CheckBox AudioServerchk;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pb_Client1;
        private System.Windows.Forms.PictureBox pb_Client2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

