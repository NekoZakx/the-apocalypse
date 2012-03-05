using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace The_Apocalypse
{
    class Direct : Area
    {
        int M = 0, B = 0, Orientation = 0;
        Position positionNow = new Position(0, 0);
        Position positionStart = new Position(0, 0);
        Position positionEnd = new Position(0, 0);

        Texture2D blank;

        public Direct(int x1, int x2, int y1, int y2,SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice)
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

            blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            UpdatePosition(spriteBatch);
        }

        void UpdatePosition(SpriteBatch spriteBatch)
        {
            positionNow.X = (int)(positionNow.X + (Orientation) * (1 / (Math.Pow(2, Math.Abs(M)))));
            positionNow.Y = (M * positionNow.X) + B;
            if(positionNow.X <= positionEnd.X && positionNow.X >= 0 && positionNow.Y >= 0 && positionNow.Y <= positionEnd.Y)
            {
                this.DrawLine(spriteBatch, this.blank, 1, Color.Yellow, new Vector2(positionNow.X + (Orientation*50), (M*(positionNow.X + (Orientation*50))+B)), new Vector2(positionNow.X,positionNow.Y));
                UpdatePosition(spriteBatch);
            }
        }

        public void GetLimit()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            positionEnd.X = Int32.Parse(file.FindReadNode("width"));
            positionEnd.Y = Int32.Parse(file.FindReadNode("height"));

            file.ReadClose();
        }

        void DrawLine(SpriteBatch batch, Texture2D blank,
              float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);
            batch.Draw(blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }
    }
}