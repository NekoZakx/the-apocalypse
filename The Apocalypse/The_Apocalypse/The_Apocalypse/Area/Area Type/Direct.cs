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
        int B = 0, Orientation = 0;
        double M = 0;
        Position positionNow = new Position(0, 0);
        Position positionStart = new Position(0, 0);
        Position positionEnd = new Position(0, 0);

        Texture2D blank;
        int oldX = 0, oldY = 0;
        public bool state = false;
        Thread t1;
        public Direct(Position player,Position mouse, GraphicsDevice GraphicsDevice)
        {
            GetLimit();
            positionStart.X = player.X;
            positionStart.Y = player.Y;
            oldX = positionStart.X;
            oldY = positionStart.Y;

            if (mouse.X - positionStart.X < 0)
            {
                Orientation = -1;
            }
            else
            {
                Orientation = 1;
            }

            try
            {
                M = (((double)mouse.Y - (double)positionStart.Y) / ((double)mouse.X - (double)positionStart.X));
            }
            catch (Exception e) { M = 0; }
            B = positionStart.Y - (int)(M * positionStart.X);

            blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            positionNow.X = positionStart.X;
            positionNow.Y = positionStart.Y;

            t1 = new Thread(new ThreadStart(UpdatePosition));
            t1.Start();
        }

        void UpdatePosition()
        {
            do{
                Thread.Sleep(1);
                oldX = positionNow.X + (Orientation * 50);
                oldY = (int)(M * oldX) + B;
                positionNow.X += (int)(Orientation * (1 / (Math.Pow(2, Math.Abs(M)))+1));
                positionNow.Y = (int)(M * positionNow.X) + B;
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
            {
                if (t1.ThreadState != ThreadState.Stopped && t1.ThreadState != ThreadState.Suspended)
                    t1.Suspend();
            }
            else
                if (t1.ThreadState == ThreadState.Suspended)
                    t1.Resume();

            this.DrawLine(spriteBatch, this.blank, 1, Color.Yellow,  new Vector2(positionNow.X, positionNow.Y),new Vector2(oldX, oldY));
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