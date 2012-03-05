using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Apocalypse
{
    class Position
    {
        private double x;
        private double y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public double X
        {
            get{return x;}
            set{x = value;}
        }

        public double Y
        {
            get{return y;}
            set{y = value;}
        }
    }
}
