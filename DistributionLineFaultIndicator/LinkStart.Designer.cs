namespace DistributionLineFaultIndicator
{
    partial class LinkStart
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWaitTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerF = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(297, 61);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "启动链路";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "应答等待时间(S):";
            // 
            // textBoxWaitTime
            // 
            this.textBoxWaitTime.Location = new System.Drawing.Point(148, 63);
            this.textBoxWaitTime.MaxLength = 5;
            this.textBoxWaitTime.Name = "textBoxWaitTime";
            this.textBoxWaitTime.Size = new System.Drawing.Size(100, 21);
            this.textBoxWaitTime.TabIndex = 2;
            this.textBoxWaitTime.Text = "30";
            this.textBoxWaitTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validating_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "状态：";
            // 
            // textBoxState
            // 
            this.textBoxState.Location = new System.Drawing.Point(110, 152);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.ReadOnly = true;
            this.textBoxState.Size = new System.Drawing.Size(188, 21);
            this.textBoxState.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerF
            // 
            this.timerF.Tick += new System.EventHandler(this.timerF_Tick);
            // 
            // LinkStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 228);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxWaitTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStart);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(417, 266);
            this.Name = "LinkStart";
            this.Text = "LinkStart";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LinkStart_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxWaitTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerF;
    }
}