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

        //Getters and setters for x and y coordinates
        public void SetXPos(double xPos)
        {
            this.xPos = xPos;
        }

        public int GetXPos()
        {
            return (int) (xPos + 0.5);//could be this.xPos
        }

        public void SetYPos(double yPos)
        {
            this.yPos = yPos;
        }

        public int GetYPos()
        {
            return (int) (yPos + 0.5); //do you know why I am doing this? I don't :/ well the neutorns can actually move, and therefore they can get a fraction number, so I am using this
            //number internally all the time, but when I want to draw something on the screen I need integer numbers, and then I am converting the number with the right rule so that 
            // 0.5 would be 1 and 0.4 would be 0 : that solves the problem with the rotating the vector thingy ;) so that should work for now let's check it su be sure
        }

        protected double GetRealXPos()
        {
            return this.xPos;
        }

        protected double GetRealYPos() 
        {
            return this.yPos;
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
