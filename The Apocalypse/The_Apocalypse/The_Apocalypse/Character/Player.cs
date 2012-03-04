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

            _position = new Position(Int32.Parse(file.FindReadNode("width")), Int32.Parse(file.FindReadNode("height")));

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


        }

        public void move()
        {
        }

        public void Update(GameTime gameTime)
        {
            //À faire
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteSheet.Frame(), new Rectangle(_position.X, _position.Y, 50, 50), Color.White);
        }

        public void LoadContent(ContentManager contentManager)
        {
            _spriteSheet = new SpriteSheet(8, @"SpriteSheet/ArrowTest/arrow", contentManager);
        }
    }
}
