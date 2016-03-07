using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Comp4_Project.Particles
{
    class Neutron : Particle
    {
        //The width of every ball
        public static int NeutronWidth = 5;

        private int velocityX;
        private int velocityY;

        public Brush PickBrush()
        {
            return Brushes.Red;//setting the colour of all neutrons to red
        }

        public void move(int maxWidth, int maxHeight)//moving each neutron
        {
            int newXPos = GetXPos() + velocityX;
            
            if (newXPos < 0)
            {
                newXPos = 0;
                velocityX = (velocityX * -1);
            }
            else if ((newXPos + Neutron.NeutronWidth) > maxWidth)
            {
                velocityX = (velocityX * -1);
            }

            int newYPos = GetYPos() + velocityY;

            if (newYPos < 0)
            {
                newYPos = 0;
                velocityY = (velocityY * -1);
            }
            else if ((newYPos + Neutron.NeutronWidth) > maxHeight)
            {
                velocityY = (velocityY * -1);
            }

            SetXPos(newXPos);
            SetYPos(newYPos);
        }

        /**
         * getters and setters for velocity
         */
        public void SetVelocityX(int vX)
        {
            velocityX = vX;
        }

        public int GetVelocityX()
        {
            return velocityX;
        }

        public void SetVelocityY(int vY)
        {
            velocityY = vY;
        }

        public int GetVelocityY()
        {
            return velocityY;
        }

    }
}
