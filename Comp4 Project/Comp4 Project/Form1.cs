using Comp4_Project.Particles;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp4_Project
{
    public partial class Form1 : Form
    {

        Neutron[] balls = new Neutron[20];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Neutron();

                balls[i].SetVelocityX(random.Next(-5, 5));//randomly selwecting a velocity fir each neutron in the x and y directions
                balls[i].SetVelocityY(random.Next(-5, 5));

                balls[i].SetXPos(random.Next(0, ClientSize.Width - Neutron.BallWidth));//randomly selecting a location for each ball within the bounds of the window
                balls[i].SetYPos(random.Next(0, ClientSize.Height - Neutron.BallWidth));
                
            }

            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            this.UpdateStyles();
        
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);

            drawBalls(e);
        }

        private void timerMoveBall_Tick(object sender, EventArgs e)
        {
            moveBalls();
            Refresh();
        }

        private void moveBalls()
        {
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i].move(ClientSize.Width, ClientSize.Height);
            }   
        }

        private void drawBalls(PaintEventArgs e)
        {
            foreach (Neutron ball in balls) 
            {
                e.Graphics.FillEllipse(ball.PickBrush(), ball.GetXPos(), ball.GetYPos(), Neutron.BallWidth, Neutron.BallWidth);
                e.Graphics.DrawEllipse(Pens.Black, ball.GetXPos(), ball.GetYPos(), Neutron.BallWidth, Neutron.BallWidth);
            }

        }
    }
}
