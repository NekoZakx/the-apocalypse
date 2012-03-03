using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Apocalypse
{
    class Normal : Monster
    {

        private string _name = "Normal";
        private int _damage = 1;
        private int _hp = 20;
        private Area _area; //À faire (Zone::Near)

        public Normal()
        {
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
