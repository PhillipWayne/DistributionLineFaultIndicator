namespace DistributionLineFaultIndicator
{
    partial class Indicator
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
            this.comboBoxIndicator = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxCellVoltage = new System.Windows.Forms.TextBox();
            this.textBoxLoadCurrent = new System.Windows.Forms.TextBox();
            this.textBoxShortCircuitType = new System.Windows.Forms.TextBox();
            this.textBoxPowerOff = new System.Windows.Forms.TextBox();
            this.textBoxPowerOn = new System.Windows.Forms.TextBox();
            this.textBoxGroundFault = new System.Windows.Forms.TextBox();
            this.textBoxShortCircuit = new System.Windows.Forms.TextBox();
            this.textBoxHeartBeat = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxIndicator
            // 
            this.comboBoxIndicator.FormattingEnabled = true;
            this.comboBoxIndicator.Items.AddRange(new object[] {
            "指示器1（1路）",
            "指示器2（1路）",
            "指示器3（1路）",
            "指示器4（2路）",
            "指示器5（2路）",
            "指示器6（2路）",
            "指示器7（3路）",
            "指示器8（3路）",
            "指示器9（3路）"});
            this.comboBoxIndicator.Location = new System.Drawing.Point(381, 40);
            this.comboBoxIndicator.Name = "comboBoxIndicator";
            this.comboBoxIndicator.Size = new System.Drawing.Size(121, 20);
            this.comboBoxIndicator.TabIndex = 47;
            this.comboBoxIndicator.SelectedIndexChanged += new System.EventHandler(this.comboBoxIndicator_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(703, 333);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 23);
            this.button2.TabIndex = 53;
            this.button2.Text = "地址下设";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(304, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 49;
            this.label14.Text = "故障指示器:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(592, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 52;
            this.button1.Text = "参数下设";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxCellVoltage
            // 
            this.textBoxCellVoltage.Location = new System.Drawing.Point(131, 204);
            this.textBoxCellVoltage.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxCellVoltage.Name = "textBoxCellVoltage";
            this.textBoxCellVoltage.ReadOnly = true;
            this.textBoxCellVoltage.Size = new System.Drawing.Size(72, 21);
            this.textBoxCellVoltage.TabIndex = 51;
            this.textBoxCellVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxLoadCurrent
            // 
            this.textBoxLoadCurrent.Location = new System.Drawing.Point(131, 181);
            this.textBoxLoadCurrent.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxLoadCurrent.Name = "textBoxLoadCurrent";
            this.textBoxLoadCurrent.ReadOnly = true;
            this.textBoxLoadCurrent.Size = new System.Drawing.Size(72, 21);
            this.textBoxLoadCurrent.TabIndex = 50;
            this.textBoxLoadCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxShortCircuitType
            // 
            this.textBoxShortCircuitType.Location = new System.Drawing.Point(173, 158);
            this.textBoxShortCircuitType.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxShortCircuitType.Name = "textBoxShortCircuitType";
            this.textBoxShortCircuitType.ReadOnly = true;
            this.textBoxShortCircuitType.Size = new System.Drawing.Size(30, 21);
            this.textBoxShortCircuitType.TabIndex = 48;
            this.textBoxShortCircuitType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxPowerOff
            // 
            this.textBoxPowerOff.Location = new System.Drawing.Point(173, 135);
            this.textBoxPowerOff.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxPowerOff.Name = "textBoxPowerOff";
            this.textBoxPowerOff.ReadOnly = true;
            this.textBoxPowerOff.Size = new System.Drawing.Size(30, 21);
            this.textBoxPowerOff.TabIndex = 46;
            this.textBoxPowerOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxPowerOn
            // 
            this.textBoxPowerOn.Location = new System.Drawing.Point(173, 112);
            this.textBoxPowerOn.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxPowerOn.Name = "textBoxPowerOn";
            this.textBoxPowerOn.ReadOnly = true;
            this.textBoxPowerOn.Size = new System.Drawing.Size(30, 21);
            this.textBoxPowerOn.TabIndex = 45;
            this.textBoxPowerOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxGroundFault
            // 
            this.textBoxGroundFault.Location = new System.Drawing.Point(173, 89);
            this.textBoxGroundFault.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxGroundFault.Name = "textBoxGroundFault";
            this.textBoxGroundFault.ReadOnly = true;
            this.textBoxGroundFault.Size = new System.Drawing.Size(30, 21);
            this.textBoxGroundFault.TabIndex = 44;
            this.textBoxGroundFault.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxShortCircuit
            // 
            this.textBoxShortCircuit.Location = new System.Drawing.Point(173, 66);
            this.textBoxShortCircuit.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxShortCircuit.Name = "textBoxShortCircuit";
            this.textBoxShortCircuit.ReadOnly = true;
            this.textBoxShortCircuit.Size = new System.Drawing.Size(30, 21);
            this.textBoxShortCircuit.TabIndex = 43;
            this.textBoxShortCircuit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxHeartBeat
            // 
            this.textBoxHeartBeat.Location = new System.Drawing.Point(173, 43);
            this.textBoxHeartBeat.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxHeartBeat.Name = "textBoxHeartBeat";
            this.textBoxHeartBeat.ReadOnly = true;
            this.textBoxHeartBeat.Size = new System.Drawing.Size(30, 21);
            this.textBoxHeartBeat.TabIndex = 42;
            this.textBoxHeartBeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 41;
            this.label8.Text = "电池电压：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 40;
            this.label7.Text = "负荷电流：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 39;
            this.label6.Text = "短路故障性质：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "线路停电：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "线路上电：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "接地故障：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "短路故障：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "指示单元心跳：";
            // 
            // Indicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 425);
            this.Controls.Add(this.comboBoxIndicator);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxCellVoltage);
            this.Controls.Add(this.textBoxLoadCurrent);
            this.Controls.Add(this.textBoxShortCircuitType);
            this.Controls.Add(this.textBoxPowerOff);
            this.Controls.Add(this.textBoxPowerOn);
            this.Controls.Add(this.textBoxGroundFault);
            this.Controls.Add(this.textBoxShortCircuit);
            this.Controls.Add(this.textBoxHeartBeat);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Indicator";
            this.Text = "Indicator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxIndicator;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxCellVoltage;
        private System.Windows.Forms.TextBox textBoxLoadCurrent;
        private System.Windows.Forms.TextBox textBoxShortCircuitType;
        private System.Windows.Forms.TextBox textBoxPowerOff;
        private System.Windows.Forms.TextBox textBoxPowerOn;
        private System.Windows.Forms.TextBox textBoxGroundFault;
        private System.Windows.Forms.TextBox textBoxShortCircuit;
        private System.Windows.Forms.TextBox textBoxHeartBeat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;



    }
}