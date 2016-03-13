﻿using System;
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
        //The width of every neutron
        public static int Size = 6; 

        private double velocityX;
        private double velocityY;

        public Brush PickBrush()
        {
            return Brushes.Red;//setting the colour of all neutrons to red
        }

        public override void Move(int maxWidth, int maxHeight)//moving each neutron
        {
            double newXPos = GetRealXPos() + velocityX;
            
            if (newXPos < 0)
            {
                newXPos = 0;
                velocityX = (velocityX * -1);
            }
            else if ((newXPos + Neutron.Size) > maxWidth)
            {
                velocityX = (velocityX * -1);
            }

            double newYPos = GetRealYPos() + velocityY;

            if (newYPos < 0)
            {
                newYPos = 0;
                velocityY = (velocityY * -1);
            }
            else if ((newYPos + Neutron.Size) > maxHeight)
            {
                velocityY = (velocityY * -1);
            }

            SetXPos(newXPos);
            SetYPos(newYPos);
        }

        protected override void OnInteraction()
        {
            
        }

        public override int GetSize()
        {
            return Neutron.Size;
        }

        protected override bool IsDisabled()
        {
            return false;
        }

        /**
         * getters and setters for velocity
         */
        public void SetVelocityX(double vX)
        {
            velocityX = vX;
        }

        public double GetVelocityX()
        {
            return velocityX;
        }

        public void SetVelocityY(double vY)
        {
            velocityY = vY;
        }

        public double GetVelocityY()
        {
            return velocityY;
        }

    }
}
