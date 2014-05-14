using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TriviaMaze
{
    class Room
    {
        Door[] doorsInRoom = new Door[4];
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
