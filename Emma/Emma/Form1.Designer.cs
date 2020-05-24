namespace Emma
{
    partial class Emma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Emma));
            this.LstCommands = new System.Windows.Forms.ListBox();
            this.TmrSpeaking = new System.Windows.Forms.Timer(this.components);
            this.LogoImg1 = new System.Windows.Forms.PictureBox();
            this.bg1 = new System.Windows.Forms.ListBox();
            this.usernameBox1 = new System.Windows.Forms.TextBox();
            this.OKbtn1 = new System.Windows.Forms.Button();
            this.bg2 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogoImg1)).BeginInit();
            this.SuspendLayout();
            // 
            // LstCommands
            // 
            this.LstCommands.BackColor = System.Drawing.Color.Cyan;
            this.LstCommands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LstCommands.ForeColor = System.Drawing.SystemColors.Desktop;
            this.LstCommands.FormattingEnabled = true;
            this.LstCommands.Location = new System.Drawing.Point(43, -14);
            this.LstCommands.Name = "LstCommands";
            this.LstCommands.Size = new System.Drawing.Size(240, 247);
            this.LstCommands.TabIndex = 0;
            // 
            // TmrSpeaking
            // 
            this.TmrSpeaking.Interval = 1000;
            // 
            // LogoImg1
            // 
            this.LogoImg1.BackColor = System.Drawing.SystemColors.Control;
            this.LogoImg1.BackgroundImage = global::Emma.Properties.Resources.Emma;
            this.LogoImg1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LogoImg1.Location = new System.Drawing.Point(0, 0);
            this.LogoImg1.Name = "LogoImg1";
            this.LogoImg1.Size = new System.Drawing.Size(42, 42);
            this.LogoImg1.TabIndex = 1;
            this.LogoImg1.TabStop = false;
            // 
            // bg1
            // 
            this.bg1.BackColor = System.Drawing.Color.Cyan;
            this.bg1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bg1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.bg1.FormattingEnabled = true;
            this.bg1.Location = new System.Drawing.Point(-20, -3);
            this.bg1.Name = "bg1";
            this.bg1.Size = new System.Drawing.Size(143, 286);
            this.bg1.TabIndex = 2;
            // 
            // usernameBox1
            // 
            this.usernameBox1.Location = new System.Drawing.Point(0, 39);
            this.usernameBox1.Name = "usernameBox1";
            this.usernameBox1.Size = new System.Drawing.Size(195, 20);
            this.usernameBox1.TabIndex = 3;
            // 
            // OKbtn1
            // 
            this.OKbtn1.Location = new System.Drawing.Point(201, 36);
            this.OKbtn1.Name = "OKbtn1";
            this.OKbtn1.Size = new System.Drawing.Size(40, 23);
            this.OKbtn1.TabIndex = 4;
            this.OKbtn1.Text = "OK";
            this.OKbtn1.UseVisualStyleBackColor = true;
            this.OKbtn1.Click += new System.EventHandler(this.OKbtn1_Click);
            // 
            // bg2
            // 
            this.bg2.BackColor = System.Drawing.Color.Cyan;
            this.bg2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bg2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.bg2.FormattingEnabled = true;
            this.bg2.Location = new System.Drawing.Point(43, -14);
            this.bg2.Name = "bg2";
            this.bg2.Size = new System.Drawing.Size(240, 247);
            this.bg2.TabIndex = 5;
            // 
            // Emma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(271, 221);
            this.ControlBox = false;
            this.Controls.Add(this.usernameBox1);
            this.Controls.Add(this.OKbtn1);
            this.Controls.Add(this.LogoImg1);
            this.Controls.Add(this.LstCommands);
            this.Controls.Add(this.bg1);
            this.Controls.Add(this.bg2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Emma";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Cyan;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Emma_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.LogoImg1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LstCommands;
        private System.Windows.Forms.Timer TmrSpeaking;
        private System.Windows.Forms.PictureBox LogoImg1;
        private System.Windows.Forms.ListBox bg1;
        private System.Windows.Forms.TextBox usernameBox1;
        private System.Windows.Forms.Button OKbtn1;
        private System.Windows.Forms.ListBox bg2;
    }
}

