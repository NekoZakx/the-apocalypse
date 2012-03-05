﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography;

namespace The_Apocalypse
{
    class Normal : Monster
    {
        private string __name = "Normal";
        private int __hp = 10;
        private Area __area; //À faire (Area::Near)
        private Position __position;
        private SpriteSheet __spriteSheet;
        private int __width = 50;
        private int __height = 50;
        private int __damage = 1;
        private Vector2 __speed = new Vector2(150, 150);
        private GraphicsDevice GraphicsDevice;
        private Vector2 limit;
        private Position _playerPosition;

        public Normal()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            int width = Int32.Parse(file.FindReadNode("width"));
            int height = Int32.Parse(file.FindReadNode("height"));

            file.ReadClose();

            int randomPosMode = getRandomNumber(4);
            int posX, posY;

            switch (randomPosMode)
            {
                case 0:
                    posX = -__width;
                    posY = getRandomNumber((byte)(height - __height - 1));
                    break;
                case 1:
                    posX = __width + width;
                    posY = getRandomNumber((byte)(height - __height - 1));
                    break;
                case 2:
                    posX = getRandomNumber((byte)(width - __width - 1));
                    posY = -__height;
                    break;
                case 3:
                    posX = getRandomNumber((byte)(width - __width - 1));
                    posY = height + __height;
                    break;
                default:
                    posX = -__width;
                    posY = -__height;
                    break;
            }

            __position = new Position(posX, posY);

            limit = new Vector2(width + __width, height + __height);
        }

        public void reset()
        {
            __hp = 10;
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            int width = Int32.Parse(file.FindReadNode("width"));
            int height = Int32.Parse(file.FindReadNode("height"));

            file.ReadClose();

            int randomPosMode = getRandomNumber(4);
            int posX, posY;

            switch (randomPosMode)
            {
                case 0:
                    posX = -__width;
                    posY = getRandomNumber((byte)(height - __height - 1));
                    break;
                case 1:
                    posX = __width + width;
                    posY = getRandomNumber((byte)(height - __height - 1));
                    break;
                case 2:
                    posX = getRandomNumber((byte)(width - __width - 1));
                    posY = -__height;
                    break;
                case 3:
                    posX = getRandomNumber((byte)(width - __width - 1));
                    posY = height + __height;
                    break;
                default:
                    posX = -__width;
                    posY = -__height;
                    break;
            }

            __position = new Position(posX, posY);
        }

        public int _damage
        {
            get
            {
                return __damage;
            }
            set
            {
                __damage = value;
            }
        }

        public Area _area
        {
            get
            {
                return __area;
            }
            set
            {
                __area = value;
            }
        }

        public SpriteSheet _spriteSheet
        {
            get
            {
                return __spriteSheet;
            }
            set
            {
                __spriteSheet = value;
            }
        }

        public int _width
        {
            get
            {
                return __width;
            }
            set
            {
                __width = value;
            }
        }

        public int _height
        {
            get
            {
                return __height;
            }
            set
            {
                __height = value;
            }
        }

        public Vector2 _speed
        {
            get
            {
                return __speed;
            }
            set
            {
                __speed = value;
            }
        }

        public Position playerPosition
        {
            get
            {
                return _playerPosition;
            }
            set
            {
                _playerPosition = value;
            }
        }

        public void attack()
        {
            //À faire
        }

        

        public void Update(GameTime gameTime)
        {
            orientation(playerPosition.X, playerPosition.Y);
            move(gameTime);
        }

        public void orientation(int pX, int pY)
        {
            double angle = Angle(__position.X, -__position.Y, pX, -pY);

            // 22.5 + 45 + 22.5 + 22.5 +45 +22.5 +22.5 +45 +22.5 +22.5 +45 +22.5 = 360

            // 22.5 + 45 + 45 + 45 + 45 +45 + 45 + 45 + 22.5 = 360

            if ((angle >= 0 && angle < 22.5) || (angle >= 337.5 && angle < 360))
                _spriteSheet.setCurrentFrame(0);
            else if (angle >= 22.5 && angle < 67.5)
                _spriteSheet.setCurrentFrame(1);
            else if (angle >= 67.5 && angle < 112.5)
                _spriteSheet.setCurrentFrame(2);
            else if (angle >= 112.5 && angle < 157.5)
                _spriteSheet.setCurrentFrame(3);
            else if (angle >= 157.5 && angle < 202.5)
                _spriteSheet.setCurrentFrame(4);
            else if (angle >= 202.5 && angle < 247.5)
                _spriteSheet.setCurrentFrame(5);
            else if (angle >= 247.5 && angle < 292.5)
                _spriteSheet.setCurrentFrame(6);
            else if (angle >= 292.5 && angle < 337.5)
                _spriteSheet.setCurrentFrame(7);

        }

        public void move(GameTime gameTime)
        {
        }

        //Pour générer un nombre aléatoire.
        //Source : http://msdn.microsoft.com/en-us/library/system.security.cryptography.rngcryptoserviceprovider.aspx
        public byte getRandomNumber(byte max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException("max");
            // Create a new instance of the RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(randomNumber);
            }
            while (!IsFair(randomNumber[0], max));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % max) + 1);
        }

        private static bool IsFair(byte pick, byte max)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / max;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return pick < max * fullSetsOfValues;
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice GraphicsDevice)
        {
            this.GraphicsDevice = GraphicsDevice;
            _spriteSheet = new SpriteSheet(8, @"SpriteSheet/ArrowTest/Monster/arrowM", contentManager);
        }

        //Fonction pour calculer l'angle entre deux points
        //Source : http://www.carlosfemmer.com/post/2006/02/Calculate-Angle-between-2-points-using-C.aspx
        private double Angle(double px1, double py1, double px2, double py2)
        {
            // Negate X and Y values
            double pxRes = px2 - px1;
            double pyRes = py2 - py1;
            double angle = 0.0;
            // Calculate the angle
            if (pxRes == 0.0)
            {
                if (pxRes == 0.0)
                    angle = 0.0;
                else if (pyRes > 0.0) angle = System.Math.PI / 2.0;
                else
                    angle = System.Math.PI * 3.0 / 2.0;
            }
            else if (pyRes == 0.0)
            {
                if (pxRes > 0.0)
                    angle = 0.0;
                else
                    angle = System.Math.PI;
            }
            else
            {
                if (pxRes < 0.0)
                    angle = System.Math.Atan(pyRes / pxRes) + System.Math.PI;
                else if (pyRes < 0.0) angle = System.Math.Atan(pyRes / pxRes) + (2 * System.Math.PI);
                else
                    angle = System.Math.Atan(pyRes / pxRes);
            }
            // Convert to degrees
            angle = angle * 180 / System.Math.PI; return angle;

        }
    }
}