using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    interface Character
    {
        string name
        {
            get;
            set;
        }
        int hp
        {
            get;
            set;
        }
        Position position
        {
            get;
            set;
        }
        SpriteSheet spriteSheet
        {
            get;
            set;
        }

        int width
        {
            get;
            set;
        }

        int height
        {
            get;
            set;
        }

        void move();

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        void LoadContent(ContentManager contentManager);
    }
}
