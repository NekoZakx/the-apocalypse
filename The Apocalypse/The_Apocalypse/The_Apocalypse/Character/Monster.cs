using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace The_Apocalypse
{
    class Monster : Character
    {
        private string _name;
        private int _hp;
        private Position _position;
        
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
            //À faire
        }
    }
}
