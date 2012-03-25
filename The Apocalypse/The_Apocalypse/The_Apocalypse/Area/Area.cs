using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    interface Area
    {
        void UpdatePosition();
        Position GetPosition();
        void Draw(SpriteBatch spriteBatch, bool pause);
        void ForceStop();
        void GetLimit();
    }
}
