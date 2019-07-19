using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        double[,] x = new double[3, 4];
        public double[,] y = new double[3, 4];
        int[] points = new int[3];

        private int dpx(double x)
        {
            int p;
            p = (int)(x + 0.5);
            return p;
        }

        public int dpy(double y)
        {
            int p;
            p = (int)(y + 0.5);
            //int max = y.Cast<int>().Max(); 
            p = panel1.Height - p;
            return p;

        }

        public void DisplayLine(int x1, int y1, int x2, int y2, System.Drawing.Pen col)
        {
            Graphics g = panel1.CreateGraphics();
           // System.Drawing.Pen col = Pens.Black;
            g.DrawLine(col, x1, panel1.Height - y1, x2, panel1.Height - y2);
        }

        public void DeleteLine(int x1, int y1, int x2, int y2)
        {
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(Pens.White, x1, panel1.Height - y1, x2, panel1.Height - y2);
        }
        public void drawPolygon(int o, System.Drawing.Pen col)
        {
            int i, j;
            for (i = 0; i < points[o]; i++)
            {
                j = (i + 1) % points[o];
                DisplayLine(dpx(x[o, i]), dpy(y[o, i]), dpx(x[o, j]), dpy(y[o, j]), col);
            }
        }

        public void DeletePolygon(int o)
        {
            int i, j;
            for (i = 0; i < points[o]; i++)
            {
                j = (i + 1) % points[o];
                DeleteLine(dpx(x[o, i]), dpy(y[o, i]), dpx(x[o, j]), dpy(y[o, j]));
            }
        }

        public void translate(int o, int i, double tx, double ty)
        {
            x[o, i] = x[o, i] + tx;
            y[o, i] = y[o, i] + ty;

        }

        public void rotate(int o, int i, double t)
        {
            double x1, y1;
            x1 = x[o, i];
            y1 = y[o, i];
            x[o, i] = x1 * Math.Cos(t) - y1 * Math.Sin(t);
            y[o, i] = x1 * Math.Sin(t) + y1 * Math.Cos(t);
        }

        public void f_rotate(int o, int i, double t, double x, double y)
        {
            translate(o, i, -x, -y);
            rotate(o, i, t);
            translate(o, i, x, y);
        }

        public void scale(int o, int i, double sx, double sy)
        {
            x[o, i] = x[o, i] * sx;
            y[o, i] = y[o, i] * sy;
        }

        public void f_scale(int o, int i, double sx, double sy, double x, double y)
        {
            translate(o, i, -x, -y);
            scale(o, i, sx, sy);
            translate(o, i, x, y);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i, n;
            int h = 1;

            //minutes
            x[0, 0] = -0; y[0, 0] = -130;
            x[0, 1] = 0; y[0, 1] = 0;
            x[0, 2] = 0; y[0, 2] = 4;
            x[0, 3] = 0; y[0, 3] = 0;
            points[0] = 4;

            //second
            x[1, 0] = 0; y[1, 0] = -150;
            x[1, 1] = 0; y[1, 1] = 0;
            x[1, 2] = 0; y[1, 2] = 1;
            x[1, 3] = 0; y[1, 3] = 0;
            points[1] = 4;

            //hour
            x[2, 0] = 0; y[2, 0] = -65;
            x[2, 1] =1; y[2, 1] = -65;
            x[2, 2] = 1; y[2, 2] = 2;
            x[2, 3] = 0; y[2, 3] = 0;
            points[2] = 4;


            for (i = 0; i < 4; i++)
            {
                translate(0, i, 250, 250);
            }

           
            for (i = 0; i < 4; i++)
            {
                translate(1, i, 250, 250);
            }

            for (i = 0; i < 4; i++)
            {
                translate(2, i, 250, 250);
            }


            while (h == 1)
            {
                for (n = 0; n < 1000; n++)
                {

                    drawPolygon(0,Pens.Black);
                    drawPolygon(1,Pens.Red);
                    drawPolygon(2,Pens.Black);
                    System.Threading.Thread.Sleep(940);
                    DeletePolygon(1);
                    DeletePolygon(0);
                    DeletePolygon(2);

                    for (i = 0; i < 4; i++)
                    {
                        f_rotate(0, i, 0.00166, 250, 250);
                        f_rotate(1, i, 0.1, 250, 250);
                        f_rotate(2,i,0.000027,250,250);
                         
                    // f_rotate(0, i, 1.005, 150, 250);
                    // f_rotate(1, i, 1.005, 150, 250);

                    }
                }
            }
        }
    }
}
