using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TriviaMaze
{
    class Player : DrawingObject
    {
        private int horizontal;
        private int vertical; 

        public Player()
        {
            this.horizontal = 0;
            this.vertical = 0;
        }
        public int getHorizontal()
        {
            return this.horizontal; 
        }
        public int getVertical()
        {
            return this.vertical;
        }
        /*
         * Moves Player either left or right (negative x will move left, positive moves right)
         * up or down (negative y moves down, positive moves up) must be +-1 or 0
         */
        public void move(int x, int y)
        { 
            horizontal += x; 
            vertical += y;

            this.setUpperLeft(new System.Windows.Point(this.getUpperLeft().X + 90 * x, this.getUpperLeft().Y + 90 * y));

            Canvas.SetTop(this.Shape, this.getUpperLeft().Y);
            Canvas.SetLeft(this.Shape, this.getUpperLeft().X);
        }
    }
  
}
