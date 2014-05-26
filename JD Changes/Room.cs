using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TriviaMaze
{
    class Room : DrawingObject
    {
        private Door[] doorsInRoom = new Door[4];//N, E, S, W

        public Door[] Doors
        {
            get
            {
                return this.doorsInRoom;
            }

            set
            {
                this.doorsInRoom = value;
            }
        }

    }
}
