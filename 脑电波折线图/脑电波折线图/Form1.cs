using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using SpeechLib;
using System.Threading;

namespace 脑电波折线图
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Form1_ControlAdded);
            InitializeComponent();
        }
        delegate void SetTextCallback(string text);
        delegate void SetLableCallback(string text1, string text2);
        delegate void addYCallback(int y1,int y2);
        bool comState = false;
        string currentCOM = "";
        int[] baudRateArr = {1200,2400,4800,9600,14400,
        19200,38400,56000,57600,115200,128000,256000 };
        int[] dataLenthArr = { 5, 6, 7, 8 };
        StopBits[] stopBitsArr = { StopBits.One, StopBits.OnePointFive, StopBits.Two, StopBits.None };
        Parity[] parityArr = { Parity.None, Parity.Space, Parity.Mark, Parity.Odd, Parity.Even };
        /// <summary>
        /// 枚举win32 api
        /// </summary>
        public enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }
        /// <summary>
        /// WMI取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        private static string[] GetHarewareInfo(HardwareEnum hardType, string propKey)
        {

            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value != null)
                        {
                            String str = hardInfo.Properties[propKey].Value.ToString();
                            if (str.Contains("(COM"))
                            {
                                strs.Add(str);
                            }
                        }
                    }
                }
                return strs.ToArray();
            }
            catch
            {
                MessageBox.Show("error");
                return null;
            }
            finally
            {
                strs = null;
            }
        }//end of func GetHarewareInfo().
         //通过WMI获取COM端口

        /// <summary>
        /// 设置控件状态
        /// </summary>
        private void setWidget(bool state)
        {
            button1.Enabled = state;
            BaudRateBox.Enabled = state;
            DataLenthBox.Enabled = state;
            ParityBox.Enabled = state;
            StopBitBox.Enabled = state;
            checkBox1.Enabled = state;
        }

        /// <summary>
        /// 刷新串口设备
        /// </summary>
        private void RefreshCOMList()
        {
            setWidget(false);
            string[] str = GetHarewareInfo(HardwareEnum.Win32_PnPEntity, "Name");//获取全部驱动名称
            try
            {
                SerialPortBox.Items.Clear();//清除下拉菜单
                for (int i = 0; i < str.Length; i++)
                {
                    SerialPortBox.Items.Add(str[i]);//重新写入
                }
                //硬件改动时不改变原来选中的设备，如果选中设备被移除，则选中第一个
                //***********************************************start
                int j = 0;
                foreach (var i in SerialPortBox.Items)//检测选中设备是否存在于新列表
                {
                    if(i.ToString() == currentCOM)
                    {
                        break;
                    }
                    j++;
                }
                if(j < SerialPortBox.Items.Count)//如果存在则不改变设置
                {
                    SerialPortBox.SelectedIndex = j;
                    SerialPortBox.SelectedItem = currentCOM;
                }
                else                             //不存在则选中第一个   移除的为选中设备
                {
                    SerialPortBox.SelectedIndex = 0;
                    if(serialPort1.IsOpen == false)
                    {
                        serialPort1.Close();
                        comState = false;
                        setWidget(true);
                        SerialPortBox.Enabled = true;
                        button1.Text = "打开串口";
                    }
                }
                //***********************************************end
            }
            catch (Exception)
            {
                MessageBox.Show("没有可用端口", "提示");
            }
            if (serialPort1.IsOpen == false)
            {
                setWidget(true);
            }
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int startIndex, endIndex;
            startIndex = SerialPortBox.SelectedItem.ToString().LastIndexOf("(");
            endIndex = SerialPortBox.SelectedItem.ToString().LastIndexOf(")");
            string comPortName = SerialPortBox.SelectedItem.ToString().Substring(startIndex + 1, endIndex - startIndex - 1);
            
            if (comState == false)
            {
                //设置串口属性
                serialPort1.StopBits = stopBitsArr[StopBitBox.SelectedIndex];
                serialPort1.Parity = parityArr[ParityBox.SelectedIndex];
                serialPort1.BaudRate = baudRateArr[BaudRateBox.SelectedIndex];
                serialPort1.DataBits = dataLenthArr[DataLenthBox.SelectedIndex];
                serialPort1.PortName = comPortName;
                try
                {
                    serialPort1.Open();//打开串口
                }
                catch
                {
                    MessageBox.Show("打开失败！");
                    return;
                }
                comState = true;
                setWidget(false);
                button1.Enabled = true;
                button1.Text = "关闭串口";
                SerialPortBox.Enabled = false;
            }
            else
            {
                serialPort1.Close();
                comState = false;
                setWidget(true);
                SerialPortBox.Enabled = true;
                button1.Text = "打开串口";
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            BaudRateBox.SelectedIndex = 8;
            DataLenthBox.SelectedIndex = 3;
            ParityBox.SelectedIndex = 0;
            StopBitBox.SelectedIndex = 0;
            RefreshCOMList();
            SerialPortBox.SelectedIndex = 0;
            chart1.Hide();
        }
        

        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x219;
            if (m.Msg == WM_DEVICECHANGE)
            {
                timer1.Enabled = true;
            }
            base.WndProc(ref m); //将系统消息传递自父类的WndProc
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            RefreshCOMList();
        }

        private void SerialPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentCOM = SerialPortBox.SelectedItem.ToString();
        }
        private void SetLableText(string text1, string text2)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.AttentionLabel.InvokeRequired)
            {
                SetLableCallback d = new SetLableCallback(SetLableText);
                this.Invoke(d, new object[] { text1, text2 });
            }
            else
            {
                this.AttentionLabel.Text = text1;
            }
            if (this.MeditationLabel.InvokeRequired)
            {
                SetLableCallback d = new SetLableCallback(SetLableText);
                this.Invoke(d, new object[] { text1, text2 });
            }
            else
            {
                this.MeditationLabel.Text = text2;
            }
        }
        private void SetNoticeLableText(string text1, string text2)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.signalLabel.InvokeRequired)
            {
                SetLableCallback d = new SetLableCallback(SetNoticeLableText);
                this.Invoke(d, new object[] { text1, text2 });
            }
            else
            {
                this.signalLabel.Text = text1;
                if (text1 == "信号强度：100" || text1 == "信号强度：99" || text1 == "信号强度：98" || text1 == "信号强度：97"
                     || text1 == "信号强度：96" || text1 == "信号强度：95")
                {
                    this.signalLabel.ForeColor = Color.LimeGreen;
                }
                else
                {
                    this.signalLabel.ForeColor = Color.Red;
                }
            }
            if (this.stateLabel.InvokeRequired)
            {
                SetLableCallback d = new SetLableCallback(SetNoticeLableText);
                this.Invoke(d, new object[] { text1, text2 });
            }
            else
            {
                this.stateLabel.Text = text2;
                if(text2 == "疲劳驾驶")
                {
                    this.stateLabel.ForeColor = Color.Red;
                }
                else
                {
                    this.stateLabel.ForeColor = Color.LimeGreen;
                }
            }
        }
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }
        private void AddText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text += text;
            }
        }
        private void addY(int y1,int y2)
        {
            if (this.chart1.InvokeRequired)
            {
                addYCallback d = new addYCallback(addY);
                this.Invoke(d, new object[] { y1,y2 });
            }
            else
            {
                this.chart1.Series["AttentionLine"].Points.AddY(y1);
                this.chart1.Series["MeditationLine"].Points.AddY(y2);
            }
        }
        private string ByteArrayToHexString(byte[] data,int num)
        {//字节数组转化为16进制字符串
            string sb="";
            for (int i=0;i<num;i++)
            {
                sb += (data[i] < 16 ? "0" : "") + Convert.ToString(data[i], 16) + " ";
            }
                
            return sb.ToUpper();
        }
        int number = 0;
        byte[] dat = new byte[20000];
        int offset = 0;
        int[] Attention = new int[10];
        int[] Meditation = new int[10];
        int AttentionAV = 0;
        int MeditationAV = 0;
        Point point = new Point(0, 0);
        Font font = new Font("Consolas", 14, FontStyle.Regular);
        System.Drawing.Pen myPen0 = new System.Drawing.Pen(System.Drawing.Color.Gray);
        System.Drawing.Pen myPen1 = new System.Drawing.Pen(System.Drawing.Color.SaddleBrown);
        System.Drawing.Pen myPen2 = new System.Drawing.Pen(System.Drawing.Color.Blue);
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);//画刷
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                number++;
                int num = serialPort1.BytesToRead;
                serialPort1.Read(dat, offset, num);
                offset += num;
                if (number >= 513 || offset > 8500)
                {
                    //SetText(ByteArrayToHexString(dat, offset));
                    for (int i = 0; i < offset - 34; i++)
                    {
                        if (dat[i] == 0xaa && dat[i + 1] == 0xaa && dat[i + 2] == 0x20 && dat[i + 3] == 0x02)
                        {
                            if (dat[i + 4] < 5)
                            {
                                AddText("Attention = " + dat[i + 6 + 24 + 2].ToString() + "\r\n");
                                AddText("Meditation = " + dat[i + 6 + 24 + 4].ToString() + "\r\n");
                                addY(dat[i + 6 + 24 + 2],dat[i + 6 + 24 + 4]);
                                for (int j = 0; j < 9; j++)
                                {
                                    Attention[j] = Attention[j + 1];
                                    Meditation[j] = Meditation[j + 1];
                                }
                                Attention[9] = dat[i + 6 + 24 + 2];
                                Meditation[9] = dat[i + 6 + 24 + 4];
                                System.Drawing.Graphics formGraphics = panel1.CreateGraphics();

                                formGraphics.Clear(Color.White);
                                formGraphics.DrawLine(myPen0, panel1.Width - 1, 0, panel1.Width - 1, panel1.Height);
                                formGraphics.DrawLine(myPen0, 0, 0, panel1.Width, 0);
                                formGraphics.DrawLine(myPen0, 0, panel1.Height - 1, panel1.Width, panel1.Height - 1);
                                for (int m = 10; m >= 0; m--)
                                {
                                    point.X = 0;
                                    point.Y = panel1.Height * m / 10;
                                    formGraphics.DrawString((100 - m * 10).ToString(), font, myBrush, point);
                                    formGraphics.DrawLine(myPen0, m * panel1.Width / 10, 0, m * panel1.Width / 10, panel1.Height);
                                    formGraphics.DrawLine(myPen0, m * panel1.Width / 10, 0, m * panel1.Width / 10, panel1.Height);
                                    formGraphics.DrawLine(myPen0, panel1.Width / 10, point.Y, panel1.Width, point.Y);
                                }
                                for (int k = 0; k < 9; k++)
                                {
                                    formGraphics.DrawLine(myPen1, (k + 1) * panel1.Width / 10, panel1.Height - Attention[k] * panel1.Height / 100, (k + 2) * panel1.Width / 10, panel1.Height - Attention[k + 1] * panel1.Height / 100);
                                    formGraphics.DrawLine(myPen2, (k + 1) * panel1.Width / 10, panel1.Height - Meditation[k] * panel1.Height / 100, (k + 2) * panel1.Width / 10, panel1.Height - Meditation[k + 1] * panel1.Height / 100);
                                    formGraphics.DrawLine(myPen1, (k + 1) * panel1.Width / 10, panel1.Height - Attention[k] * panel1.Height / 100 - 1, (k + 2) * panel1.Width / 10, panel1.Height - Attention[k + 1] * panel1.Height / 100 - 1);
                                    formGraphics.DrawLine(myPen2, (k + 1) * panel1.Width / 10, panel1.Height - Meditation[k] * panel1.Height / 100 - 1, (k + 2) * panel1.Width / 10, panel1.Height - Meditation[k + 1] * panel1.Height / 100 - 1);
                                }
                                formGraphics.Dispose();
                                AttentionAV = 0;
                                MeditationAV = 0;
                                for (int l = 0; l < 10; l++)
                                {
                                    AttentionAV += Attention[l];
                                    MeditationAV += Meditation[l];
                                }
                                AttentionAV /= 10;
                                MeditationAV /= 10;
                                SetLableText("专注度平均值：" + AttentionAV.ToString(),
                                    "放松度平均值：" + MeditationAV.ToString());
                                //timer2.Enabled = true;
                            }
                            SetNoticeLableText("信号强度："+ ((dat[i + 4] <=100)?(100- dat[i + 4]):0).ToString(),
                                AttentionAV < 35?"疲劳驾驶":"未疲劳驾驶");
                            if (AttentionAV < 35 && this.chart1.Series["AttentionLine"].Points.Count >= 9)
                            {
                                if(checkBox2.Checked == true)
                                    SpeakChinese();
                            }
                            
                        }
                    }
                    number = 0;
                    offset = 0;
                }
            }
            else
            {
                AddText(serialPort1.ReadExisting());
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null && serialPort1.IsOpen)
            {
                serialPort1.Write(textBox2.Text);
            }
        }


        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;
            if (e.KeyChar == (char)1)       // Ctrl-A 相当于输入了AscII=1的控制字符
            {
                textBox.SelectAll();
                e.Handled = true;      // 不再发出“噔”的声音
            }
        }
        private void Form1_ControlAdded(object sender, ControlEventArgs e)
        {
            //使“未来”生效
            e.Control.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Form1_ControlAdded);
            //使“子孙”生效
            foreach (Control c in e.Control.Controls)
            {
                Form1_ControlAdded(sender, new ControlEventArgs(c));
            }
            //使“过去”生效
            TextBox textBox = e.Control as TextBox;
            if (textBox != null)
            {
                textBox.KeyPress += TextBox_KeyPress;
            }
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            myBrush.Dispose();
            myPen0.Dispose();
            myPen1.Dispose();
            myPen2.Dispose();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            System.Drawing.Graphics formGraphics = panel1.CreateGraphics();

            formGraphics.Clear(Color.White);
            for (int m = 10; m >= 0; m--)
            {
                point.X = 0;
                point.Y = panel1.Height * m / 10;
                formGraphics.DrawString((100 - m * 10).ToString(), font, myBrush, point);
                formGraphics.DrawLine(myPen0, m * panel1.Width / 10, 0, m * panel1.Width / 10, panel1.Height);
                formGraphics.DrawLine(myPen0, panel1.Width / 10, point.Y, panel1.Width, point.Y);
            }
            formGraphics.DrawLine(myPen0, panel1.Width - 1, 0, panel1.Width - 1, panel1.Height);
            formGraphics.DrawLine(myPen0, 0, 0, panel1.Width, 0);
            formGraphics.DrawLine(myPen0, 0, panel1.Height - 1, panel1.Width, panel1.Height - 1);
            formGraphics.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            System.Drawing.Graphics formGraphics = panel1.CreateGraphics();

            formGraphics.Clear(Color.White);
            for (int m = 10; m >= 0; m--)
            {
                point.X = 0;
                point.Y = panel1.Height * m / 10;
                formGraphics.DrawString((100 - m * 10).ToString(), font, myBrush, point);
                formGraphics.DrawLine(myPen0, m * panel1.Width / 10, 0, m * panel1.Width / 10, panel1.Height);
                formGraphics.DrawLine(myPen0, panel1.Width / 10, point.Y, panel1.Width, point.Y);
            }
            formGraphics.DrawLine(myPen0, panel1.Width - 1, 0, panel1.Width - 1, panel1.Height);
            formGraphics.DrawLine(myPen0, 0, 0, panel1.Width, 0);
            formGraphics.DrawLine(myPen0, 0, panel1.Height - 1, panel1.Width, panel1.Height - 1);
            formGraphics.Dispose();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            chart1.Hide();
        }
        private Thread th = null;
        bool SpeakFlag = false;
        private void SpeakChinese()
        {
            if (SpeakFlag == false)
            {
                SpeakFlag = true;
                th = new System.Threading.Thread(ThreadMethod);
                th.Start(); //启动线程  
            }
        }
        public void ThreadMethod()
        {
            SpVoice voice = new SpVoice();
            int volumeBackup = voice.Volume;
            voice.Volume = 100;
            voice.Speak("您的状态略疲劳", SpeechVoiceSpeakFlags.SVSFDefault);
            Thread.Sleep(10000);
            voice.Volume = volumeBackup;
            this.SpeakFlag = false;
            this.th.Abort();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            chart1.Show();
        }
        
    }
}
