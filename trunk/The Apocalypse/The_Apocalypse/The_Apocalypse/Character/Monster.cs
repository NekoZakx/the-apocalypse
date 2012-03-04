using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    class Monster : Character
    {
        private string _name;
        private int _hp;
        private Position _position;
        private SpriteSheet _spriteSheet;
        private int _width = 50;
        private int _height = 50;
        
        int damage
        {
            get;
            set;
        }

        Area area
        {
            get;
            set;
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

        public void move()
        {
        }

        public void Update(GameTime gameTime)
        {
            //À faire
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //À faire
        }

        public void LoadContent(ContentManager contentManager)
        {
            //À faire
        }
    }
}
