using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace The_Apocalypse.Weapons.Weapon_Type
{
    class ChainSaw : Weapon
    {
        private string _name = "ChainSaw";
        private int _damage = 30;
        private int _ammo = -1;
        private List<Proximity> near;

        private float _speed = 0;

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

        public int damage
        {
            get
            {
                return _damage;
            }
            set
            {
                _damage = value;
            }
        }

        public int ammo
        {
            get
            {
                return _ammo;
            }
            set
            {
                _ammo = value;
            }
        }

        public float speed
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

        public ChainSaw()
        {
            near = new List<Proximity>();
        }

        public int hit(Position point1, Position point2)
        {
            foreach (Proximity point in near)
            {
                if (point.CompareAreatoLine(point1, point2))
                {
                    return _damage;
                }
            }
            return 0;
        }

        public void Delete()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, bool pause)
        {

        }

        public void shoot(Position playerPosition, GraphicsDevice GraphicsDevice)
        {
            MouseState mousePosition = Mouse.GetState();
            near.Add(new Proximity(playerPosition, new Position(mousePosition.X, mousePosition.Y), GraphicsDevice));
        }

        public void reset()
        {

        }
    }
}
