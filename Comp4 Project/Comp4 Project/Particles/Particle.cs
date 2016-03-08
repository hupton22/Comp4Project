using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp4_Project.Particles
{
    abstract class Particle : getCoords //Do not use a lowercase name c# name convention (afaik)
    {
        private int xPos;
        private int yPos;

        public Particle() : this(0, 0)
        {
        }

        //Just subclasses should see this
        protected Particle(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }

        /**
         *  Getter and setter for position
         */
        public void SetXPos(int xPos)
        {
            this.xPos = xPos;
        }

        public int GetXPos()
        {
            return xPos;//could be this.xPos
        }

        public void SetYPos(int yPos)
        {
            this.yPos = yPos;
        }

        public int GetYPos()
        {
            return yPos;
        }

        public abstract int GetSize(); //Returns the size of the particle, has to be implemented by subclasses, don't care how they implement it yet

        protected abstract void OnInteraction(); //This method will be called on interaction.

        protected abstract Boolean IsDisabled();

        public virtual Boolean InteractsWith(Particle otherParticle)
        {
            double minX = this.GetXPos();
            double maxX = minX + this.GetSize();

            double minY = this.GetYPos();
            double maxY = minY + this.GetSize();

            Boolean inXRange = (minX <= otherParticle.GetXPos()) && (otherParticle.GetXPos() <= maxX);
            Boolean inYRange = (minY <= otherParticle.GetYPos()) && (otherParticle.GetYPos() <= maxY);

            Boolean result = inXRange && inYRange;
            Boolean disabled = this.IsDisabled();

            if (result)
            {
                this.OnInteraction();
            }

            return result && !disabled;
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
