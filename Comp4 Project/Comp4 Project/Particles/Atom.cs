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
        /* static field for the AtomWidth*/
        public static int Size = 20;

        private bool hasSplit = false; 

        public Atom() : this(0, 0)
        {
        }

        public Atom(int xPos, int yPos) : base(xPos, yPos)
        {
        }

        public Brush PickBrush()//returns a colour for the form to fill the ellipse with
        {
            return hasSplit ? Brushes.Green : Brushes.Blue;
        }

        public override void Move(int maxWidth, int maxHeight)
        {
            //this doesn't move at all
        }

        public override int GetSize()
        {
            return Atom.Size;
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
