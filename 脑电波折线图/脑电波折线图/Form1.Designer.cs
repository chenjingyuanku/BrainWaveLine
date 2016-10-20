namespace 脑电波折线图
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.SerialPortBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.BaudRateBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DataLenthBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ParityBox = new System.Windows.Forms.ComboBox();
            this.StopBitBox = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AttentionLabel = new System.Windows.Forms.Label();
            this.MeditationLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.signalLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // SerialPortBox
            // 
            this.SerialPortBox.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.SerialPortBox.FormattingEnabled = true;
            this.SerialPortBox.Location = new System.Drawing.Point(113, 121);
            this.SerialPortBox.Name = "SerialPortBox";
            this.SerialPortBox.Size = new System.Drawing.Size(315, 30);
            this.SerialPortBox.TabIndex = 12;
            this.SerialPortBox.TabStop = false;
            this.SerialPortBox.SelectedIndexChanged += new System.EventHandler(this.SerialPortBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(445, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 69);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开串口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BaudRateBox
            // 
            this.BaudRateBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BaudRateBox.FormattingEnabled = true;
            this.BaudRateBox.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.BaudRateBox.Location = new System.Drawing.Point(113, 84);
            this.BaudRateBox.Name = "BaudRateBox";
            this.BaudRateBox.Size = new System.Drawing.Size(121, 30);
            this.BaudRateBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label1.Location = new System.Drawing.Point(22, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "波特率：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label2.Location = new System.Drawing.Point(41, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "端口：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label3.Location = new System.Drawing.Point(258, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "数据位：";
            // 
            // DataLenthBox
            // 
            this.DataLenthBox.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.DataLenthBox.FormattingEnabled = true;
            this.DataLenthBox.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.DataLenthBox.Location = new System.Drawing.Point(349, 84);
            this.DataLenthBox.Name = "DataLenthBox";
            this.DataLenthBox.Size = new System.Drawing.Size(79, 30);
            this.DataLenthBox.TabIndex = 6;
            this.DataLenthBox.Tag = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label4.Location = new System.Drawing.Point(22, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "校验位：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F);
            this.label5.Location = new System.Drawing.Point(258, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位：";
            // 
            // ParityBox
            // 
            this.ParityBox.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.ParityBox.FormattingEnabled = true;
            this.ParityBox.Items.AddRange(new object[] {
            "无",
            "0校验",
            "1校验",
            "奇校验",
            "偶校验"});
            this.ParityBox.Location = new System.Drawing.Point(113, 45);
            this.ParityBox.Name = "ParityBox";
            this.ParityBox.Size = new System.Drawing.Size(121, 30);
            this.ParityBox.TabIndex = 9;
            // 
            // StopBitBox
            // 
            this.StopBitBox.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.StopBitBox.FormattingEnabled = true;
            this.StopBitBox.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2",
            "无"});
            this.StopBitBox.Location = new System.Drawing.Point(349, 45);
            this.StopBitBox.Name = "StopBitBox";
            this.StopBitBox.Size = new System.Drawing.Size(79, 30);
            this.StopBitBox.TabIndex = 10;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(26, 172);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(494, 183);
            this.textBox1.TabIndex = 13;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(97, 361);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(342, 60);
            this.textBox2.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(445, 361);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 60);
            this.button2.TabIndex = 15;
            this.button2.Text = "发送";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(567, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 300);
            this.panel1.TabIndex = 16;
            // 
            // AttentionLabel
            // 
            this.AttentionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AttentionLabel.AutoSize = true;
            this.AttentionLabel.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttentionLabel.ForeColor = System.Drawing.Color.SaddleBrown;
            this.AttentionLabel.Location = new System.Drawing.Point(565, 378);
            this.AttentionLabel.Name = "AttentionLabel";
            this.AttentionLabel.Size = new System.Drawing.Size(160, 22);
            this.AttentionLabel.TabIndex = 17;
            this.AttentionLabel.Text = "专注度平均值：0";
            // 
            // MeditationLabel
            // 
            this.MeditationLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.MeditationLabel.AutoSize = true;
            this.MeditationLabel.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MeditationLabel.ForeColor = System.Drawing.Color.Blue;
            this.MeditationLabel.Location = new System.Drawing.Point(777, 378);
            this.MeditationLabel.Name = "MeditationLabel";
            this.MeditationLabel.Size = new System.Drawing.Size(160, 22);
            this.MeditationLabel.TabIndex = 18;
            this.MeditationLabel.Text = "放松度平均值：0";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(445, 129);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "脑电波模式";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(26, 361);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 60);
            this.button3.TabIndex = 20;
            this.button3.Text = "清空";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // signalLabel
            // 
            this.signalLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.signalLabel.AutoSize = true;
            this.signalLabel.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signalLabel.ForeColor = System.Drawing.Color.LimeGreen;
            this.signalLabel.Location = new System.Drawing.Point(565, 417);
            this.signalLabel.Name = "signalLabel";
            this.signalLabel.Size = new System.Drawing.Size(125, 22);
            this.signalLabel.TabIndex = 21;
            this.signalLabel.Text = "信号强度：0";
            // 
            // stateLabel
            // 
            this.stateLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.stateLabel.AutoSize = true;
            this.stateLabel.BackColor = System.Drawing.SystemColors.Control;
            this.stateLabel.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stateLabel.ForeColor = System.Drawing.Color.LimeGreen;
            this.stateLabel.Location = new System.Drawing.Point(777, 417);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(115, 22);
            this.stateLabel.TabIndex = 22;
            this.stateLabel.Text = "未疲劳驾驶";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 456);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.signalLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.MeditationLabel);
            this.Controls.Add(this.AttentionLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.StopBitBox);
            this.Controls.Add(this.ParityBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DataLenthBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BaudRateBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SerialPortBox);
            this.MinimumSize = new System.Drawing.Size(1020, 500);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "脑电波折线图     Designed by 陈竞远";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Form1_ControlAdded);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox SerialPortBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox BaudRateBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DataLenthBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ParityBox;
        private System.Windows.Forms.ComboBox StopBitBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label AttentionLabel;
        private System.Windows.Forms.Label MeditationLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label signalLabel;
        private System.Windows.Forms.Label stateLabel;
    }
}

