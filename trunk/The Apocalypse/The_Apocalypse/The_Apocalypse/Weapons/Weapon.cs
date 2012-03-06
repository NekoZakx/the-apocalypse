using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    interface Weapon
    {
        string name
        {
            get;
            set;
        }

        int damage
        {
            get;
            set;
        }

        int ammo
        {
            get;
            set;
        }

        float speed
        {
            get;
            set;
        }

        void shoot(Position playerPosition, GraphicsDevice GraphicsDevice);

        void reset();

        void Delete();

        void Draw(SpriteBatch spriteBatch, bool pause);

        int hit(Position point1, Position point2);

    }
}
