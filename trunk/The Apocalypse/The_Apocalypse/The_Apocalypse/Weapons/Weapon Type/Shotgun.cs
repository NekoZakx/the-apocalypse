using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Apocalypse
{
    class Shotgun : Weapon
    {
        private string _name = "Shotgun";
        private int _damage = 20;
        private int _ammo = 20;
        private List<Direct> bulletShot;
        private SoundEffect _shootSound;
        private float _soundVolume;
        private float _soundPitch;
        private float _soundPan;

        private float _speed = 60;

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

        public float soundVolume
        {
            get
            {
                return _soundVolume;
            }
            set
            {
                _soundVolume = value;
            }
        }

        public float soundPitch
        {
            get
            {
                return _soundPitch;
            }
            set
            {
                _soundPitch = value;
            }
        }

        public float soundPan
        {
            get
            {
                return _soundPan;
            }
            set
            {
                _soundPan = value;
            }
        }

        public Shotgun()
        {
            bulletShot = new List<Direct>();
            LoadPreferenceData();
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
            if (_ammo > 0)
            {
                _ammo--;
                bulletShot.Add(new Direct(playerPosition, new Position(mousePosition.X, mousePosition.Y), GraphicsDevice, -45));
                bulletShot.Add(new Direct(playerPosition, new Position(mousePosition.X, mousePosition.Y), GraphicsDevice, -27));
                bulletShot.Add(new Direct(playerPosition, new Position(mousePosition.X, mousePosition.Y), GraphicsDevice, 0));
                bulletShot.Add(new Direct(playerPosition, new Position(mousePosition.X, mousePosition.Y), GraphicsDevice, 27));
                bulletShot.Add(new Direct(playerPosition, new Position(mousePosition.X, mousePosition.Y), GraphicsDevice, 45));
                _shootSound.Play(_soundVolume, _soundPitch, _soundPan);
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

        public void LoadPreferenceData()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            _soundVolume = float.Parse(file.FindReadNode("SFXvolume"));
            _soundPitch = float.Parse(file.FindReadNode("SFXpitch"));
            _soundPan = float.Parse(file.FindReadNode("pan"));

            file.ReadClose();
        }

    }

}
