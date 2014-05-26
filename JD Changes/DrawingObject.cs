using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace TriviaMaze
{
    abstract class DrawingObject
    {
        protected Point upperLeft;
        protected int height;
        protected int width;
        protected Rectangle rectangle;
        protected Boolean door = false;
        protected Boolean player = false;

        protected DrawingObject() { }

        public Point getUpperLeft()
        {
            return this.upperLeft;
        }

        public void setUpperLeft(Point left)
        {
            upperLeft.X = left.X;
            upperLeft.Y = left.Y;
        }

        public int Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
            }
        }

        public Boolean IsPlayer
        {
            get
            {
                return this.player;
            }

            set
            {
                this.player = value;
            }
        }

        public Rectangle Shape
        {
            get
            {
                return this.rectangle;
            }

            set
            {
                this.rectangle = value;
            }
        }

        public Boolean IsDoor
        {
            get
            {
                return this.door;
            }

            set
            {
                this.door = value;
            }
        }
    }
}
