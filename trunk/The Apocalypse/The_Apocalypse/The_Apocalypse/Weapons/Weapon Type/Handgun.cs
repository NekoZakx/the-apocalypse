using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace The_Apocalypse
{
    class Handgun : Weapon
    {
        private string _name = "Handgun";
        private int _damage = 5;
        private int _ammo = -1;
        private List<Direct> bulletShooted;

        private float _speed = 15;

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

        public void reset()
        {
            foreach (Direct bullet in bulletShooted)
            {
                bullet.ForceStop();
            }
            bulletShooted = new List<Direct>();
        }

        public void Delete()
        {
            foreach (Direct bullet in bulletShooted)
            {
                bullet.ForceStop();
            }
            bulletShooted = null;
        }

        public Handgun()
        {
            bulletShooted = new List<Direct>();
        }

        public void shoot(Position playerPosition,GraphicsDevice GraphicsDevice)
        {
            MouseState mousePosition = Mouse.GetState();
            bulletShooted.Add(new Direct(playerPosition, new Position(mousePosition.X,mousePosition.Y), GraphicsDevice,0));
        }

        public void Draw(SpriteBatch spriteBatch,bool pause)
        {
            bool restart = false;
            foreach (Direct bullet in bulletShooted)
            {
                bullet.Draw(spriteBatch,pause);
                    
                if (bullet.state)
                {
                    bulletShooted.Remove(bullet);
                    restart = true;
                    break;
                }
            }
            if (restart) Draw(spriteBatch,pause);
        }
        public int hit(Position point1, Position point2)
        {
            foreach (Direct bullet in bulletShooted)
            {
                if (bullet.CompareAreatoLine(point1,point2))
                    return _damage;
            }
            return 0;
        }

    }

}
