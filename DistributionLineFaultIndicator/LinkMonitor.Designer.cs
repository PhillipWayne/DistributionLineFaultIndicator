namespace DistributionLineFaultIndicator
{
    partial class LinkMonitor
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCOTLen = new System.Windows.Forms.TextBox();
            this.textBoxPALen = new System.Windows.Forms.TextBox();
            this.textBoxLALen = new System.Windows.Forms.TextBox();
            this.textBoxInALen = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxLA = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxPA = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(169, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(265, 332);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "IP地址：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "端口号：";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(138, 37);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 21);
            this.textBoxIP.TabIndex = 12;
            this.textBoxIP.Text = "220.180.239.204";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(138, 69);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxPort.TabIndex = 13;
            this.textBoxPort.Text = "4001";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "传送原因长度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "公共地址长度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "链路地址长度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "信息体地址长度：";
            // 
            // textBoxCOTLen
            // 
            this.textBoxCOTLen.Location = new System.Drawing.Point(139, 222);
            this.textBoxCOTLen.Name = "textBoxCOTLen";
            this.textBoxCOTLen.Size = new System.Drawing.Size(100, 21);
            this.textBoxCOTLen.TabIndex = 18;
            this.textBoxCOTLen.Text = "2";
            // 
            // textBoxPALen
            // 
            this.textBoxPALen.Location = new System.Drawing.Point(139, 246);
            this.textBoxPALen.Name = "textBoxPALen";
            this.textBoxPALen.Size = new System.Drawing.Size(100, 21);
            this.textBoxPALen.TabIndex = 19;
            this.textBoxPALen.Text = "2";
            // 
            // textBoxLALen
            // 
            this.textBoxLALen.Location = new System.Drawing.Point(139, 270);
            this.textBoxLALen.Name = "textBoxLALen";
            this.textBoxLALen.Size = new System.Drawing.Size(100, 21);
            this.textBoxLALen.TabIndex = 20;
            this.textBoxLALen.Text = "2";
            // 
            // textBoxInALen
            // 
            this.textBoxInALen.Location = new System.Drawing.Point(139, 295);
            this.textBoxInALen.Name = "textBoxInALen";
            this.textBoxInALen.Size = new System.Drawing.Size(100, 21);
            this.textBoxInALen.TabIndex = 21;
            this.textBoxInALen.Text = "2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "链路地址：";
            // 
            // textBoxLA
            // 
            this.textBoxLA.Location = new System.Drawing.Point(138, 101);
            this.textBoxLA.Name = "textBoxLA";
            this.textBoxLA.Size = new System.Drawing.Size(100, 21);
            this.textBoxLA.TabIndex = 23;
            this.textBoxLA.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "公共地址：";
            // 
            // textBoxPA
            // 
            this.textBoxPA.Location = new System.Drawing.Point(138, 134);
            this.textBoxPA.Name = "textBoxPA";
            this.textBoxPA.Size = new System.Drawing.Size(100, 21);
            this.textBoxPA.TabIndex = 25;
            this.textBoxPA.Text = "1";
            // 
            // LinkMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 386);
            this.Controls.Add(this.textBoxPA);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxLA);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxInALen);
            this.Controls.Add(this.textBoxLALen);
            this.Controls.Add(this.textBoxPALen);
            this.Controls.Add(this.textBoxCOTLen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "LinkMonitor";
            this.Text = "LinkMonitor";
            this.Load += new System.EventHandler(this.LinkMonitor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCOTLen;
        private System.Windows.Forms.TextBox textBoxPALen;
        private System.Windows.Forms.TextBox textBoxLALen;
        private System.Windows.Forms.TextBox textBoxInALen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxLA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxPA;
    }
}