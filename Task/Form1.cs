using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Task
{
    public partial class Form1 : Form
    {
        int A = 600, B = 500, R, V, P;
        Point[] Pts;
        Point CL;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            
            panel1.Location = new Point(150, 0);
           
            this.Size = new Size(A, B);
        }

        //Start button
        private void button1_Click(object sender, EventArgs e)
        {
            try { A = Convert.ToInt32(textBox1.Text); } catch { }; //Window width
            try { B = Convert.ToInt32(textBox2.Text); } catch { }; //Window height
            try { R= Convert.ToInt32(textBox3.Text); } catch { }; //Circle radius
            try { P = Convert.ToInt32(textBox4.Text); } catch { }; //Number of points
            try { V = Convert.ToInt32(textBox5.Text); } catch { }; //Circle movement speed
            this.Size = new Size(A, B); //Window size
            panel1.Size = new Size(this.Width - 150, this.Height); //Panel size
            SetPoints(P, panel1);
            CL = new Point(panel1.Width / 2 - R / 2, panel1.Height / 2 - R / 2); //Circle start point
            double dx, dy;
            double x;
            PaintField(panel1);
            foreach (Point pnt in Pts)
            {
                dx = (pnt.X - CL.X) / 10;
                dy = (pnt.Y - CL.Y) / 10;
                x = 10 / Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                dx *= x;
                dy *= x;
                while (CL != pnt)
                {
                    if (Math.Sqrt(Math.Pow(CL.X - pnt.X, 2) + Math.Pow(CL.Y - pnt.Y, 2)) < R)
                    {
                        CL = pnt;
                    }
                    else
                        CL = new Point(Convert.ToInt32(CL.X + dx), Convert.ToInt32(CL.Y + dy));
                    PaintField(panel1);
                    Thread.Sleep(1000 / V * 10);

                }
            }                                 
        }

        void SetPoints(int p, Panel field) //Set the coordinates of points
        {
            Random rand = new Random();
            Pts = new Point[p];
            for (int i = 0; i < p; i++)
            {
                Pts[i] = new Point(rand.Next(R / 2 - 1, field.Width - R / 2), rand.Next(R / 2 - 1, field.Height - R / 2));

            }
        }
       
        void PaintField(Panel field) //Animation
        {
            Bitmap buf1 = new Bitmap(field.Width, field.Height);
            using (Graphics g = Graphics.FromImage(buf1))
            {
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, field.Width, field.Height));                
                foreach (Point pnt in Pts)
                    g.DrawEllipse(Pens.Red, new Rectangle(pnt.X - 2, pnt.Y - 2 , 4, 4));
                g.DrawEllipse(Pens.Purple, new Rectangle(CL.X - R / 2, CL.Y - R / 2, R, R));               
                field.CreateGraphics().DrawImageUnscaled(buf1, 0, 0);
            }
        }
    }
}
