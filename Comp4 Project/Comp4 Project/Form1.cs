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

        //public int atomNumber = 20;
        
        Atom[] atoms = new Atom[41];
        Neutron[] neutrons = new Neutron[5]; //creates and populates an array with neutron objects

        public double realDist = 0;

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

            //int arrayPos = 0;
            //atoms[i].SetXPos(random.Next(0, ClientSize.Width - Atom.AtomWidth));//randomly selecting a location for each atom within the bounds of the window, ensuring it is not spawned off the edge
            //atoms[i].SetYPos(random.Next(0, ClientSize.Height - Atom.AtomWidth));

            //for (int m = 0; m < 5; m++)//nested for loops to put atoms in a grid
            //{
            //    if (((i * 10) < (ClientSize.Width - Atom.AtomWidth+10) && ((m * 10) < (ClientSize.Width - Atom.AtomWidth+10))))
            //    {
            //        atoms[arrayPos] = new Atom();
            //        atoms[arrayPos].SetXPos(i * 10);
            //        atoms[arrayPos].SetYPos(m * 10);
            //        arrayPos = (arrayPos + 1);
            //    }                   
            //}

            //atoms[i].SetXPos(i * 30);
            //atoms[i].SetYPos(i * 30);

            //maybe add a loop here to check if the atom currently having its location set
            //is too close to any other atoms

            //for (int k = 0; k < atoms.Length; k++)
            //{
            //    for (int l = 0; l <atoms.Length; l++)
            //    {
            //        double xDist = (atoms[k].GetXPos()) - (atoms[l].GetXPos());//find difference in x coordinate
            //        double yDist = (atoms[k].GetYPos()) - (atoms[l].GetYPos());//find difference in y coordinate
            //        realDist = Math.Sqrt((xDist * xDist) + (yDist * yDist));//pythagoras
            //        int x = k;
            //        int y = l;
            //        if ((realDist < (Atom.AtomWidth)) && (x != y))//making sure we are not comparing an atom to itself!
            //        {
            //            atoms[k].SetXPos(random.Next(0, ClientSize.Width - Atom.AtomWidth));//reassign
            //            atoms[k].SetYPos(random.Next(0, ClientSize.Height - Atom.AtomWidth));
            //        }
            //    }
            //}

            for (int i = 0; i < 11; i++)
            {
                atoms[i] = new Atom();
                atoms[i].SetXPos((i * 55) + 10);
                atoms[i].SetYPos(10);               
            }

            for (int i = 11; i < 21; i++)
            {
                atoms[i] = new Atom();
                atoms[i].SetXPos(((i - 11) * 55) + 10);
                atoms[i].SetYPos(10 + Atom.AtomWidth);
            }

            for (int i = 21; i < 31; i++)
            {
                atoms[i] = new Atom();
                atoms[i].SetXPos((i - 21 * 55) + 10);
                atoms[i].SetYPos(2 * (Atom.AtomWidth + 10));
            }

            for (int i = 31; i < 41; i++)
            {
                atoms[i] = new Atom();
                atoms[i].SetXPos((i - 31 * 55) + 10);
                atoms[i].SetYPos(3 * (Atom.AtomWidth + 10));
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
            moveBalls();//look down
            Refresh();
        }

        private void moveBalls()
        {
            for (int i = 0; i < neutrons.Length; i++)
            {
                neutrons[i].move(ClientSize.Width, ClientSize.Height);
                for (int check = 0; check < atoms.Length; check++)//checks the displacement of the current neutron from all atoms in the simulation
                {
                    //double ax = neutrons[i].GetXPos();
                    double xDist = (neutrons[i].GetXPos() - 25) - (atoms[check].GetXPos());//find difference in x coordinate
                    double yDist = (neutrons[i].GetYPos() - 50) - (atoms[check].GetYPos() - 25);//find difference in y coordinate
                    realDist = Math.Sqrt((xDist * xDist) + (yDist * yDist));//pythagoras
                    if ((realDist < (Atom.AtomWidth / 2)) && (atoms[check].hasSplit == false))
                    {
                        atoms[check].split();//split the atom that the neutron has approached
                    }
                }
            }   
        }

        //private static Array addNeutrons

        private void drawBalls(PaintEventArgs e)//draws all screen objects
        {
            foreach (Neutron ball in neutrons)//names the neutron we are looking at "ball", so it is refered to later
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
