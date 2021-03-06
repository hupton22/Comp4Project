﻿using Comp4_Project.Particles;

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

       
        
        Atom[] atoms = new Atom[84]; //initialise the array of atoms
        List<Neutron> neutronList = new List<Neutron>(); //initialise the list of neutrons

        public double realDist = 0;
        
        int userSpeed = Convert.ToInt16(Speed.Text);
        userSpeed = Convert.ToInt16(speed.tex

        public Form1()
        {
            InitializeComponent();
        }

        private double DegreeToRadian(int angle)//this function is not actually used. It was part of an attempt to give all of the neutrons the same velocity...
        {//whicle still sending them off at random angles. this wouild usee trigonometry, and so a function was needed to convert angles measured in degrees to angles in radians...
            return Math.PI * angle / 180.0;//which are the default angle unit in c#
        }

        private void RotateNeutron(Neutron newNeutron, Neutron oldNeutorn, double radians) 
        {
            double sin = Math.Sin(radians);
            double cos = Math.Cos(radians);

            double newVX = oldNeutorn.GetVelocityX() * cos - oldNeutorn.GetVelocityY() * sin;
            double newVY = oldNeutorn.GetVelocityX() * sin + oldNeutorn.GetVelocityY() * cos;

            newNeutron.SetVelocityX(newVX);
            newNeutron.SetVelocityY(newVY);
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
            Random random = new Random();//create a new random object

            int startNeutrons = 1;
            for (int i = 0; i < startNeutrons; i++)
            {
                Neutron neutron = new Neutron();//create a new neutron object

               
                neutron.SetVelocityX(4);//select a random velocity in the x direction
                neutron.SetVelocityY(3);//same in the y

                neutron.SetXPos(random.Next(0, ClientSize.Width - Neutron.NeutronWidth));
                neutron.SetXPos(random.Next(0, ClientSize.Height - Neutron.NeutronWidth));

                neutronList.Add(neutron);//adding the new neutron to the list
            }
                        
            int numberOfAtomsPerRow = 12;//difining the size of the grid
            int numberOfAtomsPerColumn = 7;
            int xDiff = (ClientSize.Width - Atom.AtomWidth) / numberOfAtomsPerRow; //to calculate the difference in the width of each x of the atoms
            int yDiff = (ClientSize.Height - Atom.AtomWidth) / numberOfAtomsPerColumn; //to calculate the difference in the height of each x of the atom

            int offset = 25;
            int x = offset; //The x-coordinate of the paint area
            int y = offset; //The y-coordinate of the paint area
            int counter = 0;

            for (int i = 0; i < atoms.Length; i++)//loop for placing the atoms in a grid
            {
                //start a new row
                if (counter >= numberOfAtomsPerRow)
                {
                    y = y + yDiff;
                    x = offset;
                    counter = 0; //reset the counter
                }

                atoms[i] = new Atom(x, y);//populate the array with atom objects
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

        private void Form1_Paint(object sender, PaintEventArgs e)//this procedure handles updating of the screen
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);

            drawBalls(e);
        }

        private void timerMoveBall_Tick(object sender, EventArgs e)//this procedure is executed every time the timer "ticks"
        {
            moveBalls();
            Refresh();
        }

      

        private void moveBalls()//procedure to move all neutrons
        {
            
            foreach (Neutron neutron in neutronList)
            {
                neutron.move(ClientSize.Width, ClientSize.Height); //Move all neutrons first
            }

            List<Neutron> newNeutrons = new List<Neutron>();//temporary neutron list
            Random random = new Random();
            foreach (Particle atom in atoms)
            {
                foreach (Neutron neutron in neutronList)
                {
                    if (atom.InteractsWith(neutron))//only trigger this block if the neutron interacts with an atom
                    {
                        
                        int xPos = atom.GetXPos();//retrieve the coordinates of the atom that is splitting
                        int yPos = atom.GetYPos();

                        for (int i = 0; i < 2; i++)
                        {
                            //System.Threading.Thread.Sleep(100);
                            int vX = random.Next(1, 5);
                            int vY = random.Next(1, 5);

                            Neutron newNeutron = new Neutron();//create a  new neutron object
                            newNeutron.SetXPos(xPos);//places the new neutron in the location of the atom that is splitting
                            newNeutron.SetYPos(yPos);

                            int degree = random.Next(1, 360);
                            double radians = DegreeToRadian(degree);

                            this.RotateNeutron(newNeutron, neutron, radians);
                            //newNeutron.SetVelocityX(vX);
                            //newNeutron.SetVelocityY(vY);

                            newNeutrons.Add(newNeutron);//letting the new neutron become a menber of the temp neutron list
                        }
                    }
                }
            }

      
            neutronList.AddRange(newNeutrons);//adding the contents of the temp list to the main list
        }

 

        private void drawBalls(PaintEventArgs e)//draws all screen objects
        {
            foreach (Neutron ball in neutronList)//names the neutron we are looking at "ball", so it is refered to later
            {
                e.Graphics.FillEllipse(ball.PickBrush(), ball.GetXPos(), ball.GetYPos(), Neutron.NeutronWidth, Neutron.NeutronWidth);
                e.Graphics.DrawEllipse(Pens.Black, ball.GetXPos(), ball.GetYPos(), Neutron.NeutronWidth, Neutron.NeutronWidth);
            }

            foreach (Atom a in atoms)//draw all atoms as circles with the same diamiter as the width of the atom
            {
                e.Graphics.FillEllipse(a.PickBrush(), a.GetXPos(), a.GetYPos(), Atom.AtomWidth, Atom.AtomWidth);
                e.Graphics.DrawEllipse(Pens.Black, a.GetXPos(), a.GetYPos(), Atom.AtomWidth, Atom.AtomWidth);
            }
        }
    }
}
