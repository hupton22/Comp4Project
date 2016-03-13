using System;
using System.Collections.Generic;
using System.Drawing;//allows drawing
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;//allows sleep

namespace Comp4_Project.Particles
{
    class Atom : Particle
    {
        public Atom() : this(0, 0)
        {
        }

        public Atom(int xPos, int yPos) : base(xPos, yPos)
        {
        }

        public static int AtomWidth = 15;
        public bool hasSplit = false; 

        public Brush PickBrush()//returns a colour for the form to fill the ellipse with
        {
            if (this.hasSplit == false)
            {
                return Brushes.Blue;//returns blue if atom has not split
            }

            else if (this.hasSplit == true)
            {
                return Brushes.Green;//returns green if atom has split
            }

            else return Brushes.White;
        }//this changes the colour of the atom if it has split

        public override int GetSize()
        {
            return Atom.AtomWidth;
        }

        protected override void OnInteraction()
        {
            this.hasSplit = true;
        }

        protected override bool IsDisabled()
        {
            return hasSplit;
        }
    }
}
