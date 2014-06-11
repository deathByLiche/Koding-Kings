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
        private StackPanel questionDisplay;
        private Room[][] maze;
        private Player player;
        private dbManage dbManager;
        private Question[] questions;
        private Boolean canWin = true;

        public event EventHandler<Boolean> GameEnded;

        public Game(Canvas screen, dbManage db)
        {
            this.screen = screen;
            this.mapCanvas = (Canvas)this.screen.Children[0];
            this.questionDisplay = (StackPanel)this.screen.Children[1];
            this.questionDisplay.Visibility = Visibility.Hidden;
            this.dbManager = db;
            this.questions = this.dbManager.getQuestions();
            MazeFactory mazeFactory = new MazeFactory();
            this.maze = mazeFactory.getMaze(5);
            this.drawMaze();

            
        }

        private void checkIfGameOver()
        {
            if (this.player.getHorizontal() == 4 && this.player.getVertical() == 4)
            {
                if (this.GameEnded != null)
                    this.GameEnded(this, true);
            }

            this.mazeTraversal();

            if (!this.canWin)
            {
                if (this.GameEnded != null)
                    this.GameEnded(this, false);
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
                    roomColor.Color = Color.FromArgb(255, 112, 128, 144);
                    doorColor.Color = Color.FromArgb(255, 210, 105, 30);

                    foreach (Door d in r.Doors)
                        this.drawObject(d, doorColor, false);

                    this.drawObject(r, roomColor, false);
                }
            }

            //draw player
            this.setupPlayer();
            //draw exit
            this.drawExit();

        }

        private void drawExit()
        {
            Polygon exit = new Polygon();
            Polygon exit2 = new Polygon();

            SolidColorBrush exitColor = new SolidColorBrush();
            exitColor.Color = Color.FromArgb(255, 0, 0, 102);
            exit.Stroke = exitColor;
            exit.Fill = exitColor;
            exit2.Stroke = exitColor;
            exit2.Fill = exitColor;

            Point p1 = new Point(0, 0);
            Point p2 = new Point(40, 0);
            Point p3 = new Point(20, 40);
            Point p4 = new Point(0, 25);
            Point p5 = new Point(20, -15);
            Point p6 = new Point(40, 25);

            PointCollection points = new PointCollection();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);

            PointCollection points2 = new PointCollection();
            points2.Add(p4);
            points2.Add(p5);
            points2.Add(p6);

            exit.Points = points;
            exit2.Points = points2;

            this.mapCanvas.Children.Add(exit);
            this.mapCanvas.Children.Add(exit2);
            Canvas.SetTop(exit, 400);
            Canvas.SetLeft(exit, 390);
            Canvas.SetTop(exit2, 400);
            Canvas.SetLeft(exit2, 390);

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
            if (this.questionDisplay.Visibility == Visibility.Visible)
                return;

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
                    this.player.CurrentDoor = room.Doors[2];
                    this.player.CurrentMovement = new Point(0, -1);
                    this.evaluateRoom(room.Doors[2]);
                }
            }
            //check E
            if (player.getHorizontal() != this.maze[0].Length - 1)
            {
                if (this.maze[player.getVertical()][player.getHorizontal() + 1].Equals(room))
                {
                    this.player.CurrentDoor = room.Doors[3];
                    this.player.CurrentMovement = new Point(1, 0);
                    this.evaluateRoom(room.Doors[3]);
                }
            }
            //check S
            if (player.getVertical() != this.maze[0].Length - 1)
            {
                if (this.maze[player.getVertical() + 1][player.getHorizontal()].Equals(room))
                {
                    this.player.CurrentDoor = room.Doors[0];
                    this.player.CurrentMovement = new Point(0, 1);
                    this.evaluateRoom(room.Doors[0]);
                }
            }
            //check W
            if (player.getHorizontal() != 0)
            {
                if (this.maze[player.getVertical()][player.getHorizontal() - 1].Equals(room))
                {
                    this.player.CurrentDoor = room.Doors[1];
                    this.player.CurrentMovement = new Point(-1, 0);
                    this.evaluateRoom(room.Doors[1]);
                }
            }
        }

        private void displayQuestion()
        {
            Question q = new Question();
            q.Used = true;

            Random rand = new Random();
            while (q.Used)
            {
                q = this.questions[rand.Next(0, this.questions.Length)];
            }
            this.player.CurrentQuestion = q;

            Label category = (Label)this.questionDisplay.Children[0];
            category.Content = q.Category;

            TextBlock question = (TextBlock)this.questionDisplay.Children[1];
            question.TextWrapping = TextWrapping.Wrap;
            question.Text = q.Questions;

            //for testing
            SolidColorBrush doorColor = new SolidColorBrush();
            doorColor.Color = Color.FromArgb(255, 0, 255, 0);
            SolidColorBrush black = new SolidColorBrush();
            black.Color = Color.FromArgb(255, 0, 0, 0);

            Boolean correctAnswerUsed = false;
            Boolean dummyOneUsed = false;
            Boolean dummyTwoUsed = false;
            Boolean dummyThreeUsed = false;
            for (int i = 0; i < 4; i++)
            {
                RadioButton rb = (RadioButton)this.questionDisplay.Children[i + 2];
                if (!correctAnswerUsed && (rand.NextDouble() < .4 || i == 3))
                {
                    
                    rb.Content = q.Answer;
                    rb.Foreground = doorColor;
                    correctAnswerUsed = true;
                }
                else if(!dummyOneUsed)
                {
                    rb.Content = q.Dummy1;
                    dummyOneUsed = true;
                    rb.Foreground = black;
                }
                else if (!dummyTwoUsed)
                {
                    rb.Content = q.Dummy2;
                    dummyTwoUsed = true;
                    rb.Foreground = black;

                }
                else if (!dummyThreeUsed)
                {
                    rb.Content = q.Dummy3;
                    dummyThreeUsed = true;
                    rb.Foreground = black;
                }
            }

            this.questionDisplay.Visibility = Visibility.Visible;
        }

        private void evaluateRoom(Door door)
        {
            if (door.State == 2)//perma locked
                return;

            if(door.State == 1)//unlocked
                this.player.move((int)this.player.CurrentMovement.X, (int)this.player.CurrentMovement.Y);
            else
                this.displayQuestion();
        }

        public void evaluateQuestion()
        {
            for (int i = 0; i < 4; i++)
            {
                RadioButton rb = (RadioButton)this.questionDisplay.Children[i + 2];
                SolidColorBrush doorColorLocked = new SolidColorBrush();
                SolidColorBrush doorColorUnlocked = new SolidColorBrush();
                doorColorUnlocked.Color = Color.FromArgb(255, 124, 252, 0);
                doorColorLocked.Color = Color.FromArgb(255, 178, 34, 34);

                if (rb.IsChecked.Value && rb.Content.Equals(this.player.CurrentQuestion.Answer))
                {
                    this.player.CurrentDoor.State = 1;
                    this.changeDoorColor(this.player.CurrentDoor, doorColorUnlocked);
                    this.player.move((int)this.player.CurrentMovement.X, (int)this.player.CurrentMovement.Y);
                }
                else if(rb.IsChecked.Value)
                {
                    this.player.CurrentDoor.State = 2;
                    this.changeDoorColor(this.player.CurrentDoor, doorColorLocked);
                    //call method for maze traversal to see fi player loses
                }

            }

            this.checkIfGameOver();
            this.questionDisplay.Visibility = Visibility.Hidden;
        }

        private void changeDoorColor(Door door, SolidColorBrush doorColor)
        {
            foreach (UIElement rect in this.mapCanvas.Children)
            {
                if (rect is Polygon)
                    continue;

                if (Canvas.GetLeft(rect) == door.getUpperLeft().X && Canvas.GetTop(rect) == door.getUpperLeft().Y)
                {
                    (rect as Rectangle).Fill = doorColor;
                }
            }
        }

        public void mazeTraversal()
        {
            int [][] winnable = new int [(maze.Length * 2) - 1][];
            int x = 0, y = 0;

            for (; x < winnable.Length; x++)
            {
                int[] ara = new int[(maze.Length * 2) - 1];
                for (; y < winnable.Length; y++)
                   ara[y] = -1;
                winnable[x] = ara;
            }
            x = 0;
            y = 0;

            foreach (Room[] room in this.maze)
            {
                foreach (Room r in room)
                {
                    winnable[(x * 2)][(y * 2)] = 1;

                    //set East Door
                    if (y < (this.maze.Length - 1))
                    {
                        if (r.Doors[1].State != 2)
                            winnable[x * 2][y * 2 + 1] = 1;
                        else
                            winnable[x * 2][y * 2 + 1] = 0;
                    }


                    //set South Door
                    if (x < (this.maze.Length - 1))
                    {
                        if (r.Doors[2].State != 2)
                            winnable[x * 2 + 1][y * 2] = 1;
                        else
                            winnable[x * 2 + 1][y * 2] = 0;
                    }

                    y++;
                }
                y = 0;
                x++;
            }

            winnable[maze.Length * 2 - 2][maze.Length * 2 - 2] = 5;

            this.canWin = false;
            mazeTraversal(winnable, player.getHorizontal() * 2, player.getVertical() * 2);

            //printing for testing
            Console.WriteLine("Printing maze traversale Array:");
            for (int i = 0; i < winnable.Length; i++)
            {
                for (int t = 0; t < winnable[i].Length; t++)
                {
                    Console.Write(" " + winnable[i][t]);
                }
                Console.WriteLine();
            }
        }

        private void mazeTraversal(int[][] winnable, int positionX, int positionY)
        {
            if (winnable[positionX][positionY] == 5)
            {
                Console.WriteLine("Winnable!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                this.canWin = true;
                return;
            }

            //Move East
            if (positionX + 2 < (maze.Length * 2 - 1) && winnable[positionX + 1][positionY] != 0 && winnable[positionX + 2][positionY] != 3)
            {
                winnable[positionX][positionY] = 3;
                mazeTraversal(winnable, positionX + 2, positionY);
            }
            //Move South
            if (positionY + 2 < (maze.Length * 2 - 1) && winnable[positionX][positionY + 1] != 0 && winnable[positionX][positionY + 2] != 3)
            {
                winnable[positionX][positionY] = 3;
                mazeTraversal(winnable, positionX, positionY + 2);
            }
            //Move West
            if (positionX - 2 >= 0 && winnable[positionX - 1][positionY] != 0 && winnable[positionX - 2][positionY] != 3)
            {
                winnable[positionX][positionY] = 3;
                mazeTraversal(winnable, positionX - 2, positionY);
            }
            //Move North
            if (positionY - 2 >= 0 && winnable[positionX][positionY - 1] != 0 && winnable[positionX][positionY - 2] != 3)
            {
                winnable[positionX][positionY] = 3;
                mazeTraversal(winnable, positionX, positionY - 2);
            }
        }
    }
}
