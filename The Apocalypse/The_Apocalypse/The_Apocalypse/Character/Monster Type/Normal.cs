using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace The_Apocalypse
{
    class Normal : Monster
    {
        private string __name = "Normal";
        private int __damage = 1;
        private int __hp = 20;
        private Area __area; //À faire (Area::Near)
        private SpriteSheet __spriteSheet;
        private int __width = 50;
        private int __height = 50;

        public Normal()
        {
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

        public void attack()
        {
            //À faire
        }

        public void move()
        {
            //À faire
        }
    }
}
