using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Apocalypse
{
    class Direct : Area
    {
        int M = 0, B = 0, Orientation = 0;
        Position positionNow = new Position(0, 0);
        Position positionStart = new Position(0, 0);
        Position positionEnd = new Position(0, 0);

        public void Direct(int x1, int x2, int y1, int y2)
        {

            if (x2 - x1 < 0)
            {
                Orientation = -1;
            }
            else
            {
                Orientation = 1;
            }

            positionStart.X = x1;
            positionStart.Y = y1;
            positionEnd.X = x2;
            positionEnd.Y = y2;

            M = ((y2 - y1) / (x2 - x1));
            B = y1 - (M * x1);

        }

        public void UpdatePosition()
        {
            positionNow.X = (int)(positionNow.X + (Orientation) * (1 / (Math.Pow(2, Math.Abs(M)))));
            positionNow.Y = (M * positionNow.X) + B;
        }
    }
}