using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        void LoadContent(ContentManager contentManager);
    }
}
