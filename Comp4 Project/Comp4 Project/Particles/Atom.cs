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
        public static int AtomWidth = 50;
        public bool hasSplit = false; 

        public Brush PickBrush()
        {
            return Brushes.Blue;//setting the colour of all atoms to blue
        }

        public void split()
        {
            //change colour
            //create more neutrons
        }
    }
}
