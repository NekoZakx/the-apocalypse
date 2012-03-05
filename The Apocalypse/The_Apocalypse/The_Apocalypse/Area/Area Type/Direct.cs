using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;

namespace The_Apocalypse
{
    class Direct : Area
    {
        int speed = 1;
        Position positionNow = new Position(0, 0);
        Position positionStart = new Position(0, 0);
        Position positionEnd = new Position(0, 0);

        Texture2D blank;
        double oldX = 0, oldY = 0;
        public bool state = false;
        Thread t1;

        public Direct(Position player, Position mouse, GraphicsDevice GraphicsDevice, int variation)
        {
            GetLimit();
            positionStart.X = player.X;
            positionStart.Y = player.Y;
            positionNow.X = positionStart.X;
            positionNow.Y = positionStart.Y;
            oldX = positionStart.X;
            oldY = positionStart.Y;

            int longeur = 150;
            if (variation != 0)
            {
                double ratio = Math.Sqrt(Math.Pow((mouse.X - player.X), 2) + Math.Pow((mouse.Y - player.Y), 2)) / longeur;


                mouse.X += (variation * ratio);
                mouse.Y += (variation * ratio);
            }

            blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });



            angle = (float)Math.Atan2(mouse.Y - positionStart.Y, mouse.X - positionStart.X);

            t1 = new Thread(new ThreadStart(UpdatePosition));
            t1.Start();
        }

        double angle;
        void UpdatePosition()
        {
            do{
                Thread.Sleep(1);

                oldX = (int)positionNow.X + (Math.Cos(angle) * 10);
                oldY = (int)positionNow.Y + (Math.Sin(angle) * 10);

                positionNow.X += (Math.Cos(angle)) * speed;
                positionNow.Y += (Math.Sin(angle)) * speed;

            }while(positionNow.X < positionEnd.X && positionNow.X > 0 && positionNow.Y > 0 && positionNow.Y < positionEnd.Y);
            state = true;
            return;
        }

        public Position GetPosition()
        {
            return positionNow;
        }

        public void Draw(SpriteBatch spriteBatch,bool pause)
        {

            if (pause)
                speed = 0;
            else
                speed = 1;

            this.DrawLine(spriteBatch, this.blank, 2, Color.Yellow, new Vector2((int)positionNow.X, (int)positionNow.Y), new Vector2((int)oldX, (int)oldY));
        }

        public void ForceStop()
        {
            if (t1.ThreadState == ThreadState.Suspended)
                t1.Resume();
            t1.Abort();
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