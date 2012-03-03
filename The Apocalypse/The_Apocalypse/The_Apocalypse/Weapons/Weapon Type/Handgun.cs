using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private Area _area; //À faire (Area::Direct)

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

        public bool touchZombie
        {
            get
            {
                return _touchZombie;
            }
            set
            {
                _touchZombie = value;
            }
        }

        public bool melee
        {
            get
            {
                return _melee;
            }
            set
            {
                _melee = value;
            }
        }

        public Area area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
            }
        }

        public Handgun()
        {
        }

        public void shoot()
        {
            //À faire
        }
    }
}
