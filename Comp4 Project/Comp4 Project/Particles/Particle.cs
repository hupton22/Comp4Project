using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp4_Project.Particles
{
    class Particle : FindDistance
    {
        private int xPos;
        private int yPos;

        /**
         *  Getter and setter for position
         */
        public void SetXPos(int xPos)
        {
            this.xPos = xPos;
        }

        public int GetXPos()
        {
            return xPos;
        }

        public void SetYPos(int yPos)
        {
            this.yPos = yPos;
        }

        public int GetYPos()
        {
            return yPos;
        }

        public double DistanceTo(Particle otherParticle)
        {   
            double xDist = this.GetXPos() - otherParticle.GetXPos();
            double yDist = this.GetYPos() - otherParticle.GetYPos();
            double distance = Math.Sqrt((xDist*xDist)+(yDist*yDist));

            return distance;
        }
    }
}
