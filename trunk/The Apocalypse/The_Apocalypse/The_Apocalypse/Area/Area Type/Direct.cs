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

        bool hit = false;
        bool multikill = false;

        Texture2D blank;
        double oldX = 0, oldY = 0, angle;
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

            //Initialization de la texture 2D pour dessiner la ligne de la balle
            blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            //L'angle de la balle
            angle = (float)Math.Atan2(mouse.Y - positionStart.Y, mouse.X - positionStart.X);

            //Démarre la fonction UpdatePosition dans un Thread pour ne pas attendre que la balle finisse pour continuer de jouer.
            t1 = new Thread(new ThreadStart(UpdatePosition));
            t1.Start();
        }
        
        void UpdatePosition()
        {
            DateTime init = DateTime.Now;
            
            do{
                Thread.Sleep(1);//Attente du nombre de milliseconde indiquer. Sinon, l'éxécution est trop rapide pour voir la balle.

                if (speed == 0)
                {
                    init = init.AddSeconds((DateTime.Now - init).TotalSeconds);
                }
                

                if (state) break;

                oldX = (int)positionNow.X + (Math.Cos(angle) * 10);
                oldY = (int)positionNow.Y + (Math.Sin(angle) * 10);

                //Toute les positions entières de la ligne sont rencontrer
                positionNow.X += (Math.Cos(angle)) * speed;//incremente de 1 maximum et décrémente de -1 minimum
                positionNow.Y += (Math.Sin(angle)) * speed;//incremente de 1 maximum et décrémente de -1 minimum

                

            }while((DateTime.Now - init).TotalSeconds < 10 && positionNow.X < positionEnd.X && positionNow.X > 0 && positionNow.Y > 0 && positionNow.Y < positionEnd.Y);//Tant que la balle est pas sorti du terrain de jeu
            state = true;//Définit si la balle peut être supprimé 
            return;
        }

        //Retourne la position actuel de la balle.
        public Position GetPosition()
        {
            return positionNow;
        }

        //Dessine la balle à l'écran
        public void Draw(SpriteBatch spriteBatch,bool pause)
        {
            //Si le jeu est sur pause, l'avancement des balles est arrêté.
            if (pause)
                speed = 0;
            else
                speed = 1;

            this.DrawLine(spriteBatch, this.blank, 2, Color.Yellow, new Vector2((int)positionNow.X, (int)positionNow.Y), new Vector2((int)oldX, (int)oldY));
        }

        //Arrete le processus de force d'avancement de la balle
        public void ForceStop()
        {
            t1.Abort();
        }

        //Obtient les limites de l'écran de jeu
        public void GetLimit()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            positionEnd.X = Int32.Parse(file.FindReadNode("width"));
            positionEnd.Y = Int32.Parse(file.FindReadNode("height"));

            file.ReadClose();
        }

        //Dessine une ligne entre les points
        void DrawLine(SpriteBatch batch, Texture2D blank,
              float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);
            batch.Draw(blank, point1, null, color,
                        angle, Vector2.Zero, new Vector2(length, width),
                        SpriteEffects.None, 0);
        }

        public bool ComparePointtoLine(Position point)
        {
            if (point.X < positionNow.X && point.X  > positionNow.X)
                if (point.Y < positionNow.Y && point.Y  > positionNow.Y)
                    return true;
            return false;
        }

        public bool CompareAreatoLine(Position point1, Position point2)
        {
            if(!hit || multikill)
                if(positionNow.X < point1.X && positionNow.X > point2.X)
                {
                    if (positionNow.Y < point1.Y && positionNow.Y > point2.Y)
                    {
                        hit = true;
                        return true;
                    }
                    if (positionNow.Y > point1.Y && positionNow.Y < point2.Y)
                    {
                        hit = true;
                        return true;
                    }
                }
                else
                    if (positionNow.X > point1.X && positionNow.X < point2.X)
                    {
                        if (positionNow.Y < point1.Y && positionNow.Y > point2.Y)
                        {
                            hit = true;
                            return true;
                        }
                        if (positionNow.Y > point1.Y && positionNow.Y < point2.Y)
                        {
                            hit = true;
                            return true;
                        }
                    }

            return false;
        }
    }
}