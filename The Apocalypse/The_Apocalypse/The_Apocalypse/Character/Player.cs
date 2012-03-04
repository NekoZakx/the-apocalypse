using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace The_Apocalypse
{
    class Player : Character
    {
        private string _name;
        private int _hp;
        private Position _position;
        private SpriteSheet _spriteSheet;

        private Weapon _weapon;

        public Player(string name, int hp, Position position, Weapon weapon)
        {
            _name = name;
            _hp = hp;
            _position = position;
            _weapon = weapon;
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

        public void move(float angle)
        {
        }

        public void Update(GameTime gameTime)
        {
            //À faire
        }

        public void Draw(GameTime gameTime)
        {
            //À faire
        }

        public void LoadContent(ContentManager contentManager)
        {
            _spriteSheet = new SpriteSheet(8, @"SpriteSheet/ArrowTest/arrow", contentManager);
        }
    }
}
