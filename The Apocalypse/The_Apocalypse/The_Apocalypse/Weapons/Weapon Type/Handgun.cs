using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework.Audio;

namespace The_Apocalypse
{
    class Handgun : Weapon
    {
        private string _name = "Handgun";
        private int _damage = 5;
        private int _ammo = -1;
        private List<Direct> bulletShot;
        private SoundEffect _shootSound;

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

        public SoundEffect shootSound
        {
            get
            {
                return _shootSound;
            }
            set
            {
                _shootSound = value;
            }
        }

        public Handgun()
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

        public void shoot(Position playerPosition,GraphicsDevice GraphicsDevice)
        {
            MouseState mousePosition = Mouse.GetState();
            bulletShot.Add(new Direct(playerPosition, new Position(mousePosition.X,mousePosition.Y), GraphicsDevice,0));
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
                if (bullet.CompareAreatoLine(point1, point2))
                {
                    return _damage;
                }
                    
            }
            return 0;
        }

    }

}
