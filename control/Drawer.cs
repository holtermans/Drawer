using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace control
{
    public partial class Drawer : Form
    {
        private int MyX = 0;
        private const int Unit_length = 10;//单位格大小
        private int DrawStep = 8;//默认绘制单位
        private const int Y_Max = 512;//Y轴最大数值
        private const int MaxStep = 33;//绘制单位最大值
        private const int MinStep = 1;//绘制单位最小值
        private const int StartPrint = 32;//点坐标偏移量
        private List<byte> DataList = new List<byte>();//数据结构----线性链表
        private Pen TablePen = new Pen(Color.FromArgb(0x00, 0x00, 0x00));//轴线颜色
        private Pen LinesPen = new Pen(Color.FromArgb(0xa0, 0x00, 0x00));//波形颜色
        public Drawer()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                           ControlStyles.AllPaintingInWmPaint,
                           true);//开启双缓冲

            this.UpdateStyles();
            InitializeComponent();
        }

        private void Drawer_Load(object sender, EventArgs e)
        {

        }

        private void Drawer_Paint(object sender, PaintEventArgs e)//窗体paint事件响应方法
        {
            String Str = "";
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            e.Graphics.FillRectangle(Brushes.White, e.Graphics.ClipBounds);

            Pen p = Pens.Black;
            Brush b = new LinearGradientBrush(new Point(0, this.Width / 2), new Point(this.Height, this.Width / 2), Color.FromArgb(50, 50, 100), Color.FromArgb(50, 50, 200));
            
            Rectangle r = new Rectangle(0, 0, 5, 5);//标识圆的大小
            e.Graphics.DrawEllipse(p, r);
            
            r.X = MyX;

            e.Graphics.Clear(BackColor);
            e.Graphics.DrawEllipse(p, r);
            
        }
        public void AddData(String x)
        {
            MyX = Convert.ToInt16(x);
            Invalidate();
        }
    }
}
