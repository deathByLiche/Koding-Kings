using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TriviaMaze
{
    class MazeFactory
    {
        private const int ROOMWIDTH = 80;
        private const int ROOMHEIGHT = 80;
        private const int DOORWIDTH = 10;
        private const int DOORHEIGHT = 80;

        public MazeFactory()
        {
        }

        public Room[][] getMaze(int size)
        {
            Room[][] mazeRooms = new Room[size][];
            for (int row = 0; row < size; row++)
            {
                Room[] rooms = new Room[size];
                for (int col = 0; col < size; col++)
                {
                    Room r = new Room();
                    r.Height = ROOMHEIGHT;
                    r.Width = ROOMWIDTH;
                    r.setUpperLeft(new Point(col * ROOMWIDTH + (col + 1) * DOORWIDTH, row * ROOMHEIGHT + (row + 1) * DOORWIDTH));
                    this.setupDoors(mazeRooms, row, col, r);
                    rooms[col] = r;
                    mazeRooms[row] = rooms;
                } 
            }

            return mazeRooms;
        }

        private void setupDoors(Room[][] mazeRooms, int row, int col, Room r)
        {
            Door[] doors = new Door[4];

            //N Door
            if (row != 0)
                doors[0] = mazeRooms[row-1][col].Doors[2];
            else
                this.makeDoor(doors, DOORWIDTH, DOORHEIGHT, (col * ROOMWIDTH + (col + 1) * DOORWIDTH), (row * ROOMHEIGHT + row * DOORWIDTH), 0);

            //E Door
            this.makeDoor(doors, DOORHEIGHT + 10, DOORWIDTH, ((col + 1) * ROOMWIDTH + (col + 1) * DOORWIDTH), (row * (DOORHEIGHT + 10)), 1);

            //S Door
            this.makeDoor(doors, DOORWIDTH, DOORHEIGHT, (col * ROOMWIDTH + (col + 1) * DOORWIDTH), ((row + 1) * ROOMHEIGHT + (row + 1) * DOORWIDTH), 2);

            //W Door
            if (col != 0)
                doors[3] = mazeRooms[row][col-1].Doors[1];
            else
                this.makeDoor(doors, DOORHEIGHT + 10, DOORWIDTH, (col * ROOMWIDTH + col * DOORWIDTH), (row * (DOORHEIGHT + 10)), 3);

            r.Doors = doors;
        }

        private void makeDoor(Door[] doors, int height, int width, int ptX, int ptY, int direction)
        {
            Door d = new Door();
            d.IsDoor = true;
            d.Height = height;
            d.Width = width;
            d.setUpperLeft(new Point(ptX, ptY));
            doors[direction] = d;
        }
    }
}
