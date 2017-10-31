using System.Collections.Generic;
using System.Windows.Media;

namespace Group
{ 
    class Player
    {
        public Player(string name, string symbol, Brush color)
        {
            _name = name;
            _symbol = symbol;
            _color = color;
        }
        protected string _name;
        public string Name { get { return _name; } }
        protected bool[,] _marks = new bool[5, 5];
        //to ensure deep copy when called within class definition
        protected bool[,] Marks
        {
            get
            {
                bool[,] output = new bool[5, 5];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (_marks[i, j])
                        {
                            output[i, j] = true;
                        }
                    }
                }
                return output;
            }
        }
        /// <summary>
        /// Function to return a deep copy of the marks 2d array to call.
        /// </summary>
        /// <returns></returns>
        public bool[,] GetMarks()
        {
            bool[,] output = new bool[5, 5];
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(_marks[i,j])
                    {
                        output[i, j] = true;
                    }
                }
            }
            return output;
        }
        public void AddMark(int i, int j)
        {
            _marks[i, j] = true;
        }
        protected string _symbol;
        public string GetSymbol()
        {
            return _symbol;
        }
        protected Brush _color;
        public Brush GetColor()
        {
            return _color;
        }
    }
}
