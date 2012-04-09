using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;

namespace The_Apocalypse
{
    class Proximity : Area
    {
        double deltaX = 0, deltaY = 0;
        double Mper = 0, Yper = 0, Xper = 0, Y = 0, X = 0, slope = 0, Bper = 0;
        bool underRight = true, upperRight = true, leftOfRight = true, rightOfRight = true;
        bool hit = false;
        bool multikill = false;
        public bool state = false;

        Position positionNow = new Position(0, 0);
        Position positionStart = new Position(0, 0);
        Position positionEnd = new Position(0, 0);

        public Proximity(Position player, Position mouse)
        {
            positionStart.X = player.X;
            positionStart.Y = player.Y;
            positionNow.X = positionStart.X;
            positionNow.Y = positionStart.Y;
        }

        public bool proximity(Position player, Position normal, Position mouse, GraphicsDevice GraphicsDevice)
        {
            if (Math.Pow((normal.X - player.X), 2) + Math.Pow((normal.Y + player.Y), 2) <= 100)//Si le point se trouve dans le cercle on retourne vraie
            {
                deltaX = mouse.X - player.X;
                deltaY = mouse.Y - player.Y;

                slope = deltaY / deltaX;//On calcule la pente de la première droite

                if (slope == 0)
                {
                    Mper = double.PositiveInfinity;//On calcule la pente de la droite perpendiculaire
                }
                else
                {
                    Mper = -1 / slope;
                }

                if (Mper == 0)
                {
                    Bper = player.Y;
                }
                else
                {
                    Bper = player.Y / (Mper * player.X);//On calcule le B de la droite perpendiculaire
                }

                Yper = (Mper * player.X) + Bper;//On calcule la formule de la droite perpendiculaire

                Y = (Mper * normal.X) + Bper;//On trouve la valeur du Y pour le X de notre point sur la droite

                if (Mper == 0)
                {
                    X = Bper;
                }
                else
                {
                    X = (Y / Mper) + Bper;//On trouve la valeur du X pour le Y de notre point sur la droite
                }

                if (mouse.Y < Y)//Si le point à évalué est plus petit que le point trouvé, le point se trouve en dessous de la droite
                {
                    return underRight;
                }
                else if (mouse.Y > Y)//Sinon le point se trouve au dessus
                {
                    return upperRight;
                }

                if (mouse.X < X)//Si le point à évalué est plus petit que le point trouvé, le point se trouve à gauche de la droite
                {
                    return leftOfRight;  
                }
                else//Sinon il se trouve à droite
                {
                    return rightOfRight;
                }
            }

            return false;
        }

        public bool isInCircle(Position player, Position normal)
        {
            if (Math.Pow((normal.X - player.X), 2) + Math.Pow((normal.Y + player.Y), 2) <= 100)//Si le point se trouve dans le cercle on retourne vraie
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CompareAreatoLine(Position point1, Position point2)
        {
            if (!hit || multikill)
                if (positionNow.X > point1.X && positionNow.X < point2.X)
                {
                    if (positionNow.Y > point1.Y && positionNow.Y < point2.Y)
                    {
                        hit = true;
                    }
                }
            if (!state && hit)
            {
                if (multikill)
                    hit = false;
                else
                {
                    state = true;
                }
                return true;
            }
            return false;
        }
    }
}
