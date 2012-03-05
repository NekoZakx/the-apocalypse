using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        public Normal()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            int width = Int32.Parse(file.FindReadNode("width"));
            int height = Int32.Parse(file.FindReadNode("height"));

            file.ReadClose();
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
