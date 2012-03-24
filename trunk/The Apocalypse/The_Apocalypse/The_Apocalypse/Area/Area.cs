using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Apocalypse
{
    interface Area
    {
        Position positionStart;
        Position positionEnd;
        int orientation;
        Position positionNow;

        public Position getPosition()
        {
            //À faire
            return positionNow;
        }

        public void UpdatePosition()
        {
            //À faire
        }
    }
}
