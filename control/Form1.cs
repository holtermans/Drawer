using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace control
{
    public partial class Form1 : Form
    {
        /**定义一个全局的drawer变量***/
        Drawer displayer;
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)//用于打开或者关闭串口
        {
            if(serialPort1.IsOpen)//如果串口处于打开状态
            {
                try
                {
                    serialPort1.Close();
                }
                catch
                {

                }
                button2.Text = "打开串口";
                button2.ForeColor = Color.FromArgb(0, 0, 0);
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    button2.Text = "关闭串口";
                    button2.ForeColor = Color.FromArgb(255,0,0);
                }
                catch
                {
                    MessageBox.Show("打开串口失败", "错误");
                }
                
            }
        }

        private void Button1_Click(object sender, EventArgs e)//端口检测
        {
            SearchAndAddSerialToCombobox(serialPort1,comboBox1);
        }


        private void SearchAndAddSerialToCombobox(SerialPort Myport,ComboBox MyBox)
        {
            String[] MyString = new string[20];//定义一个字符数组
            String Buffer;//定义一个字符串
            MyBox.Items.Clear();
            int j=0;
            /***********核心代码 循环打开端口*********/
            for(int i = 1;i<20;i++)
            {
                try
                {
                    Buffer = "COM" + i.ToString();
                    Myport.PortName = Buffer;
                    Myport.Open();
                    MyString[j++] = Buffer;
                    MyBox.Items.Add(Buffer);
                    Myport.Close();
                }
                catch
                {

                }
                MyBox.Text = MyString[0];
            }



        }

        private void Button3_Click(object sender, EventArgs e)
        {
            displayer = new Drawer();
            displayer.Show();//显示窗口
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String str;
            if (!(radioButton3.Checked))//默认是字符接收
            {
                str = serialPort1.ReadLine();
                textBox2.AppendText(str);
                
            }
            else
            {
                byte data;
                data = (byte)serialPort1.ReadByte();//此处需要进行强制类型转换，将（int）类型数据转换为byte类型数据
                str = Convert.ToString(data, 10).ToUpper();
                //textBox2.AppendText("0x" + (str.Length == 1 ? "0" + str : str) + "0");
                textBox2.AppendText(str);
            }

            if (displayer != null)
                displayer.AddData(str);
        }

        private void Button4_Click(object sender, EventArgs e) //发送按键
        {
            byte[] Data = new byte[1];//作用同上集
            if (serialPort1.IsOpen)//判断串口是否打开，如果打开执行下一步操作
            {
                if (textBox1.Text != "")
                {
                    if (!(radioButton1.Checked))//如果发送模式是字符模式
                    {
                        try
                        {
                            serialPort1.WriteLine(textBox1.Text);//写数据
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("串口数据写入错误", "错误");//出错提示
                            serialPort1.Close();
                            button2.Text = "打开串口";//打开串口按钮可用
                           
                        }
                    }
                    else
                    {

                        Data[0] = Convert.ToByte(textBox1.Text, 10);
                        serialPort1.Write(Data, 0,1 );
                       /* for (int i = 0; i < (textBox1.Text.Length - textBox1.Text.Length % 2) / 2; i++)//取余3运算作用是防止用户输入的字符为奇数个
                        {
                            Data[0] = Convert.ToByte(textBox1.Text.Substring(i * 2, 2), 10);
                            serialPort1.Write(Data, 0, 1);//循环发送（如果输入字符为0A0BB,则只发送0A,0B）
                        }
                        if (textBox1.Text.Length % 2 != 0)//剩下一位单独处理
                        {
                            Data[0] = Convert.ToByte(textBox1.Text.Substring(textBox1.Text.Length - 1, 1), 16);//单独发送B（0B）
                            serialPort1.Write(Data, 0, 1);//发送
                        }*/
                    }
                }
            }
        }
    }
}
