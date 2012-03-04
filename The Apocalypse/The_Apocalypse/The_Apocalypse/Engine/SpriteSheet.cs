using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    class SpriteSheet
    {
        private Texture2D sheet;
        private int nbFrame = 0;
        private int width = 0;
        private int height = 0;

        public SpriteSheet(Texture2D sheet, int nbFrame)
        {
            this.sheet = sheet;
            this.nbFrame = nbFrame;
            this.width = sheet.Width / nbFrame;
            this.height = sheet.Height;
        }

        public Texture2D Texture2D()
        {
            return sheet;
        }

        public int Width()
        {
            return width;
        }

        public int Height()
        {
            return height;
        }

    }
}
