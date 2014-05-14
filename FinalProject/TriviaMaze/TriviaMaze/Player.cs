using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    class Player
    {
        int horizontal;
        int vertical; 
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
         * up or down (negative y moves down, positive moves up)
         */
        public void move(int x, int y)
        { 
            horizontal += x; 
            vertical += y; 
        }
    }
  
}
