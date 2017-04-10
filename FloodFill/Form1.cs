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

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
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
                        g.DrawLine(new Pen(Color.Black), prevLocation, e.Location);
                        prevLocation = e.Location;
                    }
                    break;
                case State.Fill:
                    break;
                default:
                    break;
            }
        }

        Point prevLocation;
        Queue<Point> q = new Queue<Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevLocation = e.Location;
            switch (state)
            {
                case State.Pen:
                    break;
                case State.Fill:
                    q.Enqueue(e.Location);
                    GoFloodFill();
                    break;
                default:
                    break;
            }
        }

        private bool IsGoodPoint(Point p)
        {
            if (p.X < 0) return false;
            if (p.Y < 0) return false;
            if (p.X >= pictureBox1.Width) return false;
            if (p.Y >= pictureBox1.Height) return false;


            return true;
        }
        private void GoFloodFill()
        {
            while (q.Count > 0)
            {
                Point cur = q.Dequeue();

                Fill(cur);

                Point p1 = new Point(cur.X, cur.Y + 1);
                Point p2 = new Point(cur.X + 1, cur.Y);
                Point p3 = new Point(cur.X - 1, cur.Y);
                Point p4 = new Point(cur.X, cur.Y - 1);

                if (IsGoodPoint(p1))
                {
                    q.Enqueue(p1);
                    GoFloodFill();
                }
                if (IsGoodPoint(p2))
                {
                    q.Enqueue(p2);
                    GoFloodFill();
                }\
                if (IsGoodPoint(p3))
                {
                    q.Enqueue(p3);
                    GoFloodFill();
                }
                if (IsGoodPoint(p4))
                {
                    q.Enqueue(p4);
                    GoFloodFill();
                }
            }
        }

        Graphics g;

        private void Fill(Point cur)
        {
         
            g.FillRectangle(new Pen(Color.Black).Brush, cur.X,cur.Y, 1, 1);
        }
    }

    enum State
    {
        Pen,
        Fill
    }
}
