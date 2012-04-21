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
    class ChainSaw : Weapon
    {
        private string _name = "ChainSaw";
        private int _damage = 30;
        private int _ammo = -1;
        private bool _shot = false;
        private Proximity proximity;
        private Position _playerPosition = new Position(0,0);
        private SoundEffect _shootSound;
        private float _soundVolume;
        private float _soundPitch;
        private float _soundPan;

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

        public ChainSaw()
        {
            proximity = new Proximity();
            LoadPreferenceData();
        }

        public int hit(Position point1, Position point2)
        {
            //point1 et point2 sont les points qui définissent la zone de la cible a vérifier.
            //Verifier a l'aide de la variable du positionnement du joueur
            if (_shot)
            {
                MouseState mousePosition = Mouse.GetState();
                if (proximity.evaluate(_playerPosition, point1, mousePosition))//Code qui retourne true ou false
                {
                    return _damage;
                }
            }
            _shot = false;
            return 0;
        }

        public void Delete()
        {
            //Supprimer la zone.
            _playerPosition = null; 
        }

        public void LoadSound(SoundEffect newSound)
        {
            this._shootSound = newSound;
        }

        public void Draw(SpriteBatch spriteBatch, bool pause)
        {
            return;
        }

        public void shoot(Position playerPosition, GraphicsDevice GraphicsDevice)
        {
            _playerPosition = playerPosition;
            _shot = true;
            _shootSound.Play(_soundVolume, _soundPitch, _soundPan);
        }

        public void reset()
        {
            _playerPosition.X = 0;
            _playerPosition.Y = 0;
            _shot = false;
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
