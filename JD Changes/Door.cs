using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TriviaMaze
{
    class Door : DrawingObject
    {
        // For every door, there is three possible states. 0 = locked, 1 = unlocked, 2 = locked permanently
        private int state = 0;
        private Boolean drawn;

        public int State
        {
            get
            {
                return this.state;
            }

            set
            {
                this.state = value;
            }
        }

        public Boolean Drawn
        {
            get
            {
                return this.drawn;
            }

            set
            {
                this.drawn = value;
            }
        }
    }
}
