using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace TriviaMaze
{
    class Game
    {
        private Canvas screen;
        private Canvas mapCanvas;
        private Room[][] maze;
        private Player player;

        public event EventHandler GameEnded;

        public Game(Canvas screen)
        {
            this.screen = screen;
            this.mapCanvas = (Canvas)this.screen.Children[1];
            MazeFactory mazeFactory = new MazeFactory();
            this.maze = mazeFactory.getMaze(5);
            this.drawMaze();
        }

        public void promptForUsername()
        {
            //Will do custom message box stuff here later
            Console.Out.WriteLine("Prompt for username");
        }

        //will be private
        public void checkIfGameOver()
        {
            if (1 == 1)//if they won/lose
            {
                if (this.GameEnded != null)
                    this.GameEnded(this, new EventArgs());
            }
        }

        private void drawMaze()
        {
            foreach (Room[] room in this.maze)
            {
                foreach (Room r in room)
                {
                    SolidColorBrush roomColor = new SolidColorBrush();
                    SolidColorBrush doorColor = new SolidColorBrush();
                    roomColor.Color = Color.FromArgb(255, 255, 0, 0);
                    doorColor.Color = Color.FromArgb(255, 0, 255, 0);

                    foreach (Door d in r.Doors)
                        this.drawObject(d, doorColor, false);

                    this.drawObject(r, roomColor, false);
                }
            }

            //draw player
            this.setupPlayer();

        }

        private void drawObject(DrawingObject obj, Brush brush, Boolean player)
        {
            obj.IsPlayer = player;
            obj.Shape = new Rectangle();
            obj.Shape.Height = obj.Height;
            obj.Shape.Width = obj.Width;
            obj.Shape.Fill = brush;
            this.mapCanvas.Children.Add(obj.Shape);
            Canvas.SetTop(obj.Shape, obj.getUpperLeft().Y);
            Canvas.SetLeft(obj.Shape, obj.getUpperLeft().X);

        }

        private void setupPlayer()
        {
            this.player = new Player();

            player.Height = 40;
            player.Width = 40;
            player.setUpperLeft(new Point(30, 30));

            SolidColorBrush playerColor = new SolidColorBrush();
            playerColor.Color = Color.FromArgb(255, 0, 0, 102);

            this.drawObject(player, playerColor, true);
        }

        public void mouseClick(Point mouseCoords)
        {
            foreach (Room[] row in this.maze)
            {
                foreach (Room room in row)
                {
                    if (mouseCoords.X >= room.getUpperLeft().X && mouseCoords.X <= room.getUpperLeft().X + room.Width &&
                    mouseCoords.Y >= room.getUpperLeft().Y && mouseCoords.Y <= room.getUpperLeft().Y + room.Height)
                    {
                        this.checkIfValidRoom(room);
                    }
                }  
            }
        }

        private void checkIfValidRoom(Room room)
        {
            //check N
            if (player.getVertical() != 0)
            {
                if (this.maze[player.getVertical() - 1][player.getHorizontal()].Equals(room))
                {
                    this.player.move(0, -1);
                }
            }
            //check E
            if (player.getHorizontal() != this.maze[0].Length - 1)
            {
                if (this.maze[player.getVertical()][player.getHorizontal() + 1].Equals(room))
                {
                    this.player.move(1, 0);
                }
            }
            //check S
            if (player.getVertical() != this.maze[0].Length - 1)
            {
                if (this.maze[player.getVertical() + 1][player.getHorizontal()].Equals(room))
                {
                    this.player.move(0, 1);
                }
            }
            //check W
            if (player.getHorizontal() != 0)
            {
                if (this.maze[player.getVertical()][player.getHorizontal() - 1].Equals(room))
                {
                    this.player.move(-1, 0);
                }
            }
        }
    }
}
