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
        //The width of every single ball
        public static int BallWidth = 30;

        private int velocityX;
        private int velocityY;

        private Brush color;

        public Neutron()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();
            rnd.Next(1,100);

            System.Threading.Thread.Sleep(50);

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(0, properties.Length);
            color = (Brush)properties[random].GetValue(null, null);
        }

        public Brush PickBrush()
        {
            return color;
        }

        public void move(int maxWidth, int maxHeight)
        {
            int newXPos = GetXPos() + velocityX;
            
            if (newXPos < 0)
            {
                newXPos = 0;
                velocityX = (velocityX * -1);
            }
            else if ((newXPos + Neutron.BallWidth) > maxWidth)
            {
                velocityX = (velocityX * -1);
            }

            int newYPos = GetYPos() + velocityY;

            if (newYPos < 0)
            {
                newYPos = 0;
                velocityY = (velocityY * -1);
            }
            else if ((newYPos + Neutron.BallWidth) > maxHeight)
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
