using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace The_Apocalypse
{
    class Handgun : Weapon
    {
        private string _name = "Handgun";
        private int _damage = 5;
        private int _ammo = -1;

        private float _speed = 2;
        private bool _touchZombie = true;
        private bool _melee = false;

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

        public Handgun(){}

        public void shoot(Position playerPosition,SpriteBatch spriteBatch)
        {
            MouseState mousePosition = Mouse.GetState();
            new Direct(playerPosition.X, mousePosition.X, playerPosition.Y, mousePosition.Y,spriteBatch);
        }
    }
}
