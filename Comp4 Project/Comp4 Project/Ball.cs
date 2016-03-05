using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Comp4_Project
{
    public class Ball
    {
        //The width of every single ball
        public static int BallWidth = 30;

        private int xPos;
        private int yPos;

        private int velocityX;
        private int velocityY;

        private Brush color;

        public Ball()
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

        //public Ball(int xPos, int yPos)
        //{
        //    this.yPos = yPos;
        //    this.xPos = xPos;
        //}

        public Brush PickBrush()
        {
            return color;
        }

        public void move(int maxWidth, int maxHeight)
        {
            int newXPos = xPos + velocityX;
            
            if (newXPos < 0)
            {
                newXPos = 0;
                velocityX = (velocityX * -1);
            }
            else if ((newXPos + Ball.BallWidth) > maxWidth)
            {
                velocityX = (velocityX * -1);
            }

            int newYPos = yPos + velocityY;

            if (newYPos < 0)
            {
                newYPos = 0;
                velocityY = (velocityY * -1);
            }
            else if ((newYPos + Ball.BallWidth) > maxHeight)
            {
                velocityY = (velocityY * -1);
            }

            SetXPos(newXPos);
            SetYPos(newYPos);
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
