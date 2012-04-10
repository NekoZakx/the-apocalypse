using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace The_Apocalypse
{
    class Proximity : Area
    {
        //Fait un ménage des variables inutiles.
        double deltaX = 0, deltaY = 0;
        double Mper = 0, Yper = 0, Xper = 0, Y = 0, X = 0, slope = 0, Bper = 0;
        bool hit = false;
        bool multikill = false;
        public bool state = false;

        Position positionNow = new Position(0, 0);

        //Constructeur vide
        public Proximity()
        {

        }

        public bool evaluate(Position player, Position pointToEvaluate, MouseState mouse)
        {
            if (Math.Pow((pointToEvaluate.X - player.X), 2) + Math.Pow((pointToEvaluate.Y + player.Y), 2) <= 100)//Si le point se trouve dans le cercle on retourne vraie
            {
                deltaX = mouse.X - player.X;
                deltaY = mouse.Y - player.Y;

                slope = deltaY / deltaX;//On calcule la pente de la première droite

                if (slope == 0)
                {
                    Mper = double.PositiveInfinity;
                }
                else
                {
                    Mper = -1 / slope;//On calcule la pente de la droite perpendiculaire
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

                Y = (Mper * pointToEvaluate.X) + Bper;//On trouve la valeur du Y pour le X de notre point sur la droite

                if (Mper == 0)
                {
                    X = Bper;
                }
                else
                {
                    X = (Y / Mper) + Bper;//On trouve la valeur du X pour le Y de notre point sur la droite
                }

                /***********************************************
                IL MANQUE DES CAS A ÉVALUER 
                ************************************************/
                if (mouse.Y <= Y)//Si le point à évalué est plus petit que le point trouvé, le point se trouve en dessous de la droite
                {
                    return true;
                }
                else if (mouse.Y >= Y)//Sinon le point se trouve au dessus
                {
                    return true;
                }
                else if (mouse.X <= X)//Si le point à évalué est plus petit que le point trouvé, le point se trouve à gauche de la droite
                {
                    return true;
                }
                else if(mouse.X >= X)//Sinon il se trouve à droite
                {
                    return true;
                }
            }

            return false;
        }

        public bool isInCircle(Position player, Position pointToEvaluate)
        {
            if (Math.Pow((pointToEvaluate.X - player.X), 2) + Math.Pow((pointToEvaluate.Y + player.Y), 2) <= 100)//Si le point se trouve dans le cercle on retourne vraie
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

        public void Draw(SpriteBatch spriteBatch, bool pause)
        {
            
        }

        //Arrete le processus de force d'avancement de la balle
        public void ForceStop()
        {

        }

        //Obtient les limites de l'écran de jeu
        public void GetLimit()
        {

        }

        public void UpdatePosition()
        {

        }

        //Retourne la position actuel de la balle.
        public Position GetPosition()
        {
            return positionNow;
        }

    }
}
