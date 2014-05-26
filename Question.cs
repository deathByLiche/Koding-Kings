
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    class Question
    {
        private string category = ""; //I'd much rather we did this as an int
        private int type; //mulitple choice 1, t/f 2
        private string question = "";
        private string answer = "";
        private string dummy1 = "", dummy2 = "", dummy3 = ""; //Wrong answers. For t/f, only need dummy1
    }
}
