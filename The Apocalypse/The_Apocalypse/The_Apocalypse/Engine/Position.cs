using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Apocalypse
{
    class Position
    {
        private int x;
        private int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X()
        {
            return x;
        }

        public int Y()
        {
            return y;
        }
    }
}
