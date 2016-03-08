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
        
        Atom[] atoms = new Atom[84]; 
        List<Neutron> neutronList = new List<Neutron>(); 

        public double realDist = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private double DegreeToRadian(int angle)
        {
            return Math.PI * angle / 180.0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random random = new Random();

            int startNeutrons = 1;
            for (int i = 0; i < startNeutrons; i++)
            {
                Neutron neutron = new Neutron();

                //int angle = (random.Next(1, 360));


                //double a = ();
                //neutron.SetVelocityX(Math.Cos(DegreeToRadian(angle)));

                neutron.SetVelocityX(random.Next(1, 5));
                neutron.SetVelocityY(random.Next(1, 5));

                neutron.SetXPos(random.Next(0, ClientSize.Width - Neutron.NeutronWidth));
                neutron.SetXPos(random.Next(0, ClientSize.Height - Neutron.NeutronWidth));

                neutronList.Add(neutron);
            }
                        
            int numberOfAtomsPerLine = 12;
            int numberOfAtomsPerRow = 7;
            int xDiff = (ClientSize.Width - 50) / numberOfAtomsPerLine; //to calculate the difference in the width of each x of the atoms
            int yDiff = (ClientSize.Height - 50) / numberOfAtomsPerRow; //to calculate the difference in the height of each x of the atom

            int x = xDiff; //The x-coordinate of the paint area
            int y = yDiff; //The y-coordinate of the paint area
            int counter = 0;

            for (int i = 0; i < atoms.Length; i++)
            {
                //start a new row
                if (counter >= numberOfAtomsPerLine)
                {
                    y = y + yDiff;
                    x = xDiff;
                    counter = 0; //reset the counter
                }

                atoms[i] = new Atom(x, y);
                x = x + xDiff;
                
                counter++;
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
            
            foreach (Neutron neutron in neutronList)
            {
                neutron.move(ClientSize.Width, ClientSize.Height); //Move all neutrons first
            }

            List<Neutron> newNeutrons = new List<Neutron>();
            Random random = new Random();
            foreach (Particle atom in atoms)
            {
                foreach (Particle neutron in neutronList)
                {
                    if (atom.InteractsWith(neutron))
                    {
                        //neturons can be created in here
                        int xPos = atom.GetXPos();
                        int yPos = atom.GetYPos();

                        for (int i = 0; i < 2; i++)
                        {
                            int vX = random.Next(1, 5);
                            int vY = random.Next(1, 5);

                            Neutron newNeutron = new Neutron();
                            newNeutron.SetXPos(xPos);
                            newNeutron.SetYPos(yPos);
                            newNeutron.SetVelocityX(vX);
                            newNeutron.SetVelocityY(vY);

                            newNeutrons.Add(newNeutron);
                        }
                    }
                }
            }

      
            neutronList.AddRange(newNeutrons);
        }

 

        private void drawBalls(PaintEventArgs e)//draws all screen objects
        {
            foreach (Neutron ball in neutronList)//names the neutron we are looking at "ball", so it is refered to later
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
