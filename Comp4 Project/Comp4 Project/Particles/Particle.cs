using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp4_Project.Particles
{
    abstract class Particle : getCoords 
    {
        private double xPos;
        private double yPos;

        public Particle() : this(0, 0)
        {
        }

        
        protected Particle(double xPos, double yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }

        //Any particle should be able to move, if it should not move the method is implemented without a method body
        public abstract void Move(int maxWidth, int maxHeight);

        //Get the size of an particle
        public abstract int GetSize();

        //This mehtod will be called if an particle interacts with another particle
        protected abstract void OnInteraction();

        //This method will be called to check if the particle is disabled for interaction
        protected abstract Boolean IsDisabled();

        //Getters and setters for x and y coordinates
        public void SetXPos(double xPos)
        {
            this.xPos = xPos;
        }

        public int GetXPos()
        {
            return (int)(xPos + 0.5);
        }

        public void SetYPos(double yPos)
        {
            this.yPos = yPos;
        }

        public int GetYPos()
        {
            return (int)(yPos + 0.5);
        }

         protected double GetRealXPos()
         {
             return this.xPos;
         }
        
        protected double GetRealYPos()
        {
            return this.yPos;
        }  

        /*
         *  this function returns a boolean "true" if the neutron in question is within a cetrain distance of any atoms and 
         *  if the atom that it was found to be close to has not split already 
         */
        public Boolean InteractsWith(Particle otherParticle)
        {
            Boolean result = false;

            if (!IsDisabled()) 
            {
                double minX = this.GetXPos();//retrieve the coordinates of the particle in quastion
                double maxX = minX + this.GetSize();

                double minY = this.GetYPos();
                double maxY = minY + this.GetSize();

                Boolean inXRange = (minX <= otherParticle.GetXPos()) && (otherParticle.GetXPos() <= maxX);//detects if the neutron is within a square area surroiunding the location of the atom
                Boolean inYRange = (minY <= otherParticle.GetYPos()) && (otherParticle.GetYPos() <= maxY);

                result = inXRange && inYRange;//the result is the logical and of whether the particle is within the y range and the x range

                if (result)
                {
                    this.OnInteraction();
                }
            }

            return result;
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
