using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework.Content;

namespace The_Apocalypse
{
    class Gatling : Weapon
    {
        private string _name = "Gatling";
        private int _damage = 15;
        private int _ammo = 200;
        private List<Direct> bulletShot;

        private float _speed = 1;

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

        public Gatling()
        {
            bulletShot = new List<Direct>();
        }

        public void reset()
        {
            foreach (Direct bullet in bulletShot)
            {
                bullet.ForceStop();
            }
            bulletShot = new List<Direct>();
        }

        public void Delete()
        {
            foreach (Direct bullet in bulletShot)
            {
                bullet.ForceStop();
            }
            bulletShot = null;
        }

        Random rand = new Random();
        public void shoot(Position playerPosition,GraphicsDevice GraphicsDevice)
        {
            MouseState mousePosition = Mouse.GetState();
            if (_ammo > 0)
            {
                _ammo--;
                bulletShot.Add(new Direct(playerPosition, new Position(mousePosition.X, mousePosition.Y), GraphicsDevice, rand.Next(-50, 50)));
            }

        }

        public void Draw(SpriteBatch spriteBatch,bool pause)
        {
            bool restart = false;
            foreach (Direct bullet in bulletShot)
            {
                bullet.Draw(spriteBatch,pause);
                    
                if (bullet.state)
                {
                    bulletShot.Remove(bullet);
                    restart = true;
                    break;
                }
            }
            if (restart) Draw(spriteBatch,pause);
        }

        public int hit(Position point1, Position point2)
        {
            foreach (Direct bullet in bulletShot)
            {
                if (bullet.CompareAreatoLine(point1,point2))
                    return _damage;
            }
            return 0;
        }

    }

}
