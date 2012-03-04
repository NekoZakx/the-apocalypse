using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Apocalypse
{
    class Player : Character
    {
        private string _name;
        private int _hp = 100;
        private Position _position;
        private SpriteSheet _spriteSheet;
        private int _width = 50;
        private int _height = 50;

        private Weapon _weapon;

        public Player()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            _position = new Position(Int32.Parse(file.FindReadNode("width"))/2, Int32.Parse(file.FindReadNode("height"))/2);

            file.ReadClose();
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }
        }

        public Position position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public SpriteSheet spriteSheet
        {
            get
            {
                return _spriteSheet;
            }
            set
            {
                _spriteSheet = value;
            }
        }

        public int width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public Weapon getWeapon()
        {
            return _weapon;
        }

        public void setWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void shootWeapon()
        {
            _weapon.shoot();
        }

        public void orientation()
        {
            MouseState state = Mouse.GetState();

            double angle = Angle(_position.X, position.Y, state.X, state.Y);

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

        public void move()
        {
        }

        public void Update(GameTime gameTime)
        {
            orientation();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet.Frame(), new Rectangle(_position.X, _position.Y, _width, _height), Color.White);
            spriteBatch.End();
        }

        public void LoadContent(ContentManager contentManager)
        {
            _spriteSheet = new SpriteSheet(8, @"SpriteSheet/ArrowTest/arrow", contentManager);
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
