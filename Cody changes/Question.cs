
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    public class Question
    {
        private string category = ""; //I'd much rather we did this as an int
        private int type; //mulitple choice 1, t/f 2
        private string question = "";
        private string answer = "";
        private string dummy1 = "", dummy2 = "", dummy3 = ""; //Wrong answers. For t/f, only need dummy1

        public Question()
        {

        }
        public  string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }
        public string Questions
        {
            get
            {
                return this.question;
            }
            set
            {
                this.question = value;
            }
        }
        public string Answer
        {
            get
            {
                return this.answer;
            }
            set
            {
                this.answer = value;
            }
        }
        public string Dummy1
        {
            get
            {
                return this.dummy1;
            }
            set
            {
                this.dummy1 = value;
            }
        }
        public string Dummy2
        {
            get
            {
                return this.dummy2;
            }
            set
            {
                this.dummy2 = value;
            }
        }
        public string Dummy3
        {
            get
            {
                return this.dummy3;
            }
            set
            {
                this.dummy3 = value;
            }
        }
    }
}
