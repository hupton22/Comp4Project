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

        Neutron[] neutrons = new Neutron[20]; //create an array that stores items of the custom type "neutron"
        Atom[] atoms = new Atom[10];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < neutrons.Length; i++)
            {
                neutrons[i] = new Neutron();

                neutrons[i].SetVelocityX(random.Next(-5, 5));//randomly selwecting a velocity for each neutron in the x and y directions
                neutrons[i].SetVelocityY(random.Next(-5, 5));

                neutrons[i].SetXPos(random.Next(0, ClientSize.Width - Neutron.NeutronWidth));//randomly selecting a location for each neutron within the bounds of the window
                neutrons[i].SetYPos(random.Next(0, ClientSize.Height - Neutron.NeutronWidth));
                
            }

            for (int i = 0; i < atoms.Length; i++ )
            {
                atoms[i] = new Atom();

                atoms[i].SetXPos(random.Next(0, ClientSize.Width - Atom.AtomWidth));//randomly selecting a location for each atom within the bounds of the window, ensuring it is not spawned off the edge
                atoms[i].SetYPos(random.Next(0, ClientSize.Height - Atom.AtomWidth));

                //maybe add a loop here to check if the atom currently having its location set
                //is too close to any other atoms
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
            for (int i = 0; i < neutrons.Length; i++)
            {
                neutrons[i].move(ClientSize.Width, ClientSize.Height);
                for (int check = 0; check < atoms.Length; check++)//checks the displacement of the current neutron from all atoms in the simulation
                {
                    double xDist = ((neutrons[i].GetXPos) - (atoms[check].GetXPos)) * ((neutrons[i].GetXPos) - (atoms[check].GetXPos));
                    double yDist = 
                    float realDist = (xDist * xDist) - (yDist - yDist);
                    if (realDist < 30) & (atoms[check].hasSplit == false)
                    {
                        atoms[check].split();
                    }
                }
            }   
        }
        private void drawBalls(PaintEventArgs e)//draws all screen objects
        {
            foreach (Neutron ball in neutrons)//names the neutron we are looking at "ball, so it is refered to later
            {
                e.Graphics.FillEllipse(ball.PickBrush(), ball.GetXPos(), ball.GetYPos(), Neutron.NeutronWidth, Neutron.NeutronWidth);
                e.Graphics.DrawEllipse(Pens.Black, ball.GetXPos(), ball.GetYPos(), Neutron.NeutronWidth, Neutron.NeutronWidth);
            }

            foreach (Atom a in atoms)
            {
                e.Graphics.FillEllipse(a.PickBrush(), a.GetXPos(), a.GetYPos(), Atom.AtomWidth, Atom.AtomWidth);
                e.Graphics.DrawEllipse(Pens.Black, a.GetXPos(), a.GetYPos(), Atom.AtomWidth, Atom.AtomWidth);
            }

        }
    }
}
