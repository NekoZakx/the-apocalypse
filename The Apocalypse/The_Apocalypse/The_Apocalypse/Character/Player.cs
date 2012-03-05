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
        private Vector2 _speed = new Vector2(150, 150);
        private Vector2 limit;
        private GraphicsDevice GraphicsDevice;
        private Weapon _weapon;

        public Player()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            limit = new Vector2(Int32.Parse(file.FindReadNode("width")), Int32.Parse(file.FindReadNode("height")));

            _position = new Position((int)limit.X/2, (int)limit.Y/2);

            file.ReadClose();

            /********************************************************************
             * **************************************************************** *
             *******************************************************************/

            _weapon = new Handgun();

            /********************************************************************
             * **************************************************************** *
             *******************************************************************/
        }

        public void reset()
        {
            _hp = 100;
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            limit = new Vector2(Int32.Parse(file.FindReadNode("width")), Int32.Parse(file.FindReadNode("height")));

            _position = new Position((int)limit.X / 2, (int)limit.Y / 2);

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

        public Vector2 speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
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

        DateTime wait = DateTime.Now;
        public void shootWeapon(SpriteBatch spriteBatch)
        {
            MouseState mouse = Mouse.GetState();
            double test = (DateTime.Now - wait).TotalMilliseconds;
            if(mouse.LeftButton == ButtonState.Pressed)
                /*if ((DateTime.Now - wait).TotalMilliseconds >= 1000 * _weapon.speed)
                {*/
                _weapon.shoot(position, spriteBatch, GraphicsDevice);
                    wait = DateTime.Now;
                /*}*/
        }

        public void orientation()
        {
            MouseState state = Mouse.GetState();

            double angle = Angle(_position.X, -position.Y, state.X, -state.Y);

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

        KeyboardState oldState = Keyboard.GetState();

        public void move(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.D))
            {
                //orientation = SpriteEffects.FlipHorizontally;
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementX = (int)(_speed.X * elapsedTime);

                if ((_position.X + deplacementX) < limit.X - _width)
                    _position.X += deplacementX;
                //dst.X = (int)_position.X;
            }
            /*else if (newState.IsKeyUp(Keys.D) && oldState.IsKeyDown(Keys.D))
            {
                orientation = SpriteEffects.FlipHorizontally;
            }*/
            if (newState.IsKeyDown(Keys.A))
            {
                //orientation = SpriteEffects.None;
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementX = (int)(_speed.X * elapsedTime);

                if ((_position.X - deplacementX) > 0)
                    _position.X -= deplacementX;
                //dst.X = (int)_position.X;
            }
            /*else if (newState.IsKeyUp(Keys.A) && oldState.IsKeyDown(Keys.A))
            {
                orientation = SpriteEffects.None;
            }*/
            if (newState.IsKeyDown(Keys.W))
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementY = (int)(_speed.Y * elapsedTime);

                if ((_position.Y - deplacementY) > 0)
                    _position.Y -= deplacementY;
                //dst.Y = (int)_position.Y;
            }
            else if (newState.IsKeyUp(Keys.W) && oldState.IsKeyDown(Keys.W))
            {
            }
            if (newState.IsKeyDown(Keys.S))
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementY = (int)(_speed.Y * elapsedTime);
                if ((_position.Y + deplacementY) < limit.Y - _height)
                    _position.Y += deplacementY;
                //dst.Y = (int)_position.Y;
            }
            /*else if (newState.IsKeyUp(Keys.S) && oldState.IsKeyDown(Keys.S))
            {
            }*/
            oldState = newState;
        }

        public void Update(GameTime gameTime)
        {
            orientation();
            move(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_spriteSheet.Frame(), new Rectangle(_position.X, _position.Y, _width, _height), Color.White);
            this.shootWeapon(spriteBatch);
            spriteBatch.End();
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice GraphicsDevice)
        {
            this.GraphicsDevice = GraphicsDevice;
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
