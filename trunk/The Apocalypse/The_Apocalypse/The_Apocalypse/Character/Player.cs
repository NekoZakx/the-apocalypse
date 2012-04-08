﻿using System;
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
        private int _hp = 100, _kill = 0;
        private Position _position;
        private Position _NormalPosition = new Position(0,0);
        private SpriteSheet _spriteSheet;
        private int _width = 50;
        private int _height = 50;
        private Vector2 _speed = new Vector2(150, 150);
        private Vector2 limit;
        private GraphicsDevice GraphicsDevice;
        private Weapon _weapon;
        private SpriteFont font;
        private List<Monster> observers = new List<Monster>();
        private PathFinder pathData;
        private int _score;
        private int _life = 3;
        private int _cpt = 0;

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

        public void Delete()
        {
            _weapon.Delete();
        }

        public void reset()
        {
            observers = new List<Monster>();
            _hp = 100;
            _kill = 0;
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            limit = new Vector2(Int32.Parse(file.FindReadNode("width")), Int32.Parse(file.FindReadNode("height")));

            _position = new Position((int)limit.X / 2, (int)limit.Y / 2);
            _weapon = new Handgun();

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

        public int kill
        {
            get
            {
                return _kill;
            }
            set
            {
                _kill = value;
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

        public Position NormalPosition
        {
            get
            {
                return _NormalPosition;
            }
            set
            {
                _NormalPosition = value;
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
        public void shootWeapon()
        {
            MouseState mouse = Mouse.GetState();
            double test = (DateTime.Now - wait).TotalMilliseconds;
            if(mouse.LeftButton == ButtonState.Pressed)
                if ((DateTime.Now - wait).TotalMilliseconds >= 20 * _weapon.speed)
                {
                    wait = DateTime.Now;
                    _weapon.shoot(position,this.GraphicsDevice);
                }
        }

        public void ChangeWeapon()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.D1))
            {
                _weapon = new Handgun();
            }else
            if (newState.IsKeyDown(Keys.D2))
            {
                _weapon = new AssaultRiffle();
            }else
            if (newState.IsKeyDown(Keys.D3))
            {
                _weapon = new Shotgun();
            }else
            if (newState.IsKeyDown(Keys.D4))
            {
                _weapon = new Gatling();
            }
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
            pathData.removeData(_position, _width, _height);
            
            KeyboardState newState = Keyboard.GetState();
            
            if (newState.IsKeyDown(Keys.D))
            {
                //orientation = SpriteEffects.FlipHorizontally;
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementX = (int)(_speed.X * elapsedTime);

                if ((_position.X + deplacementX) < limit.X - _width)
                    _position.X += deplacementX;
                //dst.X = (int)_position.X;
                Notify();
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
                Notify();
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
                Notify();
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
                Notify();
            }
            /*else if (newState.IsKeyUp(Keys.S) && oldState.IsKeyDown(Keys.S))
            {
            }*/
            oldState = newState;

            pathData.ChangeData(_position, _width,_height, SquareContent.Hero);
        }

        public void Update(GameTime gameTime)
        {
            orientation();
            move(gameTime);
            shootWeapon();
            ChangeWeapon();
            bulletState();
            noMoreHp();
            _cpt++;
            if(_cpt == 8)
            {
                looseHp(isInCircle());
                _cpt = 0;
            }
        }

        void bulletState()
        {
            foreach (Monster o in observers)
            {
                o.hp -= _weapon.hit(o.position, new Position((int)o.position.X + 50, (int)o.position.Y + 50));
                if (o.hp <= 0)
                    kill++;
            }
            return ;
        }

        public void Draw(SpriteBatch spriteBatch,bool pause)
        {
            spriteBatch.Draw(_spriteSheet.Frame(), new Rectangle((int)_position.X, (int)_position.Y, _width, _height), Color.White);
            _weapon.Draw(spriteBatch,pause);
            if(_weapon.ammo == 0)
                spriteBatch.DrawString(font, _weapon.ammo+" Ammo", new Vector2(0,0), Color.Red);
            else if(_weapon.ammo == -1)
                spriteBatch.DrawString(font, "INFINITE Ammo", new Vector2(0, 0), Color.White);
            else
                spriteBatch.DrawString(font, _weapon.ammo + " Ammo", new Vector2(0, 0), Color.Yellow);

            spriteBatch.DrawString(font, _kill + " KILL", new Vector2(0, 40), Color.Green);
            spriteBatch.DrawString(font, observers.Count + " ACTIVE ZOMBIES", new Vector2(0, 60), Color.White);
            spriteBatch.DrawString(font, "Score: " + _score, new Vector2(0, 80), Color.White);
            if(_life == 0)
                spriteBatch.DrawString(font, "Dead: " + _life, new Vector2(0, 100), Color.White);
            else
                spriteBatch.DrawString(font, "Life: " + _life, new Vector2(0, 100), Color.White);
        }

        public void LoadContent(ContentManager contentManager, GraphicsDevice GraphicsDevice)
        {
            this.GraphicsDevice = GraphicsDevice;
            _spriteSheet = new SpriteSheet(8, @"SpriteSheet/ArrowTest/arrow", contentManager);
            font = contentManager.Load<SpriteFont>(@"Fonts/TextFont");
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

        public void Attach(Monster Observer)
        {
            observers.Add(Observer);
            Notify();
        }

        public void Detach(Monster Observer)
        {
            Normal normalScore = new Normal();
            observers.Remove(Observer);
            //ici player score augmente

            scores += normalScore.scores;

            checkScore();
        }

        public void Notify()
        {
            foreach (Monster m in observers)
            {
                m.Update(this);
            }
        }

        public void addPathData(PathFinder pathData)
        {
            this.pathData = pathData;
        }

        public int scores
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        public int lifes
        {
            get
            {
                return _life;
            }
            set
            {
                _life = value;
            }
        }

        public int noMoreHp()
        {
            if (_hp == 0)
            {
                if (_life != 0)
                {
                    _life--;
                    _hp = 100;
                }
            }

            return _life;
        }

        public int checkScore()
        {
            if (_score % 5000 == 0 && _score != 0)
            {
                _life++;
            }

            return _life;
        }

        public int looseHp(bool isInCircle)
        {
            if(isInCircle)
            {
                _hp--;
            }

            return _hp;
        }

        public bool isInCircle()
        {
            if (Math.Pow((_NormalPosition.X - position.X), 2) + Math.Pow((_NormalPosition.Y + position.Y), 2) != 100)//Si le point se trouve dans le cercle on retourne vraie
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void proximity()
        {
            //Finir ici
            MouseState mouseState = Mouse.GetState();
            double deltaX = mouseState.X - position.X, deltaY = mouseState.Y - position.Y;
            double Mper = 0, Yper = 0, Xper = 0, Y = 0, X = 0, slope = 0, Bper = 0;

            if (Math.Pow((_NormalPosition.X - position.X), 2) + Math.Pow((_NormalPosition.Y + position.Y), 2) != 100)//Si le point se trouve dans le cercle on retourne vraie
            {
                slope = deltaY / deltaX;//On calcule la pente de la première droite

                if (slope != 0)
                {
                    Mper = -1 / slope;//On calcule la pente de la droite perpendiculaire
                }

                if (Mper != 0 || position.X != 0)
                {
                    Bper = position.Y / (Mper * position.X);//On calcule le B de la droite perpendiculaire
                }

                Yper = (Mper * position.X) + Bper;//On calcule la formule de la droite perpendiculaire

                Y = (Mper * _NormalPosition.X) + Bper;//On trouve la valeur du Y pour le X de notre point sur la droite 
                X = (Y / Mper) + Bper;//On trouve la valeur du X pour le Y de notre point sur la droite

                if (_NormalPosition.Y < Y)//Si le point à évalué est plus petit que le point trouvé, le point se trouve en dessous de la droite
                {

                }
                else//Sinon le point se trouve au dessus
                {

                }

                if (_NormalPosition.X < X)//Si le point à évalué est plus petit que le point trouvé, le point se trouve à gauche de la droite
                {

                }
                else//Sinon il se trouve à droite
                {

                }
            }
        }
    }
}
