using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace TriviaMaze
{
    class Game
    {
        private Canvas screen;
        //private Map map;

        public event EventHandler GameEnded;

        public Game(Canvas screen)
        {
            this.screen = screen;
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
    }
}
