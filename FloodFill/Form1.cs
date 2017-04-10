using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloodFill
{
    public partial class Form1 : Form
    {
        State state = State.Pen;
        Graphics g;
        Point prevLocation;
        Queue<Point> q = new Queue<Point>();

        Pen p = new Pen(Color.Black);
        Color originColor;
        Color fillColor;
        Bitmap bmp;


        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            state = State.Pen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            state = State.Fill;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (state)
            {
                case State.Pen:
                    if (e.Button == MouseButtons.Left)
                    {
                        g.DrawLine(p, prevLocation, e.Location);
                        prevLocation = e.Location;
                        pictureBox1.Refresh();
                    }
                    break;
                case State.Fill:
                    break;
                default:
                    break;
            }
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevLocation = e.Location;
            switch (state)
            {
                case State.Pen:
                    break;
                case State.Fill:
                    originColor = bmp.GetPixel(e.X, e.Y);
                    fillColor = p.Color;
                    F1(e.Location);
                    break;
                default:
                    break;
            }
        }

        private void F2(Point point)
        {
            SimpePaint.MapFill mf = new SimpePaint.MapFill();
            mf.Fill(CreateGraphics(), point, fillColor, ref bmp);
        }

        private void F1(Point point)
        {
            q.Enqueue(point);
            GoFloodFill();
        }

        private void Step(Point p)
        {
            if (p.X < 0) return;
            if (p.Y < 0) return;
            if (p.X >= pictureBox1.Width) return;
            if (p.Y >= pictureBox1.Height) return;
            if (bmp.GetPixel(p.X, p.Y) != originColor) return;
            bmp.SetPixel(p.X, p.Y, fillColor);
            q.Enqueue(p);
        }

        private void GoFloodFill()
        {
            while (q.Count > 0)
            {
                Point cur = q.Dequeue();

                Step(new Point(cur.X, cur.Y + 1));
                Step(new Point(cur.X + 1, cur.Y));
                Step(new Point(cur.X - 1, cur.Y));
                Step(new Point(cur.X, cur.Y - 1));
            }
            pictureBox1.Refresh();

        }

    }

    enum State
    {
        Pen,
        Fill
    }
}
