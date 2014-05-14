using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TriviaMaze
{
    class Door
    {
        // For every door, there is three possible states. 0 = locked, 1 = unlocked, 2 = locked permanently
        public int state = 0;
        Point upperLeft, lowerRight;

        void setUpperLeft(Point left)
        {
            upperLeft.X = left.X;
            upperLeft.Y = left.Y;
        }

        void setLowerRight(Point right)
        {
            lowerRight.X = right.X;
            lowerRight.Y = right.Y;
        }
    }
}
