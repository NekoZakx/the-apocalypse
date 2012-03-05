using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Apocalypse
{
    interface Weapon
    {
        string name
        {
            get;
            set;
        }

        int damage
        {
            get;
            set;
        }

        int ammo
        {
            get;
            set;
        }

        float speed
        {
            get;
            set;
        }

        void shoot();
    }
}
