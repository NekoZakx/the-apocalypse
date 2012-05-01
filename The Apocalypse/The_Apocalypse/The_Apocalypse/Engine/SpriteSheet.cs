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
        private Texture2D[,] sheet;
        private int currentDirection = 0;
        private int currentFrame = 1;

        public SpriteSheet(string textureName, ContentManager Content)
        {
            sheet = new Texture2D[8, 3];
            sheet[0, 0] = Content.Load<Texture2D>(textureName + "e1");
            sheet[0, 1] = Content.Load<Texture2D>(textureName + "e2");
            sheet[0, 2] = Content.Load<Texture2D>(textureName + "e3");
            sheet[1, 0] = Content.Load<Texture2D>(textureName + "ne1");
            sheet[1, 1] = Content.Load<Texture2D>(textureName + "ne2");
            sheet[1, 2] = Content.Load<Texture2D>(textureName + "ne3");
            sheet[2, 0] = Content.Load<Texture2D>(textureName + "n1");
            sheet[2, 1] = Content.Load<Texture2D>(textureName + "n2");
            sheet[2, 2] = Content.Load<Texture2D>(textureName + "n3");
            sheet[3, 0] = Content.Load<Texture2D>(textureName + "nw1");
            sheet[3, 1] = Content.Load<Texture2D>(textureName + "nw2");
            sheet[3, 2] = Content.Load<Texture2D>(textureName + "nw3");
            sheet[4, 0] = Content.Load<Texture2D>(textureName + "w1");
            sheet[4, 1] = Content.Load<Texture2D>(textureName + "w2");
            sheet[4, 2] = Content.Load<Texture2D>(textureName + "w3");
            sheet[5, 0] = Content.Load<Texture2D>(textureName + "sw1");
            sheet[5, 1] = Content.Load<Texture2D>(textureName + "sw2");
            sheet[5, 2] = Content.Load<Texture2D>(textureName + "sw3");
            sheet[6, 0] = Content.Load<Texture2D>(textureName + "s1");
            sheet[6, 1] = Content.Load<Texture2D>(textureName + "s2");
            sheet[6, 2] = Content.Load<Texture2D>(textureName + "s3");
            sheet[7, 0] = Content.Load<Texture2D>(textureName + "se1");
            sheet[7, 1] = Content.Load<Texture2D>(textureName + "se2");
            sheet[7, 2] = Content.Load<Texture2D>(textureName + "se3");
        }

        public Texture2D Frame()
        {
            return sheet[currentDirection, currentFrame];
        }

        public Texture2D Frame(int direction, int nuFrame)
        {
            return sheet[direction, nuFrame];
        }

        public void setCurrentDirection(int direction)
        {
            this.currentDirection = direction;
        }

        public void setCurrentFrame(int nuFrame)
        {
            this.currentFrame = nuFrame;
        }

        public int getCurrentFrame()
        {
            return this.currentFrame;
        }

    }
}
