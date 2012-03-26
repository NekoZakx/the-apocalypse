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

        Vector2 speed
        {
            get;
            set;
        }

        void addPathData(PathFinder pathData);

        void move(GameTime gameTime);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, bool pause);

        void LoadContent(ContentManager contentManager, GraphicsDevice GraphicsDevice);
    }
}
