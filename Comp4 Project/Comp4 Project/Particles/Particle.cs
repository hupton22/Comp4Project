using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp4_Project.Particles
{
    abstract class Particle : getCoords 
    {
        private int xPos;
        private int yPos;

        public Particle() : this(0, 0)
        {
        }

        
        protected Particle(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }

        //Getters and setters for x and y coordinates
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

        public abstract int GetSize(); //Returns the size of the particle

        protected abstract void OnInteraction(); //This method will be called on interaction.

        protected abstract Boolean IsDisabled();

        public virtual Boolean InteractsWith(Particle otherParticle)//this function returns a boolean "true" if the neutron in question is within a cetrain distance of any atoms and ...
        {//... if the atom that it was found to be close to has not split already
            double minX = this.GetXPos();//retrieve the coordinates of the particle in quastion
            double maxX = minX + this.GetSize();

            double minY = this.GetYPos();
            double maxY = minY + this.GetSize();

            Boolean inXRange = (minX <= otherParticle.GetXPos()) && (otherParticle.GetXPos() <= maxX);//detects if the neutron is within a square area surroiunding the location of the atom
            Boolean inYRange = (minY <= otherParticle.GetYPos()) && (otherParticle.GetYPos() <= maxY);

            Boolean result = inXRange && inYRange;//the result is the logical and of whether the particle is within the y range and the x range
            Boolean disabled = this.IsDisabled();

            if (result)
            {
                this.OnInteraction();
            }

            return result && !disabled;
        }

        public double DistanceTo(Particle otherParticle)//this calculates and returns the distance between two particle objects usin pythagoras's theorum a^2 + b^2 = c^2
        {   
            double xDist = this.GetXPos() - otherParticle.GetXPos();
            double yDist = this.GetYPos() - otherParticle.GetYPos();
            double distance = Math.Sqrt((xDist*xDist)+(yDist*yDist));

            return distance;
        }
    }
}
