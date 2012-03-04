using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    class SpriteSheet
    {
        private Texture2D[] sheet;
        private int nbFrame = 0;

        public SpriteSheet(int nbFrame, string textureName, ContentManager Content)
        {
            this.nbFrame = nbFrame;
            sheet = new Texture2D[nbFrame];
            for (int i = 0; i < nbFrame; i++)
                sheet[i] = Content.Load<Texture2D>(@"SpriteSheet/ArrowTest/arrow"+i); 
        }

        public Texture2D Frame(int nuFrame)
        {
            return sheet[nuFrame];
        }

    }
}
